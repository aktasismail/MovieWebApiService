using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntitiesLayer.RequestFeature
{
    public class MoviePageParameter :RequestParameters
    {
        public uint MinRate { get; set; }
        public uint MaxRate { get; set; } = 10;
        public bool ValidRate => MaxRate > MinRate;
        public string? SearchParameter { get; set; }
        public MoviePageParameter()
        {
            OrderBy = "Id";
        }
        public string? Fields { get; set; }
    }
}
