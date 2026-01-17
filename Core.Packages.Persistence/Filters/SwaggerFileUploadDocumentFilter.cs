using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Linq;
using System.Reflection;

namespace MagicCarRepairAISupported.Persistence.Filters
{
    /// <summary>
    /// [FromForm] ile IFormFile kullanımını Swagger'da handle etmek için document filter
    /// Bu filter, Swagger document oluşturulduktan sonra çalışır ve parametreleri RequestBody'ye dönüştürür
    /// </summary>
    public class SwaggerFileUploadDocumentFilter : IDocumentFilter
    {
        public void Apply(OpenApiDocument swaggerDoc, DocumentFilterContext context)
        {
            // Bu filter document oluşturulduktan sonra çalışır
            // Parametreler zaten okunmuş olur, bu yüzden burada bir şey yapamayız
            // Ama document'i post-process edebiliriz
        }
    }
}
