using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using static Data.State;

namespace Data
{
    [Serializable]
    public class CheckersGM : Game
    {
        private readonly Node[,] array = new Node[8, 8];
        private readonly Node[,] sparseArray = new Node[4, 8];
        private readonly List<Node> nodes = new List<Node>();
        private List<Node> jumpedPieces = new List<Node>();
        private Node activePiece = null;
        private int turnNumber = 0;
        private GameState gameState = GameState.PLAY;

        public enum Player
        {
            PLAYER_RED,
            PLAYER_BLACK,
            NULL
        };

        public enum GameState
        {
            PLAY, STALEMATE, RED_WIN, BLACK_WIN            
        }

        public int Turn => turnNumber;
        public Node[,] SparseArray => sparseArray;

        private Player currentPlayerTurn = Player.PLAYER_BLACK;

        public Player PieceToPlayer(State piece)
        {
            if (piece == BLACK || piece == KING_BLACK)
            {
                return Player.PLAYER_BLACK;
            }
            else if (piece == RED || piece == KING_RED)
            {
                return Player.PLAYER_RED;
            }
            else
            {
                return Player.NULL;
                //throw new Exception("Invalid piece for matching to player, did you pass an empty space?");
            }
        }

        public Player MakeTurn(Turn turn)
        {
            Point from = turn.From;
            Point to = turn.To;
            Node nFrom = sparseArray[from.X, from.Y];
            if (activePiece != null && nFrom != activePiece)
            {
                throw new Exception("Can't move this piece, must use locked piece instead");
            }
            if (PieceToPlayer(nFrom.GetState()) != currentPlayerTurn)
            {
                Console.WriteLine("Attempted to move piece of wrong player!");
                return currentPlayerTurn;
            }
            Node nTo = sparseArray[to.X, to.Y];

            var availableMoves = GetAvailableEmptyPositionsFor(from);
            var availableJumps = GetAvailableJumps(from);

            if (availableMoves.Contains(nTo))
            {
                // Actually move the piece
                MovePiece(from, to);
                return EndTurn();
            }
            else if (availableJumps.Contains(nTo))
            {
                // Jump the piece, check for available jumps, if there are any, don't switch players.
                // Lock the player to only moving this piece
                Point landingPosition = GetLandingPosition(from, to);
                if (IsEmpty(landingPosition))
                {
                    MovePiece(from, landingPosition);
                    jumpedPieces.Add(nTo);
                }
                else
                {
                    throw new Exception("Invalid Jump!");
                }
                var newAvailableJumps = GetAvailableJumps(landingPosition);
                if (newAvailableJumps.Count == 0)
                {
                    return EndTurn();
                }
                else
                {
                    activePiece = sparseArray[landingPosition.X, landingPosition.Y];
                }
            }

            // Check for stalemate, probably.
            return currentPlayerTurn;
        }

        private Point GetLandingPosition(Point from, Point to)
        {
            Node nFrom = sparseArray[from.X, from.Y];
            Node nTo = sparseArray[to.X, to.Y];

            var from_normal = nFrom.GetNormalPosition();
            var to_normal = nTo.GetNormalPosition();

            int jumpVectorXN = to_normal.X - from_normal.X;
            int jumpVectorYN = to_normal.Y - from_normal.Y;

            jumpVectorXN += to_normal.X;
            jumpVectorYN += to_normal.Y;

            return ToSparseXY(new Point(jumpVectorXN, jumpVectorYN));
        }

        private void CheckThatMovesAreAvailable()
        {
            var pieces = nodes.Where(x => PieceToPlayer(x.GetState()) == currentPlayerTurn);
            List<Node> availableMoves = new List<Node>();
            foreach (var piece in pieces)
            {
                availableMoves.AddRange(GetAvailableEmptyPositionsFor(piece));
                availableMoves.AddRange(GetAvailableJumps(piece.GetSparsePosition()));
            }
            if (availableMoves.Count == 0)
            {
                if (currentPlayerTurn == Player.PLAYER_BLACK)
                {
                    gameState = GameState.RED_WIN;
                }
                else
                {
                    gameState = GameState.BLACK_WIN;
                }
            }
        }

        private void MovePiece(Point from, Point to)
        {
            Node nFrom = sparseArray[from.X, from.Y];
            Node nTo = sparseArray[to.X, to.Y];

            if (nFrom.GetState() == EMPTY || nTo.GetState() != EMPTY)
            {
                throw new Exception("Bad move attempted!");
            }

            nTo.SetState(nFrom.GetState());
            nFrom.SetState(EMPTY);
        }

        private Player EndTurn()
        {
            Console.WriteLine("ENDING TURN!");
            activePiece = null;
            foreach (Node n in jumpedPieces)
            {
                n.SetState(EMPTY);
            }
            jumpedPieces.Clear();
            turnNumber++;
            Player nextPlayer = currentPlayerTurn;
            if (currentPlayerTurn == Player.PLAYER_RED)
            {
                nextPlayer = Player.PLAYER_BLACK;
            }
            else
            {
                nextPlayer = Player.PLAYER_RED;
            }
            currentPlayerTurn = nextPlayer;
            CheckThatMovesAreAvailable();
            return nextPlayer;
        }

        public CheckersGM()
        {
            InitializeBoard();
            PlacePieces();
            //PlaceTestPiecesBasic();
            /*
            Console.WriteLine(DebugPrintArray());
            Console.WriteLine(DebugPrintSparseArray());
            Console.WriteLine(activePiece == null ? "No active piece" : activePiece.GetStateString());
            MakeTurn(new Point(0, 0), new Point(1, 1));
            Console.WriteLine(DebugPrintArray());
            Console.WriteLine(DebugPrintSparseArray());
            Console.WriteLine(DebugPrintNodes(GetAvailableEmptyPositionsFor(new Point(1, 2)), "EMPTY"));
            Console.WriteLine(DebugPrintNodes(GetAvailableJumps(new Point(1, 2)), "JUMPS"));
            Console.WriteLine(activePiece == null ? "No active piece" : activePiece.GetStateString());
            MakeTurn(new Point(1, 2), new Point(2, 3));
            Console.WriteLine(DebugPrintArray());
            Console.WriteLine(DebugPrintSparseArray());
            Console.WriteLine(activePiece == null ? "No active piece" : activePiece.GetStateString());*/
        }

        public void DebugPrintFullBoard()
        {
            string gameStateString = "PLAYING";
            if (gameState == GameState.RED_WIN)
            {
                gameStateString = "RED WIN";
            }
            else if (gameState == GameState.BLACK_WIN)
            {
                gameStateString = "BLACK WIN";
            }
            Console.WriteLine("--- " + turnNumber + " --- PLAYER: " + (currentPlayerTurn == Player.PLAYER_RED ? "RED" : "BLACK") + " ------------- " + (gameStateString));
            //Console.WriteLine(DebugPrintSparseArray());
            Console.WriteLine(DebugPrintArray());
        }

        public void DebugPrintQueryNode(Point p)
        {
            Console.WriteLine(DebugPrintNodes(GetSpacesToCheck(p), "CONNECTIONS"));
            Console.WriteLine(DebugPrintNodes(GetAvailableEmptyPositionsFor(p), "EMPTY"));
            Console.WriteLine(DebugPrintNodes(GetAvailableJumps(p), "JUMPS"));
        }

        private void PlaceTestPiecesBasic()
        {
            PlacePiece(BLACK, 3, 0);
            PlacePiece(RED, 3, 2);
            PlacePiece(RED, 2, 2);
        }

        private void PlaceTestPiecesKing()
        {
            PlacePiece(KING_BLACK, 2, 2);
            PlacePiece(RED, 1, 1);
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

        private List<Node> GetSpacesToCheck(Point pos)
        {
            var f = new List<Node>(sparseArray[pos.X, pos.Y].GetNeighbors());
            return f;
        }

        private List<Node> GetAvailableEmptyPositionsFor(Node n)
        {
            return GetAvailableEmptyPositionsFor(n.GetSparsePosition());
        }

        private List<Node> GetAvailableEmptyPositionsForJumpingFor(Node n)
        {
            return GetAvailableEmptyPositionsForJumpingFor(n.GetSparsePosition());
        }

        private List<Node> GetAvailableEmptyPositionsFor(Point pos)
        {
            Node n = sparseArray[pos.X, pos.Y];
            State piece = n.GetState();
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
                return neighbors.Where(x => x.GetState() == EMPTY &&
                                            x.GetSparsePosition().Y > n.GetSparsePosition().Y
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

        private List<Node> GetAvailableEmptyPositionsForJumpingFor(Point pos)
        {
            Node n = sparseArray[pos.X, pos.Y];
            State piece = n.GetState();
            var neighbors = GetSpacesToCheck(pos);
            if (piece == EMPTY)
            {
                return new List<Node>();
                // maybe we should throw an exception?  or actually return a list of empty neighbors?
            }
            else
            { 
                return neighbors.Where(x => x.GetState() == EMPTY).ToList();
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
            State piece = n.GetState();
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
                                                       GetAvailableEmptyPositionsForJumpingFor(x).Count > 0
                                                       ).ToList();
                foreach (var candidate in candidates)
                {
                    Point candidatePos = candidate.GetSparsePosition();
                    Point jumpPos = GetLandingPosition(n.GetSparsePosition(), candidatePos);
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
                                                       GetAvailableEmptyPositionsForJumpingFor(x).Count > 0
                                                       ).ToList();
                foreach (var candidate in candidates)
                {
                    Point candidatePos = candidate.GetSparsePosition();
                    Point jumpPos = GetLandingPosition(n.GetSparsePosition(), candidatePos);
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
                                                       GetAvailableEmptyPositionsForJumpingFor(x).Count > 0
                                                       ).ToList();
                foreach (var candidate in candidates)
                {
                    Point candidatePos = candidate.GetSparsePosition();
                    Point jumpPos = GetLandingPosition(n.GetSparsePosition(), candidatePos);
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
                                                       GetAvailableEmptyPositionsForJumpingFor(x).Count > 0
                                                       ).ToList();
                foreach (var candidate in candidates)
                {
                    Point candidatePos = candidate.GetSparsePosition();
                    Point jumpPos = GetLandingPosition(n.GetSparsePosition(), candidatePos);
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


        private void PlacePiece(State kind, int x, int y)
        {
            var n = sparseArray[x, y];
            n.SetState(kind);
        }

        private void PlacePiece(State kind, Point pos)
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
                        s = BLACK;
                    }
                    else if (y >= 5)
                    {
                        s = RED;
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
                        nodes.Add(n);
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
                    if (x != 3 && y % 2 == 0)
                    {
                        sparseArray[x, y].LinkTo(sparseArray[x + 1, y + 1]);
                    }
                    if (x != 0 && y % 2 == 1)
                    {
                        sparseArray[x, y].LinkTo(sparseArray[x - 1, y + 1]);
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
                        display += x/2 + space;
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

        public static CheckersGM FromBytes(byte[] bytes)
        {
            using (var memStream = new MemoryStream())
            {
                var binForm = new BinaryFormatter();
                memStream.Write(bytes, 0, bytes.Length);
                memStream.Seek(0, SeekOrigin.Begin);
                var obj = (CheckersGM)binForm.Deserialize(memStream);
                return obj;
            }
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

        public Player GetCurrentPlayer()
        {
            return currentPlayerTurn;
        }

        public GameState GetCurrentGameState()
        {
            return gameState;
        }
    }
}
