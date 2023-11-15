using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntitiesLayer.DTO
{

    public record MovieDTO
    {
        public int Id { get; init; }
        public string MovieName { get; init; }
        public string MovieDetail { get; init; }
        public string Cast { get; init; }
        public string Director { get; init; }
        public decimal ImdbRate { get; set; }

    }
}
