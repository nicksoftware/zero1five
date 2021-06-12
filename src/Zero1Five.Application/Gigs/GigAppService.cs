using System;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Repositories;
using Zero1Five.AzureStorage.Gig;
using Zero1Five.Common;
using Zero1Five.Permissions;
using Zero1Five.Products;

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
        private readonly GigPictureContainerManager _gigPictureContainerManager;

        public GigAppService(
            IGigRepository repository,
            IGigManager gigManager,
            GigPictureContainerManager gigPictureContainerManager) : base(repository)
        {
            _gigManager = gigManager;
            _gigPictureContainerManager = gigPictureContainerManager;
            CreatePolicyName = Zero1FivePermissions.Gigs.Create;
            UpdatePolicyName = Zero1FivePermissions.Gigs.Edit;
            DeletePolicyName = Zero1FivePermissions.Gigs.Delete;
        }

        public override async Task<GigDto> CreateAsync(CreateGigDto input)
        {
            var storageFileName = await _gigPictureContainerManager.SaveAsync(input.CoverImage.FileName, input.CoverImage.Content);
            var createdGIg = await _gigManager.CreateAsync(input.Title, storageFileName, input.Description);
            return await MapToGetOutputDtoAsync(createdGIg);
        }

        public override async Task<GigDto> UpdateAsync(Guid id, UpdateGigDto input)
        {
            var gig = await GetGigAsync(id);

            if (input.Cover != null)
            {
                var storageFileName = await _gigPictureContainerManager
                    .UpdateAsync(gig.CoverImage,input.Cover.FileName, input.Cover.Content, true);
              
                gig.CoverImage = storageFileName;
            }
            gig.Description = input.Description;
            gig.Title = input.Title;

            return await MapToGetOutputDtoAsync( await Repository.UpdateAsync(gig));
        }
        public override async Task DeleteAsync(Guid id)
        {
            var gig = await GetGigAsync(id);
           await Repository.DeleteAsync(gig);
           await _gigPictureContainerManager.DeleteAsync(gig.CoverImage);
        }

        private async Task<Gig> GetGigAsync(Guid id)
        {
            var gig = await GetGigAsync(id);
            return gig;
        }
        public async Task<GigDto> PublishAsync(Guid id)
        {
            var gig = await Repository.FindAsync(id);

            if (gig != null)
            {
                gig.Publish();
                var publishedGig = await Repository.UpdateAsync(gig);
                return await MapToGetOutputDtoAsync(publishedGig);
            }
            throw new EntityNotFoundException(typeof(Gig), id);
        }
        public async Task<GigDto> UnpublishAsync(Guid id)
        {
            var gig = await Repository.FindAsync(id);
            
            if (gig != null)
            {
                gig.UnPublish();
                var publishedGig = await Repository.UpdateAsync(gig);
                return await MapToGetOutputDtoAsync(publishedGig);
            }
            throw new EntityNotFoundException(typeof(Gig), id);
        }
    }
}