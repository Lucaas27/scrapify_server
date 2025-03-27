using Hangfire;
using Hangfire.Console;
using Hangfire.Mongo;
using Hangfire.Mongo.Migration.Strategies;
using Hangfire.Mongo.Migration.Strategies.Backup;
using MongoDB.Driver;

namespace scrapify.API.Services;

public static class HangfireService
{
    public static void AddHangfireService(this WebApplicationBuilder builder)
    {
        var mongoUrlBuilder = new MongoUrlBuilder(builder.Configuration.GetConnectionString("HangfireDb"));
        var mongoClient = new MongoClient(mongoUrlBuilder.ToMongoUrl());
        
        // Add Hangfire services to the container
        builder.Services.AddHangfire(configuration => configuration
            .SetDataCompatibilityLevel(CompatibilityLevel.Version_180)
            .UseSimpleAssemblyNameTypeSerializer()
            .UseRecommendedSerializerSettings()
            .UseConsole()
            .UseMongoStorage(mongoClient, mongoUrlBuilder.DatabaseName, new MongoStorageOptions
            {
                MigrationOptions = new MongoMigrationOptions
                {
                    MigrationStrategy = new MigrateMongoMigrationStrategy(),
                    BackupStrategy = new CollectionMongoBackupStrategy()
                },
                CheckConnection = false,
                Prefix = "scrapify.hangfire",
                CheckQueuedJobsStrategy = CheckQueuedJobsStrategy.TailNotificationsCollection
            })
        );
        
        builder.Services.AddHangfireServer(options =>
        {
            options.ServerName = "ScrapifyServer";
            options.WorkerCount = Environment.ProcessorCount * 5; // Adjust the number of workers as needed
        });
    }
}