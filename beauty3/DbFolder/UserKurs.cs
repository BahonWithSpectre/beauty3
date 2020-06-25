using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace beauty3.DbFolder
{
    public class UserKurs
    {
        public int Id { get; set; }


        public string UserId { get; set; }
        public User User { get; set; }


        public int VideoKursId { get; set; }
        public Kurs Kurs { get; set; }


        public DateTime? PayDate { get; set; }
        public DateTime? EndDate { get; set; }

        public string PayboxNumber { get; set; }

    }
}
