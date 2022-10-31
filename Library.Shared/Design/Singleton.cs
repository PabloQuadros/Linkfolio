
namespace Library.Shared.Design
{
    public abstract class Singleton<T> where T : class, new()
    {
        private static T? _instance = null;
        private static readonly object _lock = new object();

        protected Singleton()
        {
            this.Init();
        }

        public static T GetInstance()
        {
            if (_instance == null)
            {
                lock (_lock)
                {
                    if (_instance == null)
                    {
                        _instance = new T();
                    }
                }
            }
            return _instance;
        }

        protected abstract void Init();
    }
}
