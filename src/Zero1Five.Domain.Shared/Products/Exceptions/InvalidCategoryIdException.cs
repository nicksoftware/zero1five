using System;
using Volo.Abp;

namespace Zero1Five.Products
{
    public class InvalidCategoryIdException : BusinessException
    {
        public InvalidCategoryIdException(Guid categoryId)
        :base(Zero1FiveDomainErrorCodes.InvalidCategoryIdException)
        {
            WithData("categoryId", categoryId);
        }
    }
}