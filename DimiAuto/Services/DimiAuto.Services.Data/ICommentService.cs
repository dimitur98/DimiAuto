using DimiAuto.Web.ViewModels.Ad;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DimiAuto.Services.Data
{
    public interface ICommentService
    {
        Task<IEnumerable<TModel>> GetComments<TModel>(string carId);

        Task Create(CarCommentsInputModel input);
    }
}
