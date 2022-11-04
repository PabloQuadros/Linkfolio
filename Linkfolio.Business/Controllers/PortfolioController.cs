using Linkfolio.Business.Business;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Model.Shared.User;

namespace Linkfolio.Business.Controllers
{
    /// <summary>
    /// Classe responsável por receber a(s) requisição(ões).
    /// Funções da classe:
    ///     1. Receber requisições Get/Post/Put/Delete e encaminhar o(s) dado(s) para a classe PortfolioBusiness
    ///     2. Receber o(s) dado(s) do PortfolioBusiness e mostra-lo(s) para o usuário.
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    [Authorize]
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
        /// Requisição POST
        /// Função responsável por receber uma requisição post e encaminhar os dados para a criação de um portfolio.
        /// </summary>
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
        /// Requisição GET
        /// Função responsável por receber uma requisição get e encaminhar o valor recebido para buscar os dados de um portfolio. 
        /// </summary>
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
        /// Requisição GET(All Portfolios)
        /// Função responsável por receber uma requisição get e retornar uma lista de todos os portfolios registrados de um usuário no banco de dados. 
        /// </summary>
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
        /// Requisição PUT
        /// Função responsável por receber uma requisição put e encaminhar o(s) dado(s) para atualizar o cadastro de um portfolio.
        /// </summary>
        [HttpPut("Update")]
        public object UpdatePortfolio([FromBody] PortfolioModel portfolio)
        {
            try
            {
                return Ok(this.business.UpdatePortfolio(portfolio));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }

        }

        /// <summary>
        /// Requisição DELETE
        /// Função responsável por receber uma requisição delete e encaminhar o valor para a classe PortfolioBusiness deletar um usuário.
        /// </summary>
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
