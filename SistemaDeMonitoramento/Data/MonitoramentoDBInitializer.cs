using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SistemaDeMonitoramento.Data
{
    public class MonitoramentoDBInitializer
    {
        public static void Initialize(MonitoramentoContext context)
        {
            //context.Database.EnsureDeleted();
            context.Database.EnsureCreated();
        }
    }
}
