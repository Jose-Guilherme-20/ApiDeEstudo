using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Crud_Api.Model;
using Crud_Api.Repository.Interfaces;
using Repository;
using Repository.Interfaces;

namespace Crud_Api.Services.Interfaces
{
    public class BookService : IBookService
    {
        private readonly IBookRepository _bookRepository;
        private readonly IEntityBaseRepository<Book> _entityBaseRepository;
        public BookService(IBookRepository bookRepository, IEntityBaseRepository<Book> entityBaseRepository)
        {
            _bookRepository = bookRepository;
            _entityBaseRepository = entityBaseRepository;
        }

        public Book Create(Book book)
        {
            _entityBaseRepository.Add(book);
            return book;
        }

        public async Task<List<Book>> FindAll()
        {
            throw new NotImplementedException();
        }
    }
}