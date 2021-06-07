using System;

namespace Zero1Five.Domain.Products
{
    public class Product : FullAuditedAggregateRoot<Guid>
    {
        public string Title { get; set; }
        public Guid CategoryId { get; set; }
        public string Description { get; set; }
        public string CoverImage { get; set; }

        protected Product() { }
        private Product(Guid id, string title, string description, string cover)
        {

        }
    }
}