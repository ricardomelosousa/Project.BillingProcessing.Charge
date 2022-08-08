namespace Project.BillingProcessing.Charge.Infra.Repository;
public class ChargeRepository : IChargeRepository, IDisposable
{
    private readonly IMongoCollection<Domain.ChargeEntity.Charge> _charges;
    public void Dispose() => Dispose(true);

    public ChargeRepository(IMongoDatabase database)
    {
        _charges = database.GetCollection<Domain.ChargeEntity.Charge>("Charges");
    }


    public void Dispose(bool v)
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    public IEnumerable<Domain.ChargeEntity.Charge> FilterBy(Expression<Func<Domain.ChargeEntity.Charge, bool>> filterExpression)
    {
        return _charges.Find(filterExpression).ToEnumerable();
    }   

    public async Task<Domain.ChargeEntity.Charge> FindByIdAsync(string id)
    {
        return await GetFindByIdAsync(x => x.Id == id);       
    }

    public async Task<Domain.ChargeEntity.Charge> GetFindByIdAsync(Expression<Func<Domain.ChargeEntity.Charge, bool>> predicate)
    {
        var filter = Builders<Domain.ChargeEntity.Charge>.Filter.Where(predicate);
        return (await _charges.FindAsync(filter)).FirstOrDefault();
    }
    public Task<Domain.ChargeEntity.Charge> FindOneAsync(Expression<Func<Domain.ChargeEntity.Charge, bool>> filterExpression)
    {
        return Task.Run(() => _charges.Find(filterExpression).FirstOrDefaultAsync());
    }

    public Task InsertOneAsync(Domain.ChargeEntity.Charge document)
    {
        return Task.Run(() => _charges.InsertOneAsync(document));
    }
}

