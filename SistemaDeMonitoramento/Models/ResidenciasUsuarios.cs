using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SistemaDeMonitoramento.Models
{
    public class ResidenciasUsuarios
    {
        public long? ResidenciaId { get; set; }
        public long? UsuarioId { get; set; }


        public Usuarios Usuario { get; set; }
        public Residencias Residencia { get; set; }
    }
}
