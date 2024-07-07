using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RazorLoginRegister.Data;
using RazorLoginRegister.Models;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace RazorLoginRegister.Pages
{
    public class RegisterModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public RegisterModel(ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public RegisterInputModel Input { get; set; }

        public void OnGet()
        {
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var existingUser = _context.Users.SingleOrDefault(u => u.Username == Input.Username);
            if (existingUser != null)
            {
                ModelState.AddModelError(string.Empty, "Username already exists.");
                return Page();
            }

            var user = new User
            {
                Username = Input.Username,
                Password = Input.Password, 
                Email = Input.Email
            };

            _context.Users.Add(user);
            _context.SaveChanges();

            return RedirectToPage("/Login");
        }

        public class RegisterInputModel
        {
            [Required]
            [StringLength(100, MinimumLength = 3)]
            public string Username { get; set; }

            [Required]
            [EmailAddress]
            public string Email { get; set; }

            [Required]
            [DataType(DataType.Password)]
            public string Password { get; set; }

            [Required]
            [DataType(DataType.Password)]
            [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
            public string ConfirmPassword { get; set; }
        }
    }
}
