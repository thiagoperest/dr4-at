namespace dr4_at.Models;

public class PacoteTuristico
{
    public int Id { get; set; }
    public string Titulo { get; set; } = string.Empty;
    public DateTime DataInicio { get; set; }
    public int CapacidadeMaxima { get; set; }
    public decimal Preco { get; set; }
    public DateTime? DeletedAt { get; set; }
    public bool IsDeleted => DeletedAt.HasValue;
    public List<PacoteDestino> PacoteDestinos { get; set; } = new List<PacoteDestino>();
    public List<Reserva> Reservas { get; set; } = new List<Reserva>();
}
