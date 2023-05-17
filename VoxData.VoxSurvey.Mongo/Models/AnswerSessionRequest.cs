using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace VoxData.VoxSurvey.Mongo.Models
{
    public class AnswerSessionRequest
    {
        [BsonId]
        public ObjectId Id { get; set; }

        [BsonRequired()]
        [BsonElement("SKe")]
        public string SessionKey { get; set; } = "";

        [BsonElement("Apl")]
        [BsonRequired()]
        public int IDApplication { get; set; }

        [BsonElement("Ev")]
        [BsonRequired()]
        public long IDEvaluation { get; set; }

        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        [BsonRequired()]
        [BsonElement("Dt")]
        public DateTime CreatedDate { get; set; }

        [BsonElement("Mt")]
        public Dictionary<string, string> Metadata { get; set; } = new Dictionary<string, string>();
    }
}
