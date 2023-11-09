using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
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
            DateTime EndDate = DateTime.Today.AddDays(1);

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


            ViewBag.GraphicChart = SelectedTransactions
           .Where(i => i.Categoria.Tipo == "Despesa")
           .GroupBy(j => j.Categoria.CategoriaId)
           .Select(k => new
           {
               IconeComNome = k.First().Categoria.Icone + " " + k.First().Categoria.Nome,
               amount = k.Sum(j => j.Valor),
               ValorFormatado = k.Sum(j => j.Valor).ToString("C0"),
           })
           .OrderByDescending(l => l.amount)
           .ToList();


            //renda
            List<SplineChartData> IncomeSummary = SelectedTransactions
           .Where(i => i.Categoria.Tipo == "Renda")
           .GroupBy(j => j.Data)
           .Select(k => new SplineChartData()
           {
               day = k.First().Data.ToString("dd-MMM"),
               income = k.Sum(l => l.Valor),
           }).ToList();

            //despesas
            List<SplineChartData> ExpenseSummary = SelectedTransactions
                .Where(i => i.Categoria.Tipo == "Despesa")
           .GroupBy(j => j.Data)
           .Select(k => new SplineChartData()
           {
               day = k.First().Data.ToString("dd-MMM"),
               expense = k.Sum(l => l.Valor),
           }).ToList();

            //renda e despesas juntas
            string[] Last7Days = Enumerable.Range(0, 7)
                .Select(i => StartDate.AddDays(i).ToString("dd-MMM"))
            .ToArray();
            ViewBag.SplineChartData = from day in Last7Days
                                      join income in IncomeSummary on day equals income.day into dayIncomeJoined
                                      from income in dayIncomeJoined.DefaultIfEmpty()
                                      join expense in IncomeSummary on day equals expense.day into expenseJoined
                                      from expense in expenseJoined.DefaultIfEmpty()
                                      select new
                                      {
                                          day=day,
                                          income = income == null ? 0 : income.income,
                                          expense = expense == null ? 0 : expense.expense,

                                      };


            //Transaçoes recentes
            ViewBag.RecentTransactions = await _context.Transacoes
                .Include(i => i.Categoria)
                .OrderByDescending(j => j.Data)
                .Take(5)
                .ToListAsync();
            return View();
        }
    }
    public class SplineChartData
    {
        public string day;
        public int income;
        public int expense;

    }
}


