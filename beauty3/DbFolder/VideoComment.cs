﻿using System;

namespace beauty3.DbFolder
{
    public class VideoComment
    {
        public int Id { get; set; }


        public string UserId { get; set; }
        public User User { get; set; }
        public string Text { get; set; }
        public string DateTime { get; set; }


        public int KursVideoId { get; set; }
        public KursVideo KursVideo { get; set; }
    }
}
