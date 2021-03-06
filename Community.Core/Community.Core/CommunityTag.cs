﻿using System.Collections.Generic;

namespace Community.Core
{
    public class CommunityTag
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public virtual ICollection<Community> Communitys { get; set; }
    }
}