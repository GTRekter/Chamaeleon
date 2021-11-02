using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace SharedLib
{
    public class DynamicArray<T> : IEnumerable<T>, IDisposable 
        where T : new()
    {
        private bool _Disposed = false;

        private T[] _Items;

        public T this[int index]
        {
            get
            {
                return _Items[index];
            }
            set
            {
                _Items[index] = value;
            }
        }

        public DynamicArray(int length)
        {
            _Items = new T[length];
            Console.WriteLine($"Creating DynamicArray from thread {Thread.CurrentThread.ManagedThreadId}");
        }

        public DynamicArray(IEnumerable<T> items)
        {
            _Items = items.ToArray();
            Console.WriteLine($"Creating DynamicArray from thread {Thread.CurrentThread.ManagedThreadId}");
        }

        public void Resize(int length)
        {
            T[] items = new T[length];
            for (int i = 0; i < items.Length; i++)
            {
                // initialize every item in the items array 
                items[i] = new T();
            }

            // Copy the contents of _Items into the new items array
            for (int i = 0; i < items.Length; i++)
            {
                if (_Items.Length > i )
                { 
                    items[i] = _Items[i];
                }
                else
                {
                    break;
                }
            }

            // replace the original array
            _Items = items;
        }

        public IEnumerator<T> GetEnumerator()
        {
            return (_Items as IEnumerable<T>).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _Items.GetEnumerator();
        }

        public void Dispose()
        {
            Console.WriteLine($"Disposing DynamicArray from thread {Thread.CurrentThread.ManagedThreadId}");
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if(_Disposed)
            {
                return;
            }
            else
            {
                if(disposing)
                {
                    _Items = null;
                }
            }
            _Disposed = true;
        }

        ~DynamicArray()
        {
            Console.WriteLine($"Finalizing DynamicArray from thread {Thread.CurrentThread.ManagedThreadId}");
        }
    }
}
