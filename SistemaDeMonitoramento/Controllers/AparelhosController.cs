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
    public class AparelhosController : Controller
    {
        private readonly MonitoramentoContext _context;
        public AparelhosController(MonitoramentoContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            return View(await _context.Aparelho.OrderBy(a => a.Nome).ToListAsync());
        }


        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult>Create([Bind("Nome, Modelo, Tipo")] Aparelhos aparelhos)
        {
            try
            {
                if(ModelState.IsValid)
                {
                    _context.Add(aparelhos);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
            }
            catch(DbUpdateException)
            {
                ModelState.AddModelError("", "Não foi possivel inserir os dados.");
            }
            return View(aparelhos);
        }


        public async Task<IActionResult> Edit(long? id)
        {
            if(id == null)
            {
                return NotFound();
            }
            var aparelhos = await _context.Aparelho.SingleOrDefaultAsync(a => a.AparelhoId == id);
            if(aparelhos ==  null)
            {
                return NotFound();
            }
            return View(aparelhos);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult>Edit(long? id, [Bind("AparelhoId, Nome, Modelo, Tipo")] Aparelhos aparelhos)
        {
            if(id != aparelhos.AparelhoId)
            {
                return NotFound();
            }
            if(ModelState.IsValid)
            {
                try
                {
                    _context.Update(aparelhos);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if(!AparelhosExists(aparelhos.AparelhoId))
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
            return View(aparelhos);
        }
        public bool AparelhosExists(long? id)
        {
            return _context.Aparelho.Any(a => a.AparelhoId == id);
        }


        public async Task<IActionResult> Details(long? id)
        {
            if(id == null)
            {
                return NotFound();
            }

            var aparelhos = await _context.Aparelho.SingleOrDefaultAsync(a => a.AparelhoId == id);

            if(aparelhos == null)
            {
                return NotFound();
            }
            return View(aparelhos);
        }


        public async Task<IActionResult> Delete(long? id)
        {
            if(id == null)
            {
                return NotFound();
            }
            var aparelhos = await _context.Aparelho.SingleOrDefaultAsync(a => a.AparelhoId == id);

            if(aparelhos == null)
            {
                return NotFound();
            }
            return View(aparelhos);
        }
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long? id)
        {
            var aparelhos = await _context.Aparelho.SingleOrDefaultAsync(a => a.AparelhoId == id);
            _context.Aparelho.Remove(aparelhos);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

    }
}
