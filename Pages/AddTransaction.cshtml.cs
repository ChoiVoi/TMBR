using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Http;
using RazorLoginRegister.Data;
using RazorLoginRegister.Models;
using System.ComponentModel.DataAnnotations;

namespace RazorLoginRegister.Pages
{
    public class AddTransactionModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public AddTransactionModel(ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public TransactionInputModel Input { get; set; }

        public void OnGet()
        {
        }

        public IActionResult OnPost()
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            if (userId == null)
            {
                return RedirectToPage("/Login");
            }

            if (!ModelState.IsValid)
            {
                return Page();
            }

            if (Input.Type == "Income")
            {
                var income = new Income
                {
                    Amount = Input.Amount,
                    Description = Input.Description,
                    UserId = userId.Value
                };

                _context.Incomes.Add(income);
            }
            else
            {
                var expenditure = new Expenditure
                {
                    Amount = Input.Amount,
                    Description = Input.Description,
                    UserId = userId.Value
                };

                _context.Expenditures.Add(expenditure);
            }

            _context.SaveChanges();

            return RedirectToPage("/MoneyFlow");
        }

        public class TransactionInputModel
        {
            [Required]
            public decimal Amount { get; set; }

            [Required]
            public string Description { get; set; }

            [Required]
            public string Type { get; set; }
        }
    }
}
