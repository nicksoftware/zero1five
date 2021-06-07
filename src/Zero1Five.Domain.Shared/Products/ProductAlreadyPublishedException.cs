using System;
using Volo.Abp;

namespace Zero1Five.Products
{
    public class ProductAlreadyPublishedException : BusinessException
    {
        public ProductAlreadyPublishedException(string name)
        :base(Zero1FiveDomainErrorCodes.ProductAlreadyPublishedException)
        {
            WithData("name",name);
        }
    }
}