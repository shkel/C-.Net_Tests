namespace BLL.Entities
{
    public class Training
    {
        public enum Mark {
            Failure = 0,
            LowFailure = 1,
            LowPass = 2,
            Satisfactory = 3,
            Good = 4,
            Excellent = 5
        }
        public int Id { get; set; }

        public int LessonId { get; set; }

        public int StudentId { get; set; }
        public Mark Grade { get; set; }

        public bool Attendance { get; set; }
    }
}
