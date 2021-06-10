using System;
using System.Threading.Tasks;
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
        private readonly IGigManager _gigManager;

        public GigAppService(
            IGigRepository repository,
            IGigManager gigManager) : base(repository)
        {
            _gigManager = gigManager;
            CreatePolicyName = Zero1FivePermissions.Gigs.Create;
            UpdatePolicyName = Zero1FivePermissions.Gigs.Edit;
            DeletePolicyName = Zero1FivePermissions.Gigs.Delete;
        }

        public override async Task<GigDto> CreateAsync(CreateGigDto input)
        {
            var createdGIg = await _gigManager.CreateAsync(input.Title, input.CoverImage, input.Description);
            return await MapToGetOutputDtoAsync(createdGIg);
            // return base.CreateAsync(input);
        }
    }
}