using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MazeProject
{
	public class MazeCellArray 
	{
		#region Fields
		private MazeCell[,] m_Cells = null;
		private Range<int>? m_XRange = null;
		private Range<int>? m_YRange = null;
		#endregion Fields

		#region Properties
		/// <summary>
		/// Gets the cell at the given indices
		/// </summary>
		/// <param name="x">X index</param>
		/// <param name="y">Y index</param>
		/// <returns>Cell at given index</returns>
		public MazeCell this[int x, int y]
		{
			get
			{
				return m_Cells[x, y];
			}
			set
			{
				m_Cells[x, y] = value;
			}
		}

		/// <summary>
		/// Gets the cell at the given position
		/// </summary>
        /// <param name="pos">Position of cell to get</param>
		/// <returns>Cell at given position</returns>
		public MazeCell this[Position pos]
		{
			get
			{
                return m_Cells[pos.X, pos.Y];
			}
			set
			{
                m_Cells[pos.X, pos.Y] = value;
			}
		}

		/// <summary>
		/// Gets the width of the array
		/// </summary>
		public int Width
		{
			get
			{
				return m_Cells.GetLength(0);
			}
		}

		/// <summary>
		/// Gets the height of the array
		/// </summary>
		public int Height
		{
			get
			{
				return m_Cells.GetLength(1);
			}
		}

		/// <summary>
		/// Gets the valid range of horizontal values
		/// </summary>
		public Range<int> XRange
		{
			get
			{
				if (m_XRange == null)
				{
					m_XRange = new Range<int>(0, Width - 1);
				}
				return m_XRange.Value;
			}
		}

		/// <summary>
		/// Gets the valid range of vertical values
		/// </summary>
		public Range<int> YRange
		{
			get
			{
				if (m_YRange == null)
				{
					m_YRange = new Range<int>(0, Height - 1);
				}
				return m_YRange.Value;
			}
		}
		#endregion Properties

		#region Constructors
		/// <summary>
		/// Creates a new maze cell array with the given dimensions
		/// </summary>
		/// <param name="width">Width</param>
		/// <param name="height">Height</param>
		public MazeCellArray(int width, int height)
		{
			Resize(width, height);
		}
		#endregion Constructors

		#region Methods
		/// <summary>
		/// Resets the cells in the array
		/// </summary>
		public void Reset()
		{
			for (int x = 0; x < Width; x++)
			{
				for (int y = 0; y < Height; y++)
				{
					m_Cells[x, y] = new MazeCell(new Position(x, y), DirectionEnum.All);
				}
			}
		}

		/// <summary>
		/// Resizes the array to the given dimensions and initializes all the cells
		/// </summary>
		/// <param name="width">New width</param>
		/// <param name="height">New height</param>
		public void Resize(int width, int height)
		{
            if (width < Constants.MIN_WIDTH || width > Constants.MAX_WIDTH || height < Constants.MIN_HEIGHT || height > Constants.MAX_HEIGHT) throw new ArgumentOutOfRangeException("Inavlid size for MazeCellArray");
			m_XRange = null;
			m_YRange = null;
			m_Cells = new MazeCell[width, height];
			Reset();
		}

		/// <summary>
		/// Removes the wall from the given cell and the adjacent wall's opposing wall
		/// </summary>
		/// <param name="position">Position of cell to remove wall from</param>
		/// <param name="wallToRemove">Direction of wall to remove</param>
		public void RemoveCellWall(Position position, DirectionEnum wallToRemove)
		{
			// Find adjacent cell in the direction of the wall to remove
			MazeCell cell = this[position];
			Position neighborPosition = cell.Position.MovePosition(wallToRemove);

			// Only remove walls if neighborPosition is within bounds, 
			// otherwise the given cell is on the border and walls cannot come down.
			if (XRange.Contains(neighborPosition.X) && YRange.Contains(neighborPosition.Y))
			{
				cell.RemoveWall(wallToRemove);
				MazeCell neighbor = this[neighborPosition];
				neighbor.RemoveWall(wallToRemove.GetOpposite());
			}
		}

		/// <summary>
		/// Gets the neighbor cells of the given location in the cardinal directions
		/// </summary>
		/// <param name="position">Position of cell to get neighbors for</param>
		/// <returns>Dictionary of all found neighbor cells</returns>
		public Dictionary<DirectionEnum, MazeCell> GetNeighborCellsWithAllWalls(Position position)
		{
			var neighbors = new Dictionary<DirectionEnum, MazeCell>();

			// Try each direction
			foreach (DirectionEnum dir in Constants.Cardinals)
			{
				Position neighbor = position.MovePosition(dir);
				if (XRange.Contains(neighbor.X) && YRange.Contains(neighbor.Y) && (this[neighbor].Walls == DirectionEnum.All))
				{
					neighbors[dir] = this[neighbor];
				}
			}
			return neighbors;
		}

		/// <summary>
		/// Gets the neighbor cells of the given location in the cardinal directions
		/// </summary>
		/// <param name="position">Position of cell to get neighbors for</param>
		/// <returns>Dictionary of all found neighbor cells</returns>
		public Dictionary<DirectionEnum, MazeCell> GetAccessibleNeighborCells(Position position, DirectionEnum exclude = DirectionEnum.None)
		{
			var neighbors = new Dictionary<DirectionEnum, MazeCell>();

			// Try each direction
			foreach (DirectionEnum dir in Constants.Cardinals)
			{
				if ((dir != exclude) && (!this[position].Walls.HasFlag(dir)))
				{
					Position neighbor = position.MovePosition(dir);
					neighbors[dir] = this[neighbor];
				}
			}
			return neighbors;
		}
		#endregion Methods
	}
}
