using System;
using System.Collections.Generic;
using Data;
using System.Drawing;

namespace CheckersServer
{
    class Program
    {
        static void Main(string[] args)
        {
            //var games = new List<CheckersGM> {new CheckersGM()};
            CheckersGM gm = new CheckersGM();
            while (true)
            {
                try
                {
                    gm.DebugPrintFullBoard();
                    Console.Write("ENTER MOVE>");
                    var res = ParseCommand(Console.ReadLine());
                    gm.DebugPrintQueryNode(res.Item1);
                    if (res.Item1 != res.Item2)
                    {
                        gm.MakeTurn(res.Item1, res.Item2);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.StackTrace);
                }
            }
        }

        static Tuple<Point, Point> ParseCommand(string cmd)
        {
            var values = cmd.Split(',');
            if (values.Length == 4)
            {
                var x1 = int.Parse(values[0]);
                var y1 = int.Parse(values[1]);
                var x2 = int.Parse(values[2]);
                var y2 = int.Parse(values[3]);
                var result = new Tuple<Point, Point>(new Point(x1, y1), new Point(x2, y2));
                return result;
            }
            else
            {
                var x1 = int.Parse(values[0]);
                var y1 = int.Parse(values[1]);
                var result = new Tuple<Point, Point>(new Point(x1, y1), new Point(x1, y1));
                return result;
            }
        }
    }
}
