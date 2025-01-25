﻿namespace Feramor.Abp.AuditLogging.ElasticSearch;

public static class ElasticSearchErrorCodes
{
    public const string UriMustStartWithHttpOrHttps = "ErrorCodes:Error:000001";
    public const string UriMustStartWithHttps = "ErrorCodes:Error:000002";
    public const string TestFailed = "ErrorCodes:Error:000003";
    public const string CreateIndexFailed = "ErrorCodes:Error:000003";
}