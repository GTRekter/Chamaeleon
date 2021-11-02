using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MazeProject
{
	public struct Position
	{
		#region Fields
		public int X;
        public int Y;
		#endregion Fields

		#region Constructors
		public Position(int x, int y)
		{
			X = x;
			Y = y;
		}
		#endregion Constructors

		#region Methods
		/// <summary>
		/// Creates a random Position object within the supplied ranges
		/// </summary>
		/// <param name="horizontal">Horizontal range</param>
		/// <param name="vertical">Vertical range</param>
		/// <returns>Random position within the given bounds</returns>
		public static Position GetRandomPosition(Range<int> horizontal, Range<int> vertical)
		{
			int x = Utils.Rnd.Next(horizontal.Min, horizontal.Max + 1);
			int y = Utils.Rnd.Next(vertical.Min, vertical.Max + 1);
			return new Position(x, y);
		}

		/// <summary>
		/// Gets the cell position when moved in the given direction
		/// </summary>
		/// <param name="direction">Direction to 'move' position</param>
		/// <returns></returns>
		/// <remarks>Does not alter this object</remarks>
		public Position MovePosition(DirectionEnum direction)
		{
			int x = X;
			int y = Y;

			if (direction.HasFlag(DirectionEnum.North))
			{
				y--;
			}
			if (direction.HasFlag(DirectionEnum.South))
			{
				y++;
			}
			if (direction.HasFlag(DirectionEnum.East))
			{
				x++;
			}
			if (direction.HasFlag(DirectionEnum.West))
			{
				x--;
			}
			return new Position(x, y);
		}

		/// <summary>
		/// Gets the string representation of the object
		/// </summary>
		/// <returns>String representation of the object</returns>
		public override string ToString()
		{
			return string.Format("({0}, {1})", X, Y);
		}

		/// <summary>
		/// Compares this object to the object passed in
		/// </summary>
		/// <param name="obj">Object to compare</param>
		/// <returns>true if equal, false otherwise</returns>
		public override bool Equals(object obj)
		{
			if (obj is Position)
			{
				return (this == (Position)obj);
			}
			return false;
		}

		/// <summary>
		/// Returns the hash code for this instance.
		/// </summary>
		/// <returns>A 32-bit signed integer that is the hash code for this instance.</returns>
		/// <remarks>Recommended to override if overriding Equals</remarks>
		public override int GetHashCode()
		{
			return X ^ Y;
		}
		#endregion Methods

		#region Operators
		/// <summary>
		/// Checks two Position objects for equality
		/// </summary>
		/// <param name="p1">Position 1</param>
		/// <param name="p2">Position 2</param>
		/// <returns>true if equal, false otherwise</returns>
		public static bool operator ==(Position p1, Position p2)
		{
			return (p1.X == p2.X && p1.Y == p2.Y);
		}

		/// <summary>
		/// Checks two Position objects for inequality
		/// </summary>
		/// <param name="p1">Position 1</param>
		/// <param name="p2">Position 2</param>
		/// <returns>true if not equal, false otherwise</returns>
		public static bool operator !=(Position p1, Position p2)
		{
			return !(p1 == p2);
		}
		#endregion Operators
	}
}
