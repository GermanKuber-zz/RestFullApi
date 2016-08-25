using System.Collections.Generic;

namespace Community.Core
{
    public class Community
    {
        
        public int Id { get; set; }

        public string Name { get; set; }
        public string Description { get; set; }
    
        public virtual User Owner { get; set; }
  
        public virtual ICollection<User> Users { get; set; }
      
        public virtual ICollection<CommunityTag> Tags { get; set; }
    }
  
}