using VoxData.VoxSurvey.Mongo.Models.Enums;

namespace VoxData.VoxSurvey.Mongo.Models
{
    public class Question
    {
        public int Position { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public EnumTypeQuestionEvaluation Type { get; set; }
        public bool Mandatory { get; set; }
        public int? MaxSelectedOptions { get; set; }
        public int? MinSelectedOptions { get; set; }
        public bool Hidden { get; set; }
        public IEnumerable<Option> Options { get; set; }
    }
}
