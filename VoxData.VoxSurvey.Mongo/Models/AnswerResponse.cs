using MongoDB.Bson;

namespace VoxData.VoxSurvey.Mongo.Models
{
    public class AnswerResponse
    {
        public ObjectId _id { get; set; }

        public AnswerRequest Request { get; set; }
        public AswResponse? Response { get; set; }

    }

    public class AswResponse
    {
        public DateTime Dta { get; set; }

        public An[] Ans { get; set; }
    }

    public class AnswerRequest
    {
        public Guid SKe { get; set; }

        public long Apl { get; set; }

        public long Ev { get; set; }

        public DateTime Dt { get; set; }

        public Mt Mt { get; set; }
    }

    public partial class An
    {
        public string EvQ { get; set; }

        public Dictionary<string, string> EvQo { get; set; }
    }

    public partial class Mt
    {
        public string Cpf { get; set; }
    }
}
