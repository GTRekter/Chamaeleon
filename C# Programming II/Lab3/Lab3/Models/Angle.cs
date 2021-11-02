using System;
using Lab3.Extensions;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab3.Models
{
    public class Angle
    {     
        private static decimal[,] _ConversionFactors = { { 1M, 9M / 10M, 180M / pi, 360M }, { 10M / 9M, 1M, 200M / pi, 400M }, { pi / 180M, pi / 200M, 1M, twoPi }, { 1M / 360M, 1M / 400M, 1M / twoPi, 1M } };

        private decimal _Value = 0M;

        private AngleUnits _Units = AngleUnits.Degrees;

        public const decimal pi = 3.1415926535897932384626434M;

        public const decimal twoPi = 2M * pi;

        public decimal Value
        {
            get
            {
                return _Value;
            }
            set
            {
                _Value = Normalize(value, Units);
            }
        }

        public AngleUnits Units
        {
            get
            {
                return _Units;
            }
            set
            {
                _Value = ConvertAngleValue(Value, Units, value); 
                _Units = value;
            }
        }

        #region Construnctors

        /// <summary>
        ///  Creates an Angle object with a value of 0, measured in degrees
        /// </summary>
        public Angle()
            : this(0)
        {
        }

        /// <summary>
        /// use constructor chaining to call the third constructor with the contents of other.
        /// </summary>
        /// <param name="other"></param>
        public Angle(Angle other)
            : this(other.Value)
        {
        }

        public Angle(decimal value, AngleUnits units = AngleUnits.Degrees)
        {
            Units = units;
            Value = value;        
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// The Normalize() method ensures that the provided value is within the legal range of angle 
        /// values for the given unit of measure.  
        /// Therefore, if the units parameter indicates AngleUnits.Degrees, the value parameter must be 
        /// greater than or equal to 0 and less than 360.  
        /// If the value is less than 0 then 360 must be added to value repeatedly until it is non-negative.  
        /// Likewise, if the value is greater than or equal to 360, then 360 must be repeatedly subtracted 
        /// from value until it is within the specified range.  
        /// The same technique is used for the other three angle types, just with a different maximum value: 
        /// • Degrees: 360 
        /// • Gradians: 400 
        /// • Radians: 2π - This is why the twoPi constant is provided 
        /// • Turns: 1. 
        /// Finally, return the normalized value. 
        /// </summary>
        /// <param name="value"></param>
        /// <param name="units"></param>
        /// <returns></returns>
        private static decimal Normalize(decimal value, AngleUnits units)
        {
            switch(units)
            {
                case AngleUnits.Degrees:
                    NormalizeAngle(ref value, 0, 360);
                    break;
                case AngleUnits.Gradians:
                    NormalizeAngle(ref value, 0, 400);
                    break;
                case AngleUnits.Radians:
                    NormalizeAngle(ref value, 0, twoPi);
                    break;
                case AngleUnits.Turns:
                    NormalizeAngle(ref value, 0, 1);
                    break;
            }

            return value;
        }

        private static void NormalizeAngle(ref decimal value, decimal min, decimal max)
        {
            while(value < min || value > max)
            {
                if(value < 0)
                {
                    value += max;
                }
                else if(value >= max)
                {
                    value -= max;
                }
            }         
        }

        /// <summary>
        /// This method converts the given angle value from one unit type to another.  
        /// Since the _ConversionFactors array uses AngleUnits enum values for its index values, 
        /// it is a trivial matter to get the correct conversion factor from that array: 
        /// decimal factor = _ConversionFactors[(int)toUnits, (int)fromUnits]; 
        /// Multiply this factor by the angle parameter to achieve the conversion calculation.  
        /// It would also be a wise decision to pass this result and toUnits to Normalize() to ensure 
        /// that the resulting angle is valid prior to returning the result. 
        /// </summary>
        /// <param name="angle"></param>
        /// <param name="fromUnits"></param>
        /// <param name="toUnits"></param>
        /// <returns></returns>
        private static decimal ConvertAngleValue(decimal angle, AngleUnits fromUnits, AngleUnits toUnits)
        {
            decimal factor = _ConversionFactors[(int)toUnits, (int)fromUnits];
            return Normalize(factor * angle, toUnits);
        }

        #endregion

        #region Public Methods

        public Angle ToDegrees()
        {
            return new Angle(ConvertAngleValue(Value, Units, AngleUnits.Degrees), AngleUnits.Degrees);
        }

        public Angle ToGradians()
        {
            return new Angle(ConvertAngleValue(Value, Units, AngleUnits.Gradians), AngleUnits.Gradians);
        }

        public Angle ToRadians()
        {
            return new Angle(ConvertAngleValue(Value, Units, AngleUnits.Radians), AngleUnits.Radians);
        }

        public Angle ToTurns()
        {
            return new Angle(ConvertAngleValue(Value, Units, AngleUnits.Turns), AngleUnits.Turns);
        }

        public Angle ConvertAngle(AngleUnits targetUnits)
        {
            switch(targetUnits)
            {
                case AngleUnits.Degrees:
                    return ToDegrees();
                case AngleUnits.Gradians:
                    return ToGradians();
                case AngleUnits.Radians:
                    return ToRadians();
                case AngleUnits.Turns:
                    return ToTurns();
                default:
                    return new Angle();
            }
        }

        #endregion

        #region Mathematical Operators

        /// <summary>
        /// This method adds two Angle objects together, returning the resulting value as a new Angle object.  
        /// Since the two angles may use different units of measure we are going to assume that the resulting 
        /// angle will always have the same unit of measure as the first angle(which corresponds to the left 
        /// operand, such as: a1 + a2).  
        /// Therefore, first calculate the value of a2 using the same AngleUnits as indicated in a1.I recommend 
        /// using ConvertAngleValue() using a2.Value, a2.Units and a1.Units as the input parameters.Now, 
        /// the sum of the angles can be calculated using a1.Value and the result of the above operation.
        /// The return value for this operator is a new Angle object that is constructed with the resulting 
        /// sum and a1’s Units unit of measure.
        /// Don’t worry about calling Normalize() since the constructor will take care of that for us when 
        /// it assigns to Value.
        /// </summary>
        /// <param name="a1"></param>
        /// <param name="a2"></param>
        /// <returns></returns>
        public static Angle operator + (Angle a1, Angle a2)
        {
            decimal angleToAdd = ConvertAngleValue(a2.Value, a2.Units, a1.Units);
            return new Angle(a1.Value + angleToAdd, a1.Units);
        }

        /// <summary>
        /// Use similar logic to the + operator to implement this operator
        /// </summary>
        /// <param name="a1"></param>
        /// <param name="a2"></param>
        /// <returns></returns>
        public static Angle operator - (Angle a1, Angle a2)
        {
            decimal angleToAdd = ConvertAngleValue(a2.Value, a2.Units, a1.Units);
            return new Angle(a1.Value - angleToAdd, a1.Units);
        }

        /// <summary>
        /// In addition to adding an Angle object to another Angle object, it may be desirable to 
        /// add a scalar value to an Angle object.  
        /// That means you may have the following code: 
        /// Angle angle = new Angle(45, AngleUnits.Degrees); 
        /// angle = angle + 30M; 
        /// The result of this code should have angle set with the value of 75 degrees.
        /// Notice that the value 30M is assumed to be in the same unit of measure as angle.
        /// </summary>
        /// <param name="a"></param>
        /// <param name="scalar"></param>
        /// <returns></returns>
        public static Angle operator + (Angle a, decimal scalar)
        {
            //Angle angle = new Angle(45, AngleUnits.Degrees);
            return new Angle(a.Value + scalar, a.Units);
        }

        /// <summary>
        /// This operator should use similar logic to the previous operator, subtracting 
        /// a scalar value from a given Angle object. 
        /// </summary>
        /// <param name="a"></param>
        /// <param name="scalar"></param>
        /// <returns></returns>
        public static Angle operator -(Angle a, decimal scalar)
        {
            return new Angle(a.Value - scalar, a.Units);
        }

        /// <summary>
        /// It does not make much sense to multiply two angles together like 45° * 60°.  
        /// However, it could be useful to double an angle or cut it in half.  
        /// This operator will function very much like the previous two.
        /// </summary>
        /// <param name="a"></param>
        /// <param name="scalar"></param>
        /// <returns></returns>
        public static Angle operator * (Angle a, decimal scalar)
        {
            return new Angle(a.Value * scalar, a.Units);
        }

        /// <summary>
        /// Yes, an angle can be divided as well.  
        /// The only difference with this operator is that you should throw a DivideByZeroException if scalar is 0. 
        /// </summary>
        /// <param name="a"></param>
        /// <param name="scalar"></param>
        /// <returns></returns>
        public static Angle operator / (Angle a, decimal scalar)
        {
            if(scalar == 0)
            {
                throw new DivideByZeroException("Cannot divide by zero");
            }
            return new Angle(a.Value / scalar, a.Units);
        }

        #endregion

        #region Comparison Operators 

        /// <summary>
        /// This operator determines if two Angle objects are approximately equal to each other using the 
        /// ApproximatelyEqual() extension method.  
        /// However, before you can do this calculation, both input parameters have to be tested for null 
        /// since Angle is a reference type.  
        /// But, don’t just compare a to null as you will end up in an infinite recursive loop (until the 
        /// stack is exhausted).  
        /// You must first cast a and b to object variables, named o1 and o2, respectively.  
        /// This way, when you use the == operator it will use object’s version of the operator, thus 
        /// avoiding the recursive loop.  
        /// If both o1 and o2 are null, then the result is true.  
        /// If only one of o1 or o2 is null, then the result is false.  
        /// If neither o1 nor o2 is null, then it is safe to convert the Value member of b to the same 
        /// units as a and call ApproximatelyEqual() to determine relative equality.  
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static bool operator == (Angle a, Angle b)
        {
            object o1 = a;
            object o2 = b;
            if(o1 == null && o2 == null)
            {
                return true;
            }
            if(o1 == null ^ o2 == null)
            {
                return false;
            }

            decimal angleToCompare = ConvertAngleValue(b.Value, b.Units, a.Units);
            return angleToCompare.ApproximatelyEquals(a.Value);
        }

        /// <summary>
        /// The good news is that this operator can utilize the == operator, inverting the result with the not operator (!). 
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static bool operator != (Angle a, Angle b)
        {
            return !(a == b);
        }

        /// <summary>
        /// The first inequality operator we’ll implement is the less than operator.  
        /// This is an arbitrary choice.  Like the == operator we must test a and b for null.  
        /// However, unlike ==, we don’t have to cast the input values to object before comparing the items 
        /// since there is no chance of recursion. 
        /// If both a and b are null then the result is false since if two objects are the same one cannot be 
        /// less than the other.  
        /// If only a is null, then the result is true, using the logic that null values are always less than 
        /// legitimate values.  Likewise, if only b is null, then the answer is false.  
        /// If neither value is null, then it is time to test the angular values. 
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static bool operator < (Angle a, Angle b)
        {
            if (a == null && b == null)
            {
                return true;
            }
            if (a == null ^ b == null)
            {
                return false;
            }

            Angle normAngle = b.ConvertAngle(a.Units);
            return !(a.Value.ApproximatelyEquals(normAngle.Value)) && (a.Value < normAngle.Value);
        }

        /// <summary>
        /// This operator can be written as a combination of other operators, since > is the same as 
        /// saying “not equal to and not less than”. 
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static bool operator > (Angle a, Angle b)
        {
            return !(a == b || a < b);
        }

        /// <summary>
        /// Again, we can use the logic already implemented to calculate this result: 
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static bool operator <= (Angle a, Angle b)
        {
            return (a < b || a == b);
        }

        public static bool operator >= (Angle a, Angle b)
        {
            return (a > b || a == b);
        }

        #endregion

        #region Conversion Operators

        /// <summary>
        /// This cast is used to convert an Angle object to a decimal.  
        /// Since the Value property of the Angle object a is already a decimal, that is all 
        /// that needs to be returned from this method.  
        /// Don’t worry about a being null as a NullReferenceException will be thrown from this method 
        /// when a is not valid whether or not we test for null. 
        /// </summary>
        /// <param name="a"></param>
        public static explicit operator decimal(Angle a)
        {
            return a.Value;
        }

        /// <summary>
        /// This method is very similar to the previous except the result must first be converted to double 
        /// prior to returning the value. 
        /// Since that cast already exists, use it prior to returning the value. 
        /// </summary>
        /// <param name="a"></param>
        public static explicit operator double(Angle a)
        {
            return Convert.ToDouble(a.Value);
        }

        #endregion

        #region Overrides

        public override bool Equals(object obj)
        {
            return this == obj as Angle;
        }

        public override int GetHashCode()
        {
            return ConvertAngleValue(Value, Units, AngleUnits.Degrees).GetHashCode();
        }

        public string ToString(string format, IFormatProvider formatProvider)
        {
            return new AngleFormatter().Format(format, this, formatProvider);
        }

        public string ToString(string format)
        {
            AngleFormatter fmt = new AngleFormatter();
            return fmt.Format(format, this, fmt);
        }

        public override string ToString()
        {
            return ToString(string.Empty);
        }

        #endregion
    }
}
