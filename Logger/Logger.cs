namespace Logger;


/// <summary>
/// A class that represents a logger
/// </summary>
public class Logger : ILogger
{
    /// <summary>
    /// A field that represents a string delegate that logs a message
    /// </summary>
    private Action<string> WriteMessage { get; set; }

    /// <summary>
    /// Constructor that creates a logger 
    /// Creates a console logger and a file logger
    /// </summary>
    public Logger()
    {
        ConsoleLogger consoleLogger = new();
        WriteMessage += consoleLogger.WriteToConsole;
        FileLogger fileLogger = new();
        WriteMessage += fileLogger.WriteToFile;
    }

    /// <summary>
    /// Method that logs a message
    /// </summary>
    /// <param name="msg"></param>
    public void LogMessage(string msg)
    {
        WriteMessage(msg);
    }
}
