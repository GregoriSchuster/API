using API.Context;
using API.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Services
{
    public class PersonsService : IPersonService
    {
        private readonly AppDbContext _context;

        public PersonsService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Person>> GetPeople()
        {
            try
            {
                return await _context.People.ToListAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<Person> GetPerson(int id)
        {
            try
            {
                Person Person = await _context.People.FindAsync(id);

                return Person;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task CreatePerson(Person person)
        {
            _context.People.Add(person);
            await _context.SaveChangesAsync();
        }

        public async Task UpdatePerson(Person person)
        {
            _context.Entry(person).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeletePerson(Person person)
        {
            _context.People.Remove(person);
            await _context.SaveChangesAsync();
        }
    }
}
