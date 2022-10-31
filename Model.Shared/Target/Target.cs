
namespace Model.Shared.Target
{
    public interface Target
    {
        public DateTime? Updated { get; set; }
        public DateTime Created { get; set; }
        public string Entity { get; }
    }
}
