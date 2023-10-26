namespace webapi.Model
{
    public class WordInsight
    {
        public int WordCount { get; set; }
        public List<KeyValuePair<string, int>> Words { get; set; }
    }
}
    