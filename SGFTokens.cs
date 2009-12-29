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
	class SGFTokens : ErrorManagement
	{
		public delegate void ProcessProperty(Property property);

		protected List<SGFToken> Tokens = new List<SGFToken>();

		public int Count
		{
			get
			{
				return Tokens.Count;
			}
		}

		public SGFToken this[int arg]
		{
			get
			{
				return Tokens[arg];
			}
		}

		public System.Collections.IEnumerator GetEnumerator()
		{
			foreach (SGFToken lSGFToken in Tokens)
				yield return lSGFToken;
		}

		public void Add(SGFToken token)
		{
			Tokens.Add(token);
		}

		public void AddWhitespace(string whitespace)
		{
			if (Tokens.Count > 0)
			{
				SGFToken lSGFToken = Tokens[Tokens.Count - 1];
				if (lSGFToken.Type == TokenType.WHITESPACE)
				{
					lSGFToken.Whitespace = lSGFToken.Whitespace + whitespace;
					return;
				}
			}

			Add(new SGFToken(whitespace));
		}

		public void RemovePreviousWhitespace()
		{
			while ((Tokens.Count > 0) && (Tokens[Tokens.Count - 1].Type == TokenType.WHITESPACE))
				Tokens.RemoveAt(Tokens.Count - 1);
		}

		public void RemoveAllWhiteSpace()
		{
			foreach (SGFToken lSGFToken in Tokens)
				if (lSGFToken.Type == TokenType.WHITESPACE)
					lSGFToken.Delete();

			//List<SGFToken> lNewList = new List<SGFToken>(Tokens.Count);
			//
			//foreach (SGFToken lSGFToken in Tokens)
			//	if (lSGFToken.Type != TokenType.WHITESPACE)
			//		lNewList.Add(lSGFToken);
			//
			//Tokens = lNewList;
		}

		public SGFToken LastToken()
		{
			if (Tokens.Count == 0)
				return null;

			return Tokens[Tokens.Count - 1];
		}

		public void ToFile(SaveFile lSaveFile)
		{
			foreach (SGFToken lSGFToken in Tokens)
				if (lSGFToken.Type != TokenType.DELETED)
					lSaveFile.Write(lSGFToken.ToString());
		}

		public void ProcessProperties(ProcessProperty processProperty)
		{
			foreach (SGFToken lSGFToken in Tokens)
				if (lSGFToken.Type == TokenType.PROPERTY)
					processProperty(lSGFToken.Property);
		}

		public void Pretty()
		{
			List<SGFToken> lNewList = new List<SGFToken>(Tokens.Count);
			SGFToken lSGFNewLineToken = new SGFToken(Environment.NewLine);

			int lParamCnt = 0;
			int lLineLen = 0;

			foreach (SGFToken lSGFToken in Tokens)
			{
				if (lSGFToken.Type == TokenType.OPENPARAN)
				{
					lParamCnt++;
					lNewList.Add(lSGFToken);
					lLineLen = lLineLen + lSGFToken.ToString().Length;
				}
				else if (lSGFToken.Type == TokenType.CLOSEPARAN)
				{
					lParamCnt--;
					lNewList.Add(lSGFToken);
					lNewList.Add(lSGFNewLineToken);
					lLineLen = 0;
				}
				else if (lSGFToken.Type == TokenType.SEMICOMMA)
				{
					lNewList.Add(lSGFToken);
				}
				else if (lSGFToken.Type == TokenType.PROPERTY)
				{
					bool lNewLine = false;
					SGFPropertyInfo lSGFPropertyInfo = SGFInfo.GetSGFPropertyInfo(lSGFToken.Property.Name);

					if (lSGFPropertyInfo == null)
						lNewLine = true;
					else
						if ((lSGFPropertyInfo.PropertyType == SGFPropertyType.GameInfo) || (lSGFPropertyInfo.PropertyType == SGFPropertyType.None))
							lNewLine = true;

					if (lLineLen + lSGFToken.ToString().Length > 65)
						lNewLine = true;

					if (lNewLine)
					{
						lNewList.Add(lSGFNewLineToken);
						lLineLen = 0;
					}

					lNewList.Add(lSGFToken);
					lLineLen = lLineLen + lSGFToken.ToString().Length;
				}
			}

			Tokens = lNewList;
		}


	}
}
