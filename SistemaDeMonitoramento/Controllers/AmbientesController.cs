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
    public class AmbientesController : Controller
    {
        private readonly MonitoramentoContext _context;
        public AmbientesController(MonitoramentoContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            return View(await _context.Ambiente.Include(a => a.Aparelho).OrderBy(a => a.Nome).ToListAsync());
        }

        public IActionResult Create()
        {
            var aparelhos = _context.Aparelho.OrderBy(a => a.Nome).ToList();
            aparelhos.Insert(0, new Aparelhos() { AparelhoId = 0, Nome = "Selecione o aparelho" });
            ViewBag.Aparelho = aparelhos;
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Nome, AparelhoId")] Ambientes ambientes)
        {
            try
            {
                if(ModelState.IsValid)
                {
                    _context.Add(ambientes);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
            }
            catch(DbUpdateException)
            {
                ModelState.AddModelError("", "Não foi possivel inserir dados.");
            }
            return View(ambientes);
        }


        public async Task<IActionResult> Edit(long? id)
        {
            if(id == null)
            {
                return NotFound();
            }
            var ambientes = await _context.Ambiente.SingleOrDefaultAsync(a => a.AmbienteId == id);
            if(ambientes == null)
            {
                return NotFound();
            }
            ViewBag.Aparelho = new SelectList(_context.Aparelho.OrderBy(a => a.Nome), "AparelhoId", "Nome", ambientes.AparelhoId);

            return View(ambientes);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult>Edit(long? id,[Bind("AmbienteId, Nome, AparelhoId")] Ambientes ambientes)
        {
            if(id != ambientes.AmbienteId)
            {
                return NotFound();
            }
            if(ModelState.IsValid)
            {
                try
                {
                    _context.Update(ambientes);
                    await _context.SaveChangesAsync();
                }
                catch(DbUpdateConcurrencyException)
                {
                    if(!AmbientesExists(ambientes.AmbienteId))
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
            ViewBag.Aparelho = new SelectList(_context.Aparelho.OrderBy(a => a.Nome), "AparelhoId", "Nome", ambientes.AparelhoId);
            return View(ambientes);
        }
        public bool AmbientesExists(long? id)
        {
            return _context.Ambiente.Any(a => a.AmbienteId == id);
        }


        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ambientes = await _context.Ambiente.SingleOrDefaultAsync(a => a.AmbienteId == id);
            _context.Aparelho.Where(i => ambientes.AparelhoId == i.AparelhoId).Load();

            if(ambientes == null)
            {
                return NotFound();
            }
            return View(ambientes);
        }

        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var ambientes = await _context.Ambiente.SingleOrDefaultAsync(a => a.AmbienteId == id);
            _context.Aparelho.Where(a => a.AparelhoId == ambientes.AparelhoId).Load();
            if(ambientes == null)
            {
                return NotFound();
            }
            return View(ambientes);
        }
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long? id)
        {
            var ambientes = await _context.Ambiente.SingleOrDefaultAsync(a => a.AmbienteId == id);
            _context.Ambiente.Remove(ambientes);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
