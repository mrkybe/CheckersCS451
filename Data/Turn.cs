using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
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

        public override string ToString()
        {
            string from = "from(" + From.X + ", " + From.Y + ")";
            string to = "to(" + To.X + ", " + To.Y + ")";
            string ff = Forfeit == true ? "forfiet=true" : "forfiet=false";
            return from + " | " + to + " | " + ff;
        }
    }
}
