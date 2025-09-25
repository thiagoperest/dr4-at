namespace dr4_at.Models;

public class PacoteDestino
{
    public int PacoteTuristicoId { get; set; }
    public PacoteTuristico PacoteTuristico { get; set; } = null!;

    public int DestinoId { get; set; }
    public Destino Destino { get; set; } = null!;

    public DateTime DataInclusao { get; set; }
    public int OrdemVisita { get; set; }
}