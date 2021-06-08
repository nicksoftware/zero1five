using System;
using Volo.Abp.Application.Dtos;

namespace Zero1Five.Gigs
{
    public class GigDto : FullAuditedEntityDto<Guid>
    {
        public string Title { get; set; }
        public string CoverImage { get; set; }
        public string Description { get; set; }
    }
}