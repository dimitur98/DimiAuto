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
        private readonly IDeletableEntityRepository<Comment> commentRepository;


        public CommentService(IDeletableEntityRepository<Comment> commentRepository)
        {
            this.commentRepository = commentRepository;
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
            var comments = await this.commentRepository.All().Where(x => x.CarId == carId).OrderByDescending(x => x.CreatedOn).To<TModel>().ToListAsync();
            return comments;
        }

        public async Task DeleteCommentAsync(string id)
        {
           var comment = await this.commentRepository.All().FirstOrDefaultAsync(x => x.Id == id);
           if (comment == null)
           {
                throw new NullReferenceException();
           }

           comment.IsDeleted = true;
           this.commentRepository.Update(comment);
           await this.commentRepository.SaveChangesAsync();
        }
    }
}
