namespace DimiAuto.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;

    using DimiAuto.Data.Models;

    public interface IFavoriteService
    {
        Task<UserCarFavorite> GetFavoriteCarByUserAndCarIdAsync(string carId, string userId);
    }
}
