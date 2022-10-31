using Database.Service.Repository;
using Model.Shared.User;


namespace Linkfolio.Business.Repository
{
        /// <summary>
        /// Classe responsável por se conectar com o banco de dados.
        /// Funções da classe:
        ///     1. Receber o(s) dado(s) da classe LoginBusiness.
        ///     2. Fazer a manipulação no banco de dados Lendo/Criando/Editando/Deletando o(s) dado(s) selecionados.
        /// </summary>
        public class LoginRepository : DatabaseRepository<LoginRepository, LoginModel>
        {
            protected override void Init()
            {

            }

            /// <summary>
            /// Método responsável por criar uma nova conta no banco de dados.
            /// </summary>
            /// <param name="login">Dados da conta a ser criada</param>
            public bool Create(LoginModel login)
            {
                try
                {
                    this.DatabaseConnector.Open();

                    try
                    {
                        return this.DatabaseConnector.Create(login);
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
            public LoginModel? Get(LoginModel login)
            {
                try
                {
                    this.DatabaseConnector.Open();

                    try
                    {
                        Dictionary<string, string> dictionary = new Dictionary<string, string>();
                        dictionary.Add("Gkey", login.Gkey);

                        return this.DatabaseConnector.Get<LoginModel>(dictionary, login);
                    }
                    catch (Exception e)
                    {
                        if (e.Message == "Sequence contains no elements")
                            return login;
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
            public List<LoginModel>? List(List<LoginModel>? loginList)
            {
                try
                {
                    this.DatabaseConnector.Open();

                    try
                    {
                        LoginModel login = new LoginModel();
                        return this.DatabaseConnector.List<LoginModel>(login);
                    }
                    catch (Exception e)
                    {
                        if (e.Message == "Sequence contains no elements")
                            return loginList;
                        throw new Exception(e.Message);
                    }
                }
                finally
                {
                    this.DatabaseConnector.Close();
                }
            }

            /// <summary>
            /// Método responsável por buscar determinado e-mail no banco de dados.
            /// </summary>
            /// <param name="login">Dados que referenciam o e-mail a ser procurado no banco de dados.</param>
            public LoginModel? GetEmail(LoginModel login)
            {
                try
                {
                    this.DatabaseConnector.Open();

                    try
                    {
                        Dictionary<string, string> dictionary = new Dictionary<string, string>();
                        dictionary.Add("Email", login.Email);

                        return this.DatabaseConnector.Get<LoginModel>(dictionary, login);
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

            /// <summary>
            /// Método responsável por atualizar o(s) dado(s) de uma conta no banco de dados.
            /// </summary>
            /// <param name="login">Dados para localizar a conta e os dados a serem atualizados.</param>
            public bool Update(LoginModel login)
            {
                try
                {
                    this.DatabaseConnector.Open();

                    try
                    {
                        Dictionary<string, string> dictionary = new Dictionary<string, string>();
                        dictionary.Add("Gkey", login.Gkey);

                        return this.DatabaseConnector.Update<LoginModel>(dictionary, login);
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
            public bool? Delete(LoginModel login)
            {
                try
                {
                    this.DatabaseConnector.Open();

                    try
                    {
                        Dictionary<string, string> dictionary = new Dictionary<string, string>();
                        dictionary.Add("Gkey", login.Gkey);

                        return this.DatabaseConnector.Delete<LoginModel>(dictionary, login);
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

