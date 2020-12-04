using Microsoft.EntityFrameworkCore;
using SistemaDeMonitoramento.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SistemaDeMonitoramento.Data
{
    public class MonitoramentoContext : DbContext
    {
        public MonitoramentoContext(DbContextOptions<MonitoramentoContext> options) : base(options)
        {

        }

        public DbSet<Ambientes> Ambiente { get; set; }
        public DbSet<Aparelhos> Aparelho { get; set; }
        public DbSet<Usuarios> Usuario { get; set; }
        public DbSet<Residencias> Residencia { get; set; }
        public DbSet<ResidenciasUsuarios> ResidenciaUsuario { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ResidenciasUsuarios>()
                .HasKey(x => new { x.ResidenciaId, x.UsuarioId });
        }

    }
}
