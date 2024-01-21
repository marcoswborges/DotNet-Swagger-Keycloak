using System.Web.Http;
using CustomSwagger.Filters;
using Swashbuckle.Application;

//[assembly: PreApplicationStartMethod(typeof(SwaggerConfig), "Register")]

namespace CustomSwagger
{
    public class SwaggerConfig
    {
        public static void Register(HttpConfiguration config)
        {
            var thisAssembly = typeof(SwaggerConfig).Assembly;

            config.EnableSwagger(c =>
             {
                 c.DocumentFilter<SwaggerAuthTokenOperationFilter>();
                 c.SingleApiVersion("v1", "CustomSwagger");
                 c.PrettyPrint();
                 c.IncludeXmlComments(GetXmlCommentsPath());
                 c.DescribeAllEnumsAsStrings();
                 c.OperationFilter<SwaggerAuthorizationHeaderFilter>();

             }).EnableSwaggerUi(c =>
             {
                 c.DocumentTitle("Custom Swagger");
                 c.InjectJavaScript(thisAssembly, "CustomSwagger.Content.js.keycloak.js");
                 c.InjectJavaScript(thisAssembly, "CustomSwagger.Content.js.app.js");
                 c.CustomAsset("index", thisAssembly, "CustomSwagger.Content.pages.index.html");
                 c.DisableValidator();

             });

            SwaggerConfig.MapRoutes(config);
        }

        protected static string GetXmlCommentsPath()
        {
            return System.String.Format(@"{0}\bin\CustomSwagger.xml", System.AppDomain.CurrentDomain.BaseDirectory);
        }

        private static void MapRoutes(HttpConfiguration config)
        {
            config.Routes.MapHttpRoute(
                name: "swagger_root",
                routeTemplate: "",
                defaults: null,
                constraints: null,
                handler: new RedirectHandler((message => message.RequestUri.ToString()), "swagger")
            );
        }
    }
}
