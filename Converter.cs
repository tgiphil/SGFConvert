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
	class Converter : ErrorManagement
	{
		protected string InputFileName = string.Empty;
		protected string OutputFileName = string.Empty;

		protected int BoardSize = 0;
		protected int DefaultBoardSize = 0;
		protected bool RemoveWhiteSpace = false;
		protected bool ConvertToSGFCoords = false;
		protected bool ConvertToGoCoords = false;
		protected bool Pretty = false;

		public bool ParseOptions(string[] args)
		{
			foreach (string lArg in args)
			{
				string lOption = lArg;
				string lParameter = string.Empty;
				int lPos = lOption.IndexOf(':');

				if (lPos > 0)
				{
					lParameter = lArg.Substring(lPos + 1);
					lOption = lArg.Substring(0, lPos);
				}

				if ((lOption[0] != '-') && (lOption[0] != '/'))
					return false;

				lOption = lOption.Substring(1);

				switch (lOption)
				{
					case "rws": RemoveWhiteSpace = true; break;
					case "2s": ConvertToSGFCoords = true; break;
					case "2g": ConvertToGoCoords = true; break;
					case "pretty": Pretty = true; break;
					case "o": OutputFileName = lParameter.Trim('"'); break;
					case "i": InputFileName = lParameter.Trim('"'); break;
					case "b": DefaultBoardSize = Convert.ToInt32(lParameter); break;
					default: return false;
				}
			}

			return true;
		}

		public bool Process()
		{
			BoardSize = DefaultBoardSize;
			ReadFile lInput = new ReadFile();

			if (string.IsNullOrEmpty(InputFileName))
				lInput.OpenConsole();
			else
				lInput.OpenFile(InputFileName);

			if (lInput.IsError())
				return SetErrorMessage(lInput);

			SGFTokens lTokens = SGFScanner.Tokenize(lInput);

			if (lTokens.IsError())
				return SetErrorMessage(lTokens);

			if (RemoveWhiteSpace)
				lTokens.RemoveAllWhiteSpace();

			lTokens.ProcessProperties(ProcessProperty);

			if (Pretty)
				lTokens.Pretty();

			SaveFile lOutput = new SaveFile();

			try
			{			
				if (string.IsNullOrEmpty(OutputFileName))
					lOutput.Create();
				else
					lOutput.Create(OutputFileName);

				if (lOutput.IsOk())
					lTokens.ToFile(lOutput);

				if (lOutput.IsError())
					return SetErrorMessage(lOutput);
			}
			finally
			{
				lOutput.Close();	
			}
			return true;
		}

		public void ProcessProperty(Property property)
		{
			property.Name = property.Name.Trim();
			property.Value = property.Value.Trim();

			if (property.Name == "SZ")
			{
				Int32.TryParse(property.Value, out BoardSize);
			}
			else
			{
				if (!ConvertToSGFCoords && !ConvertToGoCoords)
					return;

				SGFPropertyInfo lSGFPropertyInfo = SGFInfo.GetSGFPropertyInfo(property.Name);

				if (lSGFPropertyInfo == null)
					return;

				if ((lSGFPropertyInfo.PropertyValueType != SGFPropertyValueType.Stone) &&
					(lSGFPropertyInfo.PropertyValueType != SGFPropertyValueType.Point) &&
					(lSGFPropertyInfo.PropertyValueType != SGFPropertyValueType.Move))
					return;

				string lValue = property.Value;
				string lValue1 = lValue;
				string lValue2 = string.Empty;

				int lPos = lValue.IndexOf(':');
				if (lPos > 0)
				{
					lValue1 = lValue.Substring(0, lPos - 1);
					lValue2 = lValue.Substring(lPos + 1);
				}
				else if (lPos == 0)
				{
					lValue1 = string.Empty;
					lValue2 = lValue1.Substring(1);
				}

				Coords lCoord = new Coords(lValue1, BoardSize);

				if (ConvertToSGFCoords)
					property.Value = lCoord.ToSGFCoords(BoardSize);
				else
					if (ConvertToGoCoords)
						property.Value = lCoord.ToGoCoords();

				if (lValue2 != string.Empty)
					property.Value = property.Value + ":" + lValue2;

				if (lSGFPropertyInfo.List)
				{
					for (int i = 0; i < property.AdditionalValues.Count; i++)
					{
						string lAddtValue = property.AdditionalValues[i];

						lCoord = new Coords(lAddtValue, BoardSize);

						if (ConvertToSGFCoords)
							lAddtValue = lCoord.ToSGFCoords(BoardSize);
						else
							if (ConvertToGoCoords)
								lAddtValue = lCoord.ToGoCoords();

						property.AdditionalValues[i] = lAddtValue;
					}
				}

			}

		}


	}
}
