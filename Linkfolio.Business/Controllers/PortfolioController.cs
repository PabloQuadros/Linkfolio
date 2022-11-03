using Linkfolio.Business.Business;
using Microsoft.AspNetCore.Mvc;
using Model.Shared.User;

namespace Linkfolio.Business.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PortfolioController : ControllerBase
    {

        private readonly ILogger<PortfolioController> _logger;

        private PortfolioBusiness business;

        public PortfolioController(ILogger<PortfolioController> logger)
        {
            _logger = logger;
            this.business = PortfolioBusiness.GetInstance();
        }

        /// <summary>
        /// Requisição PUT
        /// Função responsável por receber uma requisição put e encaminhar os dados para a criação de um novo usuário.
        /// </summary>
        /// <returns> Retorna objeto do tipo object</returns>
        [HttpPost("Create")]
        public object Create([FromBody] PortfolioModel portfolio)
        {
            try
            {
                return Ok(this.business.CreatePortfolio(portfolio));
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
        public object GetPortfolio(string gkey)
        {
            try
            {
                return Ok(this.business.GetPortfolio(gkey));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }

        }

        /// <summary>
        /// Requisição GET(All Login)
        /// Função responsável por receber uma requisição get e retornar uma lista de todos os usuários registrados no banco de dados. 
        /// </summary>
        /// <returns> Retorna objeto do tipo object</returns>
        [HttpGet("GetAllPortfolio")]
        public object GetAllPortfolio(string gkey)
        {
            try
            {
                return this.business.GetAllPortfolio(gkey);
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
                return Ok(this.business.DeletePortfolio(gkey));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

    }
}
