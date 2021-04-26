namespace DemoApi.Domain
{
    public class Review
    {
        public int Id { get; set; }
        public string ReviewedBy { get; set; }
        public string Body { get; set; }
        public StarRating Rating { get; set; }
    }
}
