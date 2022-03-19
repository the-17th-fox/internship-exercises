using System;
using System.IO;
using System.Reflection;
using System.Text;

namespace Streams
{
    class Program
    {
        static void WriteText(FileStream fs, string str)
        {
            byte[] bytes = new UTF8Encoding(true).GetBytes(str);
            fs.Write(bytes, 0, bytes.Length);
        }

        static void ReadText(FileStream fs)
        {
            byte[] bytes = new byte[fs.Length];
            var enc = new UTF8Encoding(true);
            while (fs.Read(bytes, 0, bytes.Length) > 0)
            {
                Console.WriteLine($"\t{enc.GetString(bytes)}");
            }
        }

        static void WriteText(MemoryStream ms, string str)
        {
            byte[] bytes = new UTF8Encoding(true).GetBytes(str);
            ms.Write(bytes, 0, bytes.Length);
        }

        static void ReadText(MemoryStream ms)
        {
            byte[] bytes = new byte[ms.Length];
            var enc = new UTF8Encoding(true);
            while(ms.Read(bytes, 0, bytes.Length) > 0)
            {
                Console.WriteLine($"\t{enc.GetString(bytes)}");
            }
        }

        static void Main(string[] args)
        {
            var rootPath = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent;

            var filesPath = Directory.CreateDirectory($@"{rootPath}\files").FullName;
            var fsFilePath = $@"{filesPath}\TextFS.txt";
            var msFilePath = $@"{filesPath}\TextMS.txt";


            if (File.Exists(fsFilePath))
                File.Delete(fsFilePath);

            using (FileStream fs = File.Create(fsFilePath))
            {
                Console.WriteLine($">>> Writing to a file using the FileStream. \n\tPath: '{fsFilePath}'");
                WriteText(fs, "FS: Hello world!");
                WriteText(fs, "\n\tFS: Have a good day!");
                Console.WriteLine($"\tThe text have been written to the file.");
            }

            using (FileStream fs = File.OpenRead(fsFilePath))
            {
                Console.WriteLine($"\n>>> Reading a file using the FileStream. \n\tPath: '{fsFilePath}'\n");
                ReadText(fs);
            }

            if (File.Exists(msFilePath))
                File.Delete(msFilePath);

            using (MemoryStream ms = new(50))
            using (FileStream fs = File.Create(msFilePath))
            {
                Console.WriteLine($"\n\n\n>>> Writing to a file using the FileStream. \n\tPath: '{msFilePath}'");

                WriteText(ms, "MS: Hello world!");
                WriteText(ms, "\n\tMS: Have a good day!");
                

                Console.WriteLine($"\tThe text have been written to the file.");

                ms.Seek(0, SeekOrigin.Begin);
                ms.CopyTo(fs);

                Console.WriteLine($"\n>>> Reading a file using the FileStream. \n\tPath: '{msFilePath}'\n");
                ReadText(ms);                
            }
        }
    }
}
