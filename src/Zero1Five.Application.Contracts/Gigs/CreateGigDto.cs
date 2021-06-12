using System.ComponentModel.DataAnnotations;
using Zero1Five.Common;

namespace Zero1Five.Gigs
{
    public class CreateGigDto
    {

        [Required]
        [MaxLength(GigConsts.TitleMaxLength)]
        public string Title { get; set; }

        [Required]
        public SaveFileDto CoverImage { get; set; } = new();
        
        [Required]
        [MaxLength(GigConsts.DescriptionMaxLength)]
        
        public string Description { get; set; }
    }
}