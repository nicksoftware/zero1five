using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using Volo.Abp.Application.Dtos;
using Zero1Five.Categories;

namespace Zero1Five.Blazor.Pages
{
    public partial class Index
    {
        IReadOnlyList<CategoryDto> categories = new List<CategoryDto>() { new CategoryDto { Id = Guid.Empty, Name = "Choose Category" } };
        private bool arrows = true;
        private bool delimiters = true;
        private bool autocycle = true;
        private Transition transition = Transition.Slide;
        public string Search { get; set; }
        [Inject]
        public ICategoryAppService CategoryAppService { get; set; }
        struct CarousalPost
        {
            public Color Color { get; set; }
        }

        CarousalPost[] carouselPosts =
        {
            new CarousalPost{ Color = Color.Primary},
            new CarousalPost{ Color = Color.Secondary},
            new CarousalPost{ Color = Color.Warning},
            new CarousalPost{ Color = Color.Info},
            new CarousalPost{ Color = Color.Error},
        };


        protected override async Task OnInitializedAsync()
        {
            categories = (await CategoryAppService.GetListAsync(new PagedAndSortedResultRequestDto() { MaxResultCount = 1000 })).Items;
            await base.OnInitializedAsync();

        }
    }
}
