using System;
using Shouldly;
using Xunit;
using Zero1Five.TestBase;

namespace Zero1Five.Products
{
    public class ProductTests : Zero1FiveDomainTestBase
    {
        [Fact]
        public void Should_Create()
        {
            //Given
            Guid id = Guid.NewGuid();
            Guid categoryId = Guid.NewGuid();
            string title = "New Product";
            string description = "New Product";
            string cover = "coverImage.jpg";

            //When
            var gigId = Guid.Parse(Zero1FiveTestData.GigId);
            var newProduct = Product.Create(id, gigId, categoryId, title, cover);
            newProduct.Description = description;
            //Then

            newProduct.ShouldNotBeNull();
            newProduct.Id.ShouldBe(id);
            newProduct.CategoryId.ShouldBe(categoryId);
            newProduct.Title.ShouldBe(title);
            newProduct.CoverImage.ShouldBe(cover);
            newProduct.Description.ShouldBe(description);
        }

        [Fact]
        public void Should_ChangeCategory()
        {
            //Given

            //When

            //Then
        }

        [Fact]
        public void Should_Change_Cover()
        {
            //Given

            //When

            //Then
        }
    }
}