using Linkfolio.Business.Business;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Model.Shared.User;

namespace Linkfolio.Business.Controllers
{
    /// <summary>
    /// Classe responsável por receber a(s) requisição(ões).
    /// Funções da classe:
    ///     1. Receber requisições Get/Post/Put/Delete e encaminhar o(s) dado(s) para a classe MensageBusiness
    ///     2. Receber o(s) dado(s) do MensageBusiness e mostra-lo(s) para o usuário.
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    [Authorize]
    public class MessageController : ControllerBase
    {
        private readonly ILogger<MessageController> _logger;

        private MessageBusiness business;

        public MessageController(ILogger<MessageController> logger)
        {
            _logger = logger;
            this.business = MessageBusiness.GetInstance();
        }

        /// <summary>
        /// Requisição POST
        /// Função responsável por receber uma requisição post e encaminhar os dados para a criação de uma nova mensagem.
        /// </summary>
        [HttpPost("Create")]
        public object Create([FromBody] MessageModel msg)
        {
            try
            {
                return Ok(this.business.CreateMessage(msg));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        /// <summary>
        /// Requisição GET
        /// Função responsável por receber uma requisição get e encaminhar o valor recebido para buscar uma mensagem. 
        /// </summary>
        [HttpGet("Read")]
        public object GetMensage(string gkey)
        {
            try
            {
                return Ok(this.business.GetMessage(gkey));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }

        }

        /// <summary>
        /// Requisição PUT
        /// Função responsável por receber uma requisição put e encaminhar o(s) dado(s) para atualizar uma mensagem.
        /// </summary>
        [HttpPut("Update")]
        public object UpdateMensage([FromBody] MessageModel msg)
        {
            try
            {
                return Ok(this.business.UpdateMessage(msg));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }

        }

        /// <summary>
        /// Requisição DELETE
        /// Função responsável por receber uma requisição delete e encaminhar o valor para a classe MensagemBusiness deletar uma mensagem.
        /// </summary>
        [HttpDelete("Delete")]
        public object Delete(string gkey)
        {
            try
            {
                return Ok(this.business.DeleteMessage(gkey));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

    }
}
