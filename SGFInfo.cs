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
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace SGFConvert
{
	public class SGFInfo
	{
		protected static Dictionary<string, SGFPropertyInfo> Dictionary;

		protected static SGFInfo Instance = new SGFInfo();

		protected SGFInfo()
		{
			Dictionary = new Dictionary<string, SGFPropertyInfo>();
			Add("AN", "--34", SGFPropertyType.GameInfo, SGFPropertyValueType.SimpleText, false, "Annotation");
			Add("AP", "---4", SGFPropertyType.Root, SGFPropertyValueType.SimpleText, false, "Application");
			Add("B", "1234", SGFPropertyType.Move, SGFPropertyValueType.Move, false, "Black");
			Add("BL", "1234", SGFPropertyType.Move, SGFPropertyValueType.Real, false, "Black time left");
			Add("BM", "1234", SGFPropertyType.Move, SGFPropertyValueType.Double, false, "Bad Move");
			Add("BR", "1234", SGFPropertyType.GameInfo, SGFPropertyValueType.SimpleText, false, "Black rank");
			Add("BT", "--34", SGFPropertyType.GameInfo, SGFPropertyValueType.SimpleText, false, "Black team");
			Add("C", "1234", SGFPropertyType.None, SGFPropertyValueType.Text, false, "Comment");
			Add("CA", "---4", SGFPropertyType.Root, SGFPropertyValueType.SimpleText, false, "Charset");
			Add("CP", "--34", SGFPropertyType.GameInfo, SGFPropertyValueType.SimpleText, false, "Copyright");
			Add("DM", "--34", SGFPropertyType.None, SGFPropertyValueType.Double, false, "Even position");
			Add("DO", "--34", SGFPropertyType.Move, SGFPropertyValueType.None, false, "Doubtful");
			Add("DT", "1234", SGFPropertyType.GameInfo, SGFPropertyValueType.SimpleText, false, "Date");
			Add("EV", "1234", SGFPropertyType.GameInfo, SGFPropertyValueType.SimpleText, false, "Event");
			Add("FF", "-234", SGFPropertyType.Root, SGFPropertyValueType.Number, false, "Fileformat");
			Add("FG", "1234", SGFPropertyType.None, SGFPropertyValueType.Ignore, false, "Figure");
			Add("GB", "1234", SGFPropertyType.None, SGFPropertyValueType.Double, false, "Good for Black");
			Add("GC", "1234", SGFPropertyType.GameInfo, SGFPropertyValueType.Text, false, "Game comment");
			Add("GM", "1234", SGFPropertyType.Root, SGFPropertyValueType.Number, false, "Game");
			Add("GN", "1234", SGFPropertyType.GameInfo, SGFPropertyValueType.SimpleText, false, "Game name");
			Add("GW", "1234", SGFPropertyType.None, SGFPropertyValueType.Double, false, "Good for White");
			Add("HO", "--34", SGFPropertyType.None, SGFPropertyValueType.Double, false, "Hotspot");
			Add("ID", "--3-", SGFPropertyType.GameInfo, SGFPropertyValueType.SimpleText, false, "Game identifier");
			Add("IT", "--34", SGFPropertyType.Move, SGFPropertyValueType.None, false, "Interesting");
			Add("KO", "--34", SGFPropertyType.Move, SGFPropertyValueType.None, false, "Ko");
			Add("MN", "--3-", SGFPropertyType.Move, SGFPropertyValueType.Number, false, "set Move Number");
			Add("N", "12--", SGFPropertyType.None, SGFPropertyValueType.SimpleText, false, "Nodename");
			Add("OB", "--34", SGFPropertyType.Move, SGFPropertyValueType.Number, false, "OtStones Black");
			Add("ON", "--34", SGFPropertyType.GameInfo, SGFPropertyValueType.SimpleText, false, "Opening");
			Add("OT", "---4", SGFPropertyType.GameInfo, SGFPropertyValueType.SimpleText, false, "Overtime");
			Add("OW", "--34", SGFPropertyType.Move, SGFPropertyValueType.Number, false, "OtStones White");
			Add("PB", "1234", SGFPropertyType.GameInfo, SGFPropertyValueType.SimpleText, false, "Player Black");
			Add("PC", "1234", SGFPropertyType.GameInfo, SGFPropertyValueType.SimpleText, false, "Place");
			Add("PL", "1234", SGFPropertyType.Setup, SGFPropertyValueType.Color, false, "Player to play");
			Add("PM", "---4", SGFPropertyType.None, SGFPropertyValueType.Number, false, "Print Move mode");
			Add("PW", "1234", SGFPropertyType.GameInfo, SGFPropertyValueType.SimpleText, false, "Player White");
			Add("RE", "1234", SGFPropertyType.GameInfo, SGFPropertyValueType.SimpleText, false, "Result");
			Add("RO", "1234", SGFPropertyType.GameInfo, SGFPropertyValueType.SimpleText, false, "Round");
			Add("RU", "--34", SGFPropertyType.GameInfo, SGFPropertyValueType.SimpleText, false, "Rules");
			Add("SO", "1234", SGFPropertyType.GameInfo, SGFPropertyValueType.SimpleText, false, "Source");
			Add("TE", "1234", SGFPropertyType.Move, SGFPropertyValueType.Double, false, "Tesuji");
			Add("TM", "1234", SGFPropertyType.GameInfo, SGFPropertyValueType.Real, false, "Timelimit");
			Add("UC", "--34", SGFPropertyType.None, SGFPropertyValueType.Double, false, "Unclear pos");
			Add("US", "1234", SGFPropertyType.GameInfo, SGFPropertyValueType.SimpleText, false, "User");
			Add("V", "1234", SGFPropertyType.None, SGFPropertyValueType.Real, false, "Value");
			Add("W", "1234", SGFPropertyType.Move, SGFPropertyValueType.Move, false, "White");
			Add("WL", "1234", SGFPropertyType.Move, SGFPropertyValueType.Real, false, "White time left");
			Add("WR", "1234", SGFPropertyType.GameInfo, SGFPropertyValueType.SimpleText, false, "White rank");
			Add("WT", "123-", SGFPropertyType.GameInfo, SGFPropertyValueType.SimpleText, false, "White team");
			Add("AB", "1234", SGFPropertyType.Setup, SGFPropertyValueType.Stone, true, "Add Black");
			Add("AE", "1234", SGFPropertyType.Setup, SGFPropertyValueType.Point, true, "Add Empty");
			Add("AR", "---4", SGFPropertyType.None, SGFPropertyValueType.Point, true, "Arrow");
			Add("AW", "1234", SGFPropertyType.Setup, SGFPropertyValueType.Stone, true, "Add White");
			Add("CR", "--34", SGFPropertyType.None, SGFPropertyValueType.Point, true, "Circle");
			Add("DD", "---4", SGFPropertyType.None, SGFPropertyValueType.Point, true, "Dim Points");
			Add("LB", "12--", SGFPropertyType.None, SGFPropertyValueType.Point, false, "Label");
			Add("LN", "--34", SGFPropertyType.None, SGFPropertyValueType.Point, true, "Line");
			Add("MA", "--34", SGFPropertyType.None, SGFPropertyValueType.Point, true, "Mark");
			Add("SE", "--34", SGFPropertyType.None, SGFPropertyValueType.Point, true, "Selected");
			Add("SQ", "---4", SGFPropertyType.None, SGFPropertyValueType.Point, true, "Square");
			Add("TR", "--34", SGFPropertyType.None, SGFPropertyValueType.Point, true, "Triangle");
			Add("VW", "1234", SGFPropertyType.None, SGFPropertyValueType.Point, true, "View");
			Add("HA", "1234", SGFPropertyType.GameInfo, SGFPropertyValueType.Number, false, "Handicap");
			Add("KM", "1234", SGFPropertyType.GameInfo, SGFPropertyValueType.Real, false, "Komi");
			Add("TB", "1234", SGFPropertyType.None, SGFPropertyValueType.Point, true, "Territory Black");
			Add("TW", "1234", SGFPropertyType.None, SGFPropertyValueType.Point, true, "Territory White");
		}

		protected void Add(string id, string ff, SGFPropertyType propertyType, SGFPropertyValueType propertyValueType, bool list, string description)
		{
			Dictionary.Add(id, new SGFPropertyInfo(id, description, propertyType, propertyValueType, list, ff));
		}

		public static SGFPropertyInfo GetSGFPropertyInfo(string id)
		{
			return Instance.FindSGFPropertyInfo(id);
		}

		protected SGFPropertyInfo FindSGFPropertyInfo(string id)
		{
			SGFPropertyInfo lSGFPropertyInfo = null;

			if (!Dictionary.TryGetValue(id, out lSGFPropertyInfo))
				return null;

			return lSGFPropertyInfo;
		}

	}
}
