using Microsoft.AspNetCore.Http;

namespace MagicCarRepairAISupported.Application.Features.Vehicles.Commands.UploadMobilePhoto
{
    /// <summary>
    /// IFormFile wrapper - dosya adını değiştirmek için
    /// </summary>
    public class FormFileWrapper : IFormFile
    {
        private readonly IFormFile _originalFile;
        private readonly string _fileName;

        public FormFileWrapper(IFormFile originalFile, string fileName)
        {
            _originalFile = originalFile;
            _fileName = fileName;
        }

        public string ContentType => _originalFile.ContentType;
        public string ContentDisposition => _originalFile.ContentDisposition;
        public IHeaderDictionary Headers => _originalFile.Headers;
        public long Length => _originalFile.Length;
        public string Name => _originalFile.Name;
        public string FileName => _fileName;

        public Stream OpenReadStream() => _originalFile.OpenReadStream();
        public void CopyTo(Stream target) => _originalFile.CopyTo(target);
        public Task CopyToAsync(Stream target, CancellationToken cancellationToken = default) 
            => _originalFile.CopyToAsync(target, cancellationToken);
    }
}
