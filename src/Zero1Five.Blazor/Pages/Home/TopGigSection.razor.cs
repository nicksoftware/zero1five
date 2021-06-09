using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Volo.Abp.TenantManagement;
using Zero1Five.Categories;
using Zero1Five.Common;
using Zero1Five.Gigs;

namespace Zero1Five.Blazor.Pages.Home
{
    public partial class TopGigSection
    {
        
        [Inject] public IGigAppService GigAppService { get; set; }
        private IReadOnlyList<GigDto> GigList { get; set; } = new List<GigDto>() { };
        
        // private GigDto[] _gigs = new[]
        // {
        //     new Gig()
        //     {
        //         Title = "Software Developers",
        //         CoverImage =  "./images/explore/e5.jpg",
        //         Rating = 5.7f, 
        //         CategoryName = "Software",
        //         Description =
        //             "Lorem ipsum dolor sit amet, consectetur adipisicing elit, sed do eiusmod tempor incid ut labore et dolore magna aliqua..."
        //     },
        //     new Gig()
        //     {
        //         Title = "Software Developers",
        //         Rating = 5.7f,
        //         CoverImage =  "./images/explore/e4.jpg",
        //         CategoryName = "Software",
        //         Description =
        //             "Lorem ipsum dolor sit amet, consectetur adipisicing elit, sed do eiusmod tempor incid ut labore et dolore magna aliqua..."
        //     },
        //     new Gig()
        //     {
        //         Title = "Software Developers", Rating = 5.7f, CategoryName = "Software",
        //         CoverImage =  "./images/explore/e6.jpg",
        //         Description =
        //             "Lorem ipsum dolor sit amet, consectetur adipisicing elit, sed do eiusmod tempor incid ut labore et dolore magna aliqua..."
        //     }
        // };

        protected override async Task OnInitializedAsync()
        {
            var input = new PagedSortableAndFilterableRequestDto()
            {
                MaxResultCount = 3,
                Sorting = "CreationTime asc"
            };
            
            var results =await GigAppService.GetListAsync(input);
            GigList = results.Items;
            
           await base.OnInitializedAsync();
        }
    }
}