using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using sistemaFinanceiro.Data;
using sistemaFinanceiro.Models;

namespace sistemaFinanceiro.Controllers
{
    public class DashboardController : Controller
    {
        private readonly ApplicationDBContext _context;
        public DashboardController(ApplicationDBContext context)
        {
            _context = context;
        }

        public async Task<ActionResult> Index()
        {

            DateTime StartDate = DateTime.Today.AddDays(-7);
            DateTime EndDate = DateTime.Today;

            List<TransacaoModel> SelectedTransactions = await _context.Transacoes
                .Include(x => x.Categoria)
                .Where(y => y.Data >= StartDate && y.Data <= EndDate)
                .ToListAsync();


            int TotalIncome = SelectedTransactions
                .Where(i => i.Categoria.Tipo == "Renda")
                .Sum(j => j.Valor);
            ViewBag.TotalIncome = TotalIncome.ToString("C0");


            int TotalExpense = SelectedTransactions
                .Where(i => i.Categoria.Tipo == "Despesa")
                .Sum(j => j.Valor);
            ViewBag.TotalExpense = TotalExpense.ToString("C0");

            int Balance = TotalIncome - TotalExpense;

            ViewBag.Balance = Balance.ToString("C0");

            return View();

        }

    }
}
