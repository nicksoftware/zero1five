using Microsoft.AspNetCore.Components;

namespace Zero1Five.Blazor.Components
{
    public partial class Hero
    {
        [Parameter] public string Title { get; set; }   
        [Parameter] public string Subtitle { get; set; }
        [Parameter] public string Image { get; set; }
        [Parameter] public RenderFragment ChildContent { get; set; }
    }
}