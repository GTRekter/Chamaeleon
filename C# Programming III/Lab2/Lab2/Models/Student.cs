using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Lab2.Models
{
    public class Student : IDisposable
    {
        private bool _Disposed = false;
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DynamicArray<int> Scores { get; set; }

        public Student(string lastName, string firstName, int numScores)
        {
            LastName = lastName;
            FirstName = firstName;
            Scores = new DynamicArray<int>(numScores);
        }

        public void Dispose()
        {
            Console.WriteLine($"Disposing Student from thread {Thread.CurrentThread.ManagedThreadId}");
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (_Disposed)
            {
                return;
            }
            else
            {
                if (disposing)
                {
                    Scores?.Dispose();
                    Scores = null;
                }
            }
            _Disposed = true;
        }

        ~Student()
        {
            Console.WriteLine($"Finalizing Student from thread {Thread.CurrentThread.ManagedThreadId}");
        }

        public override string ToString()
        {
            return string.Format("{0, -10}{1,-10}{2,-10}{3,-10}", LastName, FirstName, Scores.Count(), Scores.Average());
        }

    }
}
