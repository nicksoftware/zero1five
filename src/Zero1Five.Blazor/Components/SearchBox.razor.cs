using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Volo.Abp.Application.Dtos;
using Volo.Abp.AspNetCore.Components.Notifications;
using Zero1Five.Categories;
using Zero1Five.Products;

namespace Zero1Five.Blazor.Components
{
    public partial class SearchBox
    {
        IReadOnlyList<CategoryDto> CategoryList = new List<CategoryDto>() { new CategoryDto { Id = Guid.Empty, Name = "Choose Category" } };

        [Inject] public ICategoryAppService CategoryAppService { get; set; }
        [Parameter]
        public CategoryDto Category { get; set; }
        
        [Parameter]
        public EventCallback<CategoryDto> CategoryChanged { get; set; }
        
        [Parameter]
        public string Keyword { get; set; }
        [Parameter] public EventCallback OnSubmit { get; set; }
        [Parameter] 
        public EventCallback<string> KeywordChanged { get; set; }

        protected override async Task OnInitializedAsync()
        {
            await LoadCategoriesAsync();
        }

        private async Task LoadCategoriesAsync()
        {
            var result = await CategoryAppService.GetListAsync(new PagedAndSortedResultRequestDto(){MaxResultCount = 1000});
            CategoryList = result.Items;
        }

        private void OnKeywordChanged(string keyword)
        {
            KeywordChanged.InvokeAsync(keyword);
        }

        private async Task OnCategoryChanged(CategoryDto cat)
        {
            await   CategoryChanged.InvokeAsync(cat);
        }

        private void HandleSearch()
        {
            OnSubmit.InvokeAsync();
        }
    }
}