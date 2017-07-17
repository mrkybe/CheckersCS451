using System;
using static Data.STATE;

namespace Data
{
    public class CheckersGM : Game
    {
        private readonly Node[,] array = new Node[8, 8];
        private readonly Node[,] sparseArray = new Node[4, 8];

        public CheckersGM()
        {
            InitializeBoard();
            //PlacePieces();
            PlaceTestPieces();

            Console.WriteLine(DebugPrintArray());
            Console.WriteLine("\n");
            Console.WriteLine(DebugPrintSparseArray());
        }

        private void PlaceTestPieces()
        {
            PlacePiece(RED, 0, 0);
            PlacePiece(BLACK, 1, 1);
        }

        private void PlacePiece(STATE kind, int x, int y)
        {
            var n = sparseArray[x, y];
            n.SetState(kind);
        }

        private void PlacePieces()
        {
            for (int y = 0; y < 8; y++)
            {
                for (int x = 0; x < 4; x++)
                {
                    var s = EMPTY;
                    if (y <= 2)
                    {
                        s = RED;
                    }
                    else if (y >= 5)
                    {
                        s = BLACK;
                    }
                    PlacePiece(s, x, y);
                }
            }
        }

        private void InitializeBoard()
        {
            for (var y = 0; y < 8; y++)
            {
                for (var x = 0; x < 8; x++)
                {
                    if ((x % 2 == 0) ^ (y % 2 == 0))
                    {
                        var coords = new Tuple<int, int>(x, y);
                        var sparseCoords = ToSparseXY(coords);
                        var n = new Node(coords, sparseCoords);

                        array[x, y] = n;
                        if (sparseArray[sparseCoords.Item1, sparseCoords.Item2] != null)
                        {
                            throw new Exception("OOPS");
                        }
                        sparseArray[sparseCoords.Item1, sparseCoords.Item2] = n;
                    }
                }
            }
            LinkNodes();
        }

        private void LinkNodes()
        {
            for (var y = 0; y < 7; y++)
            {
                for (var x = 0; x < 4; x++)
                {
                    sparseArray[x, y].LinkTo(sparseArray[x, y + 1]);
                    if (x != 3)
                    {
                        sparseArray[x, y].LinkTo(sparseArray[x + 1, y + 1]);
                    }
                    if (x != 0)
                    {
                        //sparseArray[x, y].LinkTo(sparseArray[x - 1, y + 1]);
                    }
                }
            }
        }

        public Tuple<int, int> ToSparseXY(Tuple<int, int> coords)
        {
            var x = coords.Item1;
            var y = coords.Item2;
            if (y % 2 == 0)
            {
                x = (x - 1) / 2;
            }
            else
            {
                x = x / 2;
            }
            return new Tuple<int, int>(x, y);
        }

        public string DebugPrintArray()
        {
            var display = "";
            var space = "   ";
            for (var y = -1; y < 8; y++)
            {
                for (var x = -1; x < 8; x++)
                {
                    if (x != -1 && y != -1)
                    {
                        var s = " ";
                        if (array[x, y] != null)
                        {
                            s = array[x, y].GetStateString();
                        }
                        display += s + space;
                    }
                    else if (y != -1)
                    {
                        display += y + space;
                    }
                    else if (x == -1)
                    {
                        display += " " + space;
                    }
                    else if (x != -1)
                    {
                        display += x + space;
                    }
                }
                display += "\n\n";
            }
            return display;
        }

        public string DebugPrintSparseArray()
        {
            var display = "";
            var space = "   ";
            var connectedSpaceR = " \\ ";
            var connectedSpaceL = " / ";
            var connectedSpaceX = " X ";
            var linkedSpace = "---";
            for (var y = -1; y < 8; y++)
            {
                var connectionMapVertical = " " + space;
                for (var x = -1; x < 4; x++)
                {
                    var connectionMapHorizontal = space;
                    if (x != -1 && y != -1)
                    {
                        var s = " ";
                        s = sparseArray[x, y].GetStateString();
                        if (x != 3)
                        {
                            var linked = sparseArray[x, y].IsLinkedTo(sparseArray[x + 1, y]);
                            if (linked)
                            {
                                connectionMapHorizontal = linkedSpace;
                            }
                        }
                        var RightLink = false;
                        var LeftLink = false;
                        var DownLink = false;
                        if (y != 7 && x != 3)
                        {
                            RightLink = sparseArray[x, y].IsLinkedTo(sparseArray[x + 1, y + 1]);
                        }
                        if (y != 7)
                        {
                            DownLink = sparseArray[x, y].IsLinkedTo(sparseArray[x, y + 1]);
                        }
                        if (y != 7 && x != 0)
                        {
                            LeftLink = sparseArray[x, y].IsLinkedTo(sparseArray[x - 1, y + 1]);
                        }
                        if (LeftLink)
                        {
                            var previous = connectionMapVertical[connectionMapVertical.Length - 2];
                            connectionMapVertical = connectionMapVertical.Substring(0, connectionMapVertical.Length - space.Length);
                            if (previous == '\\')
                            {
                                connectionMapVertical += connectedSpaceX;
                            }
                            else
                            {
                                connectionMapVertical += connectedSpaceL;
                            }
                        }
                        if (DownLink)
                        {
                            connectionMapVertical += "|";
                        }
                        else
                        {
                            connectionMapVertical += " ";
                        }
                        if (RightLink)
                        {
                            connectionMapVertical += connectedSpaceR;
                        }
                        else
                        {
                            connectionMapVertical += space;
                        }
                        display += s + connectionMapHorizontal;
                    }
                    else if (y != -1)
                    {
                        display += y + space;
                    }
                    else if (x == -1)
                    {
                        display += " " + space;
                    }
                    else if (x != -1)
                    {
                        display += x + space;
                    }
                }
                if (y >= 0 && y < 7)
                {
                    display += "\n" + connectionMapVertical + "\n";
                    //display += "\n" + space + " | \\" + " | \\" + " | \\" + " |" + "\n";
                }
                else
                {
                    display += "\n\n";
                }
            }
            return display;
        }
    }
}
