namespace VoxData.VoxSurvey.Mongo.Models
{
    public class Option
    {
        public bool Default { get; set; }
        public string Text { get; set; }
        public bool Editable { get; set; }
        public string? ReactiveColor { get; set; }
        public int Position { get; set; }
        public int? OpenQuestionPosition { get; set; }
    }
}
