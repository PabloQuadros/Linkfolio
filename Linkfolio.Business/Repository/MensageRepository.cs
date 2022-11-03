using Database.Service.Repository;
using Model.Shared.User;

namespace Linkfolio.Business.Repository
{
    public class MensageRepository : DatabaseRepository<MensageRepository, MensageModel>
    {
        protected override void Init()
        {
        }
        /// <summary>
        /// Método responsável por criar uma nova conta no banco de dados.
        /// </summary>
        /// <param name="login">Dados da conta a ser criada</param>
        public bool Create(MensageModel msg)
        {
            try
            {
                this.DatabaseConnector.Open();

                try
                {
                    return this.DatabaseConnector.Create(msg);
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
        /// <summary>
        /// Método responsável por ler os dados de uma conta no banco de dados.
        /// </summary>
        /// <param name="login">Dados que referenciam a conta a ser consultada no banco de dados.</param>
        public MensageModel? Get(MensageModel msg)
        {
            try
            {
                this.DatabaseConnector.Open();

                try
                {
                    Dictionary<string, string> dictionary = new Dictionary<string, string>();
                    dictionary.Add("Gkey", msg.Gkey);

                    return this.DatabaseConnector.Get<MensageModel>(dictionary, msg);
                }
                catch (Exception e)
                {
                    if (e.Message == "Sequence contains no elements")
                        return msg;
                    throw new Exception(e.Message);
                }
            }
            finally
            {
                this.DatabaseConnector.Close();
            }
        }

       
        /// <summary>
        /// Método responsável por atualizar o(s) dado(s) de uma conta no banco de dados.
        /// </summary>
        /// <param name="login">Dados para localizar a conta e os dados a serem atualizados.</param>
        public bool Update(MensageModel msg)
        {
            try
            {
                this.DatabaseConnector.Open();

                try
                {
                    Dictionary<string, string> dictionary = new Dictionary<string, string>();
                    dictionary.Add("Gkey", msg.Gkey);

                    return this.DatabaseConnector.Update<MensageModel>(dictionary, msg);
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

        /// <summary>
        /// Método responsável por deletar uma conta no banco de dados.
        /// </summary>
        /// <param name="login">Dados para localizar a conta a ser deletada.</param>
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
    }
}
