using Microsoft.Extensions.Hosting;
using Microsoft.Playwright;

namespace scrapify.Scraper.Configuration;

public class PlaywrightService : IHostedService
{
    private IPlaywright? _playwright;
    private IBrowser? _browser;

    public IPlaywright Playwright => _playwright ?? throw new InvalidOperationException("Playwright not initialized");
    public IBrowser Browser => _browser ?? throw new InvalidOperationException("Browser not initialized");

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        _playwright = await Microsoft.Playwright.Playwright.CreateAsync();
        _browser = await _playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions
        {
            Headless = true,
            SlowMo = 50,
            Args = ["--no-sandbox", "--disable-setuid-sandbox"],
        });
    }

    public async Task StopAsync(CancellationToken cancellationToken)
    {
        if (_browser != null)
            await _browser.DisposeAsync();
        _playwright?.Dispose();
    }
}