using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedLib
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public class SpecialClassAttribute : Attribute
    {
        public SpecialClassAttribute()
        {
        }

        public SpecialClassAttribute(int id)
        {
            ID = id;
        }
        public int ID { get; set; } = 0;
    }
}
