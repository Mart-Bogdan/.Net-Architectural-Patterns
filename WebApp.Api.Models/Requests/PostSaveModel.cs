namespace WebApp.Api.Models.Requests
{
    public class PostSaveModel
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Content { get; set; }
        public string Title { get; set; }
    }
}