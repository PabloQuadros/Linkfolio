using Model.Shared.Target;
using MongoDB.Bson;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Model.Shared.User
{


    public class LoginModel : Mongo
    {
        [JsonIgnore]
        public virtual ObjectId? _id { get; set; } = null;
        public virtual string Gkey { get; set; } = "";
        public virtual string Name { get; set; } = "";
        public virtual string Email { get; set; } = "";
        public virtual string Password { get; set; } = "";
        
        [JsonIgnore]
        public virtual DateTime? Updated { get; set; }
        [JsonIgnore]
        public virtual DateTime Created { get; set; }
        public virtual string Entity { get; } = "Login";
    }
}
