using System.Collections.Generic;

namespace Community.Core
{
    public class User
    {
        public User()
        {
            Communitys = new List<Community>();
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Company { get; set; }
        public virtual ICollection<Community> Communitys { get; set; }
    }
}
