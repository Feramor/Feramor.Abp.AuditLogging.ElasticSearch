﻿using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;

namespace Feramor.Abp.AuditLogging.ElasticSearch.Managers;

public interface IElasticSearchManager: ITransientDependency
{
    public Task<bool> TestConnectionAsync();
    public Task<bool> SaveLogAsync(object logInfo);
    public Task<List<string>> GetIndicesAsync();
    public Task DeleteIndicesAsync(List<string> indices);
}