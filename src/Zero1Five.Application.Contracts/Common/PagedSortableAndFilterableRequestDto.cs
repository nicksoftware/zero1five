using Volo.Abp.Application.Dtos;

namespace Zero1Five.Common
{
    public class PagedSortableAndFilterableRequestDto :PagedAndSortedResultRequestDto
    {
        public string SearchKeyword { get; set; }

    }
}