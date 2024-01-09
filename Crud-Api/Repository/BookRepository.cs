using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Crud_Api.Context;
using Crud_Api.Repository.Interfaces;
using Repository;

namespace Crud_Api.Repository
{
    public class BookRepository : EntityBaseRepository<BookRepository>, IBookRepository
    {
        public BookRepository(EntityContext context) : base(context)
        {
        }
    }
}