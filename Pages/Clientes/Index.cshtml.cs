using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using dr4_at.Data;
using dr4_at.Models;

namespace dr4_at.Pages.Clientes
{
    [Authorize]
    public class IndexModel : PageModel
    {
        private readonly Dr4AtContext _context;

        public IndexModel(Dr4AtContext context)
        {
            _context = context;
        }

        public List<Cliente> Clientes { get; set; } = new();

        public async Task OnGetAsync()
        {
            Clientes = await _context.Clientes
                .OrderBy(c => c.Nome)
                .ToListAsync();
        }
    }
}