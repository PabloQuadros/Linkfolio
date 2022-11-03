using Library.Shared.Design;
using Linkfolio.Business.Repository;
using Model.Shared.User;
using System.Text.RegularExpressions;



namespace Linkfolio.Business.Business
{
        public class MensageBusiness : Singleton<MensageBusiness>
        {
            private MensageRepository repository;

            private LoginBusiness loginBusiness;
            
            protected override void Init()
            {
                
            }

            

            /// <summary>
            /// Construtor da CLasse, que insere uma instância da Classe LoginRepository na variável repository. 
            /// </summary>
            public MensageBusiness()
            {
                this.repository = MensageRepository.GetInstance();
                this. loginBusiness = LoginBusiness.GetInstance();
             }

        public string CreateMensage(MensageModel msg)
        {
            try
            {
                msg.Mensage = msg.Mensage.Trim();
                msg.Updated = null;
                switch (msgVerification(msg))
                {
                    case 0:
                        this.repository.Create(msg);
                        return "Mensagem enviada com sucesso";
                }
                throw new Exception("Erro inesperado");
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

        }



        public MensageModel? GetMensage(string gkey)
        {
            try
            {
                string strGkeyModel = "^([0-9]{1,})$";
                MensageModel? msg= new MensageModel();
                if (!string.IsNullOrEmpty(gkey) && (Regex.IsMatch(gkey, strGkeyModel)))
                {
                    /// Retirando espaços em vazios no início e fim das variáveis.
                    msg.Gkey = gkey.Trim();
                    msg = this.repository.Get(msg);
                    if (msg == null || string.IsNullOrEmpty(msg.SenderGkey))
                    {
                        throw new Exception("Mensagem não localizada");
                    }
                    return msg;
                }
                throw new Exception("Valor inválido");
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
        public string UpdateMensage(MensageModel newMsg)
        {
            try
            {
                string strGkeyModel = "^([0-9]{1,})$";
                MensageModel? msg = new MensageModel();
                msg.Gkey = newMsg.Gkey.Trim();

                if (!string.IsNullOrEmpty(msg.Gkey) && (Regex.IsMatch(msg.Gkey, strGkeyModel)))
                {
                    msg = this.repository.Get(msg);
                    if (msg == null || string.IsNullOrEmpty(msg.SenderGkey))
                    {
                        throw new Exception("Mensagem não localizada");
                    }
                    else
                    {
                        ///!!!
                        ///Necessário rever a questão de _id, pois na requisição muda o _id e assim não é possível dar update.
                        ///Entretanto o _id teoricamente não irá vir na requisição.
                        newMsg._id = msg._id;
                        newMsg.Gkey = msg.Gkey;
                        newMsg.Created = msg.Created;
                        newMsg.SenderGkey = msg.SenderGkey;
                        newMsg.ReciverGkey = msg.ReciverGkey;
                        this.repository.Update(newMsg);
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
        /// Método de negócio responsável por excluir a conta de um certo usuário.
        /// </summary>
        /// <param name="gkey">Informação necessária para encontrar a conta do usuário no banco de dados.</param>
        public string DeleteMensage(string gkey)
        {
            try
            {
                string strGkeyModel = "^([0-9]{1,})$";
                if (!string.IsNullOrEmpty(gkey) && (Regex.IsMatch(gkey, strGkeyModel)))
                {
                    MensageModel? msg = new MensageModel();
                    msg.Gkey = gkey.Trim();
                    msg = this.repository.Get(msg);
                    if (msg== null || string.IsNullOrEmpty(msg.SenderGkey))
                    {
                        throw new Exception("Mensagem  não localizada");
                    }
                    this.repository.Delete(msg);
                    return "Mensagem excluída com exito";
                }
                throw new Exception("Valor inválido");
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }


        public int msgVerification(MensageModel msg)
        {
            try
            {
                this.loginBusiness.GetLogin(msg.ReciverGkey);
                this.loginBusiness.GetLogin(msg.SenderGkey);
                return 0;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }


    }
  }

