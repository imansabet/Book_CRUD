using CRUD_API.Model;

namespace CRUD_API.Repository
{
    public interface IBookRepository
    {
        Task<IEnumerable<Book>> GetBooksAsync(int page, int pageSize, string search);
        Task<Book> GetBookAsync(int id);
        Task AddBookAsync(Book book);
        Task UpdateBookAsync(Book book);
        Task DeleteBookAsync(int id);
    }
}
