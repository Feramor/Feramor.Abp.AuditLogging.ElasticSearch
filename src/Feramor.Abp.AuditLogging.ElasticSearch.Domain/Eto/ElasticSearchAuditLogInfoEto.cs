using Volo.Abp.Auditing;
using Volo.Abp.EventBus;

namespace Feramor.Abp.AuditLogging.ElasticSearch.Eto;

[EventName("Feramor.Abp.AuditLogging.ElasticSearch.AuditLogInfo")]
public class ElasticSearchAuditLogInfoEto : AuditLogInfo
{
}