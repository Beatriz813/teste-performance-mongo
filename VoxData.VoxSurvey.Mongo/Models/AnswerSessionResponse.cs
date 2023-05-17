using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace VoxData.VoxSurvey.Mongo.Models
{
    public class AnswerSessionResponse
    {
        [BsonId]
        public ObjectId Id { get; set; }

        [BsonElement("SKe")]
        public string? SessionKey { get; set; }

        [BsonElement("Apl")]
        [BsonRequired()]
        public int IDApplication { get; set; }

        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        [BsonRequired()]
        [BsonElement("Dt")]
        public DateTime CreatedDate { get; set; }

        [BsonElement("Ans")]
        public List<AnswerSessionAnswer> Answers { get; set; } = new List<AnswerSessionAnswer>();
    }
}
