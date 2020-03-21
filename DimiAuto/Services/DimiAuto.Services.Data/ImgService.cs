using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using DimiAuto.Data.Common.Repositories;
using DimiAuto.Models.CarModel;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DimiAuto.Services.Data
{
    public class ImgService : IImgService
    {
        private readonly Cloudinary cloudinary;
        private readonly IDeletableEntityRepository<Car> carRepository;

        public ImgService(Cloudinary cloudinary, IDeletableEntityRepository<Car> carRepository)
        {
            this.cloudinary = cloudinary;
            this.carRepository = carRepository;
        }

        public async Task<IEnumerable<string>> UploadImgsAsync(ICollection<IFormFile> files)
        {
            var list = new List<string>();
            foreach (var file in files)
            {
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
