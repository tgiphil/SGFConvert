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
using GoTraxx;

namespace SGFConvert
{
	static class SGFScanner
	{
		public static char EscapeCharacter = '\\';
		public static string WhitespaceCharacters = "\t\n\r\f ";

		public static SGFTokens Tokenize(ReadFile input)
		{
			SGFTokens lTokens = new SGFTokens();

			if (!input.IsOk())
			{
				lTokens.SetErrorMessage(input.GetErrorMessage());
				return lTokens;
			}

			while (!input.EOF)
			{
				char c = input.Peek();

				if (Char.IsLetter(c))
				{
					Property lProperty = ReadProperty(input);

					if (lProperty == null)
					{
						lTokens.SetErrorMessage("ERROR: unable to parse sgf file");
						return lTokens;
					}

					lTokens.Add(new SGFToken(lProperty));
				}
				else
				{
					input.Get(); // read past the character

					if (c == '[')
					{
						lTokens.RemovePreviousWhitespace();

						SGFToken lSGFToken = lTokens.LastToken();

						if ((lSGFToken == null) || (lSGFToken.Type != TokenType.PROPERTY))
						{
							lTokens.SetErrorMessage("ERROR: unable to parse sgf file");
							return lTokens;
						}

						string lValue = ReadPropertyValue(input);

						lSGFToken.Property.AdditionalValues.Add(lValue);
					}
					else if (c == '(')
						lTokens.Add(new SGFToken(TokenType.OPENPARAN));
					else if (c == ')')
						lTokens.Add(new SGFToken(TokenType.CLOSEPARAN));
					else if (c == ';')
						lTokens.Add(new SGFToken(TokenType.SEMICOMMA));
					else
						if (WhitespaceCharacters.IndexOf(c) >= 0)
							lTokens.AddWhitespace(c.ToString());
				}
			}

			return lTokens;
		}

		public static Property ReadProperty(ReadFile input)
		{
			string lName = string.Empty;
			string lValue = string.Empty;

			while (!input.EOF)
			{
				char c = input.Get();

				if (Char.IsLetter(c))
					lName = lName + c.ToString();

				if (c == '[')
				{
					lValue = ReadPropertyValue(input);

					return new Property(lName, lValue);
				}
			}

			return null;
		}

		public static string ReadPropertyValue(ReadFile input)
		{
			string lValue = string.Empty;
			bool lEscaped = false;

			while (!input.EOF)
			{
				char c = input.Get();

				if ((c == ']') && (!lEscaped))
					return lValue;

				lValue = lValue + c.ToString();

				if (lEscaped)
					lEscaped = false;
				else
					if (c == EscapeCharacter)
						lEscaped = true;
			}

			return lValue;
		}


	}
}
