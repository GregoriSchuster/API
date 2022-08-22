using API.Models;
using API.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class ContactController : ControllerBase
    {
        private IContactService _contactService;

        public ContactController(IContactService contactService)
        {
            _contactService = contactService;
        }

        [HttpGet]
        public async Task<ActionResult<IAsyncEnumerable<Contact>>> GetContacts()
        {
            try
            {
                IEnumerable<Contact> contacts = await _contactService.GetContacts();

                return Ok(contacts);
            }
            catch (Exception)
            {
                return BadRequest("Request inválido");
            }
        }

        [HttpGet("GetContactByIdPerson")]
        public async Task<ActionResult<IAsyncEnumerable<Person>>> GetContactByIdPerson([FromQuery] int idPerson)
        {
            try
            {
                IEnumerable<Contact> contact = await _contactService.GetContactByIdPerson(idPerson);

                return Ok(contact);
            }
            catch (Exception)
            {
                return BadRequest("Request inválido");
            }
        }

        [HttpGet("{id:int}", Name = "GetContact")]
        public async Task<ActionResult<Contact>> GetContact(int id)
        {
            try
            {
                Contact contact = await _contactService.GetContact(id);

                if (contact == null)
                    return NotFound($"Contato não encontrado, realize a pesquisa novamente.");

                return Ok(contact);
            }
            catch (Exception)
            {
                return BadRequest("Request inválido");
            }
        }

        [HttpPost]
        public async Task<ActionResult> Create(Contact contact)
        {
            try
            {
                await _contactService.CreateContact(contact);

                return CreatedAtRoute(nameof(GetContact), new { id = contact.Id }, contact);
            }
            catch (Exception)
            {
                return BadRequest("Request inválido");
            }
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> Edit(int id, [FromBody] Contact contact)
        {
            try
            {
                if (contact.Id == id)
                {
                    await _contactService.UpdateContact(contact);

                    return Ok($"Contato atualizado com sucesso");
                }
                else
                {
                    return BadRequest("Dados inconsistentes");
                }
            }
            catch (Exception)
            {
                return BadRequest("Request inválido");
            }
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                Contact contact = await _contactService.GetContact(id);

                if (contact != null)
                {
                    await _contactService.DeleteContact(contact);
                    return Ok($"Contato excluído com sucesso");
                }
                else
                {
                    return NotFound("Contato não encontrada.");
                }
            }
            catch (Exception)
            {
                return BadRequest("Request inválido");
            }
        }
    }
}
