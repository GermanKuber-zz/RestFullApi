using PagedList;

namespace Community.ViewModel.Request
{
    public class UserReturnViewModel
    {
        //TODO: Paso 17 - 6 - Creo los modelos para retornar
        public StaticPagedList<UserViewModel> Users { get; set; }

        public PagingInfoViewModel PagingInfo { get; set; }
    }
}