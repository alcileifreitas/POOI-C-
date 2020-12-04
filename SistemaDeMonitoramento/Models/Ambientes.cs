using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SistemaDeMonitoramento.Models
{
    public class Ambientes
    {
        [Key]
        public long? AmbienteId { get; set; }

        [Required]
        [StringLength(42)]
        public string Nome { get; set; }


        
        public Aparelhos Aparelho { get; set; }
        public long? AparelhoId { get; set; }

        public virtual ICollection<Residencias> Residencia { get; set; }
    }
}
