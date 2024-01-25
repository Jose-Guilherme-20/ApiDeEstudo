using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Crud_Api.Model;
using Model;

namespace Services.Interfaces
{
    public interface IPersonService
    {
        Person Create(Person person);
        Task<Person> FindyById(long id);
        Task<Person> Update(long id, Person person);
        void Delete(Person person);
        List<Person> FindAll();
        Person LoginPerson(Person person);
        List<Person> FindByName(string name, string lastName);
        ModelPagened<Person> FindWithPagedSearch(string? name, string sortDirection, int pageSize, int page);
    }
}