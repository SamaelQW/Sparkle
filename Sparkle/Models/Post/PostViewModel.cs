using Sparkle.Domain.Entities;

namespace Sparkle.Models
{
    public class PostViewModel
    {
        public Domain.Entities.Post Post { get; set; }

        public User User { get; set; }
    }
}
