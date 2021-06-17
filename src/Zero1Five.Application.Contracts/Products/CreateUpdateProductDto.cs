using System;
using System.ComponentModel.DataAnnotations;
using JetBrains.Annotations;
using Zero1Five.Common;

namespace Zero1Five.Products
{
    [Serializable]
    public class CreateUpdateProductDto
    {
        public Guid Id { get; set; }
        [Required]
        [MaxLength(ProductConsts.TitleMaxLength)]
        public string Title { get; set; }
        [Required]
        public Guid CategoryId { get; set; }
        public Guid GigId { get; set; }
        public SaveFileDto Cover { get; set; }
        [Required]
        [MaxLength(ProductConsts.DescriptionMaxLength)]
        public string Description { get; set; }
        public bool IsPublished { get; set; }
    }
}