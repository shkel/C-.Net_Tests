using DAL.Interfaces;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.Entities
{
    [Table("training_journal")]
    public class TrainingDAL : IAutoKey
    {
        public enum Score {
            Failure = 0,
            LowFailure = 1,
            LowPass = 2,
            Satisfactory = 3,
            Good = 4,
            Excellent = 5
        }
        public int Id { get; set; }

        public int LessonId { get; set; }
        public virtual LessonDAL Lesson { get; set; }

        public int StudentId { get; set; }
        public virtual UserDAL Student { get; set; }

        [Required]
        public Score Grade { get; set; }

        [Required]
        public bool Attendance { get; set; }

        public int GetKey()
        {
            return Id;
        }
    }
}
