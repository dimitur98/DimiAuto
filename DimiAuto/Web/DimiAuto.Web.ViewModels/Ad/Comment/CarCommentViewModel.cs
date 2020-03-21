namespace DimiAuto.Web.ViewModels.Ad.Comment
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using DimiAuto.Data.Models;
    using DimiAuto.Services.Mapping;

    public class CarCommentViewModel : IMapFrom<Comment>
    {
        public string Content { get; set; }

        public string UserUserName { get; set; }

        public DateTime CreatedOn { get; set; }

        public string Title { get; set; }
    }
}
