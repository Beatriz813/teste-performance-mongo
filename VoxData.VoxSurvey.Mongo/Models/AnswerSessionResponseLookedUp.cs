using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VoxData.VoxSurvey.Mongo.Models
{
    public class AnswerSessionResponseLookedUp : AnswerSessionResponse
    {
        [BsonElement("Ev")]
        public long IDEvaluation { get; set; }

        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        [BsonElement("SDt")]
        public DateTime SessionCreatedDate { get; set; }


        [BsonElement("Ttr")]
        public int? ResponseTime { get; set; }

        [BsonElement("Mt")]
        public Dictionary<string, string>? Metadata { get; set; } = new Dictionary<string, string>();
    }
}
