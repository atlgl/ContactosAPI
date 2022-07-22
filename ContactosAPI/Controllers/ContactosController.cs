using ContactosAPI.Models;
using ContactosAPI.Servicio;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.ComponentModel.DataAnnotations;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ContactosAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactosController : ControllerBase
    {
        private readonly ContactsService _contactosService;

        public ContactosController(ContactsService contactosService) =>
            _contactosService = contactosService;

        [HttpGet]
        public async Task<List<Contacto>> Get() =>
            await _contactosService.GetAsync();


        [HttpGet("name")]
        public async Task<List<Contacto>> GetWithName(String name)=>        
            await _contactosService.GetNameAsync(name);
            

            

        [HttpGet("{id:length(24)}")]
        public async Task<ActionResult<Contacto>> Get(string id)
        {
            var contacto = await _contactosService.GetAsync(id);

            if (contacto is null)
            {
                return NotFound();
            }

            return contacto;
        }

        [HttpPost]
        public async Task<IActionResult> Post(Contacto newcontacto)
        {
            await _contactosService.CreateAsync(newcontacto);

            return CreatedAtAction(nameof(Get), new { id = newcontacto.id }, newcontacto);
        }

        /*[HttpPut("{id:length(24)}")]
        public async Task<IActionResult> Update(string id, Contacto updatedcontacto)
        {
            var contacto = await _contactosService.GetAsync(id);

            if (contacto is null)
            {
                return NotFound();
            }

            updatedcontacto.Id = contacto.Id;

            await _contactosService.UpdateAsync(id, updatedcontacto);

            return NoContent();
        }*/

        /*[HttpDelete("{id:length(24)}")]
        public async Task<IActionResult> Delete(string id)
        {
            var contacto = await _contactosService.GetAsync(id);

            if (contacto is null)
            {
                return NotFound();
            }

            await _contactosService.RemoveAsync(id);

            return NoContent();
        }*/
    }
}

