using Microsoft.Extensions.Logging;
using Moq;
using System.Net.Http;
using webapi.Controllers;
using webapi.Model;
using webapi.Utility;

namespace ScrapWebpageTests.Controllers
{
    [TestClass]
    public class WebScraperControllerTests
    {
        private Mock<IWebScrapedHtmlHelper> _webScrapedHtmlHelperMock;
         private Mock<IHttpClientWrapper> _httpClientWrapperMock;
        private HttpClient _httpClient;
        private WebScraperController _webScraperController;
        private Mock<ILogger<WebScraperController>> _loggerMock;

        [TestInitialize]
        public void Setup()
        {
            _webScrapedHtmlHelperMock = new Mock<IWebScrapedHtmlHelper>();
            _httpClientWrapperMock = new Mock<IHttpClientWrapper>();
            _loggerMock = new Mock<ILogger<WebScraperController>>();
            _webScraperController = new WebScraperController(_loggerMock.Object, _webScrapedHtmlHelperMock.Object, _httpClientWrapperMock.Object);
        }

        [TestMethod]
        public async Task GetWebScraperInsights_ReturnsWordInsightForValidUrl()
        {
            // Arrange
            const string url = "https://www.gogle.com";
            var words = new List<KeyValuePair<string, int>>();
            words.Add(new KeyValuePair<string, int>("page", 1));
            const string htmlContent = "<html><head><title>This is my page</title></head><body><h1>This is my heading</h1><p>This is my paragraph.</p></body></html>";
            var expectedWordInsight = new WordInsight()
            {
               WordCount =1,
               Words = words
            };

            _webScrapedHtmlHelperMock.Setup(x => x.GetAllWords(htmlContent)).Returns(expectedWordInsight);
            _httpClientWrapperMock.Setup(x => x.GetStringAsync("https://www.gogle.com")).ReturnsAsync(htmlContent);
            // Act
            var actualWordInsight = await _webScraperController.GetWebScraperInsights(url);

            // Assert
            Assert.AreEqual(expectedWordInsight, actualWordInsight);
        }
    }
}
