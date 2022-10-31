using Database.Service.Connector;
using Library.Shared.Design;
using Model.Shared.Target;
using System.Reflection;

namespace Database.Service.Factory
{
    public class DatabaseFactory : Singleton<DatabaseFactory>
    {
        protected override void Init()
        {

        }

        public DatabaseConnector Build<T>()
        {
            if (typeof(Mongo).GetTypeInfo().IsAssignableFrom(typeof(T).Ge‌​tTypeInfo())) return MongoConnector.GetInstance();


            throw new Exception("Target connector is not implemented");
        }
    }
}
