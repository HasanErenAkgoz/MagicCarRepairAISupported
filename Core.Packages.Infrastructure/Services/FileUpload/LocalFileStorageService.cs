using MagicCarRepairAISupported.Application.Common.Services.FileUpload;
using MagicCarRepairAISupported.Domain.Entities;
using MagicCarRepairAISupported.Domain.Enums;
using MagicCarRepairAISupported.Domain.Repositories.EntityFrameworkCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace MagicCarRepairAISupported.Infrastructure.Services.FileUpload
{
    public class LocalFileStorageService : IFileStorageService
    {
        private readonly string _rootPath;
        private readonly IUploadedFileRepository _fileRepository;

        public LocalFileStorageService(IWebHostEnvironment env, IUploadedFileRepository fileRepository)
        {
            // wwwroot/uploads dizinini ayarla
            _rootPath = Path.Combine(env.WebRootPath, "uploads");

            // E�er uploads klas�r� yoksa olu�tur
            if (!Directory.Exists(_rootPath))
                Directory.CreateDirectory(_rootPath);

            _fileRepository = fileRepository;
        }

        public async Task<string> UploadFileAsync(IFormFile file, string containerName, CancellationToken cancellationToken)
        {
            var safeContainerName = GetSafeContainerName(containerName);
            var safeFileName = GetSafeFileName(file.FileName);
            string folderPath = Path.Combine(_rootPath, safeContainerName);
            if (!Directory.Exists(folderPath))
                Directory.CreateDirectory(folderPath);

            string filePath = Path.Combine(folderPath, safeFileName);

            // Dosyay� kaydet
            using var stream = new FileStream(filePath, FileMode.Create);
            await file.CopyToAsync(stream, cancellationToken);

            string fileUrl = $"/uploads/{safeContainerName}/{safeFileName}";

            var uploadedFile = new UploadedFile
            {
                FileName = safeFileName,
                FilePath = fileUrl,
                FileType = GetFileType(file.FileName),
            };

            await _fileRepository.AddAsync(uploadedFile, cancellationToken);
            return fileUrl;
        }
        public async Task<bool> DeleteFileAsync(string fileName, string containerName, CancellationToken cancellationToken)
        {
            string filePath = GetSafeFilePath(fileName, containerName);
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
                return true;
            }
            return false;
        }

        public async Task<Stream?> GetFileAsync(string fileName, string containerName)
        {
            string filePath = GetSafeFilePath(fileName, containerName);
            if (!File.Exists(filePath))
                return null;

            return new FileStream(filePath, FileMode.Open, FileAccess.Read);
        }

        private FileType GetFileType(string fileName)
        {
            string extension = Path.GetExtension(fileName).ToLower();
            return extension switch
            {
                ".jpg" or ".png" or ".jpeg" or ".gif" => FileType.Image,
                ".mp4" or ".avi" or ".mov" => FileType.Video,
                ".mp3" or ".wav" => FileType.Audio,
                ".pdf" or ".docx" or ".xlsx" => FileType.Document,
                _ => FileType.Other
            };
        }

        private string GetSafeFilePath(string fileName, string containerName)
        {
            return Path.Combine(_rootPath, GetSafeContainerName(containerName), GetSafeFileName(fileName));
        }

        private static string GetSafeFileName(string fileName)
        {
            if (string.IsNullOrWhiteSpace(fileName) || Path.GetFileName(fileName) != fileName)
            {
                throw new ArgumentException("Invalid file name.", nameof(fileName));
            }

            return fileName;
        }

        private static string GetSafeContainerName(string containerName)
        {
            if (string.IsNullOrWhiteSpace(containerName) || Path.IsPathRooted(containerName))
            {
                throw new ArgumentException("Invalid container name.", nameof(containerName));
            }

            var segments = containerName.Split(new[] { '/', '\\' }, StringSplitOptions.RemoveEmptyEntries);
            if (segments.Length == 0 || segments.Any(segment => segment is "." or ".." || Path.GetFileName(segment) != segment))
            {
                throw new ArgumentException("Invalid container name.", nameof(containerName));
            }

            return Path.Combine(segments);
        }
    }
}
