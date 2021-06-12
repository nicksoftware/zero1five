using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Zero1Five.Common;
using Zero1Five.Gigs;

namespace Zero1Five.Blazor.Pages.Gigs.Manage
{
    public partial class EditGig
    {
        
        [Parameter] public Guid Id { get; set; }
        [Inject]
        private IGigAppService GigAppService { get; set; }

        private UpdateGigDto model { get; set; } = new();
        private string PreviewImage { get; set; }
        [Inject] private NavigationManager NavigationManager { get; set; }

        protected override async Task OnInitializedAsync()
        {
            var gig =await GigAppService.GetAsync(Id);
            model = ObjectMapper.Map<GigDto,UpdateGigDto>(gig);
            PreviewImage = gig.CoverImage;
        }

        private async Task OnInputFileChange(InputFileChangeEventArgs e)
        {
            var image = e.GetMultipleFiles(1).FirstOrDefault();
            if (image == null) return;
            var byteArray = new byte[image.Size];
            await image.OpenReadStream().ReadAsync(byteArray);
            model.Cover = new SaveFileDto{ Content = byteArray, FileName = image.Name };
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
            Task.Run(UpdateGigAsync);
            StateHasChanged();
        }

        private async Task UpdateGigAsync()
        {
            await GigAppService.UpdateAsync(Id,model);
            NavigationManager.NavigateTo("/manage/gigs");
        }
    }
}