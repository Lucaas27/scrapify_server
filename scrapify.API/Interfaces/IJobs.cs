using Hangfire.Server;

namespace scrapify.API.Interfaces;

public interface IJobs
{
    void DisplayCurrentDateTime(PerformedContext? context);
}