namespace dr4_at.Models;

public class Destino
{
    public int Id { get; set; }
    public string Cidade { get; set; } = string.Empty;
    public string Pais { get; set; } = string.Empty;
    public DateTime? DeletedAt { get; set; }
    public bool IsDeleted => DeletedAt.HasValue;

    public List<PacoteDestino> PacoteDestinos { get; set; } = new List<PacoteDestino>();
}