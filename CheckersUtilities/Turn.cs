using System;
using System.Drawing;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace CheckersUtilities
{
	[Serializable]
	public class Turn
	{
		public Point From;
		public Point To;
		public Boolean Forfeit;

		public Turn(Point from, Point to, Boolean forfeit = false)
		{
			From = from;
			To = to;
			Forfeit = forfeit;
		}

		public static Turn FromBytes(byte[] bytes)
		{
			using (var memStream = new MemoryStream())
			{
				var binForm = new BinaryFormatter();
				memStream.Write(bytes, 0, bytes.Length);
				memStream.Seek(0, SeekOrigin.Begin);
				var obj = (Turn)binForm.Deserialize(memStream);
				return obj;
			}
		}

		public override string ToString()
		{
			string from = "from(" + From.X + ", " + From.Y + ")";
			string to = "to(" + To.X + ", " + To.Y + ")";
			string ff = Forfeit == true ? "forfiet=true" : "forfiet=false";
			return from + " | " + to + " | " + ff;
		}

		public byte[] ToBytes()
		{
			BinaryFormatter f = new BinaryFormatter();
			using (var ms = new MemoryStream())
			{
				f.Serialize(ms, this);
				return ms.ToArray();
			}
		}
	}
}
