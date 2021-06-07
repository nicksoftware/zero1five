using System;
using Shouldly;
using Xunit;
using Zero1Five;

namespace Zero1Five.Categories
{
    public class CategoryTests : Zero1FiveDomainTestBase
    {
        [Fact]
        public void Create_ShouldCreateCategory()
        {
            //Given
            Guid id = Guid.NewGuid();
            string name = "Test Category";
            string description = "Test Category Description";
            //When
            var result = Category.Create(id, name, description);
            //Then
            result.Id.ShouldNotBe(Guid.Empty);
            result.Id.ShouldBe(id);
            result.Name.ShouldBe(name);
            result.Description.ShouldBe(description);
        }
    }
}