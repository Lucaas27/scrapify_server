using Microsoft.Extensions.Configuration;
using Microsoft.Playwright;
using scrapify.Scraper.Interfaces;
using scrapify.Scraper.Models;

namespace scrapify.Scraper.Scrapers.Poc;

public class PocScraper(IBrowser browser, IConfiguration configuration) : IScraper
{
    private readonly string _baseUrl = configuration.GetValue<string>("Playwright:Urls:scrapingclub") 
                                       ?? throw new InvalidOperationException("Scraping club URL not configured");
    private readonly IBrowser _browser = browser ?? throw new ArgumentNullException(nameof(browser));

    public async Task Scrape()
    {
        // Create a new page
        var page = await _browser.NewPageAsync();
        
        try
        {
            await page.GotoAsync(_baseUrl);
            var products = new List<ProductPoc>();
            
            ArgumentNullException.ThrowIfNull(products);
            
            var productHtmlElements = page.Locator("css=.post");
            
            ArgumentNullException.ThrowIfNull(productHtmlElements);
            
            for (var index = 0; index < await productHtmlElements.CountAsync(); index++)
            {
                // get the current product HTML element
                var productHtmlElement = productHtmlElements.Nth(index);

                // retrieve the name and price
                var name = (await productHtmlElement.Locator("h4").TextContentAsync())?.Trim();
                var price = (await productHtmlElement.Locator("h5").TextContentAsync())?.Trim();

                // create a new Product instance and
                // add it to the list
                var product = new ProductPoc { Name = name, Price = price };
                products.Add(product);
            }
            
            foreach (var product in products)
            {
                Console.WriteLine($"Name: {product.Name}, Price: {product.Price}");
            }

        }
        finally
        {
            await page.CloseAsync();
        }
    }
}