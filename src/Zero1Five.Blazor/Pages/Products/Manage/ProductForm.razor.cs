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
using Microsoft.AspNetCore.Components.Web;
using Microsoft.Extensions.Logging;
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
        [Inject] public ILogger<ProductForm> Logger { get; set; }
        [Inject] private NavigationManager NavigationManager { get; set; }
        private CategoryDto SelectedCategory { get; set; } = new(){Name = ""};
        private GigLookUpDto SelectedGig { get; set; } = new(){Title = ""};
        private IReadOnlyList<CategoryDto> CategoryList { get; set; } = new List<CategoryDto>();
        private IReadOnlyList<GigLookUpDto> GigList { get; set; } = new List<GigLookUpDto>();

        protected override async Task OnInitializedAsync()
        {
            if (Id != null)
            {
                var productDto = await ProductAppService.GetAsync((Guid) Id);
                model  =ObjectMapper.Map<ProductDto,CreateUpdateProductDto>( productDto);
                PreviewImage = productDto.CoverImage;
            }            
            await LookUpCategoriesAsync();
            await LookUpGigsAsync();
            await base.OnInitializedAsync();
        }

        private async Task LookUpCategoriesAsync()
        {
            CategoryList = (await ProductAppService.GetLookUpCategoriesAsync()).Items;

            if (Id != null && model.Id != Guid.Empty)
                SelectedCategory = CategoryList.First(s => s.Id == model.CategoryId);
        }

        private async Task LookUpGigsAsync()
        {
            GigList = (await ProductAppService.GetGigLookUpAsync()).Items;

            if (Id != null && model.Id != Guid.Empty)
                SelectedGig = GigList.First(s => s.Id == model.GigId);
            
            StateHasChanged();
        }
        
        // private void SelectedGigChangedHandler(Guid id)
        // {
        //     if (id == Guid.Empty) return;
        //     SelectedGig = GigList.First(x => x.Id == id);
        // }
        // private void SelectedCategoryChangedHandler(Guid id)
        // {
        //     if (id == Guid.Empty) return;
        //     SelectedCategory = CategoryList.First(x => x.Id == id);
        // }
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

            if (Id != null)
            {
                await ProductAppService.UpdateAsync((Guid)Id,model);
            }
            else
            {
                await ProductAppService.CreateAsync(model);
            }

            NavigationManager.NavigateTo("/manage/products");
        }

        private Func<CategoryDto, string> ConvertCategory = p => p?.Name;
        private Func<GigLookUpDto, string> ConvertGig = p => p?.Title;


        private void GigSelectionChanged(GigLookUpDto gig)
        {
            SelectedGig = gig;
        }

        private async Task<IEnumerable<GigLookUpDto>> SearchGigs(string arg)
        {
            return await Task.FromResult(GigList.Where(x => x.Title.ToLower().Contains(arg.ToLower())).ToList());
        }
    }
}