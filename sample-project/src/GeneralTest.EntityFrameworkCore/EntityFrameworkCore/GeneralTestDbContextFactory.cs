using System;
using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace GeneralTest.EntityFrameworkCore;

/* This class is needed for EF Core console commands
 * (like Add-Migration and Update-Database commands) */
public class GeneralTestDbContextFactory : IDesignTimeDbContextFactory<GeneralTestDbContext>
{
    public GeneralTestDbContext CreateDbContext(string[] args)
    {
        var configuration = BuildConfiguration();
        
        GeneralTestEfCoreEntityExtensionMappings.Configure();

        var builder = new DbContextOptionsBuilder<GeneralTestDbContext>()
            .UseSqlServer(configuration.GetConnectionString("Default"));
        
        return new GeneralTestDbContext(builder.Options);
    }

    private static IConfigurationRoot BuildConfiguration()
    {
        var builder = new ConfigurationBuilder()
            .SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), "../GeneralTest.DbMigrator/"))
            .AddJsonFile("appsettings.json", optional: false);

        return builder.Build();
    }
}
