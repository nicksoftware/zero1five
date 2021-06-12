using System;
using System.Collections.Generic;
using Volo.Abp.Application.Dtos;
using Zero1Five.Products;

namespace Zero1Five.Gigs
{
    public class GigDto : FullAuditedEntityDto<Guid>
    {
        private string _title;
        private string _categoryName;
        private string _coverImage;
        private string _description;
        private float _rating;
        private ICollection<ProductDto> _products;
        private bool _isPublished;

        public string Title
        {
            get => _title;
            set => _title = value;
        }

        public string CategoryName
        {
            get => _categoryName;
            set => _categoryName = value;
        }

        public string CoverImage
        {
            get =>  _coverImage;
            set => _coverImage = value;
        }

        public string Description
        {
            get => _description;
            set => _description = value;
        }

        public float Rating
        {
            get => _rating;
            set => _rating = value;
        }

        public ICollection<ProductDto> Products
        {
            get => _products;
            set => _products = value;
        }

        public bool IsPublished
        {
            get => _isPublished;
            set => _isPublished = value;
        }
    }
}