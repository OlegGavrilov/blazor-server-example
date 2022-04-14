namespace BlazorApp1.Dto
{
    public class Post
    {
        public string id { get; set; }
        public string image { get; set; }
        public int likes { get; set; }
        public List<string> tags { get; set; }
        public string text { get; set; }
        public DateTime publishDate { get; set; }
        public User owner { get; set; }
    }

}
