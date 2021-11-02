using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedLib
{
    public class PointListChangedEventArgs : EventArgs
    {
        public PointListChangedEventArgs(PointListChangedOperations operation)
        {
            Operation = operation;
        }
        public PointListChangedOperations Operation { get; set; }
        public enum PointListChangedOperations
        {
            Unknown,
            Add,
            Remove,
            Insert,
            Clear,
            Update
        }
    }
}
