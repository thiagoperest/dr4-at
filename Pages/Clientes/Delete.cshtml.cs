using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using dr4_at.Data;
using dr4_at.Models;

namespace dr4_at.Pages.Clientes
{
    [Authorize]
    public class DeleteModel : PageModel
    {
        private readonly Dr4AtContext _context;

        public DeleteModel(Dr4AtContext context)
        {
            _context = context;
        }

        [BindProperty] public Cliente? Cliente { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            Cliente = await _context.Clientes
                .FirstOrDefaultAsync(c => c.Id == id);

            if (Cliente == null)
            {
                return NotFound();
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (Cliente?.Id == null)
            {
                return NotFound();
            }

            var clienteDb = await _context.Clientes
                .FirstOrDefaultAsync(c => c.Id == Cliente.Id);

            if (clienteDb != null)
            {
                clienteDb.DeletedAt = DateTime.Now;
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}