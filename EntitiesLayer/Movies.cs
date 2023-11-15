using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntitiesLayer
{
    public class Movies
    { 
        public int Id { get; set; }
        public string? Moviename { get; set; }
        public string? MovieDetail { get; set; }
        public string? Cast { get; set; }
        public string? Director { get; set; }
        public decimal ImdbRate { get; set; }

    }
}
