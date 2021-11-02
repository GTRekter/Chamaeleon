using Lab3.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab3.Models
{
    public class AngleFormatter : IFormatProvider
    {
        public object GetFormat(Type formatType)
        {
            if (typeof(ICustomFormatter).Equals(formatType))
            {
                return this;
            }
            return null;
        }

        /// <summary>
        /// This method is where the actual formatting of an Angle object will occur.  
        /// Before we look at the code to implement formatting, let’s look at the format codes we will 
        /// allow for the Angle type.  
        /// If the arg parameter is null, then throw a NullReferenceException.
        /// If the arg parameter is not an Angle object, then check if arg is an IFormattable object.  
        /// If it is then return the following result: 
        /// ((IFormattable) arg).ToString(format, formatProvider)
        /// Otherwise, just call ToString() on arg and return that value.
        /// </summary>
        /// <param name="format"></param>
        /// <param name="arg"></param>
        /// <param name="formatProvider"></param>
        /// <returns></returns>
        public string Format(string format, object arg, IFormatProvider formatProvider)
        {
            if(arg == null)
            {
                throw new NullReferenceException("arg cannot be null");
            }

            if(!(arg is Angle))
            {
                if(arg is IFormattable)
                {
                    return ((IFormattable)arg).ToString(format, formatProvider);
                }
                else
                {
                    return arg.ToString();
                }
            }
            else
            {
                /*
                * Once you know arg is an Angle, create a variable of that type and cast arg to it. 
                */
                Angle angle = (Angle)arg;
                string digits;
                string angleCode = string.Empty;
                string symbol = string.Empty;

                /*
                * If format is null, empty or starts with a ‘c’ or ‘C’ then set format to the appropriate 
                * code based on the input angle’s Units value. So, AngleUnits.Degrees would use the code “d”. 
                */
                if (string.IsNullOrEmpty(format) || format[0] == 'c' || format[0] == 'C')
                {
                    switch(angle.Units)
                    {
                        case AngleUnits.Degrees:
                            angleCode = "d";
                            break;
                        case AngleUnits.Gradians:
                            angleCode = "g";
                            break;
                        case AngleUnits.Radians:
                            angleCode = "r";
                            break;
                        case AngleUnits.Turns:
                            angleCode = "t";
                            break;
                    }
                }
                else
                {
                    angleCode = format[0].ToString();
                }

                /*
                * If format’s length is greater than 1 then attempt to convert the remaining text after the 
                * first character to an int.  
                * The Constrain() extension method can ensure the int value is kept between 0 and 9. 
                * If no digits are provided after the format code letter, then assume 2 decimal places 
                * for the output (unless it is one of the radian types which should be 5). 
                * To format the decimal portion of the number, create a format string composed of the letter
                * “f” followed by the number of places desired.
                */
                if (format.Length > 1)
                {
                    int digitsNumber = int.Parse(format.Substring(1, format.Length));
                    digitsNumber.Constrain(0, 9);

                    digits = string.Format("F{0}", digitsNumber);
                }
                else
                { 
                    if (angleCode == "r")
                    {
                        digits = "F5";
                    }
                    else
                    {
                        digits = "F2";
                    }
                }

                /*
                * You will need to convert the input Angle to the appropriate unit of measure based on
                * the format code.  Don’t forget about the ToXXXX() methods of Angle. 
                * You can use the AngleUnits extension method ToSymbol() to get the appropriate symbol 
                * suffix to append to the output string.  
                * So, to get the proper degrees symbol, use: AngleUnits.Degrees.ToSymbol()
                */
                switch (angleCode)
                {
                    case "d":
                        angle.ToDegrees();
                        symbol = AngleUnits.Degrees.ToSymbol();
                        break;
                    case "g":
                        angle.ToGradians();
                        symbol = AngleUnits.Gradians.ToSymbol();
                        break;
                    case "r":
                        angle.ToRadians();
                        symbol = AngleUnits.Radians.ToSymbol();
                        break;
                    case "t":
                        angle.ToTurns();
                        symbol = AngleUnits.Turns.ToSymbol();
                        break;
                }

                /*
                 * The “p” format code is a little tricky since it requires an additional calculation beyond
                 * converting the angle to radians.  
                 * The idea behind this format is to divide the angle value by pi, therefore showing the 
                 * value as pi-radians.  
                 * A half circle is approximately 3.14159 radians which is the same as saying 1.0 π radians. 
                 * Also note that the output for this code requires that additional “π” symbol before “rad”. 
                 */
                 if(angleCode == "p")
                 {
                    angle.Value = angle.Value / Angle.pi;
                    symbol = "π rad";
                 }

                /*
                 * Pass this format string to the converted Angle’s Value.ToString() method.  
                 * The result will contain the desired numeric value to which the proper unit suffix can 
                 * be append (see #6). 
                 */
                return string.Format("{0}{1}", angle.Value.ToString(digits), symbol);
            }

        }
    }
}
