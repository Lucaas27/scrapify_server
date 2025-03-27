using System.Globalization;
using Hangfire;
using Hangfire.Server;
using scrapify.API.Interfaces;

namespace scrapify.API.Jobs;

public class JobScheduler: IJobs
{
    [Queue("default")]
    [JobDisplayName("Display current date and time")]
    public void DisplayCurrentDateTime(PerformedContext? context)
    {
        Console.WriteLine($"Current Date and Time: {DateTime.Now.ToString(CultureInfo.CurrentCulture)}");
        Console.WriteLine($"Current Date and Time in UTC: {DateTime.UtcNow.ToString(CultureInfo.CurrentCulture)}");
    }
}