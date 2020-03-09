using System;

namespace Sparkle.Models
{
    public class AddCommentViewModel
    {
        public string Body { get; set; }

        public string PostId { get; set; }

        public string OwnerName { get; set; }
        public string OwnerSurname { get; set; }
        public string OwnerUserName { get; set; }
        public DateTime Created { get; set; } = DateTime.Now;

    }
}
