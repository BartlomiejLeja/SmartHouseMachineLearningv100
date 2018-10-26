using Microsoft.ML.Runtime.Api;

namespace SmartHouseMachineLearningv100.Services
{
    public class UsageOfLightBulbModel
    {
        [Column("0")]
        public float LightBulbID;

        [Column("1")]
        public float Month;

        [Column("2")]
        public float Day;

        [Column("3")]
        public float Time;

        [Column("4")]
        public float IsOn;
    }
}
