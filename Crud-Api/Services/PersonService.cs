using System;
using System.Collections.Generic;
using System.Linq;
using Crud_Api.Repository.Interfaces;
using Model;
using Services.Interfaces;
using Crud_Api.Context;
using Repository.Interfaces;
using System.Threading.Tasks;
using Crud_Api.Services.Interfaces;
using Crud_Api.Model;

namespace Services
{
    public class PersonService : IPersonService
    {
        private readonly IPersonRepository _personRepository;
        private readonly IEntityBaseRepository<Person> _context;
        public PersonService(IPersonRepository personRepository, IEntityBaseRepository<Person> context)
        {
            _personRepository = personRepository;
            _context = context;
        }
        public Person Create(Person person)
        {
            _context.Add(person);
            return person;
        }

        public void Delete(Person person)
        {
            _context.Remove(person);
        }

        public List<Person> FindAll()
        {
            return _context.FindAll();
        }

        public List<Person> FindByName(string name, string lastName)
        {
            return _personRepository.FindByName(name, lastName);
        }

        public ModelPagened<Person> FindWithPagedSearch(string name, string sortDirection, int pageSize, int page)
        {

            var sort = (!string.IsNullOrWhiteSpace(sortDirection)) && !sortDirection.Equals("desc") ? "asc" : "desc";
            var size = (pageSize < 1) ? 10 : pageSize;
            var offset = page > 0 ? (page - 1) * size : 0;

            string query = @"select * from person p where 1 = 1 ";
            if (!string.IsNullOrWhiteSpace(name)) query = query + $" and p.first_name like '%{name}%' ";
            query += $" order by p.first_name {sort} limit {size} offset {offset}";

            string countQuery = @" select count(*) from person p where 1 = 1 ";
            if (!string.IsNullOrWhiteSpace(name)) countQuery = countQuery + $" and p.name like '%{name}%' ";
            var persons = _context.FindWithPagedSearch(query);
            int totalResults = _context.GetCount(countQuery);

            return new ModelPagened<Person>
            {
                CurrentPage = page,
                List = persons,
                PageSize = size,
                SortDirections = sortDirection,
                TotalResults = totalResults,
            };
        }

        public async Task<Person> FindyById(long id)
        {
            return await _personRepository.FindById(id);
        }

        public Person LoginPerson(Person person)
        {
            var user = _personRepository.LoginPerson(person);
            if (user == null)
            {
                return null;
            }
            return user;


        }

        public async Task<Person> Update(long id, Person person)
        {
            var user = await _personRepository.FindById(id);
            if (user == null)
            {
                return null;
            }
            await _context.Update(user);
            return user;

        }
    }
}