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
        }

        private async Task SetPermissionsAsync()
        {
            CanCreateProduct = await AuthorizationService.IsGrantedAsync(Zero1FivePermissions.Categories.Create);

            CanEditProduct = await AuthorizationService
                .IsGrantedAsync(Zero1FivePermissions.Categories.Edit);

            CanDeleteProduct = await AuthorizationService
                .IsGrantedAsync(Zero1FivePermissions.Categories.Delete);
        }

        private async Task LookUpCategoriesAsync()
        {
            var results = ProductAppService.GetLookUpCategories();
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

        private void OpenCreateProductModal()
        {
            CreateValidationsRef.ClearAll();

            NewProduct = new();
            CreateProductModal.Show();
        }

        private void CloseCreateProductModal()
        {
            CreateProductModal.Hide();
        }

        private void OpenEditProductModal(ProductDto Product)
        {
            EditValidationsRef.ClearAll();

            EditingProductId = Product.Id;
            EditingProduct = ObjectMapper.Map<ProductDto, UpdateProductDto>(Product);
            EditProductModal.Show();
        }

        private async Task DeleteProductAsync(ProductDto Product)
        {
            var confirmMessage = L["ProductDeletionConfirmationMessage", Product.Name];
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
        }

        private async Task CreateProductAsync()
        {
            if (CreateValidationsRef.ValidateAll())
            {
                await ProductAppService.CreateAsync(NewProduct);
                await GetProductsAsync();
                CreateProductModal.Hide();
            }
        }

        private async Task UpdateProductAsync()
        {
            if (EditValidationsRef.ValidateAll())
            {
                await ProductAppService.UpdateAsync(EditingProductId, EditingProduct);
                await GetProductsAsync();
                EditProductModal.Hide();
            }
        }
    }
}