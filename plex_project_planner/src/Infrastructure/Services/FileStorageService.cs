using Microsoft.AspNetCore.Components.Forms;
using System;
using System.IO;
using System.Threading.Tasks;

namespace PlexProjectPlanner.Infrastructure.Services
{
    public interface IFileStorageService
    {
        Task<string> SaveFileAsync(IBrowserFile file, string directory = "uploads");
        Task<bool> DeleteFileAsync(string filePath);
        Task<byte[]> GetFileBytesAsync(string filePath);
        string GetContentType(string fileName);
        bool IsValidFileType(string fileName, string[] allowedExtensions);
    }

    public class FileStorageService : IFileStorageService
    {
        private readonly string _webRootPath;

        public FileStorageService(IWebHostEnvironment environment)
        {
            _webRootPath = environment.WebRootPath;
        }

        public async Task<string> SaveFileAsync(IBrowserFile file, string directory = "uploads")
        {
            try
            {
                // Validate file
                if (file == null || file.Size == 0)
                    throw new ArgumentException("File is empty or null", nameof(file));

                // Create directory if it doesn't exist
                var uploadPath = Path.Combine(_webRootPath, directory);
                if (!Directory.Exists(uploadPath))
                {
                    Directory.CreateDirectory(uploadPath);
                }

                // Generate unique filename
                var fileName = $"{Guid.NewGuid()}_{file.Name}";
                var filePath = Path.Combine(uploadPath, fileName);

                // Save file
                await using var fileStream = new FileStream(filePath, FileMode.Create);
                await file.OpenReadStream(10485760 /* 10MB max */).CopyToAsync(fileStream);

                return Path.Combine(directory, fileName);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Error saving file: {ex.Message}", ex);
            }
        }

        public async Task<bool> DeleteFileAsync(string filePath)
        {
            try
            {
                var fullPath = Path.Combine(_webRootPath, filePath);
                if (File.Exists(fullPath))
                {
                    File.Delete(fullPath);
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Error deleting file: {ex.Message}", ex);
            }
        }

        public async Task<byte[]> GetFileBytesAsync(string filePath)
        {
            try
            {
                var fullPath = Path.Combine(_webRootPath, filePath);
                if (!File.Exists(fullPath))
                    throw new FileNotFoundException($"File not found: {fullPath}");

                return await File.ReadAllBytesAsync(fullPath);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Error reading file: {ex.Message}", ex);
            }
        }

        public string GetContentType(string fileName)
        {
            var extension = Path.GetExtension(fileName).ToLowerInvariant();
            return extension switch
            {
                ".jpg" or ".jpeg" => "image/jpeg",
                ".png" => "image/png",
                ".gif" => "image/gif",
                ".pdf" => "application/pdf",
                ".doc" => "application/msword",
                ".docx" => "application/vnd.openxmlformats-officedocument.wordprocessingml.document",
                ".txt" => "text/plain",
                ".zip" => "application/zip",
                ".xls" => "application/vnd.ms-excel",
                ".xlsx" => "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                _ => "application/octet-stream"
            };
        }

        public bool IsValidFileType(string fileName, string[] allowedExtensions)
        {
            var extension = Path.GetExtension(fileName).ToLowerInvariant();
            return Array.Exists(allowedExtensions, ext => ext.ToLowerInvariant() == extension);
        }
    }
}