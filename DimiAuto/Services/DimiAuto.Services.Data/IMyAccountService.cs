using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DimiAuto.Services.Data
{
    public interface IMyAccountService
    {
        Task<ICollection<TModel>> GetMyCarsAsync<TModel>(string userId);
    }
}
