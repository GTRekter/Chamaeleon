using Lab3.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab3.Extensions
{
    public static class ExtensionMethods
    {
        /// <summary>
        /// This method determines if one decimal value is approximately equal to another.  
        /// Normally, we do not have to worry about round-off issues when using decimal values.  
        /// However, since the value π is irrational and has an infinite number of decimal places,
        /// only an approximate decimal value can be used to represent it.  
        /// Therefore, any time we convert to/from radians, a slight error may be introduced. 
        /// This method helps to work around that issue by allowing two values to be considered equal 
        /// even if they are very slightly different.  
        /// Since the numbers we are working with are generally between 0 and 400 (gradians being the largest value),
        /// the margin of error, indicated by the precision parameter, does not have to be too small.  
        /// Instead of using a hard-coded constant, however, it is an optional parameter allowing the 
        /// caller to supply their own value. 
        /// To determine if the two provided numbers are approximately equal, subtract one from the other, 
        /// calculate the absolute value of the difference (strip any negative sign) and determine if
        /// that value is less than precision.  
        /// If the difference is less than precision, then the numbers are approximately equal to each other. 
        /// Since this is an extension method of decimal, it can be called in this manner: 
        /// decimal d1 = 123.456789012345M; 
        /// decimal d2 = 123.456789012346M; 
        /// bool equal = d1.ApproximatelyEquals(d2);
        /// </summary>
        /// <param name="v1"></param>
        /// <param name="v2"></param>
        /// <param name="precision"></param>
        /// <returns></returns>
        public static bool ApproximatelyEquals(this decimal v1, decimal v2, decimal precision = 0.0000000001M)
        {
            decimal difference = Math.Abs(v1 - v2);
            if (difference < precision)
            {
                return true;
            }
            return false;            
        }

        /// <summary>
        /// The Constrain() method ensures that the parameter value is between min and max.  
        /// If value is less than min, then value is set to min.  
        /// If value is greater than max, then value is set to max.  
        /// Return value. 
        /// Since this is an extension method, it is called thusly:
        /// int i = 50; 
        /// i = i.Constrain(1, 100); 
        /// </summary>
        /// <param name="value"></param>
        /// <param name="min"></param>
        /// <param name="max"></param>
        /// <returns></returns>
        public static int Constrain(this int value, int min, int max)
        {
            if(value < min)
            {
                value = min;
            }
            else if(value > max)
            {
                value = max;
            }

            return value;
        }

        /// <summary>
        /// Extension methods can be attached to enum types as well as structs or classes.  
        /// This method accepts a value of type AngleUnits and returns the appropriate string symbol 
        /// that represents that unit type: 
        /// • Degrees: °  
        /// • Gradians: g 
        /// • Radians: rad 
        /// • Turns: tr
        /// Here’s an example of this method in action: 
        /// string unit = AngleUnits.Degrees.ToSymbol();
        /// </summary>
        /// <param name="units"></param>
        /// <returns></returns>
        public static string ToSymbol(this AngleUnits units)
        {
            switch(units)
            {
                case AngleUnits.Degrees:
                    return "°";
                case AngleUnits.Gradians:
                    return "g";
                case AngleUnits.Radians:
                    return "rad";
                case AngleUnits.Turns:
                    return "tr";
                default:
                    return string.Empty;
            }
        }
    }
}
