using Microsoft.Owin.Security;
using Microsoft.Owin.Security.OAuth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace CustomSwagger.Providers
{
    public class TokensDeAcessoProvider : OAuthAuthorizationServerProvider
    {
        public override async Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            context.Validated();
        }

        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            try
            {
                if (context.UserName != "admin" || context.Password != "admin")
                {
                    context.SetError("invalid_grant", "Usuário e/ou Senha Incorretos.");
                    return;
                }

                var props = new AuthenticationProperties(new Dictionary<string, string>
                {
                    {
                        "username", "admin"
                    },
                    {
                        "name", "Administrator"
                    }
                });

                var identity = new ClaimsIdentity(context.Options.AuthenticationType);
                var user = new AuthenticationTicket(identity, props);
                user.Identity.AddClaim(new Claim("id_user", "1"));
                context.Validated(user);
            }
            catch
            {
                context.SetError("Dados inválidos", "Usuário ou Senha estão incorretos.");
                return;
            }
        }

        public override Task TokenEndpoint(OAuthTokenEndpointContext context)
        {
            foreach (var item in context.Properties.Dictionary)
            {
                context.AdditionalResponseParameters.Add(item.Key, item.Value);
            }

            var clains = context.Identity.Claims
                .GroupBy(x => x.Type)
                .Select(y => new { Clain = y.Key, Value = y.Select(z => z.Value).ToArray() });

            //foreach (var item in clains)
            //{
            //    if (item.Clain != "id_user")
            //    {
            //        context.AdditionalResponseParameters.Add(item.Clain, JsonConvert.SerializeObject(item.Value));
            //    }
            //}

            return base.TokenEndpoint(context);
        }
    }
}