using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Http;
using RazorLoginRegister.Data;
using RazorLoginRegister.Models;
using System.ComponentModel.DataAnnotations;

namespace RazorLoginRegister.Pages
{
    public class AddIncomeModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public AddIncomeModel(ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public IncomeInputModel Input { get; set; }

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

            var income = new Income
            {
                Amount = Input.Amount,
                Description = Input.Description,
                Date = Input.Date,
                UserId = userId.Value
            };

            _context.Incomes.Add(income);
            _context.SaveChanges();

            return RedirectToPage("/IncomeDetails");
        }

        public class IncomeInputModel
        {
            [Required]
            public decimal Amount { get; set; }

            [Required]
            public string Description { get; set; }

            [Required]
            [DataType(DataType.Date)]
            public DateTime Date { get; set; }
        }
    }
}
