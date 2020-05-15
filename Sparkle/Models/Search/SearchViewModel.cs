﻿using Sparkle.Domain.Entities;
using System.Collections.Generic;

namespace Sparkle.Models
{
    public class SearchViewModel
    {
        public IEnumerable<User> SearchedUsers { get; set; }
        public IEnumerable<Post> Posts { get; set; }
        public User User { get; set; }

    }
}
