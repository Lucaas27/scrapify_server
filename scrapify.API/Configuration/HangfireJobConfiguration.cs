using Hangfire;
using scrapify.API.Interfaces;

namespace scrapify.API.Configuration;

public static class HangfireJobConfiguration
{
    public static void ConfigureHangfireJobs(this WebApplication app)
    {
        // Configure Hangfire dashboard and server
        app.UseHangfireDashboard("/hangfire", new DashboardOptions
        {
            AppPath = "/swagger",
            DashboardTitle = "Hangfire Dashboard"
        });

        using var scope = app.Services.CreateScope();
        var jobs = scope.ServiceProvider.GetRequiredService<IJobs>();
        RecurringJob.AddOrUpdate<IJobs>("DisplayCurrentDateTime", x => jobs.DisplayCurrentDateTime(null), Cron.Minutely);
    }
}