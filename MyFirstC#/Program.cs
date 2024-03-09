using System;
using System.Diagnostics;
using System.IO;

class Program
{
    static void Main()
    {
        string batFilePath = @"C:\Users\danie\IdeaProjects\LMS\out\artifacts\LMS_jar\runProgramm.bat";
        string outputDirectory = @"C:\Users\danie\Documents\messages";

        try
        {
            // Create the directory if it doesn't exist
            if (!Directory.Exists(outputDirectory))
            {
                Directory.CreateDirectory(outputDirectory);
            }

            ProcessStartInfo startInfo = new ProcessStartInfo
            {
                FileName = batFilePath,
                UseShellExecute = false,
                WorkingDirectory = @"C:\Users\danie\IdeaProjects\LMS\out\artifacts\LMS_jar\",
                RedirectStandardOutput = true,
                CreateNoWindow = true
            };

            using (Process process = new Process())
            {
                process.StartInfo = startInfo;
                process.OutputDataReceived += (sender, args) =>
                {
                    if (!string.IsNullOrEmpty(args.Data))
                    {
                        // Generate a unique filename using timestamp
                        string outputFileName = $"{DateTime.Now:yyyyMMddHHmmssfff}.txt";
                        string outputFilePath = Path.Combine(outputDirectory, outputFileName);

                        // Write the output line to the file
                        File.WriteAllText(outputFilePath, args.Data);
                    }
                };

                process.Start();
                process.BeginOutputReadLine(); // Begin asynchronous reading of the standard output

                process.WaitForExit();
            }

            Console.WriteLine("Output files written to: " + outputDirectory);
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error occurred: " + ex.Message);
        }
    }
}
