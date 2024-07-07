using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RazorLoginRegister.Data;
using RazorLoginRegister.Models;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace RazorLoginRegister.Pages
{
    public class EditIncomeModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public EditIncomeModel(ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public IncomeInputModel Input { get; set; }
        public int IncomeId { get; set; }

        public IActionResult OnGet(int id)
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            if (userId == null)
            {
                return RedirectToPage("/Login");
            }

            var income = _context.Incomes.FirstOrDefault(i => i.Id == id && i.UserId == userId.Value);
            if (income == null)
            {
                return NotFound();
            }

            IncomeId = income.Id;
            Input = new IncomeInputModel
            {
                Amount = income.Amount,
                Description = income.Description,
                Date = income.Date
            };

            return Page();
        }

        public IActionResult OnPost(int id)
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

            var income = _context.Incomes.FirstOrDefault(i => i.Id == id && i.UserId == userId.Value);
            if (income == null)
            {
                return NotFound();
            }

            income.Amount = Input.Amount;
            income.Description = Input.Description;
            income.Date = Input.Date;

            _context.SaveChanges();

            return RedirectToPage("/IncomeDetails");
        }

        public IActionResult OnPostDelete(int id)
        {
            var income = _context.Incomes.Find(id);
            if (income != null)
            {
                _context.Incomes.Remove(income);
                _context.SaveChanges();
            }
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
