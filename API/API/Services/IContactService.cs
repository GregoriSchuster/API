using API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Services
{
    public interface IContactService
    {
        Task<IEnumerable<Contact>> GetContacts();
        Task<Contact> GetContact(int id);
        Task<IEnumerable<Contact>> GetContactByIdPerson(int idPerson);
        Task CreateContact(Contact contact);
        Task UpdateContact(Contact contact);
        Task DeleteContact(Contact contact);
    }
}
