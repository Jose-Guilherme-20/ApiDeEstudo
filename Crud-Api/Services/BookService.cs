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

        public ModelPagened<Book> FindWithPagedSearch(string? name, string sortDirection, int pageSize, int page)
        {

            var sort = (!string.IsNullOrWhiteSpace(sortDirection)) && !sortDirection.Equals("desc") ? "asc" : "desc";
            var size = (pageSize < 1) ? 10 : pageSize;
            var offset = page > 0 ? (page - 1) * size : 0;

            string query = @"select * from book b where 1 = 1 ";
            if (!string.IsNullOrWhiteSpace(name)) query = query + $" and p.first_name like '%{name}%' ";
            query += $" order by b.author {sort} limit {size} offset {offset}";

            string countQuery = @" select count(*) from book b where 1 = 1 ";
            if (!string.IsNullOrWhiteSpace(name)) countQuery = countQuery + $" and b.author like '%{name}%' ";
            var persons = _entityBaseRepository.FindWithPagedSearch(query);
            int totalResults = _entityBaseRepository.GetCount(countQuery);

            return new ModelPagened<Book>
            {
                CurrentPage = page,
                List = persons,
                PageSize = size,
                SortDirections = sortDirection,
                TotalResults = totalResults,
            };
        }
    }
}