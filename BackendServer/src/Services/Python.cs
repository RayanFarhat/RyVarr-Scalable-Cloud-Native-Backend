using System.Diagnostics;

namespace BackendServer.Services;

public class Python
{
    public static string Execute(string code)
    {
        using (Process pythonProcess = new Process())
        {
            // Set the Python interpreter path and script content
            pythonProcess.StartInfo.FileName = "python3";
            pythonProcess.StartInfo.RedirectStandardInput = false;
            pythonProcess.StartInfo.RedirectStandardOutput = true;  // Set up redirection for standard output
            pythonProcess.StartInfo.UseShellExecute = false;
            pythonProcess.StartInfo.CreateNoWindow = true;
            pythonProcess.StartInfo.Arguments = $"-c \"{code}\"";
            // Start the Python process
            pythonProcess.Start();

            string output = pythonProcess.StandardOutput.ReadToEnd();

            // Wait for the Python process to exit
            pythonProcess.WaitForExit();

            // Read and display the Python process output
            return output;
        }
    }
    public static string ConvertToRawString(string input)
    {
        return input.Replace("\\", "\\\\").Replace("\n", "\\n").Replace("\t", "\\t");
        // Add more replacements for other escape characters as needed
    }
}