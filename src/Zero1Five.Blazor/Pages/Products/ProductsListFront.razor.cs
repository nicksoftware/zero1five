using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using Volo.Abp.Application.Dtos;
using Zero1Five.Categories;
using Zero1Five.Permissions;
using Zero1Five.Gigs;
namespace Zero1Five.Blazor.Pages.Products
{
    public partial class ProductsListFront
    {
        private IJSRuntime JsRuntime { get; set; }
        IReadOnlyList<CategoryDto> categories = new List<CategoryDto>() { new CategoryDto { Id = Guid.Empty, Name = "Choose Category" } };
        private string Search { get; set; }
        [Inject]
        public ICategoryAppService CategoryAppService { get; set; }

        protected override async Task OnInitializedAsync()
        {
            categories = (await CategoryAppService.GetListAsync(new PagedAndSortedResultRequestDto() { MaxResultCount = 1000 })).Items;
            await JsRuntime.InvokeVoidAsync("startCarousel");            
            await base.OnInitializedAsync();

        }  
    }
}