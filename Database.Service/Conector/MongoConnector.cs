using Library.Shared.Design;
using Model.Shared.Target;
using Model.Shared.User;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using System.Security.Authentication;

namespace Database.Service.Connector
{
    public class MongoConnector : Singleton<MongoConnector>, DatabaseConnector
    {
        private MongoClient? client = null;
        private MongoClientSettings? settings = null;
        private MongoIdentity? identity = null;
        private MongoIdentityEvidence? evidence = null;
        private IMongoDatabase? database = null;

        protected override void Init()
        {
            this.settings = new MongoClientSettings();
            settings.Server = new MongoServerAddress("localhost", 27017);
            settings.UseTls = false;
            settings.SslSettings = new SslSettings();
            settings.SslSettings.EnabledSslProtocols = SslProtocols.Tls12;



            this.client = new MongoClient(settings);
            this.database = this.client.GetDatabase("Linkfolio");

            this.Register();
        }

        public void Register()
        {
            BsonClassMap.RegisterClassMap<LoginModel>();
        }

        public void Open()
        {

        }

        public void Close()
        {

        }

        public bool Create<T>(T target)
        {
            if (this.database == null) throw new Exception("Database is null");
            if (target == null) throw new Exception("Target is null");

            try
            {
                Mongo mongo = (Mongo)target;

                var entity = this.database.GetCollection<Mongo>(mongo.Entity);

                mongo.Created = DateTime.Now;
                mongo._id = ObjectId.GenerateNewId();

                entity.InsertOne(mongo);

                return true;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Delete<T>(Dictionary<string, string> dictionary, T target)
        {
            if (this.database == null) throw new Exception("Database is null");
            if (target == null) throw new Exception("Target is null");

            try
            {
                Mongo mongo = (Mongo)target;
                var entity = this.database.GetCollection<T>(mongo.Entity);
                entity.DeleteOne(new BsonDocument(dictionary));

                return true;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public T? Get<T>(Dictionary<string, string> dictionary, T target)
        {
            if (this.database == null) throw new Exception("Database is null");
            if (target == null) throw new Exception("Target is null");

            try
            {
                Mongo mongo = (Mongo)target;
                var entity = this.database.GetCollection<T>(mongo.Entity);
                return entity.Find(new BsonDocument(dictionary)).First();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public List<T>? List<T>(T target)
        {
            if (this.database == null) throw new Exception("Database is null");
            if (target == null) throw new Exception("Target is null");

            try
            {
                Mongo mongo = (Mongo)target;
                var entity = this.database.GetCollection<T>(mongo.Entity);
                return entity.Find(new BsonDocument()).ToList();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Update<T>(Dictionary<string, string> dictionary, T target)
        {
            if (this.database == null) throw new Exception("Database is null");
            if (target == null) throw new Exception("Target is null");

            try
            {
                Mongo mongo = (Mongo)target;
                var entity = this.database.GetCollection<Mongo>(mongo.Entity);
                mongo.Updated = DateTime.Now;
                entity.ReplaceOne(new BsonDocument(dictionary), mongo);

                return true;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
