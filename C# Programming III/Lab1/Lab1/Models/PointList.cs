using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab1.Models
{
    public class PointList : IList<Point>
    {
        private List<Point> _Points = new List<Point>();

        public event EventHandler<PointListChangedEventArgs> Changed;

        public Point this[int index]
        {
            get
            {
                return _Points[index];
            }
            set
            {
                _Points[index] = value;
                Changed?.Invoke(this, new PointListChangedEventArgs(PointListChangedEventArgs.PointListChangedOperations.Update));
            }
        }

        public int Count => _Points.Count;

        public bool IsReadOnly => ((ICollection<Point>)_Points).IsReadOnly;

        public void Add(Point item)
        {
            _Points.Add(item);
            Changed?.Invoke(this, new PointListChangedEventArgs(PointListChangedEventArgs.PointListChangedOperations.Add));
        }

        public void Clear()
        {
            _Points.Clear();
            Changed?.Invoke(this, new PointListChangedEventArgs(PointListChangedEventArgs.PointListChangedOperations.Clear));
        }

        public bool Contains(Point item) => _Points.Contains(item);

        public void CopyTo(Point[] array, int arrayIndex) => _Points.CopyTo(array, arrayIndex);

        public int IndexOf(Point item) => _Points.IndexOf(item);

        public void Insert(int index, Point item)
        {
            _Points.Insert(index, item);
            Changed?.Invoke(this, new PointListChangedEventArgs(PointListChangedEventArgs.PointListChangedOperations.Insert));
        }

        public bool Remove(Point item)
        {
            bool result = _Points.Remove(item);
            if (result)
            {
                Changed?.Invoke(this, new PointListChangedEventArgs(PointListChangedEventArgs.PointListChangedOperations.Remove));
            }
            return result;         
        }

        public void RemoveAt(int index)
        {
            _Points.RemoveAt(index);
            Changed?.Invoke(this, new PointListChangedEventArgs(PointListChangedEventArgs.PointListChangedOperations.Remove));
        }

        public IEnumerator<Point> GetEnumerator()
        {
            return _Points.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _Points.GetEnumerator();
        }
    }
}
