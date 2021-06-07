using System;
using Volo.Abp.Domain.Entities.Auditing;

namespace Zero1FiveZero1Five.Application.Contracts.Products
{
    public class Product : FullAuditedAggregateRoot<Guid>
    {
        public string Name { get; set; }
        public Guid CategoryId { get; set; }
        public string CoverImage { get; set; }
        public string Description { get; set; }
        public bool IsPublished { get; set; }
    }
}