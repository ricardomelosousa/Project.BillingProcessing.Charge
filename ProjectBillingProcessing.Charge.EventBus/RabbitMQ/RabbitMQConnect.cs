namespace ProjectBillingProcessing.Charge.EventBus.RabbitMQ;
public class RabbitMQConnect : IRabbitMQConnect
{
    private readonly IConnectionFactory _connectionFactory;
    private readonly ILogger<RabbitMQConnect> _logger;
    private readonly int _retry;
    IConnection _connection;
    bool _disposed;
    object lock_object = new object();

    public RabbitMQConnect(IConnectionFactory connectionFactory, ILogger<RabbitMQConnect> logger, int retry = 5)
    {
        _connectionFactory = connectionFactory;
        _logger = logger;
        _retry = retry;
    }

    public bool IsConnected
    {
        get
        {
            return _connection != null && _connection.IsOpen && !_disposed;
        }
    }

    public IModel CreateModel()
    {
        if (!IsConnected)
        {
            throw new InvalidOperationException("Erro ao conectar ao rabbitmq");
        }

        return _connection.CreateModel();
    }

    public void Dispose()
    {
        if (_disposed) return;

        _disposed = true;

        try
        {
            _connection.ConnectionShutdown -= OnConnectionShutdown;

            _connection.Dispose();
        }
        catch (IOException ex)
        {
            _logger.LogCritical(ex.ToString());
        }
    }

    public bool TryConnect()
    {
        lock (lock_object)
        {
            var policy = RetryPolicy.Handle<SocketException>()
                .Or<BrokerUnreachableException>()
                .WaitAndRetry(_retry, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)), (ex, time) =>
                {
                    _logger.LogWarning(ex, "RabbitMQ Client could not connect after {TimeOut}s ({ExceptionMessage})", $"{time.TotalSeconds:n1}", ex.Message);
                }
            );

            policy.Execute(() =>
            {
                _connection = _connectionFactory
                        .CreateConnection();
            });

            if (IsConnected)
            {
                _connection.ConnectionShutdown += OnConnectionShutdown;
                _logger.LogInformation("RabbitMQ Client acquired a persistent connection to '{HostName}' and is subscribed to failure events", _connection.Endpoint.HostName);
                return true;
            }
            else
            {
                _logger.LogCritical("FATAL ERROR: RabbitMQ connections could not be created and opened");
                return false;
            }
        }
    }

    void OnConnectionShutdown(object sender, ShutdownEventArgs reason)
    {
        if (_disposed) return;

        _logger.LogWarning("A RabbitMQ conexão está offline. Tentando conexão....");

        TryConnect();
    }
}

