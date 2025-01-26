
## ℹ️ Description

This package is a module for the [ABP Framework](https://abp.io), designed to save audit logs to Elasticsearch using [Elastic.Clients.Elasticsearch](https://www.nuget.org/packages/Elastic.Clients.Elasticsearch) via the [Distributed Event Bus](https://abp.io/docs/latest/framework/infrastructure/event-bus/distributed). Theoretically, it should work in a microservice environment, although it has not been tested yet.

### What is ABP — Open Source Web Application Framework for ASP.NET Core

ABP provides an opinionated architecture for building enterprise software solutions with best practices on the .NET and ASP.NET Core platforms. The ABP Framework is a comprehensive open-source infrastructure for creating modern web applications by adhering to software development best practices and conventions. It includes fundamental infrastructure, production-ready startup templates, application modules, UI themes, tooling, guides, and documentation.

## ℹ️ Roadmap

- [x] Publish audit logs via the [Distributed Event Bus](https://abp.io/docs/latest/framework/infrastructure/event-bus/distributed).
- [x] Handle events and send them to [Elasticsearch](https://www.nuget.org/packages/Elastic.Clients.Elasticsearch).
- [x] Configure Elasticsearch settings using [ABP Settings](https://abp.io/docs/latest/framework/infrastructure/settings).
- [x] Provide an [Angular Module](https://www.npmjs.com/package/@feramor/ng.abp-audit-logging-elastic-search) for configuring Elasticsearch settings.
- [ ] Develop an MVC module for configuring Elasticsearch settings.
- [ ] Implement features for displaying audit logs, actions, and entity changes in Angular and MVC.

## ℹ️ Installation

### Required Packages for Configuring Elasticsearch Settings

To configure Elasticsearch settings using [ABP Settings](https://abp.io/docs/latest/framework/infrastructure/settings), the following packages are required:

- `Feramor.Abp.AuditLogging.ElasticSearch.Application`
- `Feramor.Abp.AuditLogging.ElasticSearch.Application.Contracts`
- `Feramor.Abp.AuditLogging.ElasticSearch.Domain`
- `Feramor.Abp.AuditLogging.ElasticSearch.Domain.Shared`
- `Feramor.Abp.AuditLogging.ElasticSearch.HttpApi`
- `Feramor.Abp.AuditLogging.ElasticSearch.HttpApi.Client`

### Required Packages for Saving Logs to Elasticsearch

To save logs to Elasticsearch, the following packages are needed:

- `Feramor.Abp.AuditLogging.ElasticSearch.Handler`
- `Feramor.Abp.AuditLogging.ElasticSearch.Domain`
- `Feramor.Abp.AuditLogging.ElasticSearch.Domain.Shared`

### Required Packages for Logging to Elasticsearch

To enable logging to Elasticsearch, the following packages are required:

- `Feramor.Abp.AuditLogging.ElasticSearch.Logger`
- `Feramor.Abp.AuditLogging.ElasticSearch.Domain`
- `Feramor.Abp.AuditLogging.ElasticSearch.Domain.Shared`

Additionally, you should add the `Feramor.Abp.AuditLogging.ElasticSearch.Logger` package to the `***.HttpApi.Host` project.
