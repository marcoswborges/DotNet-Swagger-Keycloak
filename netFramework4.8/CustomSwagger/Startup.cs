using CustomSwagger.Providers;
using Microsoft.Owin;
using Microsoft.Owin.Cors;
using Microsoft.Owin.Security.OAuth;
using Owin;
using System;
using System.Threading.Tasks;
using System.Web.Cors;
using System.Web.Http;

namespace CustomSwagger
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            var config = new HttpConfiguration();
            WebApiConfig.Register(config);
            SwaggerConfig.Register(config);
            ConfigureCors(app);
            AtivandoAcessTokens(app);
            app.UseWebApi(config);
        }

        private void AtivandoAcessTokens(IAppBuilder app)
        {
            var opcoesConfiguracaoToken = new OAuthAuthorizationServerOptions()
            {
                AllowInsecureHttp = true,
                TokenEndpointPath = new PathString("/token"),
                AccessTokenExpireTimeSpan = TimeSpan.FromHours(1),
                Provider = new TokensDeAcessoProvider()
            };

            app.UseOAuthAuthorizationServer(opcoesConfiguracaoToken);
            app.UseOAuthBearerAuthentication(new OAuthBearerAuthenticationOptions());
        }

        private void ConfigureCors(IAppBuilder app)
        {
            var politica = new CorsPolicy();

            politica.AllowAnyHeader = true;
            //politica.Origins.Add("http://localhost:49628");
            politica.Origins.Add("*");
            politica.Methods.Add("GET");
            politica.Methods.Add("POST");
            politica.Methods.Add("PUT");
            politica.Methods.Add("DELETE");

            var corsOptions = new CorsOptions
            {
                PolicyProvider = new CorsPolicyProvider
                {
                    PolicyResolver = context => Task.FromResult(politica)
                }
            };
            app.UseCors(corsOptions);
        }
    }
}