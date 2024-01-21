using Swashbuckle.Swagger;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Description;
using System.Web.Http.Filters;

namespace CustomSwagger.Filters
{
    /// <summary>
    /// Adds authorization parameter when invoking api methods from swagger documentation
    /// </summary>
    public class SwaggerAuthorizationHeaderFilter : IOperationFilter
    {
        /// <summary>
        /// runs when the filter is applying on the swagger response
        /// </summary>
        /// <param name="operation">the documented action</param>
        /// <param name="schemaRegistry">the data type</param>
        /// <param name="apiDescription">the api descriptor</param>
        public void Apply(Operation operation, SchemaRegistry schemaRegistry, ApiDescription apiDescription)
        {
            if (operation == null)
            {
                throw new ArgumentNullException("operation");
            }

            if (apiDescription == null)
            {
                throw new ArgumentNullException("apiDescription");
            }

            var filterPipeline = apiDescription.ActionDescriptor.GetFilterPipeline();
            var isAuthorized = filterPipeline
                    .Select(filterInfo => filterInfo.Instance)
                    .Any(filter => filter is IAuthorizationFilter);

            if (isAuthorized)
            {
                var allowAnonymous = apiDescription.ActionDescriptor.ControllerDescriptor.GetCustomAttributes<AllowAnonymousAttribute>().Any();
                if (!allowAnonymous)
                {
                    allowAnonymous = apiDescription.ActionDescriptor.GetCustomAttributes<AllowAnonymousAttribute>().Any();
                }

                if (!allowAnonymous)
                {
                    operation.parameters = operation.parameters ?? new List<Parameter>();
                    operation.parameters.Insert(
                        0,
                        new Parameter
                        {
                            name = "Authorization",
                            description = "access token",
                            required = true,
                            type = "string",
                            @in = "header"
                        });
                }
            }
        }
    }
}