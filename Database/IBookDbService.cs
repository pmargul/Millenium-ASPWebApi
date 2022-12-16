using MilleniumAspWebAPI.Database.Models;

namespace MilleniumAspWebAPI.Database
{
    public interface IBookDbService
    {
        List<Book> GetAvailableBooks();
        Book GetBookById(int bookId);
        string AddBook(Book book);
        string UpdateBook(Book book);
        string DeleteBook(int bookId);
    }
}
