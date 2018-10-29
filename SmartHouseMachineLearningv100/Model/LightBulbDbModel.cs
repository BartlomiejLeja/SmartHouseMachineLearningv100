using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace SmartHouseMachineLearningv100.Model
{
    public class LightBulbDbModel : LightBulbModel
    {
        [BsonId]
        public ObjectId Id { get; set; }
    }
}
