using Microsoft.Extensions.Configuration;
using MongoDB.Driver;

namespace Project.BillingProcessing.Charge.Infra
{
    public class MongoContext
    {
        private readonly MongoClient _client;
        private readonly IMongoDatabase _database;
        private readonly IConfiguration Configuration;

        public MongoContext(IConfiguration configuration)
        {
            Configuration = configuration;
            _client = new MongoClient(Configuration["ChargeDatabase:ConnectionString"]);
            _database = _client.GetDatabase(Configuration["ChargeDatabase:DatabaseName"]);
        }

        public IMongoClient Client => _client;

        public IMongoDatabase Database => _database;
    }
}
