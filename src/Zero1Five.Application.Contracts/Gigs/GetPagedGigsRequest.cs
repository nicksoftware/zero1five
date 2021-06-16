using System;
using Zero1Five.Common;

namespace Zero1Five.Gigs
{
    public class GetPagedGigsRequest :PagedSortableAndFilterableRequestDto
    {
        
        public Guid? CategoryId { get; set; }
    }
}