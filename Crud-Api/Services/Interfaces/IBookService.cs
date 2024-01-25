using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Crud_Api.Model;

namespace Crud_Api.Services.Interfaces
{
    public interface IBookService
    {
        Task<List<Book>> FindAll();
        Book Create(Book book);
        ModelPagened<Book> FindWithPagedSearch(string? name, string sortDirection, int pageSize, int page);
    }
}