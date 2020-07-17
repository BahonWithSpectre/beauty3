using System;

namespace beauty3.DbFolder
{
    public class Kurs
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int? Price { get; set; }

        public string PhotoUrl { get; set; }
        public DateTime? CreatDate { get; set; }
        public string Info { get; set; }

        public string AvtorName { get; set; }
        public string AvtorInfo { get; set; }
    }
}
