namespace DimiAuto.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using DimiAuto.Data.Common.Models;
    using DimiAuto.Data.Common.Repositories;
    using DimiAuto.Data.Models;
    using DimiAuto.Models.CarModel;
    using DimiAuto.Services.Mapping;
    using DimiAuto.Web.ViewModels.Ad;
    using Microsoft.EntityFrameworkCore;

    public class CommentService : ICommentService
    {
        private readonly IAuditInfo auditInfo;
        private readonly IDeletableEntityRepository<Comment> commentRepository;
        private readonly IDeletableEntityRepository<Car> carRepository;
        private readonly IAdService adService;

        public CommentService(IDeletableEntityRepository<Comment> commentRepository, IDeletableEntityRepository<Car> carRepository)
        {
            this.commentRepository = commentRepository;
            this.carRepository = carRepository;
        }

        public async Task CreateAsync(CarCommentsInputModel input)
        {
            var comment = new Comment
            {
                UserId = input.UserId,
                Content = input.Content,
                CarId = input.CarId,
                Title = input.Title,
            };

            await this.commentRepository.AddAsync(comment);

            await this.commentRepository.SaveChangesAsync();
        }

        public async Task<ICollection<TModel>> GetComments<TModel>(string carId)
        {
            var comments = await this.commentRepository.All().Where(x => x.CarId == carId).To<TModel>().ToListAsync();
            return comments;
        }

        

    }
}
