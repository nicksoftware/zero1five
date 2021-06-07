using System;
using Volo.Abp.Domain.Repositories;

namespace Zero1Five.Categories
{
    public interface ICategoryRepository : IRepository<Category, Guid>
    {
    }
}