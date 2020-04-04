using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DimiAuto.Services.Data
{
    public interface IViewService
    {
        Task AddView(string userId, string carId);

        int GetViews(string carId);
    }
}
