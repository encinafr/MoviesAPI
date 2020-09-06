using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

namespace Domain.Services
{
    public class LocalFileStore : IFileStore
    {
        private readonly IWebHostEnvironment _env;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public LocalFileStore(IWebHostEnvironment env, IHttpContextAccessor httpContextAccessor)
        {
            _env = env;
            _httpContextAccessor = httpContextAccessor;
        }

        public Task DeleteFile(string route, string container)
        {
            if (route != null)
            {
                var fileName = Path.GetFileName(route);
                var fileDirectory = Path.Combine(_env.WebRootPath, container, fileName);

                if (File.Exists(fileDirectory))
                    File.Delete(fileDirectory);
            }
            return Task.FromResult(0);
        }

        public async Task<string> EditFile(byte[] content, string extension, string container, string route, string contentType)
        {
            await DeleteFile(route, container);
            return await SaveFile(content, extension, container, contentType);
        }

        public async Task<string> SaveFile(byte[] content, string extension, string container, string contentType)
        {
            var fileName = $"{Guid.NewGuid()}{extension}";
            string folder = Path.Combine(_env.WebRootPath, container);

            if (!Directory.Exists(folder))
                Directory.CreateDirectory(folder);

            var route = Path.Combine(folder, fileName);

            await File.WriteAllBytesAsync(route, content);

             var currentUrl = $"{ _httpContextAccessor.HttpContext.Request.Scheme}://" +
                                   $"{_httpContextAccessor.HttpContext.Request.Host}";
            var urlDB = Path.Combine(currentUrl, container, fileName).Replace("\\", "/");

            return urlDB;
        }
    }
}
