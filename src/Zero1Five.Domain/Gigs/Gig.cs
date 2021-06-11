using System;
using System.Collections.Generic;
using Volo.Abp.Domain.Entities.Auditing;
using Zero1Five.Products;

namespace Zero1Five.Gigs
{
    public class Gig : FullAuditedAggregateRoot<Guid>
    {
        public string Title { get; set; }
        public string CoverImage { get; set; }
        public string Description { get; set; }
        public ICollection<Product> Products { get; set; }
        public bool IsPublished { get;private set; }

        protected Gig() { }
        private Gig(Guid id, string title, string coverImage, string description) : base(id)
        {
            Title = title;
            CoverImage = coverImage;
            Description = description;
        }

        internal static Gig Create(Guid id, string title, string coverImage, string description)
        {
            return new Gig(id, title, coverImage, description);
        }

        public void Publish()
        {
           if(IsPublished) return;

           IsPublished = true;
        }

        public void UnPublish()
        {
            if(!IsPublished) return;
            IsPublished = false;
        }
    }
}