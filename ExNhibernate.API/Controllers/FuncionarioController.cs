using ExNhibernate.API.Entities;
using ExNhibernate.API.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace ExNhibernate.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FuncionarioController : ControllerBase
    {
        private readonly FuncionarioRepository funcionarioRepository;

        public FuncionarioController(NHibernate.ISession session)
        {
            funcionarioRepository = new FuncionarioRepository(session);
        }


        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                var funcionarios = funcionarioRepository.FindAll().ToList();

                if (funcionarios == null) return NotFound();

                return Ok(funcionarios);
            }
            catch (Exception _error)
            {
                return StatusCode(StatusCodes.Status400BadRequest, _error.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(long id)
        {
            try
            {
                var funcionario = await funcionarioRepository.FindById(id);

                if (funcionario == null) return NotFound();

                return Ok(funcionario);
            }
            catch (Exception _error)
            {
                return StatusCode(StatusCodes.Status400BadRequest, _error.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Funcionario funcionario)
        {
            try
            {
                await funcionarioRepository.Add(funcionario);

                return Ok();
            }
            catch (Exception _error)
            {
                return StatusCode(StatusCodes.Status400BadRequest, _error.Message);
            }
        }

        [HttpPut]
        public async Task<IActionResult> Edit([FromBody] Funcionario funcionario)
        {
            try
            {
                await funcionarioRepository.Update(funcionario);

                return Ok();
            }
            catch (Exception _error)
            {
                return StatusCode(StatusCodes.Status400BadRequest, _error.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(long id)
        {
            try
            {
                await funcionarioRepository.Remove(id);

                return Ok();
            }
            catch (Exception _error)
            {
                return StatusCode(StatusCodes.Status400BadRequest, _error.Message);
            }
        }

    }
}
