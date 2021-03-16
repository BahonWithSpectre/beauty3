
namespace beauty3.DbFolder
{
    public class KursVideo
    {
        public int Id { get; set; }

        public string VideoName { get; set; }
        public string Info { get; set; }
        public string VideoUrl { get; set; }
        public string PhotoUrl { get; set; }
        public string FileUrl { get; set; }

        public int KursId { get; set; }
        public Kurs Kurs { get; set; }
    }
}
