using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using dr4_at.Models;

namespace dr4_at.Pages
{
    [Authorize]
    public class ClienteDetailsModel : PageModel
    {
        private static readonly List<Cliente> Clientes = new()
        {
            new Cliente { Id = 123, Nome = "João Silva", Email = "joao@email.com", DeletedAt = null },
            new Cliente { Id = 456, Nome = "Maria Santos", Email = "maria@email.com", DeletedAt = null },
            new Cliente
            {
                Id = 789, Nome = "Pedro Costa", Email = "pedro@email.com", DeletedAt = DateTime.Now.AddDays(-30)
            }
        };

        public Cliente? Cliente { get; set; }

        public IActionResult OnGet(int id)
        {
            Cliente = Clientes.FirstOrDefault(c => c.Id == id);

            return Page();
        }
    }
}