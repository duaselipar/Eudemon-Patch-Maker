using System;
using System.Diagnostics;
using System.IO;
using System.Threading;

namespace FinalPatchMaker
{
    class Program
    {
        static void Main()
        {
            Console.Title = "Patch Maker Tool";

            // Display ASCII art header
            Console.WriteLine(@"
  _____               _____      _ _                  
 |  __ \             / ____|    | (_)                 
 | |  | |_   _  __ _| (___   ___| |_ _ __   __ _ _ __ 
 | |  | | | | |/ _` |\___ \ / _ \ | | '_ \ / _` | '__|
 | |__| | |_| | (_| |____) |  __/ | | |_) | (_| | |   
 |_____/ \__,_|\__,_|_____/ \___|_|_| .__/ \__,_|_|   
                                    | |               
                                    |_|");

            try
            {
                Console.WriteLine("\n=== Creating Patch ===");

                // Required folder structure
                string inputDir = "input";
                string outputDir = "output";
                string versionFile = Path.Combine(inputDir, "version.dat");

                // Basic validation
                if (!Directory.Exists(inputDir)) throw new Exception("Missing 'input' folder");
                if (!File.Exists(versionFile)) throw new Exception("Missing 'version.dat' file");

                string version = File.ReadAllText(versionFile).Trim();
                if (string.IsNullOrEmpty(version)) throw new Exception("Version cannot be empty");

                Directory.CreateDirectory(outputDir);

                // SFX creation
                string rarExe = FindWinRar();
                if (rarExe == null) throw new Exception("WinRAR (rar.exe) not found");

                string outputFile = Path.Combine(outputDir, $"{version}.exe");
                string tempConfig = Path.GetTempFileName();

                // ONLY these 3 configuration lines as requested
                File.WriteAllText(tempConfig, "Setup=AutoPatch.exe\nSilent=2\nOverwrite=1");

                Console.WriteLine($"\nCreating {version}.exe...");

                Process.Start(new ProcessStartInfo
                {
                    FileName = rarExe,
                    Arguments = $"a -ep1 -sfx -z\"{tempConfig}\" \"{outputFile}\" \"{Path.Combine(inputDir, "*")}\"",
                    UseShellExecute = false,
                    CreateNoWindow = true
                })?.WaitForExit();

                File.Delete(tempConfig);
                Console.WriteLine($"\nSuccessfully created: {outputFile}");

                // Countdown before closing
                Console.WriteLine("\nClosing in 5 seconds...");
                for (int i = 5; i > 0; i--)
                {
                    Console.Write($"\r{i} ");
                    Thread.Sleep(1000);
                }
                return; // Exit without showing "Press any key"
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\nERROR: {ex.Message}");
                Console.WriteLine("\nPress any key to exit...");
                Console.ReadKey();
            }
        }

        static string FindWinRar()
        {
            string[] paths = {
                @"C:\Program Files\WinRAR\rar.exe",
                @"C:\Program Files (x86)\WinRAR\rar.exe",
                "rar.exe"
            };
            foreach (var path in paths) if (File.Exists(path)) return path;
            return null;
        }
    }
}