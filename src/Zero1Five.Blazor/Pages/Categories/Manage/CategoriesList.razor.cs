using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Blazorise;
using Blazorise.DataGrid;
using Microsoft.AspNetCore.Authorization;
using Volo.Abp.Application.Dtos;
using Zero1Five.Categories;
using Zero1Five.Permissions;

namespace Zero1Five.Blazor.Pages.Categories.Manage
{
    public partial class CategoriesList
    {
        private IReadOnlyList<CategoryDto> CategoryList { get; set; }

        private int PageSize { get; } = LimitedResultRequestDto.DefaultMaxResultCount;
        private int CurrentPage { get; set; }
        private string CurrentSorting { get; set; }
        private int TotalCount { get; set; }

        private bool CanCreateCategory { get; set; }
        private bool CanEditCategory { get; set; }
        private bool CanDeleteCategory { get; set; }

        private CreateUpdateCategoryDto NewCategory { get; set; }

        private Guid EditingCategoryId { get; set; }
        private CreateUpdateCategoryDto EditingCategory { get; set; }

        private Modal CreateCategoryModal { get; set; }
        private Modal EditCategoryModal { get; set; }

        private Validations CreateValidationsRef;

        private Validations EditValidationsRef;

        public CategoriesList()
        {
            NewCategory = new CreateUpdateCategoryDto();
            EditingCategory = new CreateUpdateCategoryDto();
        }

        protected override async Task OnInitializedAsync()
        {
            await SetPermissionsAsync();
            await GetCategorysAsync();
        }

        private async Task SetPermissionsAsync()
        {
            CanCreateCategory = await AuthorizationService.IsGrantedAsync(Zero1FivePermissions.Categories.Create);

            CanEditCategory = await AuthorizationService
                .IsGrantedAsync(Zero1FivePermissions.Categories.Edit);

            CanDeleteCategory = await AuthorizationService
                .IsGrantedAsync(Zero1FivePermissions.Categories.Delete);
        }

        private async Task GetCategorysAsync()
        {
            var result = await CategoryAppService.GetListAsync(
                new GetCategoryListDto
                {
                    MaxResultCount = PageSize,
                    SkipCount = CurrentPage * PageSize,
                    Sorting = CurrentSorting
                }
            );

            CategoryList = result.Items;
            TotalCount = (int)result.TotalCount;
        }

        private async Task OnDataGridReadAsync(DataGridReadDataEventArgs<CategoryDto> e)
        {
            CurrentSorting = e.Columns
                .Where(c => c.Direction != SortDirection.None)
                .Select(c => c.Field + (c.Direction == SortDirection.Descending ? " DESC" : ""))
                .JoinAsString(",");
            CurrentPage = e.Page - 1;

            await GetCategorysAsync();

            await InvokeAsync(StateHasChanged);
        }

        private void OpenCreateCategoryModal()
        {
            CreateValidationsRef.ClearAll();

            NewCategory = new CreateUpdateCategoryDto();
            CreateCategoryModal.Show();
        }

        private void CloseCreateCategoryModal()
        {
            CreateCategoryModal.Hide();
        }

        private void OpenEditCategoryModal(CategoryDto Category)
        {
            EditValidationsRef.ClearAll();

            EditingCategoryId = Category.Id;
            EditingCategory = ObjectMapper.Map<CategoryDto, CreateUpdateCategoryDto>(Category);
            EditCategoryModal.Show();
        }

        private async Task DeleteCategoryAsync(CategoryDto Category)
        {
            var confirmMessage = L["CategoryDeletionConfirmationMessage", Category.Name];
            if (!await Message.Confirm(confirmMessage))
            {
                return;
            }

            await CategoryAppService.DeleteAsync(Category.Id);
            await GetCategorysAsync();
        }

        private void CloseEditCategoryModal()
        {
            EditCategoryModal.Hide();
        }

        private async Task CreateCategoryAsync()
        {
            if (CreateValidationsRef.ValidateAll())
            {
                await CategoryAppService.CreateAsync(NewCategory);
                await GetCategorysAsync();
                CreateCategoryModal.Hide();
            }
        }

        private async Task UpdateCategoryAsync()
        {
            if (EditValidationsRef.ValidateAll())
            {
                await CategoryAppService.UpdateAsync(EditingCategoryId, EditingCategory);
                await GetCategorysAsync();
                EditCategoryModal.Hide();
            }
        }
    }
}