namespace graphql_back.Entities
{
    public class Book
    {
        public int Id { get; set; }
        public string Title { get; set; } = "";
        public string Author { get; set; } = "";
        public int PublishedYear { get; set; }
    }

}
