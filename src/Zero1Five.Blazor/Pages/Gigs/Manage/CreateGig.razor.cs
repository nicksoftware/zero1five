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
    public partial class CreateGig
    {
        [Inject] private IGigAppService GigAppService { get; set; }

        private CreateGigDto model { get; set; } = new();
        private string PreviewImage { get; set; }
        [Inject] private NavigationManager NavigationManager { get; set; }
        
        private async Task OnInputFileChange(InputFileChangeEventArgs e)
        {
            var image = e.GetMultipleFiles(1).FirstOrDefault();
            if (image == null) return;
            var byteArray = new byte[image.Size];
            await image.OpenReadStream().ReadAsync(byteArray);
            model.CoverImage.FileName =image.Name;
            model.CoverImage = new SaveFileDto{ Content = byteArray, FileName = image.Name };
            await Preview(image);
        }
        private async Task Preview(IBrowserFile file)
        {
            if(file == null) return;
            var format = "image/png";
            var imageFile = file;
            var resizedImageFile = await imageFile.RequestImageFileAsync(format, 100, 100);
            var buffer = new byte[resizedImageFile.Size];
            await resizedImageFile.OpenReadStream().ReadAsync(buffer);
            PreviewImage = $"data:{format};base64,{Convert.ToBase64String(buffer)}";
        }

        bool valid;
        private void OnValidSubmit(EditContext context)
        {
            valid = true;
            Task.Run(CreateGigAsync);
            StateHasChanged();
        }

        private async Task CreateGigAsync()
        {
            await GigAppService.CreateAsync(model);
            NavigationManager.NavigateTo("/manage/gigs");
        }
    }
}