using System;
using System.Collections.Generic;
using Volo.Abp.Application.Dtos;
using Zero1Five.Products;

namespace Zero1Five.Gigs
{
    public class GigDto : FullAuditedEntityDto<Guid>
    {
        public string Title { get; set; }
        public string CategoryName { get; set; }
        public string CoverImage { get; set; }
        public string Description { get; set; }
        public float Rating { get; set; }
        
        public  ICollection<ProductDto> Products { get; set; }
    }
}