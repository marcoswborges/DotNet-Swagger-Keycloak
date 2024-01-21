using Swashbuckle.Swagger;
using System;
using System.Collections.Generic;
using System.Web.Http.Description;

namespace CustomSwagger.Filters
{
    public class SwaggerAuthTokenOperationFilter : IDocumentFilter
    {
        /// <summary>
        /// Auth for get access token
        /// </summary>
        /// <param name="swaggerDocument">the documented action</param>
        /// <param name="schemaRegistry">the data type</param>
        /// <param name="apiExplorer">the api explorer</param>
        public void Apply(SwaggerDocument swaggerDocument, SchemaRegistry schemaRegistry, IApiExplorer apiExplorer)
        {
            swaggerDocument.paths.Add("/token", new PathItem
            {
                post = new Operation
                {
                    tags = new List<string> { "Auth" },
                    consumes = new List<string>
                    {
                        "application/x-www-form-urlencoded"
                    },
                    parameters = new List<Parameter>
                    {
                        new Parameter
                        {
                            type = "string",
                            name = "grant_type",
                            required = true,
                            @in = "formData",
                            @default = "password"
                        },
                        new Parameter
                        {
                            type = "string",
                            name = "username",
                            required = true,
                            @in = "formData"
                        },
                        new Parameter
                        {
                            type = "string",
                            name = "password",
                            required = true,
                            @in = "formData"
                        }
                    }
                }
            });
        }
    }
}