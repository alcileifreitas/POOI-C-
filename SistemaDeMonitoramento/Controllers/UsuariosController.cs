using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SistemaDeMonitoramento.Data;
using SistemaDeMonitoramento.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SistemaDeMonitoramento.Controllers
{
    public class UsuariosController : Controller
    {
        private readonly MonitoramentoContext _context;
        public UsuariosController(MonitoramentoContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            return View(await _context.Usuario.OrderBy(u => u.Nome).ToListAsync());
        }

        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult>Create([Bind("Nome, DataNascimento, CPF")] Usuarios usuarios)
        {
            try
            {
                if(ModelState.IsValid)
                {
                    _context.Add(usuarios);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
            }
            catch(DbUpdateException)
            {
                ModelState.AddModelError("", "Não foi possível inseris os dados.");
            }
            return View(usuarios);
        }

        public async Task<IActionResult>Edit(long? id)
        {
            if(id == null)
            {
                return NotFound();
            }
            var usuarios = await _context.Usuario.SingleOrDefaultAsync(u => u.UsuarioId == id);
            if(usuarios == null)
            {
                return NotFound();
            }
            return View(usuarios);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long? id, [Bind("UsuarioId, Nome, DataNascimento, CPF")]Usuarios usuarios)
        {
            if(id != usuarios.UsuarioId)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(usuarios);
                    await _context.SaveChangesAsync();
                }
                catch(DbUpdateConcurrencyException)
                {
                    if(!UsuariosExists(usuarios.UsuarioId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(usuarios);
        }

        public bool UsuariosExists(long? id)
        {
            return _context.Usuario.Any(u => u.UsuarioId == id);
        }

        public async Task<IActionResult> Details(long? id)
        {
            if(id == null)
            {
                return NotFound();
            }
            var usuarios = await _context.Usuario.SingleOrDefaultAsync(u => u.UsuarioId == id);

            if(usuarios == null)
            {
                return NotFound();
            }
            return View(usuarios);
        }


        public async Task<IActionResult> Delete(long? id)
        {
            if(id == null)
            {
                return NotFound();
            }
            var usuarios = await _context.Usuario.SingleOrDefaultAsync(u => u.UsuarioId == id);
            if (usuarios == null)
            {
                return NotFound();
            }
            return View(usuarios);
        }
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long? id)
        {
            var usuarios = await _context.Usuario.SingleOrDefaultAsync(u => u.UsuarioId == id);
            _context.Usuario.Remove(usuarios);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
