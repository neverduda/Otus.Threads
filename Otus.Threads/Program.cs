using System.Diagnostics;

namespace Otus.Threads
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Console.WriteLine("Введите путь до каталога с файлами:");
            // var folderPath = Console.ReadLine();

            var folderPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "TestFolder");

            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            var spaces = CountSpacesInFolderAsync(folderPath);

            stopwatch.Stop();
            Console.WriteLine($"Общее количество пробелов в файлах: {spaces}");
            Console.WriteLine($"Время выполнения: {stopwatch.Elapsed.TotalMilliseconds} мс");
            Console.ReadKey();
        }

        static int CountSpacesInFolderAsync(string folderPath)
        {
            string[] filePathes = Directory.GetFiles(folderPath);
            int totalSpaces = 0;
            Parallel.ForEach(filePathes, (filePath) =>
            {
                var spaces = CountSpaces(filePath);
                Interlocked.Add(ref totalSpaces, spaces);
            });
            return totalSpaces;
        }

        static int CountSpaces(string filePath)
        {
            string text = File.ReadAllText(filePath);
            int spaces = text.Count(c => c == ' ');
            return spaces;
        }
    }
}