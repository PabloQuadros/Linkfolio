using Linkfolio.Business.Business;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Model.Shared.User;

namespace Linkfolio.Business.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize]
    public class MensageController : ControllerBase
    {
        private readonly ILogger<MensageController> _logger;

        private MensageBusiness business;

        public MensageController(ILogger<MensageController> logger)
        {
            _logger = logger;
            this.business = MensageBusiness.GetInstance();
        }

        /// <summary>
        /// Requisição PUT
        /// Função responsável por receber uma requisição put e encaminhar os dados para a criação de um novo usuário.
        /// </summary>
        /// <returns> Retorna objeto do tipo object</returns>
        [HttpPost("Create")]
        public object Create([FromBody] MensageModel msg)
        {
            try
            {
                return Ok(this.business.CreateMensage(msg));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        /// <summary>
        /// Requisição GET(Login)
        /// Função responsável por receber uma requisição get e encaminhar o valor recebido para buscar os dados de um usuário existente. 
        /// </summary>
        /// <returns> Retorna objeto do tipo object</returns>

        [HttpGet("Read")]
        public object GetMensage(string gkey)
        {
            try
            {
                return Ok(this.business.GetMensage(gkey));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }

        }


        /// <summary>
        /// Requisição POST
        /// Função responsável por receber uma requisição post e encaminhar o(s) dado(s) para atualizar o cadastro de um usuário.
        /// </summary>
        /// <returns> Retorna objeto do tipo object</returns>
        [HttpPut("Update")]
        public object UpdateMensage([FromBody] MensageModel msg)
        {
            try
            {
                return Ok(this.business.UpdateMensage(msg));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }

        }

        /// <summary>
        /// Requisição DELETE
        /// Função responsável por receber uma requisição delete e encaminhar o valor para a classe LoginBusiness deletar a conta de um usuário.
        /// </summary>
        /// <returns> Retorna objeto do tipo object</returns>
        [HttpDelete("Delete")]
        public object Delete(string gkey)
        {
            try
            {
                return Ok(this.business.DeleteMensage(gkey));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

    }
}
