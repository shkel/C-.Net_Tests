using System;
using System.Runtime.Serialization;

namespace Models.Entities
{
    [DataContract]
    public class AbsenteeismReportModel
    {
        [DataMember]
        public string LectorEmail { get; set; }
        [DataMember]
        public string StudentEmail { get; set; }

        public AbsenteeismReportModel(string lectorEmail, string studentEmail)
        {
            LectorEmail = lectorEmail;
            StudentEmail = studentEmail;
        }

        public override bool Equals(object obj)
        {
            return obj is AbsenteeismReportModel report &&
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
