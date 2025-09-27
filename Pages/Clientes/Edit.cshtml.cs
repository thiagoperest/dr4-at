using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using dr4_at.Data;

namespace dr4_at.Pages.Clientes
{
    [Authorize]
    public class EditModel : PageModel
    {
        private readonly Dr4AtContext _context;

        public EditModel(Dr4AtContext context)
        {
            _context = context;
        }

        [BindProperty] public int Id { get; set; }

        [BindProperty] public ClienteEditViewModel Cliente { get; set; } = new();

        public string? MensagemSucesso { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            var cliente = await _context.Clientes
                .FirstOrDefaultAsync(c => c.Id == id);

            if (cliente == null)
            {
                return NotFound();
            }

            Id = cliente.Id;
            Cliente.Nome = cliente.Nome;
            Cliente.Email = cliente.Email;

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var clienteDb = await _context.Clientes.FindAsync(Id);

            if (clienteDb == null)
            {
                return NotFound();
            }

            clienteDb.Nome = Cliente.Nome;
            clienteDb.Email = Cliente.Email;

            try
            {
                await _context.SaveChangesAsync();
                MensagemSucesso = $"Cliente '{Cliente.Nome}' atualizado com sucesso!";
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ClienteExists(Id))
                {
                    return NotFound();
                }

                throw;
            }

            return Page();
        }

        private bool ClienteExists(int id)
        {
            return _context.Clientes.Any(c => c.Id == id);
        }
    }

    public class ClienteEditViewModel
    {
        [Required(ErrorMessage = "Nome é obrigatório")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "Nome deve ter entre 3 e 100 caracteres")]
        public string Nome { get; set; } = string.Empty;

        [Required(ErrorMessage = "Email é obrigatório")]
        [EmailAddress(ErrorMessage = "Email deve ter um formato válido")]
        public string Email { get; set; } = string.Empty;
    }
}