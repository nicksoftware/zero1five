using Zero1Five.Gigs;

namespace Zero1Five.Blazor.Pages.Home
{
    public class Gig : GigDto
    {
        public float Rating { get; set; }
    }
    public partial class TopGigSection
    {
        private Gig[] _gigs = new[]
        {
            new Gig()
            {
                Title = "Software Developers",
                CoverImage =  "./images/explore/e5.jpg",
                Rating = 5.7f, 
                CategoryName = "Software",
                Description =
                    "Lorem ipsum dolor sit amet, consectetur adipisicing elit, sed do eiusmod tempor incid ut labore et dolore magna aliqua..."
            },
            new Gig()
            {
                Title = "Software Developers",
                Rating = 5.7f,
                CoverImage =  "./images/explore/e4.jpg",
                CategoryName = "Software",
                Description =
                    "Lorem ipsum dolor sit amet, consectetur adipisicing elit, sed do eiusmod tempor incid ut labore et dolore magna aliqua..."
            },
            new Gig()
            {
                Title = "Software Developers", Rating = 5.7f, CategoryName = "Software",
                CoverImage =  "./images/explore/e6.jpg",
                Description =
                    "Lorem ipsum dolor sit amet, consectetur adipisicing elit, sed do eiusmod tempor incid ut labore et dolore magna aliqua..."
            }
        };
    }
}