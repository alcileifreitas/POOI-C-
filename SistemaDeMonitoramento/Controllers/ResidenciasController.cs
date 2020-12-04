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
    public class ResidenciasController : Controller
    {
        private readonly MonitoramentoContext _context;
        public ResidenciasController(MonitoramentoContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            return View(await _context.Residencia.Include(r => r.Ambiente).OrderBy(r => r.ResidenciaId).ToListAsync());
        }

        public IActionResult Create()
        {
            var ambientes = _context.Ambiente.OrderBy(a => a.AmbienteId).ToList();
            ambientes.Insert(0, new Ambientes() { AmbienteId = 0, Nome = "Selecone o Ambiente" });
            ViewBag.Ambiente = ambientes;
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind ("Cidade, Bairro, CEP, AmbienteId")] Residencias residencias)
        {
            try
            {
                if(ModelState.IsValid)
                {
                    _context.Add(residencias);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
            }
            catch(DbUpdateException)
            {
                ModelState.AddModelError("", "Não foi possível inserir dados.");
            }
            return View(residencias);
        }


        public async Task<IActionResult> Edit(long? id)
        {
            if(id == null)
            {
                return NotFound();
            }
            var residencias = await _context.Residencia.SingleOrDefaultAsync(r => r.ResidenciaId == id);
            if(residencias == null)
            {
                return NotFound();
            }
            ViewBag.Ambiente = new SelectList(_context.Ambiente.OrderBy(a => a.Nome), "AmbienteId", "Nome", residencias.AmbienteId);

            return View(residencias);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long? id, [Bind("ResidenciaId, Cidade, Bairro, CEP, AmbienteId")]Residencias residencias)
        {
            if(id != residencias.ResidenciaId)
            {
                return NotFound();
            }
            if(ModelState.IsValid)
            {
                try
                {
                    _context.Update(residencias);
                    await _context.SaveChangesAsync();
                }
                catch(DbUpdateConcurrencyException)
                {
                    if(!ResisdenciasExists(residencias.ResidenciaId))
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
            ViewBag.Ambiente = new SelectList(_context.Ambiente.OrderBy(a => a.Nome), "AmbienteId", "Nome", residencias.AmbienteId);
            return View(residencias);
        }
        public bool ResisdenciasExists(long? id)
        {
            return _context.Residencia.Any(r => r.ResidenciaId == id);
        }


        public async Task<IActionResult> Details(long? id)
        {
            if(id == null)
            {
                return NotFound();
            }

            var residencias = await _context.Residencia.SingleOrDefaultAsync(r => r.ResidenciaId == id);
            _context.Ambiente.Where(a => residencias.AmbienteId == a.AmbienteId).Load();

            if(residencias == null)
            {
                return NotFound();
            }
            return View(residencias);
        }


        public async Task<IActionResult> Delete(long? id)
        {
            var residencias = await _context.Residencia.SingleOrDefaultAsync(r => r.ResidenciaId == id);
            _context.Ambiente.Where(a => a.AmbienteId == residencias.AmbienteId).Load();
            if(residencias == null)
            {
                return NotFound();
            }
            return View(residencias);
        }
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long? id)
        {
            var residencias = await _context.Residencia.SingleOrDefaultAsync(r => r.ResidenciaId == id);
            _context.Residencia.Remove(residencias);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
