using System.ComponentModel.DataAnnotations;

namespace Zero1Five.Gigs
{
    public class UpdateGigDto
    {
        [Required]
        [MaxLength(GigConsts.TitleMaxLength)]
        public string Title { get; set; }
        [Required]
        [MaxLength(GigConsts.DescriptionMaxLength)]
        public string Description { get; set; }
    }
}