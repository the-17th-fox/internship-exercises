using CsvHelper;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CsvEnumerable
{
    public class CsvEnumerable<T> : IEnumerable<T>, IDisposable
    {
        private bool _disposed = false;

        private CsvReader _csvReader;

        public CsvEnumerable(CsvReader csvReader) => _csvReader = csvReader;

        public void Dispose()
        {
            if (_disposed) return;

            GC.Collect();
            _disposed = true;
        }

        public IEnumerator<T> GetEnumerator()
        {
            foreach (var item in _csvReader.GetRecords<T>())
            {
                yield return item;
            }
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
