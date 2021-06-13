using System.ComponentModel.DataAnnotations;
using Zero1Five.Common;

namespace Zero1Five.Gigs
{
    public class UpdateGigDto
    {
        [Required]
        [MaxLength(GigConsts.TitleMaxLength)]
        public string Title { get; set; }
        public SaveFileDto Cover { get; set; }
        [Required]
        [MaxLength(GigConsts.DescriptionMaxLength)]
        public string Description { get; set; }
    }
}