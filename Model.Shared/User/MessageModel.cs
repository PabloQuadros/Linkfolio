using Model.Shared.Target;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Model.Shared.User
{
    public class MessageModel : Mongo
    {
        [JsonIgnore]
        public virtual ObjectId? _id { get; set; } = null;
        public virtual string Gkey { get; set; } = "";
        public virtual string SenderGkey { get; set; } = "";
        public virtual string ReciverGkey { get; set; } = "";
        public virtual string Mensage { get; set; } = "";
        [JsonIgnore]
        public virtual DateTime? Updated { get; set; }
        [JsonIgnore]
        public virtual DateTime Created { get; set; }
        public virtual string Entity { get; } = "Message";
    }
}
