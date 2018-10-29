using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using SmartHouseMachineLearningv100.Services;

namespace SmartHouseMachineLearningv100.Model
{
    public class PredictionUsageLightBulbModel : UsageOfLightBulbModel
    {
        [BsonId]
        public ObjectId Id { get; set; }

//        public float LightBulbID;
//        
//        public float Month;
//
//        public float Day;
//
//        public float Time;
//
//        public float IsOn;
    }
}
