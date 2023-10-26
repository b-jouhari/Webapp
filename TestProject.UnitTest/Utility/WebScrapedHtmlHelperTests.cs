using Moq;
using HtmlAgilityPack;

namespace webapi.Utility.Tests
{

    [TestClass]
    public class WebScrapedHtmlHelperTests
    {
        private Mock<HtmlDocument> _htmlDocumentMock;
        private WebScrapedHtmlHelper _webScrapedHtmlHelper;

        [TestInitialize]
        public void Setup()
        {
            _htmlDocumentMock = new Mock<HtmlDocument>();
            _webScrapedHtmlHelper = new WebScrapedHtmlHelper();
        }

        [TestMethod]
        public void ScrapeImgesFromHtml_ReturnsListOfImageUrls()
        {
            // Arrange
            const string html = "<html><head><title>This is my page</title></head><body><img src=\"https://www.example.com/image1.png\" /><img src=\"https://www.example.com/image2.png\" /></body></html>";

            
            // Act
            var imageUrls = _webScrapedHtmlHelper.ScrapeImgesFromHtml(html);

            // Assert
            Assert.AreEqual(2, imageUrls.Count());
            Assert.AreEqual("https://www.example.com/image1.png", imageUrls.ToArray()[0]);
            Assert.AreEqual("https://www.example.com/image2.png", imageUrls.ToArray()[1]);
        }

        [TestMethod]
        public void ScrapeImgesFromHtml_ReturnsEmptyListForEmptyHtml()
        {
            // Arrange
            const string html = "";

            // Act
            var imageUrls = _webScrapedHtmlHelper.ScrapeImgesFromHtml(html);

            // Assert
            Assert.AreEqual(0, imageUrls.Count());
        }

        [TestMethod]
        public void GetAllWords_ReturnsWordInsightWithWordCountAndTop10Words()
        {
            // Arrange
            const string html = "<html><head><title>This is my page</title></head><body><h1> This is my heading</h1><p> This is my paragraph.</p></body></html>";

         

            // Act
            var wordInsight = _webScrapedHtmlHelper.GetAllWords(html);

            // Assert
            Assert.AreEqual(12, wordInsight.WordCount);
            
            Assert.AreEqual("page", wordInsight.Words[0].Key);
            Assert.AreEqual("heading", wordInsight.Words[1].Key);
            Assert.AreEqual("paragraph.", wordInsight.Words[2].Key);
        }

        [TestMethod]
        public void GetAllWords_ReturnsWordInsightWithEmptyWordListForEmptyHtml()
        {
            // Arrange
            const string html = "";

            // Act
            var wordInsight = _webScrapedHtmlHelper.GetAllWords(html);

            // Assert
            Assert.AreEqual(0, wordInsight.WordCount);
            Assert.AreEqual(0, wordInsight.Words.Count);
        }
    }
}
