using System.Linq;
using System.Threading.Tasks;
using Thinktecture.IdentityModel.Owin.ResourceAuthorization;

namespace Community.APi.Helper
{

    public class AuthorizationManager : ResourceAuthorizationManager
    {
        public override Task<bool> CheckAccessAsync(ResourceAuthorizationContext context)
        {
            switch (context.Resource.First().Value)
            {
                case "Communitys":
                    return AuthorizeExpenseGroup(context);
                default:
                    return Nok();
            }
        }

        private Task<bool> AuthorizeExpenseGroup(ResourceAuthorizationContext context)
        {
            switch (context.Action.First().Value)
            {
                case "Read":
                    return
                        Eval(context.Principal.HasClaim("role", "MobileReadUser")
                        || (context.Principal.HasClaim("role", "WebReadUser")));

                default:
                    return Nok();
            }
        }

    }
}