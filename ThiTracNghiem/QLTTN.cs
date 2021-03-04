using System;

namespace ThiTracNghiem
{
    partial class QLTTNDataContext
    {
        public class MyHocSinh
        {
            public string HoTen { get; set; }
            public string maHS { get; set; }
            public string maKhoi { get; set; }
            public string maLop { get; set; }
            public DateTime NgaySinh { get; set; }
        }
        public class MyDeThi
        {
            public int maDT { get; set; }
            public string TenDT { get; set; }
            public string maGV { get; set; }
            public string maKhoi { get; set; }
            public string maMH { get; set; }
            public string TenMH { get; set; }
            public TimeSpan ThoiGianLamBai { get; set; }
        }
    }
}