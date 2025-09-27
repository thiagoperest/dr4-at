using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using dr4_at.Services;

namespace dr4_at.Pages
{
    [Authorize]
    public class LogsModel : PageModel
    {
        [BindProperty]
        [Required(ErrorMessage = "Nome do cliente é obrigatório")]
        public string ClienteNome { get; set; } = string.Empty;

        [BindProperty]
        [Required(ErrorMessage = "Destino é obrigatório")]
        [Range(1, 3, ErrorMessage = "Selecione um destino válido")]
        public int PacoteId { get; set; }

        public List<string> LogsMemoria { get; set; } = new List<string>();
        public string MensagemSucesso { get; set; } = string.Empty;

        public void OnGet()
        {
            LogsMemoria = LogService.GetMemoryLogs();
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                LogsMemoria = LogService.GetMemoryLogs();
                return Page();
            }

            var reservaId = new Random().Next(1000, 9999);
            var destino = GetDestinoNome(PacoteId);
            var logMessage =
                $"Reserva #{reservaId} criada para cliente '{ClienteNome}' - Destino: {destino} (ID: {PacoteId})";

            LogService.Log(logMessage);

            MensagemSucesso = $"Reserva #{reservaId} criada para {destino}!";
            LogsMemoria = LogService.GetMemoryLogs();

            ClienteNome = string.Empty;
            PacoteId = 0;

            return Page();
        }

        private string GetDestinoNome(int pacoteId)
        {
            return pacoteId switch
            {
                1 => "Rio de Janeiro",
                2 => "Bahia",
                3 => "São Paulo",
                _ => "Destino Desconhecido"
            };
        }
    }
}