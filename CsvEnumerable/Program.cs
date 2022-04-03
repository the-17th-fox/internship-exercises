using CsvHelper;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;

namespace CsvEnumerable
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var stream = new StreamReader($"{Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent}\\text.csv"))
            using (var records = new CsvEnumerable<Person>(new CsvReader(stream, CultureInfo.InvariantCulture)))
            {
                foreach (var item in records)
                {
                    Console.WriteLine($"{item.Id}\t{item.Name}");
                }
            }
        }
    }
}
