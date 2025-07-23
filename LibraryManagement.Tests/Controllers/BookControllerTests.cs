using LibraryManagement.Application.DTOs;
using LibraryManagement.Application.Interfaces;
using LibraryManagement.WebApi.Controllers;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LibraryManagement.Tests.Controllers;

[TestFixture]
public class BookControllerTests
{
    private Mock<IBookService> _bookServiceMock;
    private BookController _controller;
    
    [SetUp]
    public void SetUp()
    {
        _bookServiceMock = new Mock<IBookService>();
        _controller = new BookController(_bookServiceMock.Object);
    }
    
    [Test]
    public async Task GetAllBooks_ReturnsOkResult_WithListOfBooks()
    {
        // Arrange
        var books = new List<BookDto>
        {
            new BookDto { Id = 1, Title = "Book 1", Author = "Author 1", ISBN = "1234567890", IsAvailable = true },
            new BookDto { Id = 2, Title = "Book 2", Author = "Author 2", ISBN = "0987654321", IsAvailable = false }
        };
        _bookServiceMock.Setup(s => s.GetAllBooksAsync()).ReturnsAsync(books);
        
        // Act
        var result = await _controller.GetAllBooks();
        
        // Assert
        Assert.IsInstanceOf<OkObjectResult>(result); // Прямое сравнение результата
        var okResult = result as OkObjectResult;
        Assert.IsNotNull(okResult);
        var returnBooks = okResult.Value as IEnumerable<BookDto>;
        Assert.AreEqual(books, returnBooks); // Сравниваем списки
    }
        [Test] 
        public async Task GetBook_BookNotFound_ReturnsNotFound()
        {
            // Arrange
            _bookServiceMock.Setup(s => s.GetBookByIdAsync(1)).ReturnsAsync((BookDto)null);
            
            // Act
            var result = await _controller.GetBookById(1);
            
            // Assert
            Assert.IsInstanceOf<NotFoundResult>(result); // Проверяем, что результат - NotFound
    }
}