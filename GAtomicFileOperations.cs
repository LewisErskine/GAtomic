using System;
using System.IO;
using System.Text;

namespace GLDoors2.GAtomic.objects
{
	/// <summary>
	/// Summary description for GLDoorsFileOperations.
	/// </summary>
	public class GLDoorsFileOperations
	{
		#region VARIABLES
		private FileStream fs;
		private String filename;
		#endregion

		#region CONTRUCTOR
		public GLDoorsFileOperations(String filename_arg)
		{
			filename = filename_arg;
		}
		#endregion

		#region CreateOpenClose
		public void OpenFileForRead ()
		{
			fs = File.Open (filename, FileMode.Open, FileAccess.Read, FileShare.None);
		}

		public void OpenFileForWrite ()
		{
			fs = File.Open (filename, FileMode.Append, FileAccess.Write, FileShare.None);
		}

		public void CreateFile ()
		{
			fs = File.Create (filename);
		}

		public void CloseFile ()
		{
			fs.Close ();
		}
		#endregion

		#region ReadWrite

		public int ReadIntFromFile ()
		{
			/// Get the number of bytes representing the integer
			Byte[] num_byte = new Byte[1];
			if (fs.Read (num_byte, 0, 1) == 0)
				return -1;

			int bytes = Convert.ToInt32 (num_byte[0]);

			/// Read the bytes, they start with the highest order one
			/// then calclulate the integer value
			Byte[] out_byte = new Byte[bytes];
			fs.Read(out_byte, 0, bytes);
			
			int val = 0;
			int temp = 0;
			for (int l = bytes; l > 0; l--)
			{
				temp = (int)Math.Pow (16, (l * 2 - 1));
				val += (Convert.ToInt32 (out_byte[bytes - l]) / 16) * temp;
				temp = (int)Math.Pow (16, (l * 2 - 2));
				val += (Convert.ToInt32 (out_byte[bytes - l]) % 16) * temp;
			}

			return val;
		}

		public bool WriteIntToFile (int val)
		{
			int bytes = 1;
			int temp = 0;

			int ind = 1;
			temp = (int)Math.Pow (16, (ind++ * 2 - 1));
			while (val / temp >= 16)
			{
				bytes++;
				temp = (int)Math.Pow (16, (ind++ * 2 - 1));
			}

			Byte[] info = new Byte[bytes + 1];

			/// Write how many bytes represent the integer
			info[0] = Convert.ToByte (bytes);
			/// Write the Bytes, starting from the highest order byte
			for (int index = bytes; index > 0; index--)
			{
				temp = (int)Math.Pow (16, (index * 2 - 1));
				int hexB = 0;
				int hexS = 0;
				hexB = val / temp;
				if (hexB > 0)
					val -= hexB * temp;
				temp = (int)Math.Pow (16, (index * 2 - 2));
				hexS = val / temp;
				if (hexS > 0)
					val -= hexS * temp;
					
				info [bytes - index + 1] = Convert.ToByte (hexB * 16 + hexS);
			}
			
			/// Write into the file.
			fs.Write (info, 0, info.Length);
			
			return true;
		}

		public bool WriteStringToFile (String val)
		{
			Encoding unicode_encoding = Encoding.Unicode;
			Byte[] val_bytes = unicode_encoding.GetBytes (val);
			
			/// Write into the file.
			fs.Write (val_bytes, 0, val_bytes.Length);
			
			return true;
		}

		/*public bool WriteStringToFile (String val)
		{
			Encoding unicode_encoding = Encoding.Unicode;
			Byte[] val_bytes = unicode_encoding.GetBytes (val);
			
			int bytes = val_bytes.Length;
			
			WriteIntToFile (bytes);

			/// Write into the file.
			fs.Write (val_bytes, 0, val_bytes.Length);
			
			return true;
		}

		public String ReadStringFromFile ()
		{
			/// Get the number of bytes representing the String
			int bytes = ReadIntFromFile ();

			/// Read the bytes, they start with the highest order one
			/// then calclulate the integer value
			Byte[] out_byte = new Byte[bytes];
			fs.Read(out_byte, 0, bytes);
			
			Encoding unicode_encoding = Encoding.Unicode;
			String val = unicode_encoding.GetString (out_byte);

			return val;
		}*/

		public bool ReadStringLineFromFile (out String line, out bool line_following)
		{
			line = "";
			line_following = false;

			Byte[] out_byte = new Byte[2];
			
			Encoding unicode_encoding = Encoding.Unicode;
			String character = "";
			while (fs.Read (out_byte, 0, 2) != 0)
			{
				if (line == null)
					line = "";
				//if (out_byte[0] == 0)
				// continue;
				//out_byte[1] = (Byte)0;
				if (out_byte[0] != (Byte)13)	// \r
				{
					character = unicode_encoding.GetString (out_byte);
					line += character;
				}
				else
				{
					//fs.Read (out_byte, 0, 1);
					// read \n
					//fs.Read (out_byte, 0, 1);
					fs.Read (out_byte, 0, 2);

					line_following = true;

					break;
				}
			}

			if (line == "" && !line_following)
				return false;

			return true;
			
		}
		#endregion
	}
}
