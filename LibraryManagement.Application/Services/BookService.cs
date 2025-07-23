using LibraryManagement.Application.DTOs;
using LibraryManagement.Application.Interfaces; // Используем интерфейс из Application
using LibraryManagement.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LibraryManagement.Application.Services
{
    public class BookService : IBookService
    {
        private readonly IBookRepository _bookRepository; // Исправлено с IBookRepositoriy

        public BookService(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository ?? throw new ArgumentNullException(nameof(bookRepository));
        }

        public async Task<IEnumerable<BookDto>> GetAllBooksAsync()
        {
            var books = await _bookRepository.GetAllAsync();
            return books.Select(book => new BookDto
            {
                Id = book.Id,
                Title = book.Title,
                Author = book.Author,
                ISBN = book.ISBN,
                IsAvailable = book.IsAvailable
            });
        }

        public async Task<BookDto> GetBookByIdAsync(int id)
        {
            var book = await _bookRepository.GetByIdAsync(id);
            if (book == null) return null;

            return new BookDto
            {
                Id = book.Id,
                Title = book.Title,
                Author = book.Author,
                ISBN = book.ISBN,
                IsAvailable = book.IsAvailable
            };
        }

        public async Task<IEnumerable<BookDto>> SearchBooksAsync(string searchTerm)
        {
            var books = await _bookRepository.SearchAsync(searchTerm);
            return books.Select(book => new BookDto
            {
                Id = book.Id,
                Title = book.Title,
                Author = book.Author,
                ISBN = book.ISBN,
                IsAvailable = book.IsAvailable
            });
        }

        public async Task<BookDto> CreateBookAsync(BookDto bookDto)
        {
            var book = new Book
            {
                Title = bookDto.Title,
                Author = bookDto.Author,
                ISBN = bookDto.ISBN,
                IsAvailable = bookDto.IsAvailable
            };

            await _bookRepository.AddAsync(book);
            bookDto.Id = book.Id; // Предполагается, что Id устанавливается после добавления
            return bookDto;
        }

        public async Task UpdateBookAsync(int id, BookDto bookDto)
        {
            var book = await _bookRepository.GetByIdAsync(id);
            if (book == null) throw new Exception("Book not found");
            book.Title = bookDto.Title;
            book.Author = bookDto.Author;
            book.ISBN = bookDto.ISBN;
            book.IsAvailable = bookDto.IsAvailable;
            await _bookRepository.UpdateAsync(book);
        }

        public async Task DeleteBookAsync(int id)
        {
            var book = await _bookRepository.GetByIdAsync(id);
            if (book == null) throw new Exception("Book not found");
            await _bookRepository.DeleteAsync(id);
        }
    }
}