using Model.Shared.Target;

namespace Database.Service.Connector
{
    public interface DatabaseConnector
    {
        public void Register();
        public void Open();
        public void Close();
        public bool Create<T>(T target);
        public bool Update<T>(Dictionary<string, string> dictionary, T target);
        public bool Delete<T>(Dictionary<string, string> dictionary, T target);
        public T? Get<T>(Dictionary<string, string> dictionary, T target);
        public List<T>? List<T>(T target);
    }
}
