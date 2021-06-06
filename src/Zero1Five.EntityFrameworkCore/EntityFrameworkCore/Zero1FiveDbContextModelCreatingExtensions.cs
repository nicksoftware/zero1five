using Microsoft.EntityFrameworkCore;
using Volo.Abp;

namespace Zero1Five.EntityFrameworkCore
{
    public static class Zero1FiveDbContextModelCreatingExtensions
    {
        public static void ConfigureZero1Five(this ModelBuilder builder)
        {
            Check.NotNull(builder, nameof(builder));

            /* Configure your own tables/entities inside here */

            //builder.Entity<YourEntity>(b =>
            //{
            //    b.ToTable(Zero1FiveConsts.DbTablePrefix + "YourEntities", Zero1FiveConsts.DbSchema);
            //    b.ConfigureByConvention(); //auto configure for the base class props
            //    //...
            //});
        }
    }
}