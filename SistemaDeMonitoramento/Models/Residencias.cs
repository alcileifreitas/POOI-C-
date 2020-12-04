using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SistemaDeMonitoramento.Models
{
    public class Residencias
    {
        [Key]
        public long? ResidenciaId { get; set; }

        [Required]
        [StringLength(50)]
        public string Cidade { get; set; }

        [Required]
        [StringLength(50)]
        public string Bairro { get; set; }

        [Required]
        [StringLength(9)]
        public string CEP { get; set; }


        public ICollection<ResidenciasUsuarios> ResidenciaUsuario { get; set; }



        public Ambientes Ambiente { get; set; }
        public long? AmbienteId { get; set; }

    }
}
