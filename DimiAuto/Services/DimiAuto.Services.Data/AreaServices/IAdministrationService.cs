namespace DimiAuto.Services.Data.AreaServices
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;

    using DimiAuto.Models.CarModel;
    using DimiAuto.Web.ViewModels.Administration;

    public interface IAdministrationService
    {

        Task ApproveAsync(string carId);

        Task<ICollection<AdViewModel>> GetAllAdsAsync();

        Task DeleteAsync(string carId);

        Task UnDeleteAsync(string carId);


        //Task PermamentDeleteAsync(string carId);
    }
}
