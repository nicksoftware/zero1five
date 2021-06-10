using System;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using Shouldly;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Repositories;
using Xunit;
using Zero1Five.Common;
using Zero1Five.Products;
using Zero1Five.TestBase;

namespace Zero1Five.Gigs
{
    public sealed class GigAppServiceTests:Zero1FiveApplicationTestBase
    {
        private readonly IGigAppService _gigAppService;
        private readonly IGigRepository _gigRepository;
        
        public GigAppServiceTests()
        {
            _gigRepository = GetRequiredService<IGigRepository>();
            _gigAppService = GetRequiredService<IGigAppService>();
        }

        [Fact]
        public async Task CreateAsync_Should_CreateGig()
        {
            var input = new CreateGigDto()
            {
                Title = "Coolest Gig",
                Description = "This is a cool new gig",
                CoverImage =  "coolgImage.jpg"
            };

            var result =await _gigAppService.CreateAsync(input);
            
            result.Id.ShouldNotBe(Guid.Empty);
            result.Title.ShouldBe(input.Title);
            result.Description.ShouldBe(input.Description);
        }

        [Fact]
        public async Task GetAsync_Should_GetGigOfGivenId()
        {

            var gigId = Guid.Parse(Zero1FiveTestData.GigId);
            var result =await _gigAppService.GetAsync(gigId);
            
            result.ShouldNotBeNull();
            result.Id.ShouldBe(gigId);
        }

        [Fact]
        public async Task GetListAsync_ShouldGetGigList()
        {
            var result = await _gigAppService.GetListAsync(new PagedSortableAndFilterableRequestDto());
            
            result.Items.Count.ShouldBeGreaterThanOrEqualTo(0);
        }
        [Fact]
        public async Task UpdateAsync_ShouldUpdateGig()
        {
            var gig =await WithUnitOfWorkAsync<Gig>(() => _gigRepository.FirstOrDefaultAsync());
            
            var input = new UpdateGigDto()
            {
                Title = "newUpdated",
                Description = "this is Some new Description here buddy"
            };

            var result =await _gigAppService.UpdateAsync(gig.Id, input);

            result.ShouldNotBeNull();
            result.Id.ShouldBe(gig.Id);
            result.Title.ShouldBe(input.Title);
            result.Description.ShouldBe(input.Description);
        }
        [Fact]
        public async Task DeleteAsync_Should_DeleteGig_Async()
        {
            //Given
            var gigs = (await _gigAppService.GetListAsync(new PagedAndSortedResultRequestDto())).Items;
            var gig = gigs[0];
            //When
            await _gigAppService.DeleteAsync(gig.Id);
            var results = (await _gigAppService.GetListAsync(new PagedAndSortedResultRequestDto())).Items;
            //Then
            results.ShouldNotContain(gig);
        }
    }
}