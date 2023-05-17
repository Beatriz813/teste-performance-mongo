using MongoDB.Bson.Serialization.Attributes;

namespace VoxData.VoxSurvey.Mongo.Models
{
    public class AnswerSessionAnswer
    {
        [BsonElement("EvQ")]
        public string IDEvaluationQuestion { get; set; }

        [BsonElement("EvQO")]
        public Dictionary<string, string> IDEvaluationQuestionOption { get; set; } = new Dictionary<string, string>();
        [BsonElement("Dt")]
        public DateTime CreatedDate { get; set; }
        [BsonElement("Mt")]
        public Dictionary<string, string> Metadata { get; set; } = new Dictionary<string, string>();
    }
}
