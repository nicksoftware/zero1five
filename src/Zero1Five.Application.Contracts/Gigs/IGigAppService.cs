using System;
using System.Collections.Generic;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using System.Threading.Tasks;
using Zero1Five.Categories;
using Zero1Five.Common;

namespace Zero1Five.Gigs
{

    public interface IGigAppService: ICrudAppService<
            GigDto,
            Guid,
            GetPagedGigsRequest,
            CreateUpdateGigDto,
            CreateUpdateGigDto>
    {
        Task<GigDto> PublishAsync(Guid id);
        Task<GigDto> UnpublishAsync(Guid id);
        Task<ListResultDto<CategoryDto>> GetLookUpCategories();
    }
}