using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using DimiAuto.Data.Common.Repositories;
using DimiAuto.Services.Mapping;
using DimiAuto.Web.ViewModels.Ad;
using DimiAuto.Models.CarModel;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace DimiAuto.Services.Data
{
    public class AdService : IAdService
    {
        private readonly DbContext db;
        private readonly Cloudinary cloudinary;
        private readonly IDeletableEntityRepository<Car> carRepository;

        public AdService(Cloudinary cloudinary, IDeletableEntityRepository<Car> carRepository)
        {
            this.cloudinary = cloudinary;
            this.carRepository = carRepository;
        }

        public async Task<string> CreateAdAsync(CreateAdInputModel input,string userId)
        {
            var car = new Car
            {
                Cc = input.Cc,
                Fuel = input.Fuel,
                Color = input.Color,
                Door = input.Door,
                Condition = input.Condition,
                EuroStandart = input.EuroStandart,
                Extras = input.Extras,
                Gearbox = input.GearBox,
                Horsepowers = input.Hp,
                Km = input.Km,
                Location = input.Location,
                Make = input.Make,
                Model = input.Model,
                Modification = input.Modification,
                MoreInformation = input.MoreInformation,
                Price = input.Price,
                Type = input.Type,
                TypeOfAd = input.TypeOfAd,
                YearOfProduction = input.YearOfProduction,
                UserId = userId,
            };
            await this.carRepository.AddAsync(car);
            await this.carRepository.SaveChangesAsync();
            return car.Id;
        }

        public async Task<IEnumerable<string>> UploadImgsAsync(ICollection<IFormFile> files)
        {
            var list = new List<string>();
            foreach (var file in files)
            {
                byte[] destinationImage;
                using (var memoryStream= new MemoryStream())
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
            var car = this.carRepository.All().FirstOrDefault(x => "id="+x.Id == id);
            car.ImgsPaths = result;
            await this.carRepository.SaveChangesAsync();
        }
        //Check for better code
        protected  string GetFirstImgOnly(string carId)
        {
            return this.carRepository.All().FirstOrDefault(x => x.Id == carId).ImgsPaths.Split(",", StringSplitOptions.RemoveEmptyEntries).First().ToString();
        }

        public IEnumerable<Car> GetAllAds()
        {
            return this.carRepository.All();
        }

        //public IEnumerable<T> GetTopSixViewsAd<T>()
        //{
        //   IQueryable<Car> query = this.carRepository.All().OrderBy(x=>x.Views).Take(6);
        //   return query.To<T>().ToList();
        //}

        public IEnumerable<CarAdsViewModel> GetTopFourViewsAd()
        {
            return this.carRepository.All().Take(4).Select(x => new CarAdsViewModel 
            {
                Id = x.Id,
                ImgPad = this.carRepository
                    .All()
                    .FirstOrDefault(a => a.Id == x.Id)
                    .ImgsPaths.Split(",", StringSplitOptions.RemoveEmptyEntries)
                    .First()
                    .ToString(),               
                Make = x.Make,
                Model = x.Model,
                Modification = x.Modification,
                Price = x.Price,
                UserId = x.UserId,
                Year = x.YearOfProduction.ToString("dd.mm.yyyy"),
                Fuel = x.Fuel
            }).ToList();
        }
       
    }
}
