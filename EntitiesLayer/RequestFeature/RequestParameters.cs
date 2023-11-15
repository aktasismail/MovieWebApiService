using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace EntitiesLayer.RequestFeature
{
    public abstract class RequestParameters
    {
        const int Maxpagesize = 30;
        public int Pagenumber { get; set; }
        private int pagesize { get; set; }
        public int Pagesize 
        {
            get { return pagesize; }
            set { pagesize = value > Maxpagesize ? Maxpagesize : value;   }
            //  set { pagesize = value > Maxpagesize ? Maxpagesize 
            //  pagesize parametresi maxpagesizedan büyük ise maxpagesize dön
            //  : value; } => değil ise parametreden gelen değeri dön
        }
        public string? OrderBy { get; set; }
    }
}
