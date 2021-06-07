using System;

namespace Zero1Five.Products
{
    public class PublishProductDto
    {
        public Guid Product { get; set; }
        public bool IsPublished { get; set; }
    }
}