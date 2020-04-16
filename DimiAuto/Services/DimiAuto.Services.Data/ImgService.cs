namespace DimiAuto.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using CloudinaryDotNet;
    using CloudinaryDotNet.Actions;
    using DimiAuto.Common;
    using DimiAuto.Data.Common.Repositories;
    using DimiAuto.Models.CarModel;
    using DimiAuto.Web.ViewModels.Img;
    using Microsoft.AspNetCore.Http;

    public class ImgService : IImgService
    {
        private const int ImgMaxLength = 10 * 1024 * 1024;

        private readonly Cloudinary cloudinary;
        private readonly IDeletableEntityRepository<Car> carRepository;

        public ImgService(Cloudinary cloudinary, IDeletableEntityRepository<Car> carRepository)
        {
            this.cloudinary = cloudinary;
            this.carRepository = carRepository;
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

                if (file.Length >= ImgMaxLength)
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
                    var url = res.Uri.AbsoluteUri.Split("/", StringSplitOptions.RemoveEmptyEntries).ToList();

                    list.Add(url[url.Count - 2] + "/" + url[url.Count - 1]);
                }
            }

            return list;
        }

        public async Task AddImgToCurrentAdAsync(string result, string id)
        {
            var car = this.carRepository.All().FirstOrDefault(x => x.Id == id);
            if (car == null)
            {
                throw new NullReferenceException();
            }

            if (car.ImgsPaths == GlobalConstants.DefaultImgCar)
            {
                car.ImgsPaths = result;
            }
            else
            {
                car.ImgsPaths += "," + result;
            }

            await this.carRepository.SaveChangesAsync();
        }
    }
}
