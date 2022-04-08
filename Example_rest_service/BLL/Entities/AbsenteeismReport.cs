using System;

namespace BLL.Entities
{
    public class AbsenteeismReport
    {
        public string LectorEmail { get; set; }
        public string StudentEmail { get; set; }

        public AbsenteeismReport(string lectorEmail, string studentEmail)
        {
            LectorEmail = lectorEmail;
            StudentEmail = studentEmail;
        }

        public override bool Equals(object obj)
        {
            return obj is AbsenteeismReport report &&
                   LectorEmail == report.LectorEmail &&
                   StudentEmail == report.StudentEmail;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(LectorEmail, StudentEmail);
        }

        public override string ToString()
        {
            return String.Format("[LectorEmail={0}; StudentEmail={1}]", LectorEmail, StudentEmail);
        }
    }
}
