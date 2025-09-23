using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using dr4_at.Models;

namespace dr4_at.Pages
{
    [Authorize]
    public class EventoModel : PageModel
    {
        private static readonly List<PacoteTuristico> Pacotes = new()
        {
            new PacoteTuristico
                { Id = 1, Titulo = "Rio de Janeiro", CapacidadeMaxima = 3, Reservas = new List<Reserva>() },
            new PacoteTuristico { Id = 2, Titulo = "Bahia", CapacidadeMaxima = 2, Reservas = new List<Reserva>() },
            new PacoteTuristico { Id = 3, Titulo = "São Paulo", CapacidadeMaxima = 5, Reservas = new List<Reserva>() }
        };

        private static readonly List<string> AlertasConsole = new();
        private static bool _eventoConfigurado = false;

        [BindProperty]
        [Required(ErrorMessage = "Nome do cliente é obrigatório")]
        public string NomeCliente { get; set; } = string.Empty;

        [BindProperty]
        [Required(ErrorMessage = "Pacote é obrigatório")]
        [Range(1, 3, ErrorMessage = "Selecione um pacote válido")]
        public int PacoteId { get; set; }

        public List<string> AlertasCapacidade { get; set; } = new();
        public string MensagemSucesso { get; set; } = string.Empty;
        public List<string> ReservasAtivas { get; set; } = new();

        public void OnGet()
        {
            ConfigurarEvento();
            AlertasCapacidade = new List<string>(AlertasConsole);
            CarregarReservasAtivas();
        }

        public IActionResult OnPost()
        {
            ConfigurarEvento();

            if (!ModelState.IsValid)
            {
                AlertasCapacidade = new List<string>(AlertasConsole);
                CarregarReservasAtivas();
                return Page();
            }

            var pacote = Pacotes.FirstOrDefault(p => p.Id == PacoteId);
            if (pacote != null)
            {
                var novaReserva = new Reserva
                {
                    Id = new Random().Next(1000, 9999),
                    ClienteId = new Random().Next(100, 999),
                    PacoteTuristicoId = pacote.Id,
                    DataReserva = DateTime.Now
                };

                pacote.Reservas.Add(novaReserva);

                Reserva.CheckCapacity(pacote);

                MensagemSucesso = $"Reserva #{novaReserva.Id} criada para {NomeCliente} no pacote {pacote.Titulo}";
            }

            AlertasCapacidade = new List<string>(AlertasConsole);
            CarregarReservasAtivas();
            NomeCliente = string.Empty;
            PacoteId = 0;

            return Page();
        }

        private void ConfigurarEvento()
        {
            if (!_eventoConfigurado)
            {
                Reserva.CapacityReached += LogToConsole;
                _eventoConfigurado = true;
            }
        }

        private void LogToConsole(string message)
        {
            Console.WriteLine(message);
            AlertasConsole.Add(message);
        }

        private void CarregarReservasAtivas()
        {
            ReservasAtivas = new List<string>();
            foreach (var pacote in Pacotes)
            {
                if (pacote.Reservas.Any())
                {
                    ReservasAtivas.Add($"{pacote.Titulo}: {pacote.Reservas.Count}/{pacote.CapacidadeMaxima} reservas");
                }
            }
        }
    }
}