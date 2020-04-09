using DimiAuto.Models.CarModel;
using DimiAuto.Web.ViewModels.Administration;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;


namespace DimiAuto.Services.Data.AreaServices
{
    public interface IAdministrationService
    {

        Task ApproveAsync(string carId);

        Task<IEnumerable<AdViewModel>> GetAllAdsAsync();

        Task DeleteAsync(string carId);

        Task UnDeleteAsync(string carId);


        //Task PermamentDeleteAsync(string carId);
    }
}
