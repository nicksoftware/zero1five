using System;
using System.ComponentModel.DataAnnotations;

namespace Zero1Five.Categories
{
    public class CreateUpdateCategoryDto
    {
        [Required]
        [MaxLength(CategoryConsts.NameMaxlength)]
        public string Name { get; set; }

        [Required]
        [MaxLength(CategoryConsts.DescriptionMaxlength)]
        public string Description { get; set; }
    }
}