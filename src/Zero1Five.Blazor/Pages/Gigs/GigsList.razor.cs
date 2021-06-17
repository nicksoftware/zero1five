using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Microsoft.AspNetCore.Components;
using Volo.Abp.TenantManagement;
using Zero1Five.Categories;
using Zero1Five.Common;
using Zero1Five.Gigs;

namespace Zero1Five.Blazor.Pages.Gigs
{
    public partial class GigsList
    {
        private long TotalCount { get; set; }
        [Parameter]
        public string Keyword { get; set; }
        [Inject] public NavigationManager NavigationManager { get; set; }
        [Parameter]
        [CanBeNull] public CategoryDto Category { get; set; }

        [Inject] public IGigAppService GigAppService { get; set; }
        private IReadOnlyList<GigDto> GigList { get; set; } = new List<GigDto>() { };
        
        protected override async Task OnInitializedAsync()
        {
            await LoadAsync();
        }

        private async Task LoadAsync()
        {
            var input = new GetPagedGigsRequest
            {
                Filter = Keyword,
                CategoryId =  Category?.Id
            };
            var results =await GigAppService.GetListAsync(input);
            GigList = results.Items;
            TotalCount = results.TotalCount;
        }
        private async Task CategoryChanged(CategoryDto category)
        {
            Category = category;
            Console.WriteLine(Category);
            await LoadAsync();
        }

        private void HandleGigClicked(GigDto gig)
        {
            NavigationManager.NavigateTo("/gigs/detail/"+gig.Id);   
        }
        private async Task KeywordChanged(string keyword)
        {
            Keyword = keyword;
            await LoadAsync();
        }
    }
}