using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VoxData.VoxSurvey.Mongo.Models
{
    public class Section
    {
        public int Position { get; set; }
        public string? Title { get; set; }
        public string? Subtitle { get; set; }
        public IEnumerable<Question> Questions { get; set; }
    }
}
