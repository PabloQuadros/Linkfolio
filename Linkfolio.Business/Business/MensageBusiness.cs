using Library.Shared.Design;
using Linkfolio.Business.Repository;
using Model.Shared.User;
using System.Text.RegularExpressions;



namespace Linkfolio.Business.Business
{
    /// <summary>
    /// Classe responsável pelos modelos de negócio.
    /// Funções da classe: 
    ///    1. Receber os dados passados pelo MensageController.
    ///    2. Aplicar a(s) regra(s) de negócio nos dados recebidos.
    ///    3. Encaminhar/receber a(s) informação(ões) para/da classe MensageRepository.
    /// </summary>
    public class MensageBusiness : Singleton<MensageBusiness>
    {
        private MensageRepository repository;

        private LoginBusiness loginBusiness;

        protected override void Init()
        {

        }

        public MensageBusiness()
        {
            this.repository = MensageRepository.GetInstance();
            this.loginBusiness = LoginBusiness.GetInstance();
        }


        /// <summary>
        /// Método de negócio responsável por criar uma mensagem.
        /// Verifica se os dados estão de acordo usando o método msgVerification, caso os dados estejam de acordo , são salvos no banco de dados.
        /// Se os dados não estiverem de acordo nenhum dado é salvo, retornando uma mensagem de erro relativo ao dado que não está de acordo.
        /// </summary>
        /// <param name="msg">Informações necessárias para criar a mensagem.</param>
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


        /// <summary>
        /// Método de negócio responsável por localizar uma mensagem.
        /// </summary>
        /// <param name="gkey">Informações necessárias para localizar a mensagem.</param>
        public MensageModel? GetMensage(string gkey)
        {
            try
            {
                string strGkeyModel = "^([0-9]{1,})$";
                MensageModel? msg = new MensageModel();
                if (!string.IsNullOrEmpty(gkey) && (Regex.IsMatch(gkey, strGkeyModel)))
                {
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
        /// Método de negócio responsável por atualizar o(s) dado(s) de uma mensagem.
        /// </summary>
        /// <param name="newMsg">Informações necessárias para encontrar a mensage no banco de dados e a(s) informação(ões) a ser(em) atualizada(s).</param>
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
        /// Método de negócio responsável por excluir uma mensagem.
        /// </summary>
        /// <param name="gkey">Informação necessária para encontrar a mensagem no banco de dados.</param>
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
                    if (msg == null || string.IsNullOrEmpty(msg.SenderGkey))
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

        /// <summary>
        ///Método responsável por validar se o remetente e o destinatário existem.
        /// </summary>
        /// <param name="msg">Dados a serem validados</param>
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

