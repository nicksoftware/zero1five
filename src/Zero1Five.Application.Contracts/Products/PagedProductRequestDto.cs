using System;
using Zero1Five.Common;

namespace Zero1Five.Products
{
    public class PagedProductRequestDto:PagedSortableAndFilterableRequestDto
    {
        public Guid? CategoryId { get; set; }
    }
}