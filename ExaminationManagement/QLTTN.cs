using System;
namespace ExaminationManagement
{
    partial class QLTTNDataContext
    {
        public class MyStudent
        {
            public string name { get; set; }
            public string id { get; set; }
            public string grade_id { get; set; }
            public string class_id { get; set; }
            public DateTime dob { get; set; }
        }
        public class MyPaper
        {
            public int id { get; set; }
            public string sub_id { get; set; }
            public string grade_id { get; set; }
            public string teacher_id { get; set; }
            public string papr_name { get; set; }
            public string name { get; set; }
            public TimeSpan toTime { get; set; }
        }

    }
}