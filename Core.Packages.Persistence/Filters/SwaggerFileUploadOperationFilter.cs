using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Linq;
using System.Reflection;

namespace MagicCarRepairAISupported.Persistence.Filters
{
    /// <summary>
    /// [FromForm] ile IFormFile kullanımını Swagger'da handle etmek için operation filter
    /// </summary>
    public class SwaggerFileUploadOperationFilter : IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            // Tüm parametreleri al
            var parameters = context.MethodInfo.GetParameters();
            
            // IFormFile parametrelerini bul
            var fileParams = parameters
                .Where(p => p.ParameterType == typeof(IFormFile) || 
                           p.ParameterType == typeof(IEnumerable<IFormFile>))
                .ToList();

            // [FromForm] attribute'ü olan parametreleri bul
            var formParams = parameters
                .Where(p => p.GetCustomAttributes(typeof(FromFormAttribute), false).Any() ||
                           p.GetCustomAttributes(typeof(FromFormAttribute), true).Any())
                .ToList();

            // Eğer IFormFile veya [FromForm] parametreleri varsa multipart/form-data olarak ekle
            if (fileParams.Any() || formParams.Any())
            {
                var properties = new Dictionary<string, OpenApiSchema>();
                var requiredProperties = new HashSet<string>();
                
                // File parametrelerini ekle
                foreach (var fileParam in fileParams)
                {
                    var paramName = fileParam.Name!;
                    properties[paramName] = new OpenApiSchema
                    {
                        Type = "string",
                        Format = "binary",
                        Description = "File to upload"
                    };
                    requiredProperties.Add(paramName);
                }
                
                // Diğer [FromForm] parametrelerini ekle
                foreach (var formParam in formParams.Where(p => !fileParams.Contains(p)))
                {
                    var paramName = formParam.Name!;
                    
                    if (formParam.ParameterType == typeof(string))
                    {
                        properties[paramName] = new OpenApiSchema
                        {
                            Type = "string"
                        };
                    }
                    else if (formParam.ParameterType == typeof(int) || formParam.ParameterType == typeof(int?))
                    {
                        properties[paramName] = new OpenApiSchema
                        {
                            Type = "integer",
                            Format = "int32"
                        };
                    }
                    else if (formParam.ParameterType == typeof(bool) || formParam.ParameterType == typeof(bool?))
                    {
                        properties[paramName] = new OpenApiSchema
                        {
                            Type = "boolean"
                        };
                    }
                    else if (formParam.ParameterType == typeof(double) || formParam.ParameterType == typeof(double?))
                    {
                        properties[paramName] = new OpenApiSchema
                        {
                            Type = "number",
                            Format = "double"
                        };
                    }
                    else if (formParam.ParameterType == typeof(decimal) || formParam.ParameterType == typeof(decimal?))
                    {
                        properties[paramName] = new OpenApiSchema
                        {
                            Type = "number",
                            Format = "decimal"
                        };
                    }
                    
                    // Eğer parametre nullable değilse required olarak işaretle
                    if (!formParam.ParameterType.IsValueType || 
                        (formParam.ParameterType.IsGenericType && 
                         formParam.ParameterType.GetGenericTypeDefinition() == typeof(Nullable<>)))
                    {
                        // Nullable, required değil
                    }
                    else
                    {
                        requiredProperties.Add(paramName);
                    }
                }
                
                // RequestBody'yi ayarla
                operation.RequestBody = new OpenApiRequestBody
                {
                    Required = fileParams.Any(),
                    Content = new Dictionary<string, OpenApiMediaType>
                    {
                        ["multipart/form-data"] = new OpenApiMediaType
                        {
                            Schema = new OpenApiSchema
                            {
                                Type = "object",
                                Properties = properties,
                                Required = requiredProperties
                            }
                        }
                    }
                };
                
                // [FromForm] parametrelerini operation.Parameters'dan kaldır
                // (çünkü artık RequestBody'de yer alıyorlar)
                var formParamNames = formParams.Select(p => p.Name).ToHashSet();
                if (operation.Parameters != null)
                {
                    operation.Parameters = operation.Parameters
                        .Where(p => p.Name == null || !formParamNames.Contains(p.Name))
                        .ToList();
                }
                else
                {
                    operation.Parameters = new List<OpenApiParameter>();
                }
            }
        }
    }
}
