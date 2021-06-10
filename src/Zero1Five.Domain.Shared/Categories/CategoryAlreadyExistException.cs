using Volo.Abp;

namespace Zero1Five.Categories
{
    public class CategoryAlreadyExistException:BusinessException
    {
        public CategoryAlreadyExistException(string name)
        :base(Zero1FiveDomainErrorCodes.CategoryAlreadyExistException)
        {
            WithData(nameof(name), name);
        }
    }
}