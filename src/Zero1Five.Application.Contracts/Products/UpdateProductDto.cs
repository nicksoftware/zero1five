using System;

namespace Zero1Five.Products
{
    public class UpdateProductDto
    {
        public string Name { get; set; }
        public Guid CategoryId { get; set; }
        public string CoverImage { get; set; }
        public string Description { get; set; }
        public bool IsPublished { get; set; }
    }
}