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
        private IReadOnlyList<GigDto> GigList { get; set; } = new List<GigDto>();
        protected override async Task OnInitializedAsync()
        {
            var input = new GetPagedGigsRequest()
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