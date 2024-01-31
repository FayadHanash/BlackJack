namespace Logger;

/// <summary>
/// Class that represents a file logger
/// </summary>
public class FileLogger
{
    /// <summary>
    /// Method that logs a message to a file 
    /// The file is located in the bin\Debug folder
    /// </summary>
    /// <param name="msg"></param>
    public void WriteToFile(string msg)
    {
        string path = AppDomain.CurrentDomain.BaseDirectory + "log.txt";
        using (System.IO.StreamWriter file = new System.IO.StreamWriter(path, true))
        {
            file.WriteLine(msg);
        }
    }
}

