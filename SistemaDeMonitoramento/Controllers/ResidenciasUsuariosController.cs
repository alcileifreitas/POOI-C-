using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SistemaDeMonitoramento.Data;
using SistemaDeMonitoramento.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SistemaDeMonitoramento.Controllers
{
    public class ResidenciasUsuariosController : Controller
    {
        private readonly MonitoramentoContext _context;
        public ResidenciasUsuariosController(MonitoramentoContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            return View(await _context.ResidenciaUsuario.Include(r => r.Residencia).Include(r => r.Usuario).OrderBy(r => r.ResidenciaId).ToListAsync());
        }

        public IActionResult Create()
        {
            var residencias = _context.Residencia.OrderBy(r => r.CEP).ToList();
            residencias.Insert(0, new Residencias() { ResidenciaId = 0, CEP = "Selecione a residencia" });
            ViewBag.Residencia = residencias;

            var usuarios = _context.Usuario.OrderBy(u => u.CPF).ToList();
            usuarios.Insert(0, new Usuarios() { UsuarioId = 0, CPF = "Selecione o usuario" });
            ViewBag.Usuario = usuarios;
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ResidenciaId, UsuarioId")] ResidenciasUsuarios residenciasUsuarios)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _context.Add(residenciasUsuarios);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
            }
            catch (DbUpdateException)
            {
                ModelState.AddModelError("", "Não foi possível inserir dados.");
            }
            return View(residenciasUsuarios);
        }

    }
}
