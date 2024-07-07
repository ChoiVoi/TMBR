using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RazorLoginRegister.Data;
using RazorLoginRegister.Models;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace RazorLoginRegister.Pages
{
    public class EditExpenditureModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public EditExpenditureModel(ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public ExpenditureInputModel Input { get; set; }
        public int ExpenditureId { get; set; }

        public IActionResult OnGet(int id)
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            if (userId == null)
            {
                return RedirectToPage("/Login");
            }

            var expenditure = _context.Expenditures.FirstOrDefault(e => e.Id == id && e.UserId == userId.Value);
            if (expenditure == null)
            {
                return NotFound();
            }

            ExpenditureId = expenditure.Id;
            Input = new ExpenditureInputModel
            {
                Amount = expenditure.Amount,
                Description = expenditure.Description,
                Date = expenditure.Date
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

            var expenditure = _context.Expenditures.FirstOrDefault(e => e.Id == id && e.UserId == userId.Value);
            if (expenditure == null)
            {
                return NotFound();
            }

            expenditure.Amount = Input.Amount;
            expenditure.Description = Input.Description;
            expenditure.Date = Input.Date;

            _context.SaveChanges();

            return RedirectToPage("/ExpenditureDetails");
        }

        public IActionResult OnPostDelete(int id)
        {
            var expenditure = _context.Expenditures.Find(id);
            if (expenditure != null)
            {
                _context.Expenditures.Remove(expenditure);
                _context.SaveChanges();
            }
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
