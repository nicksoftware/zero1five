using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Blazorise;
using Blazorise.DataGrid;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;
using Volo.Abp.Application.Dtos;
using Zero1Five.Categories;
using Zero1Five.Permissions;
using Zero1Five.Products;

namespace Zero1Five.Blazor.Pages.Products.Manage
{
    public partial class ProductsList
    {
        [Inject]
        public IProductAppService ProductAppService { get; set; }
        private IReadOnlyList<ProductDto> ProductList { get; set; }

        private int PageSize { get; } = LimitedResultRequestDto.DefaultMaxResultCount;
        private int CurrentPage { get; set; }
        private string CurrentSorting { get; set; }
        private int TotalCount { get; set; }

        private bool CanCreateProduct { get; set; }
        private bool CanEditProduct { get; set; }
        private bool CanDeleteProduct { get; set; }
        private bool CanPublish { get; set; }

        private CategoryDto SelectedCategory { get; set; } = new();
        private IReadOnlyList<CategoryDto> CategoryList { get; set; } = new List<CategoryDto>();
        private CreateProductDto NewProduct { get; set; }

        private Guid EditingProductId { get; set; }
        private UpdateProductDto EditingProduct { get; set; }

        private Modal CreateProductModal { get; set; }
        private Modal EditProductModal { get; set; }

        private Validations CreateValidationsRef;

        private Validations EditValidationsRef;

        public ProductsList()
        {
            NewProduct = new();
            EditingProduct = new();
        }

        protected override async Task OnInitializedAsync()
        {
            await SetPermissionsAsync();
            await GetProductsAsync();
            await LookUpCategoriesAsync();
        }

        private async Task SetPermissionsAsync()
        {
            CanCreateProduct = await AuthorizationService.IsGrantedAsync(Zero1FivePermissions.Products.Create);

            CanPublish = await AuthorizationService.IsGrantedAnyAsync(Zero1FivePermissions.Products.Publish);

            CanEditProduct = await AuthorizationService
                .IsGrantedAsync(Zero1FivePermissions.Products.Edit);

            CanDeleteProduct = await AuthorizationService
                .IsGrantedAsync(Zero1FivePermissions.Products.Delete);
        }

        private async Task LookUpCategoriesAsync()
        {
            CategoryList = (await ProductAppService.GetLookUpCategories()).Items;
        }
        private void SelectedCategoryChangedHandler(Guid id)
        {
            if (id == Guid.Empty) return;

            SelectedCategory = CategoryList.First(x => x.Id == id);
        }
        private async Task GetProductsAsync()
        {
            var result = await ProductAppService.GetListAsync(
                new GetProductListDto
                {
                    MaxResultCount = PageSize,
                    SkipCount = CurrentPage * PageSize,
                    Sorting = CurrentSorting
                }
            );

            ProductList = result.Items;
            TotalCount = (int)result.TotalCount;
        }

        private async Task OnDataGridReadAsync(DataGridReadDataEventArgs<ProductDto> e)
        {
            CurrentSorting = e.Columns
                .Where(c => c.Direction != SortDirection.None)
                .Select(c => c.Field + (c.Direction == SortDirection.Descending ? " DESC" : ""))
                .JoinAsString(",");
            CurrentPage = e.Page - 1;

            await GetProductsAsync();

            await InvokeAsync(StateHasChanged);
        }

        private async Task HandlePublish(ProductDto product)
        {
            Guid id = Guid.Empty;
            if (product.IsPublished)
                id = await ProductAppService.UnPublishAsync(product.Id);
            else
                id = await ProductAppService.PublishAsync(product.Id);

            if (id != Guid.Empty)
            {
                var message = !product.IsPublished ? "Published " : "UnPublished";
                Message.Success($"Product successfully {message}");
            }
            else
            {
                var message = product.IsPublished ? "Published " : "UnPublished";
                Message.Error("Failed to " + message);
            }
            await InvokeAsync(StateHasChanged);
            await GetProductsAsync();

        }

        private void OpenCreateProductModal()
        {
            CreateValidationsRef.ClearAll();
            NewProduct = new();
            CreateProductModal.Show();

        }

        private void CloseCreateProductModal()
        {
            CreateProductModal.Hide();
            SelectedCategory = new();
        }

        private void OpenEditProductModal(ProductDto Product)
        {
            EditValidationsRef.ClearAll();

            EditingProductId = Product.Id;
            SelectedCategory = CategoryList.First(x => x.Id == Product.CategoryId);

            EditingProduct = ObjectMapper.Map<ProductDto, UpdateProductDto>(Product);
            EditProductModal.Show();
        }

        private async Task DeleteProductAsync(ProductDto Product)
        {
            var confirmMessage = L["ProductDeletionConfirmationMessage", Product.Title];
            if (!await Message.Confirm(confirmMessage))
            {
                return;
            }

            await ProductAppService.DeleteAsync(Product.Id);
            await GetProductsAsync();
        }

        private void CloseEditProductModal()
        {
            EditProductModal.Hide();
            SelectedCategory = new();
        }

        private async Task CreateProductAsync()
        {
            if (CreateValidationsRef.ValidateAll())
            {
                NewProduct.CategoryId = SelectedCategory.Id;
                await ProductAppService.CreateAsync(NewProduct);
                await GetProductsAsync();
                CreateProductModal.Hide();
            }
        }

        private async Task FetchLookUpCategoriesAsync()
        {
            CategoryList = (await ProductAppService.GetLookUpCategories()).Items;
        }

        private async Task UpdateProductAsync()
        {
            if (EditValidationsRef.ValidateAll())
            {
                EditingProduct.CategoryId = SelectedCategory.Id;
                await ProductAppService.UpdateAsync(EditingProductId, EditingProduct);
                await GetProductsAsync();
                EditProductModal.Hide();
            }
        }
    }
}