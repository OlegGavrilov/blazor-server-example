namespace BlazorApp1.Dto
{
    public class PostsRoot
    {
        public List<Post> data { get; set; }
        public int total { get; set; }
        public int page { get; set; }
        public int limit { get; set; }
    }

}
