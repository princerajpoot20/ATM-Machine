namespace ATM_Machine.src.Logger;

public class Logger
{
    private static string _LogFilePath = @"C:\Users\prajpoot\OneDrive - WatchGuard Technologies Inc\Project\ATM\ATM Machine\ATM Machine\src\Database\Log.csv";
    public static void LogMessage(string message)
    {
        try
        {
            using (StreamWriter sw = new StreamWriter(_LogFilePath, true))
            {
                string logEntry = $"{DateTime.Now}, {message}";
                sw.WriteLine(logEntry);
            }
        }
        catch (IOException ex)
        {
            Console.WriteLine($"An I/O error occurred: {ex.Message}");
        }
        catch (UnauthorizedAccessException ex)
        {
            // When access C Drive file without administration permission
            Console.WriteLine($"Access violation: {ex.Message}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An unexpected error occurred: {ex.Message}");
        }
    }
}
