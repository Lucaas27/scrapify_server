using Microsoft.AspNetCore.Mvc;
using Microsoft.Playwright;
using scrapify.Scraper.Scrapers.Poc;

namespace scrapify.API.Controllers;

[ApiController]
[Route("[controller]")]
public class Scrapers(
    ILogger<Scrapers> logger,
    IBrowser browser,
    IConfiguration configuration
)
    : ControllerBase
{
    [HttpGet(Name = "Scrape")]
    public async Task Get()
    {
        var pocScraper = new PocScraper(browser, configuration);
        await pocScraper.Scrape();
        logger.LogInformation("Scraping completed.");
    }
}