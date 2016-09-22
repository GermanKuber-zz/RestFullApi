using System.Linq;
using System.Threading.Tasks;
using Thinktecture.IdentityModel.Owin.ResourceAuthorization;

namespace Community.Mvc.Helpers
{
    //TODO: Paso 26 - 5 - Implementamos manejador
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
                    // to be able to read an expense group, the user must be in the
                    // WebReadUser role
                    return Eval(context.Principal.HasClaim("role", "WebReadUser"));
                case "Write":
                    // to be able to create an expense group, the user must be in the
                    // WebWriteUser role
                    return Eval(context.Principal.HasClaim("role", "WebWriteUser"));
                default:
                    return Nok();
            }
        }


    }
}