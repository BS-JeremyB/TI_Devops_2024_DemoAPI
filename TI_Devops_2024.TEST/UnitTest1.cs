using Moq;
using System.Collections.Generic;
using TI_Devops_2024_DemoAspMvc.BLL.Services;
using TI_Devops_2024_DemoAspMvc.DAL.Interfaces;
using TI_Devops_2024_DemoAspMvc.Domain.Entities;
using Xunit;

namespace BookServiceTests
{
    public class BookServiceTests
    {


        private readonly Mock<IBookRepository> _bookRepositoryMock;
        private readonly BookService _bookService;

        public BookServiceTests()
        {
            _bookRepositoryMock = new Mock<IBookRepository>();
            _bookService = new BookService(_bookRepositoryMock.Object);
        }

        [Fact]
        public void GetAll_ShouldReturnAllBooks()
        {
            // Arrange
            var books = new List<Book>
                {
                    new Book { ISBN = "978-3-16-148410-0", Title = "Test Book 1" },
                    new Book { ISBN = "978-1-56619-909-4", Title = "Test Book 2" }
                };

            _bookRepositoryMock.Setup(repo => repo.GetAll()).Returns(books);

            // Act
            var result = _bookService.GetAll();

            // Assert
            Assert.Equal(books, result);
        }

        [Fact]
        public void GetByISBN_ShouldReturnBook_WhenBookExists()
        {
            // Arrange
            var book = new Book { ISBN = "978-3-16-148410-0", Title = "Test Book" };
            _bookRepositoryMock.Setup(repo => repo.GetById("978-3-16-148410-0")).Returns(book);

            // Act
            var result = _bookService.GetByISBN("978-3-16-148410-0");

            // Assert
            Assert.Equal(book, result);
        }

        [Fact]
        public void GetByISBN_ShouldThrowException_WhenBookDoesNotExist()
        {
            // Arrange
            _bookRepositoryMock.Setup(repo => repo.GetById("978-3-16-148410-0")).Returns((Book)null);

            // Act & Assert
            Assert.Throws<KeyNotFoundException>(() => _bookService.GetByISBN("978-3-16-148410-0"));
        }

        [Fact]
        public void Create_ShouldThrowException_WhenBookAlreadyExists()
        {
            // Arrange
            var book = new Book { ISBN = "978-3-16-148410-0", Title = "Test Book" };
            _bookRepositoryMock.Setup(repo => repo.ExistByUnicityCriteria(book)).Returns(true);

            // Act & Assert
            Assert.Throws<Exception>(() => _bookService.Create(book));
        }

        [Fact]
        public void Create_ShouldReturnISBN_WhenBookIsCreated()
        {
            // Arrange
            var book = new Book { ISBN = "978-3-16-148410-0", Title = "Test Book" };
            _bookRepositoryMock.Setup(repo => repo.ExistByUnicityCriteria(book)).Returns(false);
            _bookRepositoryMock.Setup(repo => repo.Create(book)).Returns(book.ISBN);

            // Act
            var result = _bookService.Create(book);

            // Assert
            Assert.Equal(book.ISBN, result);
        }

        [Fact]
        public void Update_ShouldThrowException_WhenBookDoesNotExist()
        {
            // Arrange
            var book = new Book { ISBN = "978-3-16-148410-0", Title = "Test Book" };
            _bookRepositoryMock.Setup(repo => repo.ExistByISBN("978-3-16-148410-0")).Returns(false);

            // Act & Assert
            Assert.Throws<KeyNotFoundException>(() => _bookService.Update("978-3-16-148410-0", book));
        }

        [Fact]
        public void Delete_ShouldThrowException_WhenBookDoesNotExist()
        {
            // Arrange
            _bookRepositoryMock.Setup(repo => repo.ExistByISBN("978-3-16-148410-0")).Returns(false);

            // Act & Assert
            Assert.Throws<KeyNotFoundException>(() => _bookService.Delete("978-3-16-148410-0"));
        }

        [Fact]
        public void Delete_ShouldReturnTrue_WhenBookIsDeleted()
        {
            // Arrange
            _bookRepositoryMock.Setup(repo => repo.ExistByISBN("978-3-16-148410-0")).Returns(true);
            _bookRepositoryMock.Setup(repo => repo.Delete("978-3-16-148410-0")).Returns(true);

            // Act
            var result = _bookService.Delete("978-3-16-148410-0");

            // Assert
            Assert.True(result);
        }
    }
}
