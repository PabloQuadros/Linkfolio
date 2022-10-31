using MongoDB.Bson;

namespace Model.Shared.Target
{
    public interface Mongo : Target
    {
        public ObjectId? _id { get; set; }
    }
}
