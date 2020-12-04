using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SistemaDeMonitoramento.Models
{
    public class Aparelhos
    {
        [Key]
        public long? AparelhoId { get; set; }

        [Required]
        [StringLength(42)]
        public string Nome { get; set; }

        [Required]
        [StringLength(20)]
        public string Modelo { get; set; }

        [Required]
        [StringLength(20)]
        public string Tipo { get; set; }


        public virtual ICollection<Ambientes> Ambiente { get; set; }
    }
}
