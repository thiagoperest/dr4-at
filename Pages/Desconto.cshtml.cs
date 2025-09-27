using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace dr4_at.Pages
{
    public delegate decimal CalculateDelegate(decimal preco);

    [Authorize]
    public class DescontoModel : PageModel
    {
        [BindProperty]
        [Required(ErrorMessage = "O preço é obrigatório")]
        [Range(0.01, double.MaxValue, ErrorMessage = "O preço deve ser maior que zero")]
        public decimal Preco { get; set; }

        public decimal? PrecoComDesconto { get; set; }

        public void OnGet()
        {
            // Inicialização da página
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            // 10% de desconto
            CalculateDelegate calcularDesconto = (preco) => preco * 0.9m;

            PrecoComDesconto = calcularDesconto(Preco);

            return Page();
        }
    }
}