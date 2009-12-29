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
using System.Collections.Generic;
using System.Text;

namespace SGFConvert
{
	public class Property
	{
		public string Name = string.Empty;
		public string Value = string.Empty;
		public List<string> AdditionalValues = new List<string>();

		public Property()
		{
		}

		public Property(string name)
		{
			Name = name;
		}

		public Property(string name, string value)
		{
			Name = name;
			Value = value;
		}

		public override string ToString()
		{
			StringBuilder lString = new StringBuilder();

			lString.Append(Name);
			lString.Append("[");
			lString.Append(Value);
			lString.Append("]");

			foreach (string lAddValue in AdditionalValues)
			{
				lString.Append("[");
				lString.Append(lAddValue);
				lString.Append("]");
			}

			return lString.ToString();
		}
	};
}
