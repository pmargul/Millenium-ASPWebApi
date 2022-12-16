using MilleniumAspWebAPI.Database.Models;

namespace MilleniumAspWebAPI.Database
{
    public class BookDbService : IBookDbService
    {
        private BooksDbContext _dbContext;

        public BookDbService(BooksDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public string AddBook(Book book)
        {
            using(var dbContext = _dbContext)
            {
                try
                {
                    dbContext.Books.Add(book);
                    dbContext.SaveChanges();
                    return String.Empty;
                } catch(Exception ex)
                {
                    return ex.Message;
                }
            }
        }

        public string DeleteBook(int bookId)
        {
            using (var dbContext = _dbContext)
            {
                try
                {
                    Book bookToBeRemoved = new Book()
                    {
                        Id = bookId
                    };

                    dbContext.Books.Attach(bookToBeRemoved);
                    dbContext.Books.Remove(bookToBeRemoved);
                    dbContext.SaveChanges();
                    return String.Empty;
                }
                catch (Exception ex)
                {
                    return ex.Message;
                }
            }
        }

        public Book GetBookById(int bookId)
        {
            using (var dbContext = _dbContext)
            {
                Book result = dbContext.Books.Single(el => el.Id == bookId);
                return result;
            }
        }

        public List<Book> GetAvailableBooks()
        {
            using (var dbContext = _dbContext)
            {
                List<Book> result = dbContext.Books.Where(el => el.IsAvailable).ToList();
                return result;
            }
        }

        public string UpdateBook(Book updateBook)
        {
            using (var dbContext = _dbContext)
            {
                try
                {
                    Book? result = dbContext.Books.Single(b => b.Id == updateBook.Id);
                    result.Name = updateBook.Name;

                    dbContext.Books.Update(result);
                    dbContext.SaveChanges();
                    return String.Empty;
                }
                catch (Exception ex)
                {
                    return ex.Message;
                }
            }
        }
    }
}
