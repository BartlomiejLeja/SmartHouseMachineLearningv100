using Microsoft.ML.Runtime.Api;

namespace SmartHouseMachineLearningv100.Services
{
    public class UsageOfLightBulbPredictionModel
    {
        [ColumnName("Score")]
        public float IsOn;
    }
}
