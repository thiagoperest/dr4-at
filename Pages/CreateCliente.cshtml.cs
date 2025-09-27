using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace dr4_at.Pages
{
    [Authorize]
    public class CreateClienteModel : PageModel
    {
        [BindProperty] public ClienteViewModel Cliente { get; set; } = new();

        public string? MensagemSucesso { get; set; }

        public void OnGet()
        {
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var clienteId = new Random().Next(1000, 9999);
            MensagemSucesso = $"Cliente #{clienteId} '{Cliente.Nome}' criado com sucesso!";

            Cliente = new ClienteViewModel();

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