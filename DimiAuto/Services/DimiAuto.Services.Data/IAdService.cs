using CloudinaryDotNet;
using DimiAuto.Web.ViewModels.Ad;
using FinalProject.Models.CarModel;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DimiAuto.Services.Data
{
    public interface IAdService
    {
        Task<string> CreateAdAsync(CreateAdInputModel input, string userId);

        Task<IEnumerable<string>> UploadImgs(ICollection<IFormFile> files);

        Task AddImgToCurrentAd(string result,string id);
    }
}
