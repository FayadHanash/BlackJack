using System;
using System.Diagnostics;
namespace Logger;

/// <summary>
/// Class that represents a console logger
/// </summary>
public class ConsoleLogger
{
    /// <summary>
    /// Method that logs a message to the console
    /// </summary>
    /// <param name="msg"></param>
    public void WriteToConsole(string msg)
    {
        //Console.WriteLine(msg);
        //Trace.WriteLine(msg);
        Debug.WriteLine(msg);
    }
}
