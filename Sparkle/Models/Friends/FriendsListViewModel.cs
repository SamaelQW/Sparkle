using Sparkle.Domain.Entities;
using System.Collections.Generic;

namespace Sparkle.Models
{
    public class FriendsListViewModel
    {
        public User User { get; set; }
        public List<Friend> Friends { get; set; }


    }
}
