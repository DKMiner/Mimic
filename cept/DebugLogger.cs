using System;
using System.IO;

namespace Cept
{
    public class DebugLogger
    {
        public static DebugLogger Global = new DebugLogger("global.txt");

        private StreamWriter writer;

        public DebugLogger(string fileName)
        {
            var logDirectory = Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
                "CeptAutoAccept",
                "logs");
            Directory.CreateDirectory(logDirectory);
            writer = new StreamWriter(Path.Combine(logDirectory, fileName), true);
            writer.AutoFlush = true;
            writer.WriteLine($"\n\n\n --- {DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss")} --- ");
            writer.WriteLine($"Started logging to {fileName}...");
        }

        public void WriteError(string error)
        {
            writer.WriteLine($"[ERROR {DateTime.Now.ToString("HH:mm:ss")}] {error}");
        }

        public void WriteMessage(string message)
        {
            writer.WriteLine($"[MSG {DateTime.Now.ToString("HH:mm:ss")}] {message}");
        }

        public void WriteWarning(string warning)
        {
            writer.WriteLine($"[WRN {DateTime.Now.ToString("HH:mm:ss")}] {warning}");
        }
    }
}
