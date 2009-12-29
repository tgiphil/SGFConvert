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
	public class SaveFile : ErrorManagement
	{
		protected StreamWriter Stream;

		public SaveFile()
		{
		}

		public void Write(char c)
		{
			try
			{
				Stream.Write(c);
			}
			catch (Exception e)
			{
				SetErrorMessage("ERROR: " + e.Message);
			}
		}

		public void WriteLine()
		{
			try
			{
				Stream.WriteLine();
			}
			catch (Exception e)
			{
				SetErrorMessage("ERROR: " + e.Message);
			}
		}

		public void WriteLine(char c)
		{
			try
			{
				Stream.WriteLine(c);
			}
			catch (Exception e)
			{
				SetErrorMessage("ERROR: " + e.Message);
			}
		}

		public void Write(string str)
		{
			try
			{
				Stream.Write(str);
			}
			catch (Exception e)
			{
				SetErrorMessage("ERROR: " + e.Message);
			}
		}

		public void Write(string str, int len)
		{
			try
			{
				Stream.WriteLine(str.Substring(0, len));
			}
			catch (Exception e)
			{
				SetErrorMessage("ERROR: " + e.Message);
			}
		}

		public void WriteLine(string str)
		{
			try
			{
				Stream.WriteLine(str);
			}
			catch (Exception e)
			{
				SetErrorMessage("ERROR: " + e.Message);
			}
		}

		public bool Create(string filename)
		{
			ClearErrorMessages();

			try
			{
				Stream = new StreamWriter(filename);
				return true;
			}
			catch (Exception e)
			{
				if (Stream != null)
					Stream.Close();
				return SetErrorMessage("ERROR: " + e.Message);
			}
		}

		public bool Create()
		{
			ClearErrorMessages();

			try
			{
				Stream = new StreamWriter(System.Console.OpenStandardOutput());
				return true;
			}
			catch (Exception e)
			{
				if (Stream != null)
					Stream.Close();
				return SetErrorMessage("ERROR: " + e.Message);
			}
		}

		public void Close()
		{
			Stream.Close();
			Stream = null;
		}

	}
}
