using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;


Random _random = new Random();
int _counter = 0;

object _locker = new();
object _threadCreationLocker = new();
AutoResetEvent _realeseThreadEvent = new(false);

List<Thread> _threads = new();

for (int i = 1; i < 11; i++)
{
    lock (_threadCreationLocker)
    {
        _threads.Add(new Thread(DoWorkFirst));
        _threads.Last().Name = $"Thread_{i}";
        _threads.Last().Start(i);
    }
}

while (_threads.Where(t => t.IsAlive).FirstOrDefault() != null)
{
    Console.Write("\tPress enter to realese the another thread.");
    Console.ReadLine();
    _realeseThreadEvent.Set();
    Thread.Sleep(1000);
}


void DoWorkFirst(object msg)
{
    _realeseThreadEvent.WaitOne();
    bool locked = false;
    try
    {
        Monitor.Enter(_locker, ref locked);

        ChangeColor((int)msg);

        Thread.Sleep(_random.Next(1, 4) * 300);
        for (var i = 0; i < 3; i++)
        {
            Console.WriteLine($"Message: {msg}; Counter: {_counter}; Inner iteration number: {i}");
        }
        _counter++;
    }
    finally
    {
        Console.ResetColor();
        Thread.CurrentThread.Abort();
        if (locked) Monitor.Exit(_locker);
    }
}

void ChangeColor(int index)
{
    Console.ForegroundColor = (ConsoleColor)Enum.GetValues(typeof(ConsoleColor)).GetValue(index);
}