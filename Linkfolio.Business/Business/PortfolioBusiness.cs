using Library.Shared.Design;
using Linkfolio.Business.Repository;
using Model.Shared.User;
using System.Text.RegularExpressions;

namespace Linkfolio.Business.Business
{
    public class PortfolioBusiness : Singleton<PortfolioBusiness>
    {
        private PortfolioRepository repository;

        private LoginBusiness loginBusiness;
        protected override void Init()
        {
        }
        /// <summary>
        /// Construtor da CLasse, que insere uma instância da Classe LoginRepository na variável repository. 
        /// </summary>
        public PortfolioBusiness()
        {
            this.repository = PortfolioRepository.GetInstance();
            this.loginBusiness = LoginBusiness.GetInstance();
        }

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

        public int portfolioVerification(PortfolioModel portfolio)
        {
            try
            {
                //string strTitleModel = "^([0-9a-zA-Z]{1,})$";
                //string strDescriptionModel = "^([0-9a-zA-Z]{1,})$";
                this.loginBusiness.GetLogin(portfolio.UserGkey);
                if (string.IsNullOrEmpty(portfolio.Title))
                {
                    return 1;
                }
                // if(!Regex.IsMatch(portfolio.Title, strTitleModel))
                //{
                  //  return 1;
               // }
                if (string.IsNullOrEmpty(portfolio.Description))
                {
                    return 2;
                }
                //if(!Regex.IsMatch(portfolio.Description, strDescriptionModel))
                //{
                //  return 2;
                // }

                return 0;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

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
                    portfolio =  this.repository.Get(portfolio);
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
        /// Método de negócio responsável por trazer as informações sobre todas as contas de usuário registradas. 
        /// Retorna uma lista com o(s) usuário(s) encontrado(s).
        /// </summary>
        public List<PortfolioModel> GetAllPortfolio(string gkey)
        {
            try
            {
                string strGkeyModel = "^([0-9]{1,})$";
                if(!string.IsNullOrEmpty(gkey) && (Regex.IsMatch(gkey, strGkeyModel))) 
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
                        if(p.UserGkey == gkey)
                        {
                            portfolioReturn.Add(p);
                        }
                    }
                    if(portfolioReturn.Any() == false)
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
