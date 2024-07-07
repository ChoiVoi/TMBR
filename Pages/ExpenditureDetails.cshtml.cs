using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Http;
using RazorLoginRegister.Data;
using RazorLoginRegister.Models;
using System.Collections.Generic;
using System.Linq;

namespace RazorLoginRegister.Pages
{
    public class ExpenditureDetailsModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public ExpenditureDetailsModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public List<Expenditure> Expenditures { get; set; }
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
        public decimal TotalExpenditure { get; set; }

        public IActionResult OnGet(int currentPage = 1)
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            if (userId == null)
            {
                return RedirectToPage("/Login");
            }

            const int pageSize = 5;
            var expendituresQuery = _context.Expenditures.Where(e => e.UserId == userId.Value);
            var totalExpenditures = expendituresQuery.Count();

            TotalPages = (int)System.Math.Ceiling(totalExpenditures / (double)pageSize);
            CurrentPage = currentPage;
            TotalExpenditure = expendituresQuery.AsEnumerable().Sum(e => e.Amount);

            Expenditures = expendituresQuery
                .OrderBy(e => e.Date)
                .Skip((CurrentPage - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            return Page();
        }
    }
}
