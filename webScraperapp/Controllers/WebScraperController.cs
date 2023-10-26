using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using webapi.Model;
using webapi.Utility;

namespace webapi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WebScraperController : ControllerBase
    {
        private readonly ILogger<WebScraperController> _logger;
        private readonly IWebScrapedHtmlHelper _webScrapedHtmlHelper;
        private readonly IHttpClientWrapper _httpClientWrapper;
        public WebScraperController(ILogger<WebScraperController> logger, IWebScrapedHtmlHelper webScrapedHtmlHelper, IHttpClientWrapper httpClientWrapper)
        {
            _logger = logger;
            _webScrapedHtmlHelper = webScrapedHtmlHelper;
            _httpClientWrapper = httpClientWrapper;
        }

        [HttpGet(Name = "GetWebScraperInsights")]
        [Route("GetWebScraperInsights")]
        public async Task<WordInsight> GetWebScraperInsights(string url)
        {
            using (var client = new HttpClient())
            {
                var response = _httpClientWrapper.GetStringAsync(url);
                return _webScrapedHtmlHelper.GetAllWords(response.Result);
            }
        }

        [HttpGet(Name = "GetWebScrapedImages")]
        [Route("GetWebScrapedImages")]
        public async Task<IEnumerable<string>> GetWebScrapedImages(string url)
        {
            using (var client = new HttpClient())
            {
                var response = await client.GetStringAsync(url);
                return _webScrapedHtmlHelper.ScrapeImgesFromHtml(response);
            }
        }

        [HttpGet(Name = "GetWebScrapedImagesWithAltText")]
        [Route("GetWebScrapedImagesWithAltText")]
        public async Task<IEnumerable<GridImage>> GetWebScrapedImagesWithAltText(string url)
        {
            using (var client = new HttpClient())
            {
                var response = await client.GetStringAsync(url);
                return _webScrapedHtmlHelper.ScrapeImgesFromHtmlWithAltText(response);
            }
        }
    }
}