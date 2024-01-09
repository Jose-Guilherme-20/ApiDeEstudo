using Crud_Api.Model;
using Microsoft.EntityFrameworkCore;
using Model;

namespace Crud_Api.Context
{
    public class EntityContext : DbContext
    {
        public EntityContext()
        {

        }
        public EntityContext(DbContextOptions<EntityContext> options) : base(options)
        {

        }
        public DbSet<Person> Persons { get; set; }
        public DbSet<Book> Books { get; set; }
    }
}