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
	public enum TokenType : byte
	{
		OPENPARAN = 0,
		CLOSEPARAN,
		SEMICOMMA,
		PROPERTY,
		WHITESPACE,
		DELETED
	};

	class SGFToken
	{
		public TokenType Type;
		public string Whitespace;
		public Property Property;

		public SGFToken(TokenType type)
		{
			Type = type;
		}

		public SGFToken(string value)
		{
			Type = TokenType.WHITESPACE;
			Whitespace = value;
		}

		public SGFToken(Property property)
		{
			Type = TokenType.PROPERTY;
			Property = property;
		}

		public void Delete()
		{
			Type = TokenType.DELETED;
			Property = null;
			Whitespace = null;
		}

		public override string ToString()
		{
			switch (Type)
			{
				case TokenType.OPENPARAN: return "(";
				case TokenType.CLOSEPARAN: return ")";
				case TokenType.SEMICOMMA: return ";";
				case TokenType.WHITESPACE: return Whitespace;
				case TokenType.PROPERTY: return Property.ToString();
				case TokenType.DELETED: return string.Empty;
				default: return string.Empty;
			}
		}
	}
}
