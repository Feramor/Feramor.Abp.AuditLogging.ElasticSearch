using System.Threading.Tasks;

namespace GeneralTest.Data;

public interface IGeneralTestDbSchemaMigrator
{
    Task MigrateAsync();
}
