namespace dr4_at.Models;

public class Reserva
{
    public int Id { get; set; }
    public int ClienteId { get; set; }
    public Cliente Cliente { get; set; } = null!;
    public int PacoteTuristicoId { get; set; }
    public PacoteTuristico PacoteTuristico { get; set; } = null!;
    public DateTime DataReserva { get; set; }

    public static event Action<string>? CapacityReached;

    public static void CheckCapacity(PacoteTuristico pacote)
    {
        if (pacote.Reservas.Count > pacote.CapacidadeMaxima)
        {
            CapacityReached?.Invoke(
                $"ALERTA: Capacidade ultrapassada para o pacote '{pacote.Titulo}' ({pacote.Reservas.Count}/{pacote.CapacidadeMaxima})");
        }
    }
}