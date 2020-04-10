using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DimiAuto.Services.Data
{
    public interface IViewService
    {
        Task AddViewAsync(string user, string carId);

        int GetViewsCount(string carId);
    }
}
