using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntitiesLayer.LinkModels
{

    public class LinkResource
    {
        public LinkResource()
        {
        }
        public List<Links> Links { get; set; } = new List<Links>();
    }
}
