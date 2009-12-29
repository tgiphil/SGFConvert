/*
 * Copyright (c) 2007 Philipp Garcia (phil@gotraxx.org)
 * 
 * This file is part of GoTraxx (www.gotraxx.org).
 * 
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, 
 * INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A 
 * PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT 
 * HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION 
 * OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE 
 * SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
 * 
 * This license governs use of the accompanying software. If you use the software, you 
 * accept this license. If you do not accept the license, do not use the software.
 * 
 * Permission is granted to anyone to use this software for any noncommercial purpose, 
 * and to alter it and redistribute it freely, subject to the following restrictions:
 * 
 * 1. The origin of this software must not be misrepresented; you must not claim that 
 *    you wrote the original software. 
 * 
 * 2. Altered source versions must be plainly marked as such, and must not be 
 *    misrepresented as being the original software.
 * 
 * 3. If you bring a patent claim against the original author or any contributor over 
 *    patents that you claim are infringed by the software, your patent license from 
 *    such contributor to the software ends automatically.
 * 
 * 4. This software may not be used in whole, nor in part, to enter any competition 
 *    without written permission from the original author. 
 * 
 * 5. This notice may not be removed or altered from any source distribution.
 * 
 */

using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace GoTraxx
{
	public class ReadFile : ErrorManagement
	{
		protected StreamReader Stream;

		protected bool _EOF = true;

		public ReadFile()
		{
		}

		public ReadFile(string filename)
		{
			OpenFile(filename);
		}

		public bool EOF
		{
			get
			{
				return _EOF;
			}
		}

		public char Get()
		{
			if (EOF)
				return '\0';

			try
			{
				int c = Stream.Read();

				if (c < 0)
				{
					_EOF = true;

					Stream.Close();

					return '\0';
				}

				return (char)c;
			}
			catch (Exception e)
			{
				SetErrorMessage("ERROR: " + e.Message);
				_EOF = true;
				return '\0';
			}
		}

		public char Peek()
		{
			if (EOF)
				return '\0';

			try
			{
				int c = Stream.Peek();

				if (c < 0)
				{
					_EOF = true;
					Stream.Close();
					return '\0';
				}

				return (char)c;
			}
			catch (Exception e)
			{
				SetErrorMessage("ERROR: " + e.Message);
				_EOF = true;
				return '\0';
			}
		}

		public void Get(ref char c)
		{
			c = Get();
		}

		public string ReadLine(char eol, int max)
		{
			StringBuilder Line = new StringBuilder(max);

			while ((Line.Length < max) && (!EOF))
			{
				char c = Get();
				if (c != eol)
					Line.Append(Get());
			}

			return Line.ToString();
		}

		public string ReadLine(char eol)
		{
			StringBuilder Line = new StringBuilder(1024);

			while (!EOF)
			{
				char c = Get();
				if (c != eol)
					Line.Append(Get());
			}

			return Line.ToString();
		}

		public string ReadLine()
		{
			return ReadLine('\n');
		}

		public string ReadPart(char seperator, char eol)
		{
			StringBuilder Line = new StringBuilder(1024);

			while ((!EOF))
			{
				char c = Get();

				if (c == eol)
					break;
				if (c == seperator)
					break;

				Line.Append(Get());
			}

			return Line.ToString();
		}

		public string ReadPart(char seperator)
		{
			return ReadPart(seperator, '\n');
		}

		public bool OpenFile(string filename)
		{
			ClearErrorMessages();
			_EOF = true;
			try
			{
				Stream = new StreamReader(filename);
				_EOF = false;
				return true;
			}
			catch (Exception e)
			{
				return SetErrorMessage("ERROR: " + e.Message);
			}
		}

		public bool OpenConsole()
		{
			ClearErrorMessages();
			_EOF = true;
			try
			{
				Stream = new StreamReader(System.Console.OpenStandardInput());
				_EOF = false;
				return true;
			}
			catch (Exception e)
			{
				return SetErrorMessage("ERROR: " + e.Message);
			}
		}
	}
}
