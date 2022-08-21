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
    public class PersonController : ControllerBase
    {
        private IPersonService _personService;

        public PersonController(IPersonService personService)
        {
            _personService = personService;
        }

        [HttpGet]
        public async Task<ActionResult<IAsyncEnumerable<Person>>> GetPeople()
        {
            try
            {
                IEnumerable<Person> people = await _personService.GetPeople();

                return Ok(people);
            }
            catch (Exception)
            {
                return BadRequest("Request inválido");
            }
        }

        [HttpGet("PeopleByName")]
        public async Task<ActionResult<IAsyncEnumerable<Person>>> GetPeopleByName([FromQuery] string name)
        {
            try
            {
                IEnumerable<Person> people = await _personService.GetPeopleByName(name);

                if (people.Count() == 0)
                    return NotFound($"Nenhuma pessoa encontrada, realize a pesquisa novamente.");

                return Ok(people);
            }
            catch (Exception)
            {
                return BadRequest("Request inválido");
            }
        }

        [HttpGet("{id:int}", Name="GetPerson")]
        public async Task<ActionResult<Person>> GetPerson([FromQuery] int id)
        {
            try
            {
                Person person = await _personService.GetPerson(id);

                if (person == null)
                    return NotFound($"Pessoa não encontrada, realize a pesquisa novamente.");

                return Ok(person);
            }
            catch (Exception)
            {
                return BadRequest("Request inválido");
            }
        }

        [HttpPost]
        public async Task<ActionResult> Create(Person person)
        {
            try
            {
                await _personService.CreatePerson(person);

                return CreatedAtRoute(nameof(GetPerson), new { id = person.Id }, person);
            }
            catch (Exception)
            {
                return BadRequest("Request inválido");
            }
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> Edit(int id, [FromBody] Person person)
        {
            try
            {
                if (person.Id == id)
                {
                    await _personService.UpdatePerson(person);

                    return Ok($"Pessoa atualizada com sucesso");
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
                Person person = await _personService.GetPerson(id);

                if (person != null)
                {
                    await _personService.DeletePerson(person);
                    return Ok($"Pessoa excluída com sucesso");
                }
                else
                {
                    return NotFound("Pessoa não encontrada.");
                }
            }
            catch (Exception)
            {
                return BadRequest("Request inválido");
            }
        }
    }
}
