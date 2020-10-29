using System.Collections.Generic;
using beauty3.DbFolder;
namespace beauty3.ViewModels.AccountViewModels
{
    public class VideoViewModel
    {
        public List<User> Users { get; set; }
        public KursVideo KursVideo { get; set; }
        public List<VideoComment> VideoComments { get; set; }

        public VideoViewModel()
        {
            Users = new List<User>();
            VideoComments = new List<VideoComment>();
        }
    }
}
