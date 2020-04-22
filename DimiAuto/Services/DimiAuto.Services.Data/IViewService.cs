namespace DimiAuto.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;

    public interface IViewService
    {
        Task AddViewAsync(string user, string carId);

        int GetViewsCount(string carId);
    }
}
