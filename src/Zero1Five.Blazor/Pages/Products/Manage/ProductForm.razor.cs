using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using Blazorise;
using JetBrains.Annotations;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Routing;
using Zero1Five.Categories;
using Zero1Five.Gigs;
using Zero1Five.Products;

namespace Zero1Five.Blazor.Pages.Products.Manage
{
    public partial class ProductForm
    {
        [Inject]
        private IProductAppService ProductAppService { get; set; }

        private CreateUpdateProductDto Product { get; set; } = new();
        
        [Parameter]
        public  Guid? Id { get; set; }
        [Inject] private NavigationManager NavigationManager { get; set; }
        private CategoryDto SelectedCategory { get; set; } = new();
        private GigLookUpDto SelectedGig { get; set; } = new();
        private IReadOnlyList<CategoryDto> CategoryList { get; set; } = new List<CategoryDto>();
        private IReadOnlyList<GigLookUpDto> GigList { get; set; } = new List<GigLookUpDto>();
        
        private Validations CreateValidationsRef;
        private Validations EditValidationsRef;

        protected override Task OnParametersSetAsync()
        {

            // if (Product.Id == Guid.Empty)
            //     SelectedGig = GigList.First();
            // else
            //     SelectedCategory = CategoryList.First(x => x.Id == Product.CategoryId);
            return base.OnParametersSetAsync();
        }

        protected override async Task OnInitializedAsync()
        {
            await LookUpCategoriesAsync();
            await LookUpGigsAsync();
            if (Id != Guid.Empty)
            {
                Product  =ObjectMapper.Map<ProductDto,CreateUpdateProductDto>( await ProductAppService.GetAsync((Guid)Id));
            }            
            await base.OnInitializedAsync();
        }

        private async Task LookUpCategoriesAsync()
        {
            CategoryList = (await ProductAppService.GetLookUpCategoriesAsync()).Items;

            if (Product.Id != Guid.Empty)
                SelectedCategory = CategoryList.First(s => s.Id == Product.CategoryId);
        }

        private async Task LookUpGigsAsync()
        {
            GigList = (await ProductAppService.GetGigLookUpAsync()).Items;

            if (Product.Id != Guid.Empty)
                SelectedGig = GigList.First(s => s.Id == Product.GigId);
        }
        
        private void SelectedGigChangedHandler(Guid id)
        {
            if (id == Guid.Empty) return;
            SelectedGig = GigList.First(x => x.Id == id);
        }
        private void SelectedCategoryChangedHandler(Guid id)
        {
            if (id == Guid.Empty) return;
            SelectedCategory = CategoryList.First(x => x.Id == id);
        }
        private async Task CreateUpdateProductAsync()
        {
            if (!CreateValidationsRef.ValidateAll())
            {
                return;
            }

            Product.CategoryId = SelectedCategory.Id;
            Product.GigId = SelectedGig.Id;

            if (Id == Guid.Empty)
            {
                await ProductAppService.CreateAsync(Product);
            }
            else
            {
                await ProductAppService.UpdateAsync(Product.Id,Product);
            }

            NavigationManager.NavigateTo("/manage/products");
        }
    }
}