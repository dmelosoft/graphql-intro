using graphql_back.Data;
using graphql_back.Entities;

namespace graphql_back.GraphQL
{
    public class Query
    {
        public IQueryable<Book> GetBooks([Service] AppDbContext context) =>
            context.Books;

        public Book? GetBookById(int id, [Service] AppDbContext context) =>
            context.Books.FirstOrDefault(b => b.Id == id);
    }

}
