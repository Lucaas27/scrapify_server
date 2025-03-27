using Microsoft.Extensions.DependencyInjection;
using Microsoft.Playwright;
using scrapify.Scraper.Configuration;
using scrapify.Scraper.Interfaces;
using scrapify.Scraper.Scrapers.Poc;

namespace scrapify.Scraper.Extensions;

public static class ServiceCollectionExtensions
{
    public static void AddScraperServices(this IServiceCollection services)
    {
        
        services.AddSingleton<PlaywrightService>();
        services.AddHostedService(sp => sp.GetRequiredService<PlaywrightService>());
        services.AddScoped<IScraper, PocScraper>();
        services.AddSingleton<IPlaywright>(sp => 
            sp.GetRequiredService<PlaywrightService>().Playwright);
        services.AddSingleton<IBrowser>(sp => 
            sp.GetRequiredService<PlaywrightService>().Browser);
    }
}