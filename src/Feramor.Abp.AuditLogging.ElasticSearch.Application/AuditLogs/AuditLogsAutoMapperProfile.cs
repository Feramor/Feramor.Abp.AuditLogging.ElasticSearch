using AutoMapper;
using Feramor.Abp.AuditLogging.ElasticSearch.Data;

namespace Feramor.Abp.AuditLogging.ElasticSearch.AuditLogs;

public class AuditLogsAutoMapperProfile : Profile
{
    public AuditLogsAutoMapperProfile()
    {
        CreateMap<ElasticSearchAuditLog, AuditLogDto>().MapExtraProperties();
        
        CreateMap<ElasticSearchEntityChange, EntityChangeDto>().MapExtraProperties();

        CreateMap<ElasticSearchEntityPropertyChange, EntityPropertyChangeDto>();

        CreateMap<ElasticSearchAuditLogAction, AuditLogActionDto>().MapExtraProperties();
    }
}