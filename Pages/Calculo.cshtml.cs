using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace dr4_at.Pages
{
    [Authorize]
    public class CalculoModel : PageModel
    {
        [BindProperty]
        [Required(ErrorMessage = "Quantidade de participantes é obrigatória")]
        [Range(1, int.MaxValue, ErrorMessage = "Quantidade deve ser maior que zero")]
        public int QuantidadeParticipantes { get; set; }

        [BindProperty]
        [Required(ErrorMessage = "Preço do pacote é obrigatório")]
        [Range(1, int.MaxValue, ErrorMessage = "Preço deve ser maior que zero")]
        public int PrecoPacote { get; set; }

        public decimal? ValorTotal { get; set; }

        public void OnGet()
        {
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            Func<int, int, decimal> calcularValorTotal = (participantes, preco) => participantes * preco;

            ValorTotal = calcularValorTotal(QuantidadeParticipantes, PrecoPacote);

            return Page();
        }
    }
}