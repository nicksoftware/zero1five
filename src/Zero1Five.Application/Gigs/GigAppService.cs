using System;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;
using Zero1Five.Permissions;

namespace Zero1Five.Gigs
{
    public class GigAppService :
        CrudAppService<
            Gig, GigDto, Guid,
            PagedAndSortedResultRequestDto,
            CreateGigDto,
            UpdateGigDto>,
            IGigAppService
    {
        public GigAppService(IGigRepository repository) : base(repository)
        {
            CreatePolicyName = Zero1FivePermissions.Gigs.Create;
            UpdatePolicyName = Zero1FivePermissions.Gigs.Edit;
            DeletePolicyName = Zero1FivePermissions.Gigs.Delete;
        }
    }
}