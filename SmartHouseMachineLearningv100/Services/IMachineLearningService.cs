using System.Threading.Tasks;
using Microsoft.ML.Legacy;

namespace SmartHouseMachineLearningv100.Services
{
    public interface IMachineLearningService
    {
        Task<bool> Predicate(UsageOfLightBulbModel usageOfLightBulbModel);
        Task<PredictionModel<UsageOfLightBulbModel, UsageOfLightBulbPredictionModel>> Train();
    }
}
