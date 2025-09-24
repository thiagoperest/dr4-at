using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace dr4_at.Pages
{
    [Authorize]
    public class CreateReservaModel : PageModel
    {
        [BindProperty]
        [Required(ErrorMessage = "Nome do cliente é obrigatório")]
        [MinLength(3, ErrorMessage = "Nome deve ter pelo menos 3 caracteres")]
        public string NomeCliente { get; set; } = string.Empty;

        [BindProperty]
        [Required(ErrorMessage = "ID do pacote é obrigatório")]
        [Range(1, int.MaxValue, ErrorMessage = "ID do pacote deve ser maior que zero")]
        public int PacoteId { get; set; }

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

            var reservaId = new Random().Next(1000, 9999);
            MensagemSucesso = $"Reserva #{reservaId} criada com sucesso para {NomeCliente}!";

            NomeCliente = string.Empty;
            PacoteId = 0;

            return Page();
        }
    }
}