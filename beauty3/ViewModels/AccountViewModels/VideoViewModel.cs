using System.Collections.Generic;
using beauty3.DbFolder;
namespace beauty3.ViewModels.AccountViewModels
{
    public class VideoViewModel
    {
        public KursVideo KursVideo { get; set; }
        public List<VideoComment> VideoComments { get; set; }

        public VideoViewModel()
        {
            VideoComments = new List<VideoComment>();
        }
    }
}
