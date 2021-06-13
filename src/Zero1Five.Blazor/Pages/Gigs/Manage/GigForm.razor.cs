using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Blazorise;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Zero1Five.Categories;
using Zero1Five.Common;
using Zero1Five.Gigs;
using Zero1Five.Products;

namespace Zero1Five.Blazor.Pages.Gigs.Manage
{
    public partial class GigForm
    {
        [Parameter] public Guid? Id { get; set; }
        [Inject] private IGigAppService GigAppService { get; set; }
        private string PreviewImage { get; set; } = Constants.DefaultCover;

        private CreateUpdateGigDto model { get; set; } = new();
        [Inject] private NavigationManager NavigationManager { get; set; }

        protected override async Task OnInitializedAsync()
        {
            if (Id != null)
            {
                var gig = await GigAppService.GetAsync((Guid) Id);
                model = ObjectMapper.Map<GigDto, CreateUpdateGigDto>(gig);
                PreviewImage = gig.CoverImage;
            }
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
            Task.Run(CreateOrUpdateGigAsync);
            StateHasChanged();
        }

        private async Task CreateOrUpdateGigAsync()
        {
            if (Id == null)
            {
                await GigAppService.CreateAsync(model);
            }
            else
            {
                await GigAppService.UpdateAsync((Guid)Id, model);
            }

            NavigationManager.NavigateTo("/manage/gigs");
        }
    }
}