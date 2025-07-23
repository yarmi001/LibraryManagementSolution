using LibraryManagement.Application.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LibraryManagement.Application.Interfaces;

public interface IBookService
{
    Task<IEnumerable<BookDto>> GetAllBooksAsync();
    Task<BookDto> GetBookByIdAsync(int id);
    Task<IEnumerable<BookDto>> SearchBooksAsync(string searchTerm);
    Task<BookDto> CreateBookAsync(BookDto bookDto);
    Task UpdateBookAsync(int id, BookDto bookDto);
    Task DeleteBookAsync(int id);
}