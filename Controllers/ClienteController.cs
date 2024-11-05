using Microsoft.AspNetCore.Mvc;
using CuscuzBank.Models;
using CuscuzBank.Data;
using Microsoft.IdentityModel.Logging;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authorization;

namespace CuscuzBank.Controllers
{
    [ApiController]
    [Route("api/Clientes")]
    public class ClienteController : ControllerBase
    {
        private readonly ClienteContext _context;

        public ClienteController(ClienteContext context)
        {
            _context = context;
        }
        /// <summary>
        /// Se Cliente.Id for encontrato, retorna status code 200.
        /// Se Cliente.Id não for encontrato, então retorna status code 404 e mensagem de erro "Cliente não encontrato".
        /// <summary>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<Cliente>> GetCliente(string id)
        {
            //Se o banco de dados não for encontrado, retorna erro 404
            if (_context.Clientes == null)
            {
                return NotFound();
            }

            var cliente = await _context.Clientes.FindAsync(id);

            //Se o cliente não existe no banco de dados, retorna erro 404

            if (cliente == null)
            {
                return NotFound();
            }

            return cliente;
        }
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesDefaultResponseType]
        /*
            {
              "cpf": "70627364497",
              "nome": "Everton",
              "situacao": "Regular",
              "dataNascimento": "2024-10-22",
              "obito": false,
              "id": "A5s4d2",
              "endereco": {
                "id": "LAsd8",
                "logradouro": "Rua flor do sertão",
                "numero": 184,
                "bairro": "Jordão",
                "cidade": "Recife",
                "estado": "PE",
                "cep": "51020000"
              },
              "rendaMensal": 10256.00
            }
        */
        public async Task<ActionResult<Cliente>> PostCliente(Cliente cliente)
        {
            if (_context.Clientes == null)
            {
                return Problem("Erro ao criar um produto, contate o suporte!");
            }

            if (!ModelState.IsValid)
            {
                return ValidationProblem(new ValidationProblemDetails(ModelState)
                {
                    Title = "Um ou mais erros de validação ocorreram!"
                });
            }
            if (ClienteExiste(cliente.Id))
            {
                return Conflict();
            }
            _context.Clientes.Add(cliente);
            try
            {
                await _context.SaveChangesAsync();
                return CreatedAtAction(nameof(GetCliente), new { id = cliente.Id }, cliente);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro ao salvar informações: {ex}");
            }

        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<Cliente>> PutCliente(string id, Cliente cliente)
        {
            if (id != cliente.Id) return BadRequest();

            if (!ModelState.IsValid) return ValidationProblem(ModelState);

            //A linha de código abaixo permite modificar uma entidade que já está atachada em memória,
            //já está salvo em memória, garantindo que não haja erro por haver o já salvo em memória.
            _context.Entry(cliente).State = EntityState.Modified;
            //_context.Clientes.Update(cliente);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                //Para caso de concorrência de atualização do dado no banco de dados.
                if (!ClienteExiste(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();

        }
        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<Cliente>> DeleteCliente(string id)
        {
            if (_context.Clientes == null) return NotFound();

            var cliente = await _context.Clientes.FindAsync(id);

            if (cliente == null) return NotFound();

            _context.Clientes.Remove(cliente);
            await _context.SaveChangesAsync();
            return NoContent();

        }

        private bool ClienteExiste(string id)
        {
            return (_context.Clientes?.Any(c => c.Id == id)).GetValueOrDefault();
        }
    }
}
