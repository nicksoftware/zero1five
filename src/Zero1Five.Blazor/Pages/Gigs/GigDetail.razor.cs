using System;
using Microsoft.AspNetCore.Components;

namespace Zero1Five.Blazor.Pages.Gigs
{
    public partial class GigDetail
    {
        [Parameter] public Guid Id { get; set; }
    }
}