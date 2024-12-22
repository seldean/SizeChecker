using System;
using System.IO;

namespace SizeChecker
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Prompt the user for the folder path
            Console.WriteLine("Enter the folder path to check for large files:");
            string folderPath = Console.ReadLine();

            // Ensure the folder exists
            if (!Directory.Exists(folderPath))
            {
                Console.WriteLine("The specified folder does not exist. Please check the path and try again.");
                return;
            }

            // Prompt the user for the size threshold in MB
            Console.WriteLine("Enter the size threshold in MB:");
            if (!long.TryParse(Console.ReadLine(), out long sizeThresholdMb) || sizeThresholdMb <= 0)
            {
                Console.WriteLine("Invalid size threshold. Please enter a positive number.");
                return;
            }

            // Convert the size threshold to bytes
            long sizeThreshold = sizeThresholdMb * 1024 * 1024;

            Console.WriteLine($"Files in '{folderPath}' that are {sizeThresholdMb}MB or larger:");

            try
            {
                // Get all files in the folder (including subfolders if needed)
                var files = Directory.GetFiles(folderPath, "*", SearchOption.TopDirectoryOnly);

                bool largeFileFound = false;

                foreach (var file in files)
                {
                    FileInfo fileInfo = new FileInfo(file);

                    if (fileInfo.Length >= sizeThreshold)
                    {
                        Console.WriteLine($"{fileInfo.Name} - {fileInfo.Length / (1024 * 1024)} MB");
                        largeFileFound = true;
                    }
                }

                if (!largeFileFound)
                {
                    Console.WriteLine($"No files {sizeThresholdMb}MB or larger were found in the specified folder.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred while checking the folder:");
                Console.WriteLine(ex.Message);
            }

            Console.WriteLine("Press any key to exit.");
            Console.ReadKey();
        }
    }
}
