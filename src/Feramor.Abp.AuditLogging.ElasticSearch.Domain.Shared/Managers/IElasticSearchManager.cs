using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;

namespace Feramor.Abp.AuditLogging.ElasticSearch.Managers;

public interface IElasticSearchManager: ITransientDependency
{
    public Task<bool> TestConnectionAsync();
}