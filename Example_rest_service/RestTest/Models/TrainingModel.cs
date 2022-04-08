using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Models.Entities
{
    public class TrainingModel
    {
        public enum Grade
        {
            Failure = 0,
            LowFailure = 1,
            LowPass = 2,
            Satisfactory = 3,
            Good = 4,
            Excellent = 5
        }
        public int Id { get; set; }
        [Required]
        public int LessonId { get; set; }
        [Required]
        public int StudentId { get; set; }
        [DefaultValue(Grade.Failure)]
        public Grade HomeWorkMark { get; set; }

        public bool Attendance { get; set; }
    }
}
