using Model.Shared.Target;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Shared.User
{
    public class PortfolioModel : Mongo
    {
        public virtual ObjectId? _id { get; set; } = null;
        public virtual string Gkey { get; set; } = "";
        public virtual string UserGkey { get; set; } = "";
        public virtual string Title { get; set; } = "";
        public virtual string Description { get; set; } = "";
        public virtual DateTime? Updated { get; set; }
        public virtual DateTime Created { get; set; }
        public virtual string Entity { get; } = "Portfolio";
    }
}
