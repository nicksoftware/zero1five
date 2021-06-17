using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Castle.DynamicProxy.Generators.Emitters;
using JetBrains.Annotations;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using Volo.Abp.Application.Dtos;
using Zero1Five.Categories;
using Zero1Five.Permissions;
using Zero1Five.Gigs;
using Zero1Five.Products;

namespace Zero1Five.Blazor.Pages.Products
{
    public partial class ProductsListFront
    {
        [CanBeNull] private CategoryDto Category { get; set; }
        private string Keyword { get; set; }
        private IReadOnlyList<ProductDto> ProductList { get; set; } = new List<ProductDto>();

        [Inject] public IProductAppService ProductAppService { get; set; }

        protected override async Task OnInitializedAsync()
        {
            await LoadAsync();
        }

        private async Task LoadAsync()
        {
            var input = new PagedProductRequestDto()
            {
                Filter = Keyword,
                CategoryId = Category?.Id
            };
            var result = await ProductAppService.GetListAsync(input);
            ProductList = result.Items;
        }
    }
}