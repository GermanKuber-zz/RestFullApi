using Marvin.JsonPatch;

namespace Community.ViewModel.Request
{
    public class PatchViewModel
    {
        public int Id { get; set; }
        public JsonPatchDocument<UserViewModel> Model { get; set; }


    }
}