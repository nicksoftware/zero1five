using System;
using Volo.Abp;

namespace Zero1Five.Products
{
    public class ProductAlreadyUnpublishedException : BusinessException
    {
        public ProductAlreadyUnpublishedException(string title)
            : base(Zero1FiveDomainErrorCodes.ProductAlreadyUnpublishedException)
        {
            WithData("title", title);
        }
    }
}