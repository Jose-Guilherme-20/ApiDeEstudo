using System.Collections.Generic;
using System.Threading.Tasks;
using Model;

namespace Crud_Api.Repository.Interfaces
{
    public interface IPersonRepository
    {
        Task<Person> FindById(long id);
        Person LoginPerson(Person person);
        List<Person> FindByName(string firstName, string lastName );
    }
}