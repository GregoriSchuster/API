using API.Context;
using API.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Services
{
    public class ContactsService : IContactService
    {
        private readonly AppDbContext _context;

        public ContactsService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Contact>> GetContacts()
        {
            try
            {
                return await _context.Contacts.ToListAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<IEnumerable<Contact>> GetContactByIdPerson(int idPerson)
        {
            try
            {
                IEnumerable<Contact> contact = await _context.Contacts.Where(x => x.IdPerson == idPerson).ToListAsync();

                return contact;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<Contact> GetContact(int id)
        {
            try
            {
                Contact contact = await _context.Contacts.FindAsync(id);

                return contact;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task CreateContact(Contact contact)
        {
            _context.Contacts.Add(contact);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateContact(Contact contact)
        {
            _context.Entry(contact).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteContact(Contact contact)
        {
            _context.Contacts.Remove(contact);
            await _context.SaveChangesAsync();
        }

    }
}
