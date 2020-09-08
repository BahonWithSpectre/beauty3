using System.Collections.Generic;
using beauty3.DbFolder;
namespace beauty3.ViewModels.AdminViewModels
{
    public class AboutUserView
    {
        public User User { get; set; }

        public List<Kurs> Kurs { get; set; }
        public List<UserKurs> UserKurs { get; set; }


        public AboutUserView()
        {
            Kurs = new List<Kurs>();
            UserKurs = new List<UserKurs>();
        }
    }
}
