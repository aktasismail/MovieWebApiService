using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntitiesLayer.RequestFeature
{
    public class PageList<T>:List<T>
    {
        public MetaData MetaData { get; set; }
        public PageList(List<T> items, int count, int pagenumber, int pagesize)
        {
            MetaData = new MetaData()
            {
                CurrentPage = pagenumber,
                PageCount = count,
                PageSize = pagesize,
                TotalPage = (int)Math.Ceiling(count / (double)pagesize)
            };
            AddRange(items);
        }
         public static PageList<T> ToPageList(IEnumerable<T> item,int pagesize,int pagenumber)
        {
            var count = item.Count();
            var items = item.Skip((pagenumber-1)*pagesize)
                .Take(pagesize).ToList();
            return new PageList<T>(items, count, pagenumber, pagesize);
        }

    }
}
