using DimiAuto.Models.CarModel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;


namespace DimiAuto.Services.Data.AreaServices
{
    public interface IAdministrationService
    {

        Task ApproveAsync(string carId);

        Task<IEnumerable<TModel>> GetAllAdsAsync<TModel>();

        Task DeleteAsync(string carId);

        Task UnDeleteAsync(string carId);


        //Task PermamentDeleteAsync(string carId);
    }
}
