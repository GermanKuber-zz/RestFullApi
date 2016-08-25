using System.Collections.Generic;

namespace Community.ViewModel.Request
{
    public class CommunityTagWithCommunitysViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public IList<CommunityViewModel> Communitys { get; set; }
    }
}