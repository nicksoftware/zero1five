using System;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Zero1Five.Gigs
{

    public interface IGigAppService: ICrudAppService<
            GigDto,
            Guid,
            PagedAndSortedResultRequestDto,
            CreateGigDto,
            UpdateGigDto>
    {

    }
}