using API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Services
{
    public interface IPersonService
    {
        Task<IEnumerable<Person>> GetPeople();
        Task<Person> GetPerson(int id);
        Task CreatePerson(Person person);
        Task UpdatePerson(Person person);
        Task DeletePerson(Person person);
    }
}
