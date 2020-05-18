using System;

namespace Sparkle.Domain.Entities
{
    public class Like
    {
        public string OwnerUsername { get; set; }
        public DateTime Liked { get; set; } = DateTime.Now;
    }
}
