namespace DimiAuto.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;

    using DimiAuto.Web.ViewModels.Ad;

    public interface ICommentService
    {
        Task<ICollection<TModel>> GetComments<TModel>(string carId);

        Task CreateAsync(CarCommentsInputModel input);

        Task DeleteCommentAsync(string id);
    }
}
