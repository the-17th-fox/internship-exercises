using CsvHelper;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;

namespace CsvEnumerable
{
    class Program
    {
        private static int _idCounter = 1;
        private static readonly List<object> Roles = new()
        {
            new { Id = _idCounter++},
            new { Id = _idCounter++ }
        };
        static void Main(string[] args)
        {
            foreach (var item in Roles)
            {
                Console.WriteLine(item);
            }
            //using (var stream = new StreamReader($"{Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent}\\text.csv"))
            //using (var records = new CsvEnumerable<Person>(new CsvReader(stream, CultureInfo.InvariantCulture)))
            //{
            //    foreach (var item in records)
            //    {
            //        Console.WriteLine($"{item.Id}\t{item.Name}");
            //    }
            //}
        }
    }
}
