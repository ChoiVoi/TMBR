using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Http;
using RazorLoginRegister.Data;
using RazorLoginRegister.Models;
using System.Collections.Generic;
using System.Linq;

namespace RazorLoginRegister.Pages
{
    public class IncomeDetailsModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public IncomeDetailsModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public List<Income> Incomes { get; set; }
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
        public decimal TotalIncome { get; set; }

        public IActionResult OnGet(int currentPage = 1)
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            if (userId == null)
            {
                return RedirectToPage("/Login");
            }

            const int pageSize = 5;
            var incomesQuery = _context.Incomes.Where(i => i.UserId == userId.Value);
            var totalIncomes = incomesQuery.Count();

            TotalPages = (int)System.Math.Ceiling(totalIncomes / (double)pageSize);
            CurrentPage = currentPage;
            TotalIncome = incomesQuery.AsEnumerable().Sum(i => i.Amount);

            Incomes = incomesQuery
                .OrderBy(i => i.Date)
                .Skip((CurrentPage - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            return Page();
        }
    }
}
