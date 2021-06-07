using System;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Zero1Five.Permissions;

namespace Zero1Five.Categories
{
    public class CategoryAppService :
        CrudAppService<Category, CategoryDto, Guid, PagedAndSortedResultRequestDto,
        CreateUpdateCategoryDto, CreateUpdateCategoryDto>, ICategoryAppService
    {
        public CategoryAppService(ICategoryRepository repository) : base(repository)
        {
            GetPolicyName = Zero1FivePermissions.Category.Default;
            GetListPolicyName = Zero1FivePermissions.Category.Default;
            CreatePolicyName = Zero1FivePermissions.Category.Create;
            UpdatePolicyName = Zero1FivePermissions.Category.Update;
            DeletePolicyName = Zero1FivePermissions.Category.Delete;
        }
    }
}