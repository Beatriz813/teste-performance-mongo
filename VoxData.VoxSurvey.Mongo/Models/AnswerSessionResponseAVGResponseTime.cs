using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace VoxData.VoxSurvey.Mongo.Models
{
    public class AnswerSessionResponseAVGResponseTime
    {
        [BsonId]
        public ObjectId? Id { get; set; }

        [BsonElement("avg")]
        public double ResponseTimeAVG { get; set; }
    }
}
