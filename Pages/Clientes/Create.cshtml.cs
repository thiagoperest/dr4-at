using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using dr4_at.Data;
using dr4_at.Models;

namespace dr4_at.Pages.Clientes
{
    [Authorize]
    public class CreateModel : PageModel
    {
        private readonly Dr4AtContext _context;

        public CreateModel(Dr4AtContext context)
        {
            _context = context;
        }

        [BindProperty] public ClienteViewModel Cliente { get; set; } = new();

        public string? MensagemSucesso { get; set; }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var cliente = new Cliente
            {
                Nome = Cliente.Nome,
                Email = Cliente.Email
            };

            _context.Clientes.Add(cliente);
            await _context.SaveChangesAsync();

            MensagemSucesso = $"Cliente '{cliente.Nome}' cadastrado com sucesso!";
            Cliente = new ClienteViewModel();
            ModelState.Clear();

            return Page();
        }
    }

    public class ClienteViewModel
    {
        [Required(ErrorMessage = "Nome é obrigatório")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "Nome deve ter entre 3 e 100 caracteres")]
        public string Nome { get; set; } = string.Empty;

        [Required(ErrorMessage = "Email é obrigatório")]
        [EmailAddress(ErrorMessage = "Email deve ter um formato válido")]
        [StringLength(150, ErrorMessage = "Email deve ter no máximo 150 caracteres")]
        public string Email { get; set; } = string.Empty;
    }
}