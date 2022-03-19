using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoggingProxy
{
    public class LogginProxy <T> : DynamicObject
    {
        public T CreateInstance(T obj)
        {
            throw new NotImplementedException();
        }

    }
}
