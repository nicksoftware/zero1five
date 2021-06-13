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
            var title = "first Title";
            var cover = "image.jpg";
            var firstCategory = Guid.NewGuid();
            var product = Product.Create(
                id: Guid.NewGuid(),
                gigId:Guid.NewGuid(),
                categoryId: firstCategory,
                title: title,
                cover: cover
            );
            //When
                product.ChangeCategory(Guid.NewGuid());
            //Then
                product.CategoryId.ShouldNotBe(firstCategory);            
        }
        [Fact]
        public void Should_Change_Cover()
        {
            //Given
            var title = "first Title";
            var cover = "image.jpg";
            
            var firstCategory = Guid.NewGuid();
            
            var product = Product.Create(
                id: Guid.NewGuid(),
                gigId:Guid.NewGuid(),
                categoryId: firstCategory,
                title: title,
                cover: cover
            );
            //When
            var newImage = "newImage.jpg";
            product.SetCover(newImage);
            //Then
            product.CoverImage.ShouldNotBe(cover);   
            product.CoverImage.ShouldBe(newImage);
        }
    }
}