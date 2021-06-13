using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using Blazorise;
using JetBrains.Annotations;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Components.Routing;
using Zero1Five.Categories;
using Zero1Five.Common;
using Zero1Five.Gigs;
using Zero1Five.Products;

namespace Zero1Five.Blazor.Pages.Products.Manage
{
    public partial class ProductForm
    {
        [Inject]
        private IProductAppService ProductAppService { get; set; }

        private CreateUpdateProductDto model { get; set; } = new();
        private string PreviewImage { get; set; } = Constants.DefaultCover;
        [Parameter]
        public  Guid? Id { get; set; }
        [Inject] private NavigationManager NavigationManager { get; set; }
        private CategoryDto SelectedCategory { get; set; } = new(){Name = ""};
        private GigLookUpDto SelectedGig { get; set; } = new(){Title = ""};
        private IReadOnlyList<CategoryDto> CategoryList { get; set; } = new List<CategoryDto>();
        private IReadOnlyList<GigLookUpDto> GigList { get; set; } = new List<GigLookUpDto>();

        protected override async Task OnInitializedAsync()
        {
            if (Id != null)
            {
                model  =ObjectMapper.Map<ProductDto,CreateUpdateProductDto>( await ProductAppService.GetAsync((Guid)Id));
            }            
            await LookUpCategoriesAsync();
            await LookUpGigsAsync();
            await base.OnInitializedAsync();
        }

        private async Task LookUpCategoriesAsync()
        {
            CategoryList = (await ProductAppService.GetLookUpCategoriesAsync()).Items;

            if (Id != null)
                SelectedCategory = CategoryList.First(s => s.Id == model.CategoryId);
        }

        private async Task LookUpGigsAsync()
        {
            GigList = (await ProductAppService.GetGigLookUpAsync()).Items;

            if (Id != null)
                SelectedGig = GigList.First(s => s.Id == model.GigId);
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
        private async Task OnInputFileChange(InputFileChangeEventArgs e)
        {
            var image = e.GetMultipleFiles(1).FirstOrDefault();
            if (image == null) return;
            var byteArray = new byte[image.Size];
            await image.OpenReadStream().ReadAsync(byteArray);
            model.Cover = new SaveFileDto {Content = byteArray, FileName = image.Name};
            await Preview(image);
        }

        private async Task Preview(IBrowserFile file)
        {
            var format = "image/png";
            var imageFile = file;
            var resizedImageFile = await imageFile.RequestImageFileAsync(format, 800, 400);
            var buffer = new byte[resizedImageFile.Size];
            await resizedImageFile.OpenReadStream().ReadAsync(buffer);
            PreviewImage = $"data:{format};base64,{Convert.ToBase64String(buffer)}";
        }
        private void OnValidSubmit(EditContext context)
        {
            Task.Run(CreateUpdateProductAsync);
            StateHasChanged();
        }
        private async Task CreateUpdateProductAsync()
        {
            model.CategoryId = SelectedCategory.Id;
            model.GigId = SelectedGig.Id;

            if (Id == null)
            {
                await ProductAppService.CreateAsync(model);
            }
            else
            {
                await ProductAppService.UpdateAsync((Guid)Id,model);
            }

            NavigationManager.NavigateTo("/manage/products");
        }

        private Func<CategoryDto, string> ConvertCategory = p => p?.Name;
        private Func<GigLookUpDto, string> ConvertGig = p => p?.Title;
        

    }
}