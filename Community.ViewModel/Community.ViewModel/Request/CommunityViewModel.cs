using System.Collections.Generic;

namespace Community.ViewModel.Request
{
    public class CommunityViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public IList<CommunityTagViewModel> Tags { get; set; }
    }

}