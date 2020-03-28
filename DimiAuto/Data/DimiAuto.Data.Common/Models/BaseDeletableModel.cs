namespace DimiAuto.Data.Common.Models
{
    using System;

    public abstract class BaseDeletableModel<TKey> : IDeletableEntity<TKey>, IDeletableEntity
    {
        public bool IsDeleted { get; set; }

        public DateTime? DeletedOn { get; set; }
    }
}
