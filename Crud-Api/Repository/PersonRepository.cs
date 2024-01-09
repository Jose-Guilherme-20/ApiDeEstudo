using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Crud_Api.Context;
using Crud_Api.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using Model;

namespace Repository
{
    public class PersonRepository : EntityBaseRepository<PersonRepository>, IPersonRepository
    {
        public PersonRepository(EntityContext context) : base(context)
        {
        }
        public async Task<Person> FindById(long id)
        {
            return await Db.Persons.FirstOrDefaultAsync(x => x.Id.Equals(id));
        }

        public Person LoginPerson(Person person)
        {
            return Db.Persons.Where(x => x.Name.Equals(person.Name) && x.LastName.Equals(person.LastName)).FirstOrDefault();
        }
        public List<Person> FindByName(string name, string lastName)
        {
            if (!string.IsNullOrWhiteSpace(name) && !string.IsNullOrWhiteSpace(lastName))
            {
                return Db.Persons.Where(p => p.Name.Contains(name) && p.LastName.Contains(lastName)).ToList();
            }
            else if (string.IsNullOrWhiteSpace(name) && !string.IsNullOrWhiteSpace(lastName))
            {
                return Db.Persons.Where(p => p.LastName.Contains(lastName)).ToList();
            }
            else if (!string.IsNullOrWhiteSpace(name) && string.IsNullOrWhiteSpace(lastName))
            {
                return Db.Persons.Where(p => p.Name.Contains(name)).ToList();
            }
            return null;

        }
    }
}