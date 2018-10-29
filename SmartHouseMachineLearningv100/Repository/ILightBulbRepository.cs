using System.Collections.Generic;
using System.Threading.Tasks;
using SmartHouseMachineLearningv100.Model;

namespace SmartHouseMachineLearningv100.Repository
{
    public interface ILightBulbRepository
    {
        Task<IEnumerable<PredictionUsageLightBulbModel>> GetAllLightBulbs();
        Task<PredictionUsageLightBulbModel> GetLightBulb(int lightBulbID);
        Task Create(PredictionUsageLightBulbModel lightBulbDbModel);
        Task<bool> Delete(int lightBulbID);
        Task<bool> Update(PredictionUsageLightBulbModel lightBulbDbModel);
    }
}
