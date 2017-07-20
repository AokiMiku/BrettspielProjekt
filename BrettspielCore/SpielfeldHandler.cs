using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows;
using System.Windows.Controls;

namespace BrettspielCore
{
	/// <summary>
	/// Verwaltet das Spielfeld.
	/// </summary>
	public class SpielfeldHandler
	{
		#region Felder
		internal MouseButtonState mausStatus = MouseButtonState.Released;
		private Grid grid;
		public struct ImageControls
		{
			public static Image img00;
			public static Image img03;
			public static Image img06;
			public static Image img11;
			public static Image img13;
			public static Image img15;
			public static Image img22;
			public static Image img23;
			public static Image img24;
			public static Image img30;
			public static Image img31;
			public static Image img32;
			public static Image img34;
			public static Image img35;
			public static Image img36;
			public static Image img42;
			public static Image img43;
			public static Image img44;
			public static Image img51;
			public static Image img53;
			public static Image img55;
			public static Image img60;
			public static Image img63;
			public static Image img66;
		}
		internal int spalteAlt = 0;
		internal int reiheAlt = 0;
		private int[,] spielSteineGesetzt = new int[7, 7];
		internal int[,] SpielSteineGesetzt
		{
			get { return spielSteineGesetzt; }
		}
		internal Dictionary<int, bool> hatMuehle = new Dictionary<int, bool>();
		#endregion Felder

		#region Konstruktoren
		internal SpielfeldHandler(Grid grid)
		{
			this.grid = grid;
			hatMuehle[1] = false;
			hatMuehle[2] = false;
		}
		#endregion Konstruktoren

		#region Methoden
		internal bool ValidateClickedCell(out int colIndex, out int rowIndex)
		{
			Point mousePosition = Mouse.GetPosition(grid);

			int col = 0;
			double width = 0;
			foreach (ColumnDefinition item in grid.ColumnDefinitions)
			{
				width += item.ActualWidth;
				if (width >= mousePosition.X)
				{
					break;
				}
				col++;
			}

			int row = 0;
			double height = 0;
			foreach (RowDefinition item in grid.RowDefinitions)
			{
				height += item.ActualHeight;
				if (height >= mousePosition.Y)
				{
					break;
				}
				row++;
			}

			if (col < grid.ColumnDefinitions.Count && row < grid.RowDefinitions.Count)
			{
				colIndex = col;
				rowIndex = row;
				return true;
			}
			else
			{
				colIndex = 0;
				rowIndex = 0;
				return false;
			}

		}

		internal Image SelectImageControl(int colIndex, int rowIndex)
		{
			if (colIndex == 0 && rowIndex == 0)
			{
				return ImageControls.img00;
			}
			else if (colIndex == 3 && rowIndex == 0)
			{
				return ImageControls.img30;
			}
			else if (colIndex == 6 && rowIndex == 0)
			{
				return ImageControls.img60;
			}
			else if (colIndex == 1 && rowIndex == 1)
			{
				return ImageControls.img11;
			}
			else if (colIndex == 3 && rowIndex == 1)
			{
				return ImageControls.img31;
			}
			else if (colIndex == 5 && rowIndex == 1)
			{
				return ImageControls.img51;
			}
			else if (colIndex == 2 && rowIndex == 2)
			{
				return ImageControls.img22;
			}
			else if (colIndex == 3 && rowIndex == 2)
			{
				return ImageControls.img32;
			}
			else if (colIndex == 4 && rowIndex == 2)
			{
				return ImageControls.img42;
			}
			else if (colIndex == 0 && rowIndex == 3)
			{
				return ImageControls.img03;
			}
			else if (colIndex == 1 && rowIndex == 3)
			{
				return ImageControls.img13;
			}
			else if (colIndex == 2 && rowIndex == 3)
			{
				return ImageControls.img23;
			}
			else if (colIndex == 4 && rowIndex == 3)
			{
				return ImageControls.img43;
			}
			else if (colIndex == 5 && rowIndex == 3)
			{
				return ImageControls.img53;
			}
			else if (colIndex == 6 && rowIndex == 3)
			{
				return ImageControls.img63;
			}
			else if (colIndex == 2 && rowIndex == 4)
			{
				return ImageControls.img24;
			}
			else if (colIndex == 3 && rowIndex == 4)
			{
				return ImageControls.img34;
			}
			else if (colIndex == 4 && rowIndex == 4)
			{
				return ImageControls.img44;
			}
			else if (colIndex == 1 && rowIndex == 5)
			{
				return ImageControls.img15;
			}
			else if (colIndex == 3 && rowIndex == 5)
			{
				return ImageControls.img35;
			}
			else if (colIndex == 5 && rowIndex == 5)
			{
				return ImageControls.img55;
			}
			else if (colIndex == 0 && rowIndex == 6)
			{
				return ImageControls.img06;
			}
			else if (colIndex == 3 && rowIndex == 6)
			{
				return ImageControls.img36;
			}
			else if (colIndex == 6 && rowIndex == 6)
			{
				return ImageControls.img66;
			}
			else
			{
				return null;
			}
		}

		internal bool DarfZiehen(int colIndex, int rowIndex)
		{
			if (colIndex == 3 && rowIndex == 3)
			{
				return false;
			}
			if (colIndex == spalteAlt && rowIndex == reiheAlt)
			{
				return true;
			}

			bool rtn = false;

			if (spalteAlt == 0 && reiheAlt == 0)
			{
				if ((colIndex == 3 && rowIndex == 0) || (colIndex == 0 && rowIndex == 3))
				{
					rtn = true;
				}
			}
			else if (spalteAlt == 0 && reiheAlt == 3)
			{
				if ((colIndex == 0 && rowIndex == 0) || (colIndex == 0 && rowIndex == 6) || (colIndex == 1 && rowIndex == 3))
				{
					rtn = true;
				}
			}
			else if (spalteAlt == 0 && reiheAlt == 6)
			{
				if ((colIndex == 0 && rowIndex == 3) || (colIndex == 3 && rowIndex == 6))
				{
					rtn = true;
				}
			}
			else if (spalteAlt == 1 && reiheAlt == 1)
			{
				if ((colIndex == 3 && rowIndex == 1) || (colIndex == 1 && rowIndex == 3))
				{
					rtn = true;
				}
			}
			else if (spalteAlt == 1 && reiheAlt == 3)
			{
				if ((colIndex == 1 && rowIndex == 1) || (colIndex == 1 && rowIndex == 5) || (colIndex == 0 && rowIndex == 3) || (colIndex == 2 && rowIndex == 3))
				{
					rtn = true;
				}
			}
			else if (spalteAlt == 1 && reiheAlt == 5)
			{
				if ((colIndex == 1 && rowIndex == 3) || (colIndex == 3 && rowIndex == 5))
				{
					rtn = true;
				}
			}
			else if (spalteAlt == 2 && reiheAlt == 2)
			{
				if ((colIndex == 3 && rowIndex == 2) || (colIndex == 2 && rowIndex == 3))
				{
					rtn = true;
				}
			}
			else if (spalteAlt == 2 && reiheAlt == 3)
			{
				if ((colIndex == 2 && rowIndex == 2) || (colIndex == 1 && rowIndex == 3) || (colIndex == 2 && rowIndex == 4))
				{
					rtn = true;
				}
			}
			else if (spalteAlt == 2 && reiheAlt == 4)
			{
				if ((colIndex == 2 && rowIndex == 3) || (colIndex == 3 && rowIndex == 4))
				{
					rtn = true;
				}
			}
			else if (spalteAlt == 3 && reiheAlt == 0)
			{
				if ((colIndex == 0 && rowIndex == 0) || (colIndex == 6 && rowIndex == 0) || (colIndex == 3 && rowIndex == 1))
				{
					rtn = true;
				}
			}
			else if (spalteAlt == 3 && reiheAlt == 1)
			{
				if ((colIndex == 3 && rowIndex == 0) || (colIndex == 1 && rowIndex == 1) || (colIndex == 5 && rowIndex == 1) || (colIndex == 3 && rowIndex == 2))
				{
					rtn = true;
				}
			}
			else if (spalteAlt == 3 && reiheAlt == 2)
			{
				if ((colIndex == 3 && rowIndex == 1) || (colIndex == 2 && rowIndex == 2) || (colIndex == 4 && rowIndex == 2))
				{
					rtn = true;
				}
			}
			else if (spalteAlt == 3 && reiheAlt == 4)
			{
				if ((colIndex == 2 && rowIndex == 4) || (colIndex == 4 && rowIndex == 4) || (colIndex == 3 && rowIndex == 5))
				{
					rtn = true;
				}
			}
			else if (spalteAlt == 3 && reiheAlt == 5)
			{
				if ((colIndex == 3 && rowIndex == 4) || (colIndex == 1 && rowIndex == 5) || (colIndex == 5 && rowIndex == 5) || (colIndex == 3 && rowIndex == 6))
				{
					rtn = true;
				}
			}
			else if (spalteAlt == 3 && reiheAlt == 6)
			{
				if ((colIndex == 3 && rowIndex == 5) || (colIndex == 0 && rowIndex == 6) || (colIndex == 6 && rowIndex == 6))
				{
					rtn = true;
				}
			}
			else if (spalteAlt == 4 && reiheAlt == 2)
			{
				if ((colIndex == 3 && rowIndex == 2) || (colIndex == 4 && rowIndex == 3))
				{
					rtn = true;
				}
			}
			else if (spalteAlt == 4 && reiheAlt == 3)
			{
				if ((colIndex == 4 && rowIndex == 2) || (colIndex == 5 && rowIndex == 3) || (colIndex == 4 && rowIndex == 4))
				{
					rtn = true;
				}
			}
			else if (spalteAlt == 4 && reiheAlt == 4)
			{
				if ((colIndex == 4 && rowIndex == 3) || (colIndex == 3 && rowIndex == 4))
				{
					rtn = true;
				}
			}
			else if (spalteAlt == 5 && reiheAlt == 1)
			{
				if ((colIndex == 3 && rowIndex == 1) || (colIndex == 5 && rowIndex == 3))
				{
					rtn = true;
				}
			}
			else if (spalteAlt == 5 && reiheAlt == 3)
			{
				if ((colIndex == 5 && rowIndex == 1) || (colIndex == 4 && rowIndex == 3) || (colIndex == 6 && rowIndex == 3) || (colIndex == 5 && rowIndex == 5))
				{
					rtn = true;
				}
			}
			else if (spalteAlt == 5 && reiheAlt == 5)
			{
				if ((colIndex == 5 && rowIndex == 3) || (colIndex == 3 && rowIndex == 5))
				{
					rtn = true;
				}
			}
			else if (spalteAlt == 6 && reiheAlt == 0)
			{
				if ((colIndex == 3 && rowIndex == 0) || (colIndex == 6 && rowIndex == 3))
				{
					rtn = true;
				}
			}
			else if (spalteAlt == 6 && reiheAlt == 3)
			{
				if ((colIndex == 6 && rowIndex == 0) || (colIndex == 5 && rowIndex == 3) || (colIndex == 6 && rowIndex == 6))
				{
					rtn = true;
				}
			}
			else if (spalteAlt == 6 && reiheAlt == 6)
			{
				if ((colIndex == 6 && rowIndex == 3) || (colIndex == 3 && rowIndex == 6))
				{
					rtn = true;
				}
			}

			return rtn;
		}

		internal bool Muehle(int colIndex, int rowIndex, int player)
		{
			if (player == -1)
			{
				return false;
			}

			bool rtn = false;

			if (spielSteineGesetzt[colIndex, rowIndex] == player)
			{
				if (colIndex == 0 && rowIndex == 0)
				{
					if ((spielSteineGesetzt[0, 3] != 0 && spielSteineGesetzt[0, 6] != 0) ||
						(spielSteineGesetzt[3, 0] != 0 && spielSteineGesetzt[6, 0] != 0))
					{
						if ((spielSteineGesetzt[0, 0] == spielSteineGesetzt[0, 3] && spielSteineGesetzt[0, 0] == spielSteineGesetzt[0, 6]) ||
							(spielSteineGesetzt[0, 0] == spielSteineGesetzt[3, 0] && spielSteineGesetzt[0, 0] == spielSteineGesetzt[6, 0]))
						{
							return true;
						}
					}
				}
				else if (colIndex == 3 && rowIndex == 0)
				{
					if ((spielSteineGesetzt[3, 1] != 0 && spielSteineGesetzt[3, 2] != 0) ||
						(spielSteineGesetzt[0, 0] != 0 && spielSteineGesetzt[6, 0] != 0))
					{
						if ((spielSteineGesetzt[3, 0] == spielSteineGesetzt[3, 1] && spielSteineGesetzt[3, 0] == spielSteineGesetzt[3, 2]) ||
							(spielSteineGesetzt[3, 0] == spielSteineGesetzt[0, 0] && spielSteineGesetzt[3, 0] == spielSteineGesetzt[6, 0]))
						{
							return true;
						}
					}
				}
				else if (colIndex == 6 && rowIndex == 0)
				{
					if ((spielSteineGesetzt[6, 3] != 0 && spielSteineGesetzt[6, 6] != 0) ||
						(spielSteineGesetzt[0, 0] != 0 && spielSteineGesetzt[3, 0] != 0))
					{
						if ((spielSteineGesetzt[6, 0] == spielSteineGesetzt[6, 3] && spielSteineGesetzt[6, 0] == spielSteineGesetzt[6, 6]) ||
							(spielSteineGesetzt[6, 0] == spielSteineGesetzt[0, 0] && spielSteineGesetzt[6, 0] == spielSteineGesetzt[3, 0]))
						{
							return true;
						}
					}
				}
				else if (colIndex == 1 && rowIndex == 1)
				{
					if ((spielSteineGesetzt[3, 1] != 0 && spielSteineGesetzt[5, 1] != 0) ||
						(spielSteineGesetzt[1, 3] != 0 && spielSteineGesetzt[1, 5] != 0))
					{
						if ((spielSteineGesetzt[1, 1] == spielSteineGesetzt[3, 1] && spielSteineGesetzt[1, 1] == spielSteineGesetzt[5, 1]) ||
							(spielSteineGesetzt[1, 1] == spielSteineGesetzt[1, 3] && spielSteineGesetzt[1, 1] == spielSteineGesetzt[1, 5]))
						{
							return true;
						}
					}
				}
				else if (colIndex == 3 && rowIndex == 1)
				{
					if ((spielSteineGesetzt[1, 1] != 0 && spielSteineGesetzt[5, 1] != 0) ||
						(spielSteineGesetzt[3, 0] != 0 && spielSteineGesetzt[3, 2] != 0))
					{
						if ((spielSteineGesetzt[3, 1] == spielSteineGesetzt[1, 1] && spielSteineGesetzt[3, 1] == spielSteineGesetzt[5, 1]) ||
							(spielSteineGesetzt[3, 1] == spielSteineGesetzt[3, 0] && spielSteineGesetzt[3, 1] == spielSteineGesetzt[3, 2]))
						{
							return true;
						}
					}
				}
				else if (colIndex == 5 && rowIndex == 1)
				{
					if ((spielSteineGesetzt[1, 1] != 0 && spielSteineGesetzt[3, 1] != 0) ||
						(spielSteineGesetzt[5, 3] != 0 && spielSteineGesetzt[5, 5] != 0))
					{
						if ((spielSteineGesetzt[5, 1] == spielSteineGesetzt[1, 1] && spielSteineGesetzt[5, 1] == spielSteineGesetzt[3, 1]) ||
							(spielSteineGesetzt[5, 1] == spielSteineGesetzt[5, 3] && spielSteineGesetzt[5, 1] == spielSteineGesetzt[5, 5]))
						{
							return true;
						}
					}
				}
				else if (colIndex == 2 && rowIndex == 2)
				{
					if ((spielSteineGesetzt[3, 2] != 0 && spielSteineGesetzt[4, 2] != 0) ||
						(spielSteineGesetzt[2, 3] != 0 && spielSteineGesetzt[2, 4] != 0))
					{
						if ((spielSteineGesetzt[2, 2] == spielSteineGesetzt[3, 2] && spielSteineGesetzt[2, 2] == spielSteineGesetzt[4, 2]) ||
							(spielSteineGesetzt[2, 2] == spielSteineGesetzt[2, 3] && spielSteineGesetzt[2, 2] == spielSteineGesetzt[2, 4]))
						{
							return true;
						}
					}
				}
				else if (colIndex == 3 && rowIndex == 2)
				{
					if ((spielSteineGesetzt[2, 2] != 0 && spielSteineGesetzt[4, 2] != 0) ||
						(spielSteineGesetzt[3, 0] != 0 && spielSteineGesetzt[3, 1] != 0))
					{
						if ((spielSteineGesetzt[3, 2] == spielSteineGesetzt[2, 2] && spielSteineGesetzt[3, 2] == spielSteineGesetzt[4, 2]) ||
							(spielSteineGesetzt[3, 2] == spielSteineGesetzt[3, 0] && spielSteineGesetzt[3, 2] == spielSteineGesetzt[3, 1]))
						{
							return true;
						}
					}
				}
				else if (colIndex == 4 && rowIndex == 2)
				{
					if ((spielSteineGesetzt[2, 2] != 0 && spielSteineGesetzt[3, 2] != 0) ||
						(spielSteineGesetzt[4, 3] != 0 && spielSteineGesetzt[4, 4] != 0))
					{
						if ((spielSteineGesetzt[4, 2] == spielSteineGesetzt[2, 2] && spielSteineGesetzt[4, 2] == spielSteineGesetzt[3, 2]) ||
							(spielSteineGesetzt[4, 2] == spielSteineGesetzt[4, 3] && spielSteineGesetzt[4, 2] == spielSteineGesetzt[4, 4]))
						{
							return true;
						}
					}
				}
				else if (colIndex == 0 && rowIndex == 3)
				{
					if ((spielSteineGesetzt[1, 3] != 0 && spielSteineGesetzt[2, 3] != 0) ||
						(spielSteineGesetzt[0, 0] != 0 && spielSteineGesetzt[0, 6] != 0))
					{
						if ((spielSteineGesetzt[0, 3] == spielSteineGesetzt[1, 3] && spielSteineGesetzt[0, 3] == spielSteineGesetzt[2, 3]) ||
							(spielSteineGesetzt[0, 3] == spielSteineGesetzt[0, 0] && spielSteineGesetzt[0, 3] == spielSteineGesetzt[0, 6]))
						{
							return true;
						}
					}
				}
				else if (colIndex == 1 && rowIndex == 3)
				{
					if ((spielSteineGesetzt[0, 3] != 0 && spielSteineGesetzt[2, 3] != 0) ||
						(spielSteineGesetzt[1, 1] != 0 && spielSteineGesetzt[1, 5] != 0))
					{
						if ((spielSteineGesetzt[1, 3] == spielSteineGesetzt[0, 3] && spielSteineGesetzt[1, 3] == spielSteineGesetzt[2, 3]) ||
							(spielSteineGesetzt[1, 3] == spielSteineGesetzt[1, 1] && spielSteineGesetzt[1, 3] == spielSteineGesetzt[1, 5]))
						{
							return true;
						}
					}
				}
				else if (colIndex == 2 && rowIndex == 3)
				{
					if ((spielSteineGesetzt[0, 3] != 0 && spielSteineGesetzt[1, 3] != 0) ||
						(spielSteineGesetzt[2, 2] != 0 && spielSteineGesetzt[2, 4] != 0))
					{
						if ((spielSteineGesetzt[2, 3] == spielSteineGesetzt[0, 3] && spielSteineGesetzt[2, 3] == spielSteineGesetzt[1, 3]) ||
							(spielSteineGesetzt[2, 3] == spielSteineGesetzt[2, 2] && spielSteineGesetzt[2, 3] == spielSteineGesetzt[2, 4]))
						{
							return true;
						}
					}
				}
				else if (colIndex == 4 && rowIndex == 3)
				{
					if ((spielSteineGesetzt[5, 3] != 0 && spielSteineGesetzt[6, 3] != 0) ||
						(spielSteineGesetzt[4, 2] != 0 && spielSteineGesetzt[4, 4] != 0))
					{
						if ((spielSteineGesetzt[4, 3] == spielSteineGesetzt[5, 3] && spielSteineGesetzt[4, 3] == spielSteineGesetzt[6, 3]) ||
							(spielSteineGesetzt[4, 3] == spielSteineGesetzt[4, 2] && spielSteineGesetzt[4, 3] == spielSteineGesetzt[4, 4]))
						{
							return true;
						}
					}
				}
				else if (colIndex == 5 && rowIndex == 3)
				{
					if ((spielSteineGesetzt[4, 3] != 0 && spielSteineGesetzt[6, 3] != 0) ||
						(spielSteineGesetzt[5, 1] != 0 && spielSteineGesetzt[5, 5] != 0))
					{
						if ((spielSteineGesetzt[5, 3] == spielSteineGesetzt[4, 3] && spielSteineGesetzt[5, 3] == spielSteineGesetzt[6, 3]) ||
							(spielSteineGesetzt[5, 3] == spielSteineGesetzt[5, 1] && spielSteineGesetzt[5, 3] == spielSteineGesetzt[5, 5]))
						{
							return true;
						}
					}
				}
				else if (colIndex == 6 && rowIndex == 3)
				{
					if ((spielSteineGesetzt[4, 3] != 0 && spielSteineGesetzt[5, 3] != 0) ||
						(spielSteineGesetzt[6, 0] != 0 && spielSteineGesetzt[6, 6] != 0))
					{
						if ((spielSteineGesetzt[6, 3] == spielSteineGesetzt[4, 3] && spielSteineGesetzt[6, 3] == spielSteineGesetzt[5, 3]) ||
							(spielSteineGesetzt[6, 3] == spielSteineGesetzt[6, 0] && spielSteineGesetzt[6, 3] == spielSteineGesetzt[6, 6]))
						{
							return true;
						}
					}
				}
				else if (colIndex == 2 && rowIndex == 4)
				{
					if ((spielSteineGesetzt[3, 4] != 0 && spielSteineGesetzt[4, 4] != 0) ||
						(spielSteineGesetzt[2, 2] != 0 && spielSteineGesetzt[2, 3] != 0))
					{
						if ((spielSteineGesetzt[2, 4] == spielSteineGesetzt[3, 4] && spielSteineGesetzt[2, 4] == spielSteineGesetzt[4, 4]) ||
							(spielSteineGesetzt[2, 4] == spielSteineGesetzt[2, 2] && spielSteineGesetzt[2, 4] == spielSteineGesetzt[2, 3]))
						{
							return true;
						}
					}
				}
				else if (colIndex == 3 && rowIndex == 4)
				{
					if ((spielSteineGesetzt[2, 4] != 0 && spielSteineGesetzt[4, 4] != 0) ||
						(spielSteineGesetzt[3, 5] != 0 && spielSteineGesetzt[3, 6] != 0))
					{
						if ((spielSteineGesetzt[3, 4] == spielSteineGesetzt[2, 4] && spielSteineGesetzt[3, 4] == spielSteineGesetzt[4, 4]) ||
							(spielSteineGesetzt[3, 4] == spielSteineGesetzt[3, 5] && spielSteineGesetzt[3, 4] == spielSteineGesetzt[3, 6]))
						{
							return true;
						}
					}
				}
				else if (colIndex == 4 && rowIndex == 4)
				{
					if ((spielSteineGesetzt[2, 4] != 0 && spielSteineGesetzt[3, 4] != 0) ||
						(spielSteineGesetzt[4, 2] != 0 && spielSteineGesetzt[4, 3] != 0))
					{
						if ((spielSteineGesetzt[4, 4] == spielSteineGesetzt[2, 4] && spielSteineGesetzt[4, 4] == spielSteineGesetzt[3, 4]) ||
							(spielSteineGesetzt[4, 4] == spielSteineGesetzt[4, 2] && spielSteineGesetzt[4, 4] == spielSteineGesetzt[4, 3]))
						{
							return true;
						}
					}
				}
				else if (colIndex == 1 && rowIndex == 5)
				{
					if ((spielSteineGesetzt[3, 5] != 0 && spielSteineGesetzt[5, 5] != 0) ||
						(spielSteineGesetzt[1, 1] != 0 && spielSteineGesetzt[1, 3] != 0))
					{
						if ((spielSteineGesetzt[1, 5] == spielSteineGesetzt[3, 5] && spielSteineGesetzt[1, 5] == spielSteineGesetzt[5, 5]) ||
							(spielSteineGesetzt[1, 5] == spielSteineGesetzt[1, 1] && spielSteineGesetzt[1, 5] == spielSteineGesetzt[1, 3]))
						{
							return true;
						}
					}
				}
				else if (colIndex == 3 && rowIndex == 5)
				{
					if ((spielSteineGesetzt[1, 5] != 0 && spielSteineGesetzt[5, 5] != 0) ||
						(spielSteineGesetzt[3, 4] != 0 && spielSteineGesetzt[3, 6] != 0))
					{
						if ((spielSteineGesetzt[3, 5] == spielSteineGesetzt[1, 5] && spielSteineGesetzt[3, 5] == spielSteineGesetzt[5, 5]) ||
							(spielSteineGesetzt[3, 5] == spielSteineGesetzt[3, 4] && spielSteineGesetzt[3, 5] == spielSteineGesetzt[3, 6]))
						{
							return true;
						}
					}
				}
				else if (colIndex == 5 && rowIndex == 5)
				{
					if ((spielSteineGesetzt[1, 5] != 0 && spielSteineGesetzt[3, 5] != 0) ||
						(spielSteineGesetzt[5, 1] != 0 && spielSteineGesetzt[5, 3] != 0))
					{
						if ((spielSteineGesetzt[5, 5] == spielSteineGesetzt[1, 5] && spielSteineGesetzt[5, 5] == spielSteineGesetzt[3, 5]) ||
							(spielSteineGesetzt[5, 5] == spielSteineGesetzt[5, 1] && spielSteineGesetzt[5, 5] == spielSteineGesetzt[5, 3]))
						{
							return true;
						}
					}
				}
				else if (colIndex == 0 && rowIndex == 6)
				{
					if ((spielSteineGesetzt[3, 6] != 0 && spielSteineGesetzt[6, 6] != 0) ||
						(spielSteineGesetzt[0, 0] != 0 && spielSteineGesetzt[0, 3] != 0))
					{
						if ((spielSteineGesetzt[0, 6] == spielSteineGesetzt[3, 6] && spielSteineGesetzt[0, 6] == spielSteineGesetzt[6, 6]) ||
							(spielSteineGesetzt[0, 6] == spielSteineGesetzt[0, 0] && spielSteineGesetzt[0, 6] == spielSteineGesetzt[0, 3]))
						{
							return true;
						}
					}
				}
				else if (colIndex == 3 && rowIndex == 6)
				{
					if ((spielSteineGesetzt[0, 6] != 0 && spielSteineGesetzt[6, 6] != 0) ||
						(spielSteineGesetzt[3, 4] != 0 && spielSteineGesetzt[3, 5] != 0))
					{
						if ((spielSteineGesetzt[3, 6] == spielSteineGesetzt[0, 6] && spielSteineGesetzt[3, 6] == spielSteineGesetzt[6, 6]) ||
							(spielSteineGesetzt[3, 6] == spielSteineGesetzt[3, 4] && spielSteineGesetzt[3, 6] == spielSteineGesetzt[3, 5]))
						{
							return true;
						}
					}
				}
				else if (colIndex == 6 && rowIndex == 6)
				{
					if ((spielSteineGesetzt[0, 6] != 0 && spielSteineGesetzt[3, 6] != 0) ||
						(spielSteineGesetzt[6, 0] != 0 && spielSteineGesetzt[6, 3] != 0))
					{
						if ((spielSteineGesetzt[6, 6] == spielSteineGesetzt[0, 6] && spielSteineGesetzt[6, 6] == spielSteineGesetzt[3, 6]) ||
							(spielSteineGesetzt[6, 6] == spielSteineGesetzt[6, 0] && spielSteineGesetzt[6, 6] == spielSteineGesetzt[6, 3]))
						{
							return true;
						}
					}
				}
			}
			return rtn;
		}
		
		#endregion Methoden
	}
}