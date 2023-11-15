using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntitiesLayer.DTO
{
    public abstract record MovieValidationDto
    {

        [Required]
        [MaxLength(30,ErrorMessage="En Fazla 30 karakter girilebilir.")]
        public string MovieName { get; init; }
        [Required]
        public string MovieDetail { get; init; }
        [Required]
        public string Cast { get; init; }
        [Required]
        [MaxLength(200, ErrorMessage = "En Fazla 200 karakter girilebilir.")]
        public string Director { get; init; }
        [Required]
        [Range(1, 10, ErrorMessage = "1 ile 10 arası değer alabilir.")]
        public decimal ImdbRate { get; set; }
    }
}
