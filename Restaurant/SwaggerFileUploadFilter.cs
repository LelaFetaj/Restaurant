using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Restaurant.API
{
    public class SwaggerFileUploadFilter : IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            if (operation.Parameters == null)
                return;

            foreach (var parameter in operation.Parameters.ToList())
            {
                var attribute = parameter.Description?.ToLower();
                if (attribute == "file" || attribute == "formfile")
                {
                    operation.Parameters.Remove(parameter);
                    operation.RequestBody = new OpenApiRequestBody
                    {
                        Content = new Dictionary<string, OpenApiMediaType>
                        {
                            ["multipart/form-data"] = new OpenApiMediaType
                            {
                                Schema = new OpenApiSchema
                                {
                                    Type = "object",
                                    Properties = new Dictionary<string, OpenApiSchema>
                                    {
                                        ["file"] = new OpenApiSchema
                                        {
                                            Type = "string",
                                            Format = "binary"
                                        }
                                    },
                                    Required = new HashSet<string> { "file" }
                                }
                            }
                        }
                    };
                    break;
                }
            }
        }
    }
}
