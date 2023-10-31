using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using sistemaFinanceiro.Data;
using sistemaFinanceiro.Models;

namespace sistemaFinanceiro.Controllers
{
    public class TransacaoController : Controller
    {
        private readonly ApplicationDBContext _context;

        public TransacaoController(ApplicationDBContext context)
        {
            _context = context;
        }

        // GET: Transacao
        public async Task<IActionResult> Index()
        {
            var applicationDBContext = _context.Transacoes.Include(t => t.Categoria);
            return View(await applicationDBContext.ToListAsync());
        }

        // GET: Transacao/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Transacoes == null)
            {
                return NotFound();
            }

            var transacaoModel = await _context.Transacoes
                .Include(t => t.Categoria)
                .FirstOrDefaultAsync(m => m.TransacaoId == id);
            if (transacaoModel == null)
            {
                return NotFound();
            }

            return View(transacaoModel);
        }

        // GET: Transacao/Create
        public IActionResult Create()
        {
            ViewData["CategoriaId"] = new SelectList(_context.Categorias, "CategoriaId", "CategoriaId");
            return View();
        }

        // POST: Transacao/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TransacaoId,CategoriaId,Valor,Descricao,Data")] TransacaoModel transacaoModel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(transacaoModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoriaId"] = new SelectList(_context.Categorias, "CategoriaId", "CategoriaId", transacaoModel.CategoriaId);
            return View(transacaoModel);
        }

        // GET: Transacao/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Transacoes == null)
            {
                return NotFound();
            }

            var transacaoModel = await _context.Transacoes.FindAsync(id);
            if (transacaoModel == null)
            {
                return NotFound();
            }
            ViewData["CategoriaId"] = new SelectList(_context.Categorias, "CategoriaId", "CategoriaId", transacaoModel.CategoriaId);
            return View(transacaoModel);
        }

        // POST: Transacao/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("TransacaoId,CategoriaId,Valor,Descricao,Data")] TransacaoModel transacaoModel)
        {
            if (id != transacaoModel.TransacaoId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(transacaoModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TransacaoModelExists(transacaoModel.TransacaoId))
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
            ViewData["CategoriaId"] = new SelectList(_context.Categorias, "CategoriaId", "CategoriaId", transacaoModel.CategoriaId);
            return View(transacaoModel);
        }

        // GET: Transacao/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Transacoes == null)
            {
                return NotFound();
            }

            var transacaoModel = await _context.Transacoes
                .Include(t => t.Categoria)
                .FirstOrDefaultAsync(m => m.TransacaoId == id);
            if (transacaoModel == null)
            {
                return NotFound();
            }

            return View(transacaoModel);
        }

        // POST: Transacao/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Transacoes == null)
            {
                return Problem("Entity set 'ApplicationDBContext.Transacoes'  is null.");
            }
            var transacaoModel = await _context.Transacoes.FindAsync(id);
            if (transacaoModel != null)
            {
                _context.Transacoes.Remove(transacaoModel);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TransacaoModelExists(int id)
        {
          return (_context.Transacoes?.Any(e => e.TransacaoId == id)).GetValueOrDefault();
        }
    }
}
