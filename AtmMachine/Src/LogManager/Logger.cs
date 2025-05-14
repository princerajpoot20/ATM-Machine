namespace AtmMachine.LogManager;

static class Logger
{
    #region PrivateDataMember
    private const string LogFilePath = @"..\..\..\Src\Database\Log.csv";
    #endregion

    #region InternalMethod
    internal static void LogMessage(string message)
    {
        try
        {
            using var streamWriter = new StreamWriter(LogFilePath, true);
            var logEntry = $"{DateTime.Now}, {message}";
            streamWriter.WriteLine(logEntry);
        }
        catch (IOException exception)
        {
            Console.WriteLine($"An I/O error occurred: {exception.Message}");
        }
        catch (UnauthorizedAccessException exception)
        {
            Console.WriteLine($"Access violation: {exception.Message}");
        }
        catch (Exception exception)
        {
            Console.WriteLine($"An unexpected error occurred: {exception.Message}");
        }
    }
    #endregion
}