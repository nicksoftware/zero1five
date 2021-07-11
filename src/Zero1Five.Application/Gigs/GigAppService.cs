using System;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Entities;
using Zero1Five.Categories;
using Zero1Five.Permissions;
using Zero1Five.Products;

namespace Zero1Five.Gigs
{
    public class GigAppService :
        CrudAppService<
            Gig, GigDto, Guid,
            GetPagedGigsRequest,
            CreateUpdateGigDto,
            CreateUpdateGigDto>,
        IGigAppService
    {
        private readonly IGigManager _gigManager;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IGigPictureContainerManager _gigPictureContainerManager;

        public GigAppService(
            IGigRepository repository,
            IGigManager gigManager,
            ICategoryRepository categoryRepository,
            IGigPictureContainerManager gigPictureContainerManager) : base(repository)
        {
            _gigManager = gigManager;
            _categoryRepository = categoryRepository;
            _gigPictureContainerManager = gigPictureContainerManager;
            SetPermissions();
        }

        private void SetPermissions()
        {
            CreatePolicyName = Zero1FivePermissions.Gigs.Create;
            UpdatePolicyName = Zero1FivePermissions.Gigs.Edit;
            DeletePolicyName = Zero1FivePermissions.Gigs.Delete;
        }

        [Authorize(Zero1FivePermissions.Gigs.Create)]
        public override async Task<GigDto> CreateAsync(CreateUpdateGigDto input)
        {
            var storageFileName =
                await _gigPictureContainerManager.SaveAsync(input.Cover.FileName, input.Cover.Content);
            var createdGIg = await _gigManager.CreateAsync(
                input.Title,
                input.CategoryId,
                storageFileName,
                input.Description);

            return await MapToGetOutputDtoAsync(createdGIg);
        }

        [Authorize(Zero1FivePermissions.Gigs.Edit)]
        public override async Task<GigDto> UpdateAsync(Guid id, CreateUpdateGigDto input)
        {
            var gig = await GetGigIfExistsAsync(id);

            if (input.Cover != null)
            {
                var storageFileName = await _gigPictureContainerManager
                    .UpdateAsync(gig.CoverImage, input.Cover.FileName, input.Cover.Content, true);

                gig.CoverImage = storageFileName;
            }

            gig.Description = input.Description;
            gig.Title = input.Title;

            return await MapToGetOutputDtoAsync(await Repository.UpdateAsync(gig));
        }
        public override async Task<PagedResultDto<GigDto>> GetListAsync(GetPagedGigsRequest input)
        {
            var filter = input.Filter?.ToLower().Trim();
            var canFilterByKeyword = !string.IsNullOrEmpty(filter);
            var queryable = await Repository.GetQueryableAsync();
    
            var query =
                from gig in queryable
                join category in _categoryRepository on gig.CategoryId equals category.Id
                select new { category, gig};

            //if input CategoryIdFilter is not null also Filter by Category  
            query = query.WhereIf(input.CategoryId != null, x => x.gig.CategoryId == input.CategoryId);
            
            //Filter by keyword
            query = query.WhereIf(canFilterByKeyword, x =>
                x.gig.Title.ToLower().Contains(filter) ||
                x.gig.Description.ToLower().Contains(filter));
            
            query = query
                .OrderBy(NormalizeSorting(input.Sorting))
                .Skip(input.SkipCount)
                .Take(input.MaxResultCount);

            var queryResult = await AsyncExecuter.ToListAsync(query);
            var gigListDto = queryResult.Select(x =>
            {
                var dto = MapToGetOutputDto(x.gig);
                dto.CategoryName = x.category.Name;
                return dto;
            }).ToList();
            
            var totalCount = await Repository.GetCountAsync();
            return new PagedResultDto<GigDto>()
            {
                TotalCount = totalCount,
                Items = gigListDto,
            };
        }
        public override async Task<GigDto> GetAsync(Guid id)
        { 
            await GetGigIfExistsAsync(id);
            var queryable = await Repository.GetQueryableAsync();
            
            var query =
                from gig in queryable
                join category in _categoryRepository on gig.CategoryId equals category.Id
                where gig.Id == id
                select new {gig, category};
            
            var result = await AsyncExecuter.FirstAsync(query);
            var dto = await MapToGetOutputDtoAsync(result.gig);
            dto.CategoryName = result.category.Name;
            
            return dto;
        }
        [Authorize(Zero1FivePermissions.Gigs.Delete)]
        public override async Task DeleteAsync(Guid id)
        {
            var gig = await GetGigIfExistsAsync(id);
            await Repository.DeleteAsync(gig);
            await _gigPictureContainerManager.DeleteAsync(gig.CoverImage);
        }
        [Authorize(Zero1FivePermissions.Gigs.Publish)]

        public async Task<GigDto> PublishAsync(Guid id)
        {
            var gig = await GetGigIfExistsAsync(id);
            gig.Publish();
            var publishedGig = await Repository.UpdateAsync(gig);
            return await MapToGetOutputDtoAsync(publishedGig);
        }
        
        [Authorize(Zero1FivePermissions.Gigs.Publish)]
        public async Task<GigDto> UnpublishAsync(Guid id)
        {
            var gig = await GetGigIfExistsAsync(id);
            gig.UnPublish();

            var publishedGig = await Repository.UpdateAsync(gig);
            return await MapToGetOutputDtoAsync(publishedGig);
        }

        public async Task<ListResultDto<CategoryDto>> GetLookUpCategoriesAsync()
        {
            var categories = await _categoryRepository.GetListAsync();

            var categoryDtoList = categories.Select(c => new CategoryDto
            {
                Id = c.Id,
                Name = c.Name,
                Description = c.Description
            }).ToList();

            return new ListResultDto<CategoryDto>
            {
                Items = categoryDtoList
            };
        }

        public async Task<GigDto> ChangeCoverAsync(Guid id, ChangeGigImageDto input)
        {
            var gig = await GetGigIfExistsAsync(id);
            var imageFileName = await _gigPictureContainerManager.UpdateAsync(
                gig.CoverImage,
                input.CoverImage,
                input.Content,
                true);
            
            await _gigManager.ChangeCoverImageAsync(gig, imageFileName);
            var result = await Repository.UpdateAsync(gig);
            return await MapToGetOutputDtoAsync(result);
        }

        private async Task<Gig> GetGigIfExistsAsync(Guid id)
        {
            var gigDb = await Repository.FindAsync(id);
            if (gigDb == null)
            {
                throw new EntityNotFoundException(typeof(Gig), id);
            }

            return gigDb;
        }
        
        private static string NormalizeSorting(string sorting)
        {
            if (string.IsNullOrEmpty(sorting))
            {
                return $"gig.{nameof(Product.Title)}";
            }

            if (sorting.Contains("categoryName", StringComparison.OrdinalIgnoreCase))
            {
                return sorting.Replace("categoryName", "category.Name", StringComparison.OrdinalIgnoreCase);
            }

            return $"gig.{sorting}";
        }
    }
}