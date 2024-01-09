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