/* 
* Copyright (C) 2007 Philipp Garcia (phil@gotraxx.org)
*
* This file is part of SGFConvert.
*
* SGFConvert is free software; you can redistribute it and/or modify
* it under the terms of the GNU General Public License as published by
* the Free Software Foundation; either version 3 of the License, or
* (at your option) any later version.
*
* HouseBot is distributed in the hope that it will be useful,
* but WITHOUT ANY WARRANTY; without even the implied warranty of
* MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
* GNU General Public License for more details.
*
* You should have received a copy of the GNU General Public License
* along with this program.  If not, see <http://www.gnu.org/licenses/>.
*
*/

using System;

namespace SGFConvert
{

	public class Coords
	{
		public int X;
		public int Y;

		public Coords(int x, int y)
		{
			X = x;
			Y = y;
		}

		public Coords(string s, int boardSize)
		{
			string lString = s.Trim().ToLower();

			if ((lString.Length < 2) || (lString == "tt") || (lString == "pass") || (lString == "resign"))
			{
				X = -1;
				Y = -1;
				return;
			}

			char lA = lString[0];

			if (!Char.IsDigit(lString[1]))
			{
				X = lA - 'a';
				Y = boardSize - (lString[1] - 'a') - 1;
			}
			else
			{
				X = (lA < 'i') ? lA - 'a' : lA - 'a' - 1;
				Y = Convert.ToInt32(lString.Substring(1)) - 1;
			}
		}

		public bool IsPass()
		{
			return ((X == -1) || (Y == -1));
		}

		public Coords PASS
		{
			get
			{
				return new Coords(-1, -1);
			}
		}

		public string ToGoCoords()
		{
			if (IsPass())
				return "PASS";

			int lColumn = (X >= 'I' - 'A') ? X - 1 : X;

			return Convert.ToString(Convert.ToChar(lColumn + 'A')) + Convert.ToString(Y + 1);

		}

		public String ToSGFCoords(int boardSize)
		{
			if (IsPass())
				return "";

			int lColumn = X;
			int lRow = boardSize - Y - 1;

			return Convert.ToString(Convert.ToChar(lColumn + 'a')) + Convert.ToString(Convert.ToChar(lRow + 'a'));
		}
	};

}
