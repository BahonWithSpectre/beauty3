
namespace beauty3.DbFolder
{
    public class PodComment
    {
        public int Id { get; set; }

        public string UserId { get; set; }
        public User User { get; set; }
        public string Text { get; set; }
        public string DateTime { get; set; }
        
        public int VideoCommentId { get; set; }
        public VideoComment VideoComment { get; set; }
    }
}
