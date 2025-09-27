using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace dr4_at.Pages
{
    [Authorize]
    public class CreatePacoteTuristicoModel : PageModel
    {
        [BindProperty]
        [Required(ErrorMessage = "Nome é obrigatório")]
        [MinLength(3, ErrorMessage = "Nome deve ter pelo menos 3 caracteres")]
        public string Nome { get; set; } = string.Empty;

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

            var pacoteId = new Random().Next(1000, 9999);
            MensagemSucesso = $"Pacote Turístico #{pacoteId} '{Nome}' criado com sucesso!";

            Nome = string.Empty;

            return Page();
        }
    }
}