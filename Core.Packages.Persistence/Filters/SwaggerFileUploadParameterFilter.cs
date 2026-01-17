using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Linq;

namespace MagicCarRepairAISupported.Persistence.Filters
{
    /// <summary>
    /// [FromForm] ile IFormFile kullanımını Swagger'da handle etmek için parameter filter
    /// Bu filter, Swagger'ın parametreleri okurken hata vermesini engeller
    /// </summary>
    public class SwaggerFileUploadParameterFilter : IParameterFilter
    {
        public void Apply(Microsoft.OpenApi.Models.OpenApiParameter parameter, ParameterFilterContext context)
        {
            var paramInfo = context.ParameterInfo;
            
            // Eğer parametre [FromForm] ve IFormFile ise
            if (paramInfo != null)
            {
                var hasFromForm = paramInfo.GetCustomAttributes(typeof(FromFormAttribute), false).Any() ||
                                 paramInfo.GetCustomAttributes(typeof(FromFormAttribute), true).Any();
                
                var isFormFile = paramInfo.ParameterType == typeof(IFormFile) || 
                                paramInfo.ParameterType == typeof(IEnumerable<IFormFile>);
                
                if (hasFromForm && isFormFile)
                {
                    // Parametreyi geçici olarak string olarak işaretle
                    // OperationFilter'da RequestBody olarak düzgün şekilde ekleyeceğiz
                    parameter.Schema = new Microsoft.OpenApi.Models.OpenApiSchema
                    {
                        Type = "string",
                        Format = "binary"
                    };
                }
            }
        }
    }
}
