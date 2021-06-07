using System;
using Volo.Abp.Application.Dtos;

namespace Zero1Five.Products
{
    public class ProductDto : FullAuditedEntityDto<Guid>
    {
        public string Title { get; set; }
        public Guid CategoryId { get; set; }
        public string CoverImage { get; set; }
        public string Description { get; set; }
        public bool IsPublished { get; set; }
    }
}