using System;
using Volo.Abp.Application.Dtos;

namespace Zero1Five.Products
{
    public class ProductDto : FullAuditedEntityDto<Guid>
    {
        public Guid CategoryId { get; set; }
        public Guid GigId { get; set; }
        public string Title { get; set; }
        
        public string CategoryName { get; set; }
        public string GigName { get; set; }
        public string CoverImage { get; set; }
        public string Description { get; set; }
        public bool IsPublished { get; set; }
    }
}