using Database.Service.Repository;
using Model.Shared.User;


namespace Linkfolio.Business.Repository
{
    public class LoginRepository : DatabaseRepository<LoginRepository, LoginModel>
    {
        protected override void Init()
        {

        }

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

