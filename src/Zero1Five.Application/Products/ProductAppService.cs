using System;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Zero1Five.Products
{
    public class ProductAppService :
        CrudAppService<
            Product, ProductDto, Guid,
            PagedAndSortedResultRequestDto,
            CreateProductDto,
            UpdateProductDto>,
            IProductAppService
    {
        public ProductAppService(IProductRepository repository) : base(repository)
        {
            // GetPolicyName = Zero1FivePermissions.Products.Default;
            // GetListPolicyName = Zero1FivePermissions.Products.Default;
            // CreatePolicyName = Zero1FivePermissions.Products.Create;
            // UpdatePolicyName = Zero1FivePermissions.Products.Update;
            // DeletePolicyName = Zero1FivePermissions.Products.Delete;
        }
    }
}