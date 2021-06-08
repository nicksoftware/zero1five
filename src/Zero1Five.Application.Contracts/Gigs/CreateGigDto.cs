using System.ComponentModel.DataAnnotations;

namespace Zero1Five.Gigs
{
    public class CreateGigDto
    {

        [Required]
        [MaxLength(GigConsts.TitleMaxLength)]
        public string Title { get; set; }
        [Required]
        [MaxLength(GigConsts.CoverImageMaxLength)]
        public string CoverImage { get; set; }
        [Required]
        [MaxLength(GigConsts.DescriptionMaxLength)]
        public string Description { get; set; }
    }
}