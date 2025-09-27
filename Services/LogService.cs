namespace dr4_at.Services;

public class LogService
{
    private static readonly List<string> MemoryLogs = new List<string>();

    public static Action<string> LogAction { get; set; } = null!;

    static LogService()
    {
        LogAction = LogToConsole;
        LogAction += LogToFile;
        LogAction += LogToMemory;
    }

    public static void LogToConsole(string message)
    {
        var timestamp = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
        Console.WriteLine($"[CONSOLE] {timestamp}: {message}");
    }

    public static void LogToFile(string message)
    {
        try
        {
            var timestamp = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
            var logEntry = $"[FILE] {timestamp}: {message}";

            var logDir = "logs";
            if (!Directory.Exists(logDir))
            {
                Directory.CreateDirectory(logDir);
            }

            var fileName = Path.Combine(logDir, "sistema.log");
            File.AppendAllText(fileName, logEntry + Environment.NewLine);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Erro ao escrever log em arquivo: {ex.Message}");
        }
    }

    public static void LogToMemory(string message)
    {
        var timestamp = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
        var logEntry = $"[MEMORY] {timestamp}: {message}";

        lock (MemoryLogs)
        {
            MemoryLogs.Add(logEntry);

            if (MemoryLogs.Count > 50)
            {
                MemoryLogs.RemoveAt(0);
            }
        }
    }

    public static void Log(string message)
    {
        LogAction?.Invoke(message);
    }

    public static List<string> GetMemoryLogs()
    {
        lock (MemoryLogs)
        {
            return new List<string>(MemoryLogs);
        }
    }
}