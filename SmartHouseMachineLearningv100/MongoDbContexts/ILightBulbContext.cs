using MongoDB.Driver;
using SmartHouseMachineLearningv100.Model;

namespace SmartHouseMachineLearningv100.MongoDbContexts
{
    public interface ILightBulbContext
    {
        IMongoCollection<PredictionUsageLightBulbModel> LightBulbDBModel { get; }
    }
}
