using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using MongoDB.Driver;
using SmartHouseMachineLearningv100.Model;

namespace SmartHouseMachineLearningv100.MongoDbContexts
{
    public class LightBulbContext : ILightBulbContext
    {
        private readonly IMongoDatabase _db;
        private IConfiguration _configuration;
        private readonly ILogger _logger;

        public LightBulbContext(IConfiguration configuration, ILogger<LightBulbContext> logger)
        {
            _configuration = configuration;
            var connectionString = _configuration["MongoDB:ConnectionString"];
            var databaseName = _configuration["MongoDB:Database"];
            var client = new MongoClient(connectionString);
            _db = client.GetDatabase(databaseName);
        }

        public IMongoCollection<PredictionUsageLightBulbModel> LightBulbDBModel =>
            _db.GetCollection<PredictionUsageLightBulbModel>("PredictionTrainingData");
    }
}
