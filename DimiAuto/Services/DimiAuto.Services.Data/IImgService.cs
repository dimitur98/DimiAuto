using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DimiAuto.Services.Data
{
    public interface IImgService
    {
        Task AddImgToCurrentAdAsync(string result, string id);

        Task<IEnumerable<string>> UploadImgsAsync(ICollection<IFormFile> files);
    }
}
