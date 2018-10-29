using SmartHouseMachineLearningv100.Model;
using System.Collections.Generic;
using System.Threading.Tasks;
using MongoDB.Driver;
using SmartHouseMachineLearningv100.MongoDbContexts;

namespace SmartHouseMachineLearningv100.Repository
{
    public class LightBulbRepository : ILightBulbRepository
    {
        private readonly ILightBulbContext _context;

        public LightBulbRepository(ILightBulbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<PredictionUsageLightBulbModel>> GetAllLightBulbs()
        {
            return await _context
                .LightBulbDBModel
                .Find(_ => true)
                .ToListAsync();
        }
        public Task<PredictionUsageLightBulbModel> GetLightBulb(int lightBulbID)
        {
            FilterDefinition<PredictionUsageLightBulbModel> filter = Builders<PredictionUsageLightBulbModel>.Filter.Eq(m => m.LightBulbID, lightBulbID);
            return _context
                .LightBulbDBModel
                .Find(filter)
                .FirstOrDefaultAsync();
        }

        public async Task Create(PredictionUsageLightBulbModel lightBulbDbModel)
        {
            await _context.LightBulbDBModel.InsertOneAsync(lightBulbDbModel);
        }
        public async Task<bool> Update(PredictionUsageLightBulbModel lightBulbDbModel)
        {
            ReplaceOneResult updateResult =
                await _context
                    .LightBulbDBModel
                    .ReplaceOneAsync(
                        filter: g => g.Id == lightBulbDbModel.Id,
                        replacement: lightBulbDbModel);
            return updateResult.IsAcknowledged
                   && updateResult.ModifiedCount > 0;
        }
        public async Task<bool> Delete(int lightBulbID)
        {
            FilterDefinition<PredictionUsageLightBulbModel> filter = Builders<PredictionUsageLightBulbModel>.Filter.Eq(m => m.LightBulbID, lightBulbID);
            DeleteResult deleteResult = await _context
                .LightBulbDBModel
                .DeleteOneAsync(filter);
            return deleteResult.IsAcknowledged
                   && deleteResult.DeletedCount > 0;
        }
    }
}
