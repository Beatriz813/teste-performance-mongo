using MongoDB.Bson.Serialization.Attributes;

namespace VoxData.VoxSurvey.Mongo.Models
{
    public class GroupModel
    {
        public string Id { get; set; }
        [BsonElement("Cnt")]
        public int Count { get; set; }
    }
}
