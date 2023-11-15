using EntitiesLayer.LinkModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntitiesLayer
{
    public class LinkResponse
    {
        public bool Haslink { get; set; }
        public List<Entity> Entities { get; set; }
        public LinkCollectionWrapper<Entity> LinkedEntity { get; set; }
        public LinkResponse()
        {
            Entities = new List<Entity>();
            LinkedEntity = new LinkCollectionWrapper<Entity>();
        }
    }
}
