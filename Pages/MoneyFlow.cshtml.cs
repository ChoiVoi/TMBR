using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Http;
using RazorLoginRegister.Data;
using RazorLoginRegister.Models;
using System.Linq;

namespace RazorLoginRegister.Pages
{
    public class MoneyFlowModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public MoneyFlowModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public decimal TotalIncome { get; set; }
        public decimal TotalExpenditure { get; set; }

        public IActionResult OnGet()
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            if (userId == null)
            {
                return RedirectToPage("/Login");
            }

            TotalIncome = _context.Incomes
                .Where(i => i.UserId == userId.Value)
                .AsEnumerable() 
                .Sum(i => i.Amount);

            TotalExpenditure = _context.Expenditures
                .Where(e => e.UserId == userId.Value)
                .AsEnumerable()
                .Sum(e => e.Amount);

            return Page();
        }

        public IActionResult OnPostLogout()
        {
            HttpContext.Session.Remove("UserId");
            return RedirectToPage("/Login");
        }
    }
}
