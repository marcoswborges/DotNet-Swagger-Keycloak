using System.Security.Claims;
using System.Threading;

namespace CustomSwagger.Helper
{
    public static class TokenClaimsHelper
    {
        public static string IdUser

        {
            get
            {
                ClaimsPrincipal claims = Thread.CurrentPrincipal as ClaimsPrincipal;
                string id = claims.FindFirst(x => x.Type == "id_user").Value.ToString();
                return id;
            }
        }
    }
}