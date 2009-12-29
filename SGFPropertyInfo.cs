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
	public enum SGFPropertyType { None, Setup, GameInfo, Root, Move };
	public enum SGFPropertyValueType { None, Number, Real, Double, Color, SimpleText, Text, Point, Move, Stone, Ignore };
	public enum SGFPropertyValueExtType { None, Point, Ignore }

	public class SGFPropertyInfo
	{
		public string ID = string.Empty;
		public string Description = string.Empty;
		public SGFPropertyType PropertyType = SGFPropertyType.None;
		public SGFPropertyValueType PropertyValueType = SGFPropertyValueType.None;
		public bool List = false;
		protected string FF = string.Empty;

		public SGFPropertyInfo(string id, string description, SGFPropertyType propertyType, SGFPropertyValueType propertyValueType, bool list, string ff)
		{
			ID = id;
			Description = description;
			PropertyType = propertyType;
			PropertyValueType = propertyValueType;
			List = list;
			FF = ff;
		}
	}

}
