using System.Diagnostics;

namespace Otus.Threads
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            // Console.WriteLine("Введите путь до каталога с файлами:");
            // var folderPath = Console.ReadLine();

            var folderPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "TestFolder");

            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            var spaces = await CountSpacesInFolderAsync(folderPath);

            stopwatch.Stop();
            Console.WriteLine($"Общее количество пробелов в файлах: {spaces}");
            Console.WriteLine($"Время выполнения: {stopwatch.Elapsed.TotalMilliseconds} мс");
            Console.ReadKey();
        }

        static async Task<int> CountSpacesInFolderAsync(string folderPath)
        {
            string[] filePathes = Directory.GetFiles(folderPath);
            int totalSpaces = 0;

            Task<int>[] tasks = new Task<int>[filePathes.Length];

            for (int i = 0; i < filePathes.Length; i++)
            {
                tasks[i] = CountSpacesAsync(filePathes[i]);
            }

            await Task.WhenAll(tasks);

            foreach (var task in tasks)
            {
                totalSpaces += task.Result;
            }

            return totalSpaces;
        }

        static async Task<int> CountSpacesAsync(string filePath)
        {
            string text = File.ReadAllText(filePath);
            int spaces = text.Count(c => c == ' ');
            return spaces;
        }
    }
}