using System;
using Xunit;
using System.Threading.Tasks;
using Shouldly;
using Zero1Five.TestBase;

namespace Zero1Five.Gigs
{
    public class GigTests
    {
       private readonly Guid _categoryId = Guid.Parse(Zero1FiveTestData.CategoryId);

       readonly Guid _id = Guid.NewGuid();
        readonly string _title = "new Gig";
        readonly string _description = "new gig description";
        readonly string _coverImage = "someImage.com";
        [Fact]
        public  void CreateAsync_Should_CreateANewGig()
        {
            var result = Gig.Create(_id,_categoryId, _title, _coverImage, _description);
            
            result.Id.ShouldBe(_id);
            result.Title.ShouldBe(_title);
            result.Description.ShouldBe(_description);
            result.CoverImage.ShouldBe(_coverImage);
        }

        [Fact]
        public void PublishGigAsync_Should_PublishGig()
        {
            var gig = Gig.Create(_id, _categoryId,_title, _coverImage, _description);
            gig.Publish();
            gig.IsPublished.ShouldBe(true);
        }
        
        [Fact]
        public void UnPublishGigAsync_Should_UnPublishGig()
        {
            var gig = Gig.Create(_id, _categoryId,_title, _coverImage, _description);
            gig.UnPublish();
            gig.IsPublished.ShouldBe(false);
        }
    }
}