namespace DimiAuto.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;

    using DimiAuto.Web.ViewModels.Img;
    using Microsoft.AspNetCore.Http;

    public interface IImgService
    {
        Task AddImgToCurrentAdAsync(string result, string id);

        Task<string> UploadImgAsync(IFormFile file);

        bool IsValidImg(string fileExtension, string contentType, long size);
    }
}
