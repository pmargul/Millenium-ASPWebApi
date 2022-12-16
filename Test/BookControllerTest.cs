using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MilleniumAspWebAPI.Database;
using MilleniumAspWebAPI.Database.Models;
using Moq;

namespace MilleniumAspWebAPI.Test
{
    [TestClass]
    public class BookControllerTest
    {
        public BookControllerTest()
        {
            var mockSet = new Mock<DbSet<Book>>();
            var mockContext = new Mock<BooksDbContext>();
            mockContext.Setup(m => m.Books).Returns(mockSet.Object);

            var service = new BookDbService(mockContext.Object);
        }

        [TestMethod]
        public void GetRequest_GetAvailableBook()
        {
            var data = new List<Book>
            {
                new Book { Id = 1, Name = "BBB", IsAvailable = true },
                new Book { Id = 2, Name = "DDD", IsAvailable = false },
                new Book { Id = 3, Name = "CCC", IsAvailable = true },
            }.AsQueryable();

            var mockSet = new Mock<DbSet<Book>>();
            mockSet.As<IQueryable<Book>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<Book>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<Book>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<Book>>().Setup(m => m.GetEnumerator()).Returns(() => data.GetEnumerator());

            var mockContext = new Mock<BooksDbContext>();
            mockContext.Setup(c => c.Books).Returns(mockSet.Object);

            var service = new BookDbService(mockContext.Object);
            var books = service.GetAvailableBooks();

            Assert.AreEqual(1, books.Count);
        }
        [TestMethod]
        public void PostRequest_AddBook()
        {
            var data = new List<Book>
            {
                new Book { Id = 1, Name = "BBB", IsAvailable = true },
                new Book { Id = 2, Name = "DDD", IsAvailable = false },
                new Book { Id = 3, Name = "CCC", IsAvailable = true },
            }.AsQueryable();

            var mockSet = new Mock<DbSet<Book>>();
            mockSet.As<IQueryable<Book>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<Book>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<Book>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<Book>>().Setup(m => m.GetEnumerator()).Returns(() => data.GetEnumerator());

            var mockContext = new Mock<BooksDbContext>();
            mockContext.Setup(c => c.Books).Returns(mockSet.Object);

            var service = new BookDbService(mockContext.Object);

            // Act
            var newBook = new Book()
            {
                Name = "AAA",
                Description = "Desc def",
                IsAvailable = true
            };
            var available = service.GetAvailableBooks();

            // Assert
            Assert.AreEqual(2, available.Count);
        }
    }
}
