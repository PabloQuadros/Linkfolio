using Database.Service.Repository;
using Model.Shared.User;

namespace Linkfolio.Business.Repository
{
    public class MensageRepository : DatabaseRepository<MensageRepository, MensageModel>
    {
        protected override void Init()
        {
        }

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
