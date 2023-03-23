using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace JoggingTime.Helpers
{
    public class SwaggerHeader : IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {

            operation.Parameters.Add(new OpenApiParameter()
            {
                Name = "token",
                AllowEmptyValue = false,
                In = ParameterLocation.Header,
                Schema = new OpenApiSchema() { Type = "string" },
                Required = false
            });
            

        }
    }
}
