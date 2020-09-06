using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Services
{
    public interface IFileStore
    {
        Task<string> EditFile(byte[] content, string extension, string container,
                                string route, string contentType);

        Task DeleteFile(string route, string container);
        Task<string> SaveFile(byte[] content, string extension, string container, string contentType);
    }
}
