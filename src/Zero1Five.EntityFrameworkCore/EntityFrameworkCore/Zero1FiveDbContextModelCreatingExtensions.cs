using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Volo.Abp;

namespace Zero1Five.EntityFrameworkCore
{
    public static class Zero1FiveDbContextModelCreatingExtensions
    {
        public static void ConfigureZero1Five(this ModelBuilder builder)
        {
            Check.NotNull(builder, nameof(builder));
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}