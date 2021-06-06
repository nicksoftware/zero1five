using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;

namespace Zero1Five.Data
{
    /* This is used if database provider does't define
     * IZero1FiveDbSchemaMigrator implementation.
     */
    public class NullZero1FiveDbSchemaMigrator : IZero1FiveDbSchemaMigrator, ITransientDependency
    {
        public Task MigrateAsync()
        {
            return Task.CompletedTask;
        }
    }
}