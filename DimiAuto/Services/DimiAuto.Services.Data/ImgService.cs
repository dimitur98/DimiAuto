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

        private readonly Cloudinary cloudinary;
        private readonly IDeletableEntityRepository<Car> carRepository;

        public ImgService(Cloudinary cloudinary, IDeletableEntityRepository<Car> carRepository)
        {
            this.cloudinary = cloudinary;
            this.carRepository = carRepository;
        }

        public async Task<string> UploadImgAsync(IFormFile file)
        {
            var list = new List<string>();

            if (file == null)
            {
                return string.Empty;
            }

            var fileFileExtension = Path.GetExtension(file.FileName);
            var contentType = file.ContentType;
            var size = file.Length;

            if (!this.IsValidImg(fileFileExtension, contentType, size) || file == null)
            {
                return string.Empty;
            }

            byte[] destinationImage;
            using (var memoryStream = new MemoryStream())
            {
                await file.CopyToAsync(memoryStream);
                destinationImage = memoryStream.ToArray();
            }
            var result = string.Empty;
            using (var destinationStream = new MemoryStream(destinationImage))
            {
                var uploadParams = new ImageUploadParams()
                {
                    File = new FileDescription(file.FileName, destinationStream),
                };
                var res = await this.cloudinary.UploadAsync(uploadParams);
                var url = res.Uri.AbsoluteUri.Split("/", StringSplitOptions.RemoveEmptyEntries).ToList();

                result = url[url.Count - 2] + "/" + url[url.Count - 1];
            }

            return result;
        }

        public bool IsValidImg(string fileExtension, string contentType, long size)
        {

            if (!string.Equals(fileExtension, ".jpg", StringComparison.OrdinalIgnoreCase)
                && !string.Equals(fileExtension, ".png", StringComparison.OrdinalIgnoreCase)
                && !string.Equals(fileExtension, ".jpeg", StringComparison.OrdinalIgnoreCase))
            {
                return false;
            }

            if (!string.Equals(contentType, "image/jpg", StringComparison.OrdinalIgnoreCase) &&
                !string.Equals(contentType, "image/jpeg", StringComparison.OrdinalIgnoreCase) &&
                !string.Equals(contentType, "image/pjpeg", StringComparison.OrdinalIgnoreCase) &&
                !string.Equals(contentType, "image/x-png", StringComparison.OrdinalIgnoreCase) &&
                !string.Equals(contentType, "image/png", StringComparison.OrdinalIgnoreCase))
            {
                return false;
            }

            if (size >= GlobalConstants.ImgMaxLength)
            {
                return false;
            }

            return true;
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
