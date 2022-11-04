using Library.Shared.Design;
using Linkfolio.Business.Repository;
using Model.Shared.User;
using System.Text.RegularExpressions;

namespace Linkfolio.Business.Business
{
    /// <summary>
    /// Classe responsável pelos modelos de negócio.
    /// Funções da classe: 
    ///    1. Receber os dados passados pelo PortfolioController.
    ///    2. Aplicar a(s) regra(s) de negócio nos dados recebidos.
    ///    3. Encaminhar/receber a(s) informação(ões) para/da classe PortfolioRepository.
    /// </summary>
    public class PortfolioBusiness : Singleton<PortfolioBusiness>
    {
        private PortfolioRepository repository;

        private LoginBusiness loginBusiness;
        protected override void Init()
        {
        }

        public PortfolioBusiness()
        {
            this.repository = PortfolioRepository.GetInstance();
            this.loginBusiness = LoginBusiness.GetInstance();
        }


        /// <summary>
        /// Método de negócio responsável por criar um portfolio.
        /// Verifica se os dados estão de acordo usando o método portfolioVerification, caso os dados estejam de acordo , são salvos no banco de dados.
        /// Se os dados não estiverem de acordo nenhum dado é salvo, retornando uma mensagem de erro relativo ao dado que não está de acordo.
        /// </summary>
        /// <param name="portfolio">Informações necessárias para criar a mensagem.</param>
        public string CreatePortfolio(PortfolioModel portfolio)
        {
            try
            {

                portfolio.Title = portfolio.Title.Trim();
                portfolio.Description = portfolio.Description.Trim();
                portfolio.Gkey = portfolio.Gkey.Trim();
                portfolio.Updated = null;
                switch (portfolioVerification(portfolio))
                {
                    case 0:
                        this.repository.Create(portfolio);
                        return "Portfolio criado com sucesso";
                    case 1:
                        return "Titulo inválido.";
                    case 2:
                        return "Descrição inválida.";
                }

                throw new Exception("Erro inesperado");
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

        }

        /// <summary>
        /// Método de negócio responsável por validar certos dados do portfolio.
        /// </summary>
        /// <param name="portfolio">Informações necessárias para localizar o portfolio.</param>
        public int portfolioVerification(PortfolioModel portfolio)
        {
            try
            {
                this.loginBusiness.GetLogin(portfolio.UserGkey);
                if (string.IsNullOrEmpty(portfolio.Title))
                {
                    return 1;
                }
                if (string.IsNullOrEmpty(portfolio.Description))
                {
                    return 2;
                }

                return 0;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Método de negócio responsável por localizar um portfolio.
        /// </summary>
        /// <param name="gkey">Informações necessárias para localizar o portfolio.</param>
        public PortfolioModel? GetPortfolio(string gkey)
        {
            try
            {
                string strGkeyModel = "^([0-9]{1,})$";
                PortfolioModel? portfolio = new PortfolioModel();
                if (!string.IsNullOrEmpty(gkey) && (Regex.IsMatch(gkey, strGkeyModel)))
                {
                    /// Retirando espaços em vazios no início e fim das variáveis.
                    portfolio.Gkey = gkey.Trim();
                    portfolio = this.repository.Get(portfolio);
                    if (portfolio == null || string.IsNullOrEmpty(portfolio.Title))
                    {
                        throw new Exception("Portfolio não localizado");
                    }
                    return portfolio;

                }
                throw new Exception("Valor inválido");
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

        }

        /// <summary>
        /// Método de negócio responsável por trazer as informações sobre todos os portfolios de um usuário. 
        /// </summary>
        public List<PortfolioModel> GetAllPortfolio(string gkey)
        {
            try
            {
                string strGkeyModel = "^([0-9]{1,})$";
                if (!string.IsNullOrEmpty(gkey) && (Regex.IsMatch(gkey, strGkeyModel)))
                {
                    this.loginBusiness.GetLogin(gkey);
                    List<PortfolioModel> portfolioList = new List<PortfolioModel>();
                    portfolioList = this.repository.List(portfolioList);
                    if (portfolioList == null || portfolioList.Any() == false)
                    {
                        throw new Exception("Nenhum portifolio registrado");
                    }
                    List<PortfolioModel> portfolioReturn = new List<PortfolioModel>();
                    foreach (PortfolioModel p in portfolioList)
                    {
                        if (p.UserGkey == gkey)
                        {
                            portfolioReturn.Add(p);
                        }
                    }
                    if (portfolioReturn.Any() == false)
                    {
                        throw new Exception("Nenhum portifolio registrado nesse usuário");
                    }
                    return portfolioReturn;
                }
                throw new Exception("Valor inválido");
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

        }

        /// <summary>
        /// Método de negócio responsável por atualizar o(s) dado(s) de um portfolio.
        /// </summary>
        /// <param name="newPortfolio">Informações necessárias para encontrar o portfolio no banco de dados e a(s) informação(ões) a ser(em) atualizada(s).</param>
        public string UpdatePortfolio(PortfolioModel newPortfolio)
        {
            try
            {
                string strGkeyModel = "^([0-9]{1,})$";
                PortfolioModel? portfolio = new PortfolioModel();
                portfolio.Gkey = newPortfolio.Gkey.Trim();

                if (!string.IsNullOrEmpty(portfolio.Gkey) && (Regex.IsMatch(portfolio.Gkey, strGkeyModel)))
                {
                    portfolio = this.repository.Get(portfolio);
                    if (portfolio == null || string.IsNullOrEmpty(portfolio.Title))
                    {
                        throw new Exception("Portfolio não localizada");
                    }
                    else
                    {
                        newPortfolio._id = portfolio._id;
                        newPortfolio.Gkey = portfolio.Gkey;
                        newPortfolio.Created = portfolio.Created;
                        newPortfolio.UserGkey = portfolio.UserGkey;
                        if (string.IsNullOrEmpty(newPortfolio.Title.Trim()))
                        {
                            throw new Exception("Valor de título inválido");
                        }
                        if (newPortfolio.Title == portfolio.Title)
                        {
                            newPortfolio.Title = portfolio.Title;
                        }
                        if (string.IsNullOrEmpty(newPortfolio.Description.Trim()))
                        {
                            throw new Exception("Valor de descrição inválido");
                        }
                        if (newPortfolio.Description == portfolio.Description)
                        {
                            newPortfolio.Description = portfolio.Description;
                        }
                        newPortfolio.Description = newPortfolio.Description.Trim();
                        newPortfolio.Title = newPortfolio.Title.Trim();
                        this.repository.Update(newPortfolio);
                        return "Atualização realizado com sucesso";
                    }
                    throw new Exception("Erro inesperado");

                }
                throw new Exception("Valor inválido");
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }


        /// <summary>
        /// Método de negócio responsável por excluir um portfolio.
        /// </summary>
        /// <param name="gkey">Informação necessária para encontrar o portfolio no banco de dados.</param>
        public string DeletePortfolio(string gkey)
        {
            try
            {
                string strGkeyModel = "^([0-9]{1,})$";
                if (!string.IsNullOrEmpty(gkey) && (Regex.IsMatch(gkey, strGkeyModel)))
                {
                    PortfolioModel? portfolio = new PortfolioModel();
                    portfolio.Gkey = gkey.Trim();
                    portfolio = this.repository.Get(portfolio);
                    if (portfolio == null || string.IsNullOrEmpty(portfolio.Title))
                    {
                        throw new Exception("Portfolio não localizado");
                    }
                    this.repository.Delete(portfolio);
                    return "Portfolio excluída com exito";
                }
                throw new Exception("Valor inválido");
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }



    }
}
