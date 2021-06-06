using System.Threading.Tasks;

namespace Zero1Five.Data
{
    public interface IZero1FiveDbSchemaMigrator
    {
        Task MigrateAsync();
    }
}
