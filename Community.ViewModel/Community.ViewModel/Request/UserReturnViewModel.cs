using PagedList;

namespace Community.ViewModel.Request
{
    public class UserReturnViewModel
    {
  
        public StaticPagedList<UserViewModel> Users { get; set; }

        public PagingInfoViewModel PagingInfo { get; set; }
    }
}