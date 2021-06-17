using System;

namespace Zero1Five.Products
{
    public class ProductBuilder
    {
        private Guid _productId;
        private Guid _categoryId = Guid.NewGuid();
        private Guid _gigId = Guid.NewGuid();
        private string _title = "";
        private string _description = "";
        private string _coverImage = "";
        private bool _isPublished = false;

        public ProductBuilder(Guid productId)
        {
            _productId = productId;
        }
        public Product Build()
        {
            var product =
            Product.Create(
            _productId,
            gigId: _gigId,
            categoryId: _categoryId,
            _title,
            _coverImage);

            product.Description = _description;
            product.IsPublished = _isPublished;

            return product;
        }


        public ProductBuilder WithCategoryId(Guid value)
        {
            _categoryId = value;
            return this;
        }

        public ProductBuilder WithGigId(Guid value)
        {
            _gigId = value;
            return this;
        }

        public ProductBuilder WithTitle(string value)
        {
            _title = value;
            return this;
        }

        public ProductBuilder WithDescription(string value)
        {
            _description = value;
            return this;
        }

        public ProductBuilder WithCoverImage(string value)
        {
            _coverImage = value;
            return this;
        }

        public ProductBuilder IsPublished(bool value = false)
        {
            _isPublished = value;
            return this;
        }
    }
}
