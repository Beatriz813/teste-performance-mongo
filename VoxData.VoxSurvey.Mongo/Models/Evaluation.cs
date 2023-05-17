using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace VoxData.VoxSurvey.Mongo.Models
{
    public class Evaluation
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string _id { get; set; }
        public long IdCompanyLevel { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public string? Greeting { get; set; }
        public string? Finish { get; set; }
        public DateTime DateStart { get; set; }
        public DateTime DateEnd { get; set; }
        public int? AmounResponse { get; set; }
        public TimeSpan? TimeoutResponse { get; set; }
        public long? IdEvaluationVariable { get; set; }
        public bool Active { get; set; }
        public string? Title { get; set; }
        public string? Subtitle { get; set; }
        public string? UrlLogo { get; set; }
        public string? Background { get; set; }
        public string PrimaryColor { get; set; } = "#000000";
        public string TextColor { get; set; } = "#ffffff";
        public bool Editable { get; set; }
        public string CampaingKey { get; set; }
        public IEnumerable<Section> Sections { get; set; }
        public EvaluationVariable? EvaluationVariable { get; set; }
    }
}
