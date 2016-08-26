using PagedList;

namespace Community.ViewModel.Request
{
    public class CommunityReturnViewModel
    {
        public StaticPagedList<CommunityViewModel> Communitys { get; set; }

        public PagingInfoViewModel PagingInfo { get; set; }
    }
}