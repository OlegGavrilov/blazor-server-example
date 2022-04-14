namespace BlazorApp1.Dto
{
    public class UsersRoot
    {
        public List<User> data { get; set; } = new List<User>();
        public int total { get; set; }
        public int page { get; set; }
        public int limit { get; set; }
    }

}
