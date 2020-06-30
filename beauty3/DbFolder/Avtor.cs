
namespace beauty3.DbFolder
{
    public class Avtor
    {
        public int Id { get; set; }
        public string AvtorName { get; set; }
        public string Info { get; set; }
        public int? Price { get; set; }





        public int KursId { get; set; }
        public Kurs Kurs { get; set; }
    }
}
