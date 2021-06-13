using System;
using Volo.Abp;
using Volo.Abp.Domain.Entities.Auditing;

namespace Zero1Five.Products
{
    public class Product : FullAuditedAggregateRoot<Guid>
    {
        public Guid CategoryId { get; private set; }
        public Guid GigId { get; private set; }
        public string Title { get;  set; }
        public string Description { get;  set; }
        public string CoverImage { get; private set; }
        public bool IsPublished { get; set; }
        protected Product() { }
        private Product(Guid id, Guid gigId, Guid categoryId, string title, string cover)
        : base(id)
        {
            GigId = gigId;
            CategoryId = categoryId;
            Title = title;
            CoverImage = cover;
        }

        public static Product Create(Guid id, Guid gigId, Guid categoryId, string title, string cover)
        {
            return new Product(id, gigId, categoryId, title, cover);
        }

        internal void SetCover(string cover)
        {
            CoverImage = Check.NotNullOrWhiteSpace(cover, nameof(cover));
        }
        internal void ChangeCategory(Guid categoryId)
        {
            if (categoryId == Guid.Empty) throw new InvalidCategoryIdException(categoryId);

            CategoryId = categoryId;
        }
    }
}