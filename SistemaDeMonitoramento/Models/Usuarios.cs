using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SistemaDeMonitoramento.Models
{
    public class Usuarios
    {
        [Key]
        public long? UsuarioId { get; set; }

        [Required]
        [StringLength(42)]
        public string Nome { get; set; }

        [Required]
        [DataType(DataType.Date, ErrorMessage = "Data em formato inválido")]
        public DateTime? DataNascimento { get; set; }

        [Required]
        [StringLength(14)]
        public string CPF { get; set; }


        public ICollection<ResidenciasUsuarios> ResidenciaUsuario { get; set; }

    }
}
