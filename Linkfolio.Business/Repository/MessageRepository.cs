using Database.Service.Repository;
using Model.Shared.User;

namespace Linkfolio.Business.Repository
{
    public class MessageRepository : DatabaseRepository<MessageRepository, MessageModel>
    {
        protected override void Init()
        {
        }

        public bool Create(MessageModel msg)
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

        public MessageModel? Get(MessageModel msg)
        {
            try
            {
                this.DatabaseConnector.Open();

                try
                {
                    Dictionary<string, string> dictionary = new Dictionary<string, string>();
                    dictionary.Add("Gkey", msg.Gkey);

                    return this.DatabaseConnector.Get<MessageModel>(dictionary, msg);
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



        public bool Update(MessageModel msg)
        {
            try
            {
                this.DatabaseConnector.Open();

                try
                {
                    Dictionary<string, string> dictionary = new Dictionary<string, string>();
                    dictionary.Add("Gkey", msg.Gkey);

                    return this.DatabaseConnector.Update<MessageModel>(dictionary, msg);
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


        public bool? Delete(MessageModel msg)
        {
            try
            {
                this.DatabaseConnector.Open();

                try
                {
                    Dictionary<string, string> dictionary = new Dictionary<string, string>();
                    dictionary.Add("Gkey", msg.Gkey);

                    return this.DatabaseConnector.Delete<MessageModel>(dictionary, msg);
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
