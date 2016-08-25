using Marvin.JsonPatch;

namespace Community.ViewModel.Request
{
    public class PatchViewModelBase<TType> where TType : class
    {
        public int Id { get; set; }
        public JsonPatchDocument<TType> Model { get; set; }
    }
}