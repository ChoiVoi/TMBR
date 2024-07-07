using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Http;
using RazorLoginRegister.Data;
using RazorLoginRegister.Models;
using System.ComponentModel.DataAnnotations;

namespace RazorLoginRegister.Pages
{
    public class AddExpenditureModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public AddExpenditureModel(ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public ExpenditureInputModel Input { get; set; }

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

            var expenditure = new Expenditure
            {
                Amount = Input.Amount,
                Description = Input.Description,
                Date = Input.Date,
                UserId = userId.Value
            };

            _context.Expenditures.Add(expenditure);
            _context.SaveChanges();

            return RedirectToPage("/ExpenditureDetails");
        }

        public class ExpenditureInputModel
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
