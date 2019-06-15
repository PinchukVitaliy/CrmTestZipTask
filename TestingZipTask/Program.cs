using System;
using System.IO;
using System.IO.Compression;

namespace TestingZipTask
{
    class Program
    {
        static void Main(string[] args)
        {
            //путь к архиву
            string zipPath = @"C:\Users\Zigo\Desktop\arhive\testZip.zip";
            //временная папка
            string extractPath = @"C:\Users\Zigo\Desktop\arhive\test";
            A(zipPath, extractPath);
        }

        private static void A(string zipPath, string extractPath)
        {
            Directory.CreateDirectory(extractPath);
            using (ZipArchive archive = ZipFile.OpenRead(zipPath))
            {
                foreach (ZipArchiveEntry entry in archive.Entries)
                {
                    if (entry.FullName.EndsWith(".csv", StringComparison.OrdinalIgnoreCase))
                    {
                        //полный путь куда будут помещаться файлы
                        string destinationPath = Path.GetFullPath(Path.Combine(extractPath, entry.FullName));
                        //вытаскиваем нужные нам файлы и сохраняем у временную папку
                        entry.ExtractToFile(Path.Combine(extractPath, entry.Name));
                        //что-то делаем с нашим файлом 
                        B(destinationPath);
                    }
                }
            }
            //Console.ReadKey();
            //удаляем временную папку с файламы
            Directory.Delete(extractPath, true);
        }
        private static void B(string filePath)
        {
            FileInfo fileInf = new FileInfo(filePath);
            if (fileInf.Exists)
            {
                //для теста просто выводим информацию о файле
                Console.WriteLine("Имя файла: {0}", fileInf.Name);
                Console.WriteLine("Время создания: {0}", fileInf.CreationTime);
                Console.WriteLine("Размер: {0}", fileInf.Length);
            }
        }
    }
}
