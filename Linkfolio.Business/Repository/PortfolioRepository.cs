using Database.Service.Repository;
using Model.Shared.User;

namespace Linkfolio.Business.Repository
{
    public class PortfolioRepository : DatabaseRepository<PortfolioRepository, PortfolioModel>
    {
        protected override void Init()
        {
        }

        public bool Create(PortfolioModel portfolio)
        {
            try
            {
                this.DatabaseConnector.Open();

                try
                {
                    return this.DatabaseConnector.Create(portfolio);
                }
                catch (Exception e)
                {
                    throw new Exception(e.Message);
                }
            }
            finally
            {
                this.DatabaseConnector.Close();
            }
        }


        public PortfolioModel? Get(PortfolioModel portfolio)
        {
            try
            {
                this.DatabaseConnector.Open();

                try
                {
                    Dictionary<string, string> dictionary = new Dictionary<string, string>();
                    dictionary.Add("Gkey", portfolio.Gkey);

                    return this.DatabaseConnector.Get<PortfolioModel>(dictionary, portfolio);
                }
                catch (Exception e)
                {
                    if (e.Message == "Sequence contains no elements")
                        return portfolio;
                    throw new Exception(e.Message);
                }
            }
            finally
            {
                this.DatabaseConnector.Close();
            }
        }


        public bool? Delete(MensageModel msg)
        {
            try
            {
                this.DatabaseConnector.Open();

                try
                {
                    Dictionary<string, string> dictionary = new Dictionary<string, string>();
                    dictionary.Add("Gkey", msg.Gkey);

                    return this.DatabaseConnector.Delete<MensageModel>(dictionary, msg);
                }
                catch (Exception e)
                {
                    if (e.Message == "Sequence contains no elements")
                    {
                        return null;
                    }
                    throw new Exception(e.Message);
                }
            }
            finally
            {
                this.DatabaseConnector.Close();
            }
        }


        public List<PortfolioModel>? List(List<PortfolioModel>? portfolioList)
        {
            try
            {
                this.DatabaseConnector.Open();

                try
                {
                    PortfolioModel portfolio = new PortfolioModel();
                    return this.DatabaseConnector.List<PortfolioModel>(portfolio);
                }
                catch (Exception e)
                {
                    if (e.Message == "Sequence contains no elements")
                        return portfolioList;
                    throw new Exception(e.Message);
                }
            }
            finally
            {
                this.DatabaseConnector.Close();
            }
        }


        public bool Update(PortfolioModel portfolio)
        {
            try
            {
                this.DatabaseConnector.Open();

                try
                {
                    Dictionary<string, string> dictionary = new Dictionary<string, string>();
                    dictionary.Add("Gkey", portfolio.Gkey);

                    return this.DatabaseConnector.Update<PortfolioModel>(dictionary, portfolio);
                }
                catch (Exception e)
                {
                    throw new Exception(e.Message);
                }
            }
            finally
            {
                this.DatabaseConnector.Close();
            }
        }



        public bool? Delete(PortfolioModel portfolio)
        {
            try
            {
                this.DatabaseConnector.Open();

                try
                {
                    Dictionary<string, string> dictionary = new Dictionary<string, string>();
                    dictionary.Add("Gkey", portfolio.Gkey);

                    return this.DatabaseConnector.Delete<PortfolioModel>(dictionary, portfolio);
                }
                catch (Exception e)
                {
                    if (e.Message == "Sequence contains no elements")
                    {
                        return null;
                    }
                    throw new Exception(e.Message);
                }
            }
            finally
            {
                this.DatabaseConnector.Close();
            }
        }
    }
}
