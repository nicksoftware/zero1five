using System;
using System.ComponentModel.DataAnnotations;

namespace Zero1Five.Products
{
    [Serializable]
    public class CreateProductDto
    {
        [Required]
        [MaxLength(ProductConsts.TitleMaxLength)]
        public string Title { get; set; }
        [Required]
        public Guid CategoryId { get; set; }
        public Guid GigId { get; set; }
        [Required]
        [MaxLength(ProductConsts.CoverMaxLength)]
        public string CoverImage { get; set; }
        [Required]
        [MaxLength(ProductConsts.DescriptionMaxLength)]
        public string Description { get; set; }
        public bool IsPublished { get; set; }
    }
}