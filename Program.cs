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
	class Program
	{
		static int Main(string[] args)
		{
			Converter lConverter = new Converter();

			if (!lConverter.ParseOptions(args))
			{
				Console.Error.WriteLine("ERROR: Invalid options");
				return -1;
			}

			if (!lConverter.Process())
			{
				Console.Error.WriteLine(lConverter.GetErrorMessage());
				return -1;
			}

			return 0;
		}

	}
}
