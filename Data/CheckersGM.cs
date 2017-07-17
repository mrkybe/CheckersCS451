using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using System.Linq;
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
            Console.WriteLine(DebugPrintNodes(GetAvailableEmptyPositionsFor(new Point(1, 1)), "EMPTY"));
            Console.WriteLine(DebugPrintNodes(GetAvailableJumps(new Point(1, 1)), "JUMPS"));
        }

        private void PlaceTestPieces()
        {
            PlacePiece(BLACK, 0, 0);
            PlacePiece(RED, 1, 1);
            PlacePiece(BLACK, 0, 1);
            PlacePiece(BLACK, 0, 2);
            PlacePiece(BLACK, 2, 2);
        }

        private string DebugPrintNodes(List<Node> points, string title = "")
        {
            string s = title + "\n";
            foreach (Node n in points)
            {
                Point p = n.GetSparsePosition();
                s += n.GetStateString() + "(" + p.X + ", " + p.Y + ")";
                s += "\n";
            }
            return s;
        }

        private ReadOnlyCollection<Node> GetSpacesToCheck(Point pos)
        {
            return sparseArray[pos.X, pos.Y].GetNeighbors();
        }

        private List<Node> GetAvailableEmptyPositionsFor(Node n)
        {
            return GetAvailableEmptyPositionsFor(n.GetSparsePosition());   
        }

        private List<Node> GetAvailableEmptyPositionsFor(Point pos)
        {
            Node n = sparseArray[pos.X, pos.Y];
            STATE piece = n.GetState();
            var neighbors = GetSpacesToCheck(pos);
            if (piece == EMPTY)
            {
                return new List<Node>();
                // maybe we should throw an exception?  or actually return a list of empty neighbors?
            }
            else if (piece == RED)
            {
                return neighbors.Where(x => x.GetState() == EMPTY &&
                                            x.GetSparsePosition().Y < n.GetSparsePosition().Y
                                            ).ToList();
            }
            else if (piece == BLACK)
            {
                return neighbors.Where(x => x.GetState() == EMPTY// &&
                                            //x.GetSparsePosition().Y > n.GetSparsePosition().Y
                                            ).ToList();
            }
            else if (piece == KING_RED || piece == KING_BLACK)
            {
                return neighbors.Where(x => x.GetState() == EMPTY).ToList();
            }
            else
            {
                throw new NullReferenceException("");
            }
        }

        private bool IsEmpty(Point pos)
        {
            if (pos.X < 0 || pos.X > 3)
                return false;
            if (pos.Y < 0 || pos.Y > 8)
                return false;
            return sparseArray[pos.X, pos.Y].GetState() == EMPTY;
        }

        private List<Node> GetAvailableJumps(Point pos)
        {
            Node n = sparseArray[pos.X, pos.Y];
            STATE piece = n.GetState();
            var neighbors = GetSpacesToCheck(pos);

            List<Node> validJumps = new List<Node>();
            if (piece == EMPTY)
            {
                return validJumps;
                // maybe we should throw an exception?  or actually return a list of empty neighbors?
            }
            else if (piece == RED)
            {
                var candidates = neighbors.Where(x => (x.GetState() == BLACK || x.GetState() == KING_BLACK) &&
                                                       x.GetSparsePosition().Y < n.GetSparsePosition().Y &&
                                                       GetAvailableEmptyPositionsFor(x).Count > 0
                                                       ).ToList();
                foreach (var candidate in candidates)
                {
                    Point candidatePos = candidate.GetSparsePosition();
                    int jumpVectorX = candidatePos.X - n.GetSparsePosition().X;
                    int jumpVectorY = candidatePos.Y - n.GetSparsePosition().Y;
                    Point jumpPos = new Point(candidatePos.X + jumpVectorX, candidatePos.Y + jumpVectorY);
                    if (IsEmpty(jumpPos))
                    {
                        validJumps.Add(candidate);
                    }
                }
                return validJumps;
            }
            else if (piece == BLACK)
            {
                var candidates = neighbors.Where(x => (x.GetState() == RED || x.GetState() == KING_RED) &&
                                                       x.GetSparsePosition().Y > n.GetSparsePosition().Y &&
                                                       GetAvailableEmptyPositionsFor(x).Count > 0
                                                       ).ToList();
                foreach (var candidate in candidates)
                {
                    Point candidatePos = candidate.GetSparsePosition();
                    int jumpVectorX = candidatePos.X - n.GetSparsePosition().X;
                    int jumpVectorY = candidatePos.Y - n.GetSparsePosition().Y;
                    Point jumpPos = new Point(candidatePos.X + jumpVectorX, candidatePos.Y + jumpVectorY);
                    if (IsEmpty(jumpPos))
                    {
                        validJumps.Add(candidate);
                    }
                }
                return validJumps;
            }
            else if (piece == KING_RED)
            {
                var candidates = neighbors.Where(x => (x.GetState() == BLACK || x.GetState() == KING_BLACK) &&
                                                       GetAvailableEmptyPositionsFor(x).Count > 0
                                                       ).ToList();
                foreach (var candidate in candidates)
                {
                    Point candidatePos = candidate.GetSparsePosition();
                    int jumpVectorX = candidatePos.X - n.GetSparsePosition().X;
                    int jumpVectorY = candidatePos.Y - n.GetSparsePosition().Y;
                    Point jumpPos = new Point(candidatePos.X + jumpVectorX, candidatePos.Y + jumpVectorY);
                    if (IsEmpty(jumpPos))
                    {
                        validJumps.Add(candidate);
                    }
                }
                return validJumps;
            }
            else if (piece == KING_BLACK)
            {
                var candidates = neighbors.Where(x => (x.GetState() == RED || x.GetState() == KING_RED) &&
                                                       GetAvailableEmptyPositionsFor(x).Count > 0
                                                       ).ToList();
                foreach (var candidate in candidates)
                {
                    Point candidatePos = candidate.GetSparsePosition();
                    int jumpVectorX = candidatePos.X - n.GetSparsePosition().X;
                    int jumpVectorY = candidatePos.Y - n.GetSparsePosition().Y;
                    Point jumpPos = new Point(candidatePos.X + jumpVectorX, candidatePos.Y + jumpVectorY);
                    if (IsEmpty(jumpPos))
                    {
                        validJumps.Add(candidate);
                    }
                }
                return validJumps;
            }
            return null;
        }

        private bool CheckForStalemate() { return false; }


        private void PlacePiece(STATE kind, int x, int y)
        {
            var n = sparseArray[x, y];
            n.SetState(kind);
        }

        private void PlacePiece(STATE kind, Point pos)
        {
            PlacePiece(kind, pos.X, pos.Y);
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
                        var coords = new Point(x, y);
                        var sparseCoords = ToSparseXY(coords);
                        var n = new Node(coords, sparseCoords);

                        array[x, y] = n;
                        if (sparseArray[sparseCoords.X, sparseCoords.Y] != null)
                        {
                            throw new Exception("OOPS");
                        }
                        sparseArray[sparseCoords.X, sparseCoords.Y] = n;
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

        public Point ToSparseXY(Point coords)
        {
            var x = coords.X;
            var y = coords.Y;
            if (y % 2 == 0)
            {
                x = (x - 1) / 2;
            }
            else
            {
                x = x / 2;
            }
            return new Point(x, y);
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
