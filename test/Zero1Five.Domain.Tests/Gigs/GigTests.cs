using System;
using Xunit;
using System.Threading.Tasks;
using Shouldly;

namespace Zero1Five.Gigs
{
    public class GigTests
    {

        Guid id = Guid.NewGuid();
        string title = "new Gig";
        string description = "new gig description";
        string coverImage = "someImage.com";
        [Fact]
        public async Task CreateAsync_Should_CreateANewGig()
        {

            
            var result = Gig.Create(id, title, coverImage, description);
            
            result.Id.ShouldBe(id);
            result.Title.ShouldBe(title);
            result.Description.ShouldBe(description);
            result.CoverImage.ShouldBe(coverImage);
        }

        [Fact]
        public async Task PublishGigAsync_Should_PublishGig()
        {
            var gig = Gig.Create(id, title, coverImage, description);
            gig.Publish();
            gig.IsPublished.ShouldBe(true);
        }
        
        [Fact]
        public async Task UnPublishGigAsync_Should_UnPublishGig()
        {
            var gig = Gig.Create(id, title, coverImage, description);
            gig.UnPublish();
            gig.IsPublished.ShouldBe(false);
        }
    }
}