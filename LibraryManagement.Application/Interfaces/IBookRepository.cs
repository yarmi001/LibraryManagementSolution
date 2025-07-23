using LibraryManagement.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;
using LibraryManagement.Application.DTOs;

namespace LibraryManagement.Application.Interfaces;

public interface IBookRepository
{
    Task<List<Book>> GetAllAsync();
    Task<Book> GetByIdAsync(int id);
    Task<IEnumerable<Book>> SearchAsync(string searchTerm);
    Task AddAsync(Book book);
    Task UpdateAsync(Book book);
    Task DeleteAsync(int id);
}