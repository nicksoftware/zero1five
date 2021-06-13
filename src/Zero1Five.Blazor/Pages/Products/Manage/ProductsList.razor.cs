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
using Zero1Five.Gigs;
using Zero1Five.Permissions;
using Zero1Five.Products;

namespace Zero1Five.Blazor.Pages.Products.Manage
{
    public partial class ProductsList
    {
        [Inject]
        public IProductAppService ProductAppService { get; set; }
        private IReadOnlyList<ProductDto> ProductList { get; set; }
    [Inject] public NavigationManager NavigationManager { get; set; }
        private int PageSize { get; } = LimitedResultRequestDto.DefaultMaxResultCount;
        private int CurrentPage { get; set; }
        private string CurrentSorting { get; set; }
        private int TotalCount { get; set; }

        private bool CanCreateProduct { get; set; }
        private bool CanEditProduct { get; set; }
        private bool CanDeleteProduct { get; set; }
        private bool CanPublish { get; set; }

        protected override async Task OnInitializedAsync()
        {
            await SetPermissionsAsync();
            await GetProductsAsync();
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

        private async Task HandleProductSubmitted(CreateUpdateProductDto product)
        {
           await GetProductsAsync();
        }
        private async Task GetProductsAsync()
        {
            var result = await ProductAppService.GetListAsync(
                new PagedProductRequestDto
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
                await Message.Success($"Product successfully {message}");
            }
            else
            {
                var message = product.IsPublished ? "Published " : "UnPublished";
                await Message.Error("Failed to " + message);
            }
            await InvokeAsync(StateHasChanged);
            await GetProductsAsync();

        }

        private void OpenProductForm(ProductDto product = null)
        {
            Guid? id = product?.Id;
            NavigationManager.NavigateTo("/manage/products/editor/"+id);
        }
        private async Task DeleteProductAsync(ProductDto product)
        {
            var confirmMessage = L["ProductDeletionConfirmationMessage", product.Title];
            
            if (!await Message.Confirm(confirmMessage)) return;

            await ProductAppService.DeleteAsync(product.Id);
            await GetProductsAsync();
        }
    }
}