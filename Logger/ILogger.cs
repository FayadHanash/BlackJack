namespace Logger;

/// <summary>
/// An interface that represents a logger
/// </summary>
public interface ILogger
{
    /// <summary>
    /// Method that logs a message
    /// </summary>
    /// <param name="msg"></param>
    void LogMessage(string msg);
}
