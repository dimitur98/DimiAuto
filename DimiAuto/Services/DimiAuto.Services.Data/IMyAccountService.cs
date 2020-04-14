using DimiAuto.Models.CarModel;
using DimiAuto.Web.ViewModels.MyAccount;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DimiAuto.Services.Data
{
    public interface IMyAccountService
    {
        Task<ICollection<MyCarsViewModel>> GetMyCarsAsync(string userId);

        Task DeleteAccount(string userId);
    }
}
