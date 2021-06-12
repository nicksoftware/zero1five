using System;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using System.Threading.Tasks;
using Zero1Five.Common;

namespace Zero1Five.Gigs
{

    public interface IGigAppService: ICrudAppService<
            GigDto,
            Guid,
            PagedAndSortedResultRequestDto,
            CreateGigDto,
            UpdateGigDto>
    {
        Task<GigDto> PublishAsync(Guid id);
        Task<GigDto> UnpublishAsync(Guid id);
    }
}