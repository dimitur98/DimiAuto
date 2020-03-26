using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using DimiAuto.Data.Common.Repositories;
using DimiAuto.Models.CarModel;
using DimiAuto.Web.ViewModels.Img;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using DimiAuto.Data.Models;
using Microsoft.AspNetCore.Identity;

namespace DimiAuto.Services.Data
{
    public class ImgService : IImgService
    {
        private readonly Cloudinary cloudinary;
        private readonly IDeletableEntityRepository<Car> carRepository;
        private readonly IAdService adService;
        private readonly UserManager<ApplicationUser> userManager;

        public ImgService(Cloudinary cloudinary, IDeletableEntityRepository<Car> carRepository, IAdService adService)
        {
            this.cloudinary = cloudinary;
            this.carRepository = carRepository;
            this.adService = adService;
        }

        public async Task<IEnumerable<string>> UploadImgsAsync(ImgUploadInputModel input)
        {
            var list = new List<string>();
            for (int i = 1; i <= 10; i++)
            {

                var file = (IFormFile)input.GetType().GetProperty("File" + i).GetValue(input, null);
                if (file == null)
                {
                    continue;
                }

                var fileFileExtension = Path.GetExtension(file.FileName);
                if (!string.Equals(fileFileExtension, ".jpg", StringComparison.OrdinalIgnoreCase)
                    && !string.Equals(fileFileExtension, ".png", StringComparison.OrdinalIgnoreCase)
                    && !string.Equals(fileFileExtension, ".jpeg", StringComparison.OrdinalIgnoreCase))
                {
                    continue;

                }

                if (!string.Equals(file.ContentType, "image/jpg", StringComparison.OrdinalIgnoreCase) &&
                    !string.Equals(file.ContentType, "image/jpeg", StringComparison.OrdinalIgnoreCase) &&
                    !string.Equals(file.ContentType, "image/pjpeg", StringComparison.OrdinalIgnoreCase) &&                    
                    !string.Equals(file.ContentType, "image/x-png", StringComparison.OrdinalIgnoreCase) &&
                    !string.Equals(file.ContentType, "image/png", StringComparison.OrdinalIgnoreCase))
                {
                    continue;
                }

                if (file.Length > 10485760)
                {
                    continue;
                }
                
                byte[] destinationImage;
                using (var memoryStream = new MemoryStream())
                {
                    await file.CopyToAsync(memoryStream);
                    destinationImage = memoryStream.ToArray();
                }

                using (var destinationStream = new MemoryStream(destinationImage))
                {
                    var uploadParams = new ImageUploadParams()
                    {
                        File = new FileDescription(file.FileName, destinationStream),
                    };
                    var res = await this.cloudinary.UploadAsync(uploadParams);
                    list.Add(res.Uri.AbsoluteUri);
                }
            }

            return list;
        }

        public async Task AddImgToCurrentAdAsync(string result, string id)
        {
            var car = this.carRepository.All().FirstOrDefault(x => "id=" + x.Id == id);
            car.ImgsPaths = result;
            await this.carRepository.SaveChangesAsync();
        }

    }
}
