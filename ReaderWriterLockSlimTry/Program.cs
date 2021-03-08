using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Threading;

namespace ReaderWriterLockSlimTry
{
    static class Program
    {
        static void Main(string[] args)
        {
            ReaderWriterLockSlim readerWriterLockSlim = new ReaderWriterLockSlim();
            List<int> items = new List<int>();
            Random rand = new Random();
            new Thread(Read).Start();
            new Thread(Read).Start();
            new Thread(Read).Start();
            new Thread(Write).Start("A");
            new Thread(Write).Start("B");
            Console.Read();

            void Read()
            {
                while (true)
                {
                    readerWriterLockSlim.EnterReadLock();
                    foreach (int i in items) Thread.Sleep(10);
                    readerWriterLockSlim.ExitReadLock();
                }
            }
            void Write(object threadID)
            {
                while (true)
                {
                    int newNumber = GetRandNum(50);
                    readerWriterLockSlim.EnterWriteLock();
                    items.Add(newNumber);
                    readerWriterLockSlim.ExitWriteLock();
                    Console.WriteLine("Thread " + threadID + " added " + newNumber);
                    Thread.Sleep(100);
                }
            }
            int GetRandNum(int max)
            {
                lock (rand)
                    return rand.Next(max);
            }
        }
    }
}
