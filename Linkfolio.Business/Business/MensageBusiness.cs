using Library.Shared.Design;
using Linkfolio.Business.Repository;
using Model.Shared.User;
using System.Text.RegularExpressions;



namespace Linkfolio.Business.Business
{
    public class MensageBusiness
    {
        /// <summary>
        /// Classe responsável pelos modelos de negócio.
        /// Funções da classe: 
        ///    1. Receber os dados passados pelo LoginController.
        ///    2. Aplicar a(s) regra(s) de negócio nos dados recebidos.
        ///    3. Encaminhar/receber a(s) informação(ões) para/da classe LoginRepository.
        /// </summary>
        public class LoginBusiness : Singleton<LoginBusiness>
        {
            private LoginRepository repository;

            protected override void Init()
            {

            }

            /// <summary>
            /// Construtor da CLasse, que insere uma instância da Classe LoginRepository na variável repository. 
            /// </summary>
            public LoginBusiness()
            {
                this.repository = LoginRepository.GetInstance();
            }

            /// <summary>
            /// Método de negócio responsável por criar a conta do usuário.
            /// Verifica se os dados estão de acordo usando o método dataVerification, caso estejam os dados do usuário são salvos no banco de dados.
            /// Se os dados não estiverem de acordo nenhum dado é salvo, retornando uma mensagem de erro relativo ao dado que não está de acordo.
            /// </summary>
            /// <param name="login">Informações necessárias para criar a conta do usuário.</param>
            public string CreateLogin(LoginModel login)
            {
                try
                {
                    login.Name = login.Name.Trim();
                    login.Email = login.Email.Trim();
                    login.Password = login.Password.Trim();

                    switch (dataVerification(login))
                    {
                        case 0:
                            this.repository.Create(login);
                            return "Cadastro realizado com sucesso";
                        case 1:
                            throw new Exception("E-mail já cadastrado");
                        case 2:
                            throw new Exception("Nome de usuário inválido");
                        case 3:
                            throw new Exception("Email inválido");
                        case 4:
                            throw new Exception("Senha inválida");
                    }
                    throw new Exception("Erro inesperado");
                }
                catch (Exception e)
                {
                    throw new Exception(e.Message);
                }

            }

            /// <summary>
            /// Método de negócio responsável por trazer as informações sobre a conta do usuário.
            /// Retorna um único usuário, caso não encontre é retornado NULL.
            /// </summary>
            /// <param name="gkey">Informações necessárias para encontrar a conta do usuário no banco de dados.</param>
            public LoginModel? GetLogin(string gkey)
            {
                try
                {
                    string strGkeyModel = "^([0-9]{1,})$";
                    LoginModel? login = new LoginModel();
                    if (!string.IsNullOrEmpty(gkey) && (Regex.IsMatch(gkey, strGkeyModel)))
                    {
                        /// Retirando espaços em vazios no início e fim das variáveis.
                        login.Gkey = gkey.Trim();
                        login = this.repository.Get(login);
                        if (login == null || string.IsNullOrEmpty(login.Name))
                        {
                            throw new Exception("Usuário não localizado");
                        }
                        return login;
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
            public List<LoginModel> GetAllLogin()
            {
                try
                {
                    List<LoginModel> loginList = new List<LoginModel>();
                    loginList = this.repository.List(loginList);
                    if (loginList == null || loginList.Any() == false)
                    {
                        throw new Exception("Nenhum usuário registrado");
                    }
                    return loginList;
                }
                catch (Exception e)
                {
                    throw new Exception(e.Message);
                }

            }

            /// <summary>
            /// Método de negócio responsável por atualizar o(s) dado(s) da conta de um certo usuário.
            /// Nesse método é chamado o método dataUpdateVerification para verificar se os dados estão de acordo.
            /// </summary>
            /// <param name="newLogin">Informações necessárias para encontrar a conta do usuário no banco de dados e a(s) informação(ões) a ser(em) atualizada(s).</param>
            public string UpdateLogin(LoginModel newLogin)
            {
                try
                {
                    string strGkeyModel = "^([0-9]{1,})$";
                    LoginModel? login = new LoginModel();
                    login.Gkey = newLogin.Gkey.Trim();

                    if (!string.IsNullOrEmpty(login.Gkey) && (Regex.IsMatch(login.Gkey, strGkeyModel)))
                    {
                        login = this.repository.Get(login);
                        if (login == null || string.IsNullOrEmpty(login.Name))
                        {
                            throw new Exception("Usuário não localizado");
                        }
                        else
                        {
                            newLogin.Name = newLogin.Name.Trim();
                            newLogin.Email = newLogin.Email.Trim();
                            newLogin.Password = newLogin.Password.Trim();
                            ///!!!
                            ///Necessário rever a questão de _id, pois na requisição muda o _id e assim não é possível dar update.
                            ///Entretanto o _id teoricamente não irá vir na requisição.
                            newLogin._id = login._id;
                            switch (dataUpdateVerification(newLogin))
                            {
                                case 0:
                                    this.repository.Update(newLogin);
                                    return "Atualização realizado com sucesso";
                                case 1:
                                    throw new Exception("Email já cadastrado");
                                case 2:
                                    throw new Exception("Nome de usuário inválido");
                                case 3:
                                    throw new Exception("Email inválido");
                                case 4:
                                    throw new Exception("Senha inválida");

                            }
                            throw new Exception("Erro inesperado");
                        }
                    }
                    throw new Exception("Valor inválido");
                }
                catch (Exception e)
                {
                    throw new Exception(e.Message);
                }
            }

            /// <summary>
            /// Método de negócio responsável por excluir a conta de um certo usuário.
            /// </summary>
            /// <param name="gkey">Informação necessária para encontrar a conta do usuário no banco de dados.</param>
            public string DeleteLogin(string gkey)
            {
                try
                {
                    string strGkeyModel = "^([0-9]{1,})$";
                    if (!string.IsNullOrEmpty(gkey) && (System.Text.RegularExpressions.Regex.IsMatch(gkey, strGkeyModel)))
                    {
                        LoginModel? login = new LoginModel();
                        login.Gkey = gkey.Trim();
                        login = this.repository.Get(login);
                        if (login == null || string.IsNullOrEmpty(login.Name))
                        {
                            throw new Exception("Usuário não localizado");
                        }
                        this.repository.Delete(login);
                        return "Usuário excluído com exito";
                    }
                    throw new Exception("Valor inválido");
                }
                catch (Exception e)
                {
                    throw new Exception(e.Message);
                }
            }

            /// <summary>
            ///Método responsável por fazer a validação dos dados Nome/E-mail/Password para verificar se os mesmos estão de acordo.
            ///Esse método também verifica se o e-mail já está registrado.
            /// </summary>
            /// <param name="login">Dados a serem validados</param>
            /// <returns></returns>
            public int dataVerification(LoginModel login)
            {
                try
                {
                    string strEmailModel = "^([0-9a-zA-Z]([-.\\w]*[0-9a-zA-Z])*@([0-9a-zA-Z][-\\w]*[0-9a-zA-Z]\\.)+[a-zA-Z]{2,9})$";
                    string strPasswordModel = "^.*(?=.{8,})(?=.*[@#$%^&+=])(?=.*\\d)(?=.*[a-z])(?=.*[A-Z]).*$";
                    if (this.repository.GetEmail(login) != null)
                        return 1;
                    if (string.IsNullOrEmpty(login.Name))
                        return 2;
                    if (string.IsNullOrEmpty(login.Email))
                        return 3;
                    if (!Regex.IsMatch(login.Email, strEmailModel))
                        return 3;
                    if (string.IsNullOrEmpty(login.Password))
                        return 4;
                    if (!Regex.IsMatch(login.Password, strPasswordModel))
                        return 4;
                    return 0;
                }
                catch (Exception e)
                {
                    throw new Exception(e.Message);
                }
            }

            /// <summary>
            /// Método utilizado para atualizar o cadastro de um usuário. 
            /// Método responsável por fazer a validação dos dados Nome/E-mail/Password para verificar se os mesmos estão de acordo.
            /// Esse método também verifica se o e-mail já está registrado, porem verifica também se o e-mail já registrado é do mesmo usuário que está sendo feita a atualização de dados.
            /// </summary>
            /// <param name="login">Dados a serem validados</param>
            /// <returns></returns>
            public int dataUpdateVerification(LoginModel login)
            {
                try
                {
                    string strEmailModel = "^([0-9a-zA-Z]([-.\\w]*[0-9a-zA-Z])*@([0-9a-zA-Z][-\\w]*[0-9a-zA-Z]\\.)+[a-zA-Z]{2,9})$";
                    string strPasswordModel = "^.*(?=.{8,})(?=.*[@#$%^&+=])(?=.*\\d)(?=.*[a-z])(?=.*[A-Z]).*$";
                    LoginModel? loginAux = new LoginModel();
                    loginAux = this.repository.GetEmail(login);

                    if (loginAux != null)
                        if (login.Gkey != loginAux.Gkey)
                            return 1;
                    if (string.IsNullOrEmpty(login.Name))
                        return 2;
                    if (string.IsNullOrEmpty(login.Email))
                        return 3;
                    if (!System.Text.RegularExpressions.Regex.IsMatch(login.Email, strEmailModel))
                        return 3;
                    if (string.IsNullOrEmpty(login.Password))
                        return 4;
                    if (!System.Text.RegularExpressions.Regex.IsMatch(login.Password, strPasswordModel))
                        return 4;
                    return 0;
                }
                catch (Exception e)
                {
                    throw new Exception(e.Message);
                }

            }
        }
    }
}
