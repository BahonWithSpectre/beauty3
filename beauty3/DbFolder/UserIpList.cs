using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace beauty3.DbFolder
{
    public class UserIpList
    {
        public int Id { get; set; }

        public string Ip { get; set; }


        public string UserId { get; set; }
        public User User { get; set; }
    }
}
