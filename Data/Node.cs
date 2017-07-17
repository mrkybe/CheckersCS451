using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Drawing;
using System.Linq;

namespace Data
{
    internal enum STATE
    {
        RED,
        BLACK,
        KING_RED,
        KING_BLACK,
        EMPTY,
        NULL
    }

    internal class Node
    {
        private readonly List<Node> connectedNodes = new List<Node>();
        private Point normalCoords;
        private Point sparseCoords;
        private STATE state = STATE.EMPTY;

        public Node(Point normalCoords, Point sparseCoords)
        {
            this.normalCoords = normalCoords;
            this.sparseCoords = sparseCoords;
        }

        public Point GetSparsePosition()
        {
            return new Point(sparseCoords.X, sparseCoords.Y);
        }


        public void LinkTo(Node n, bool origin = true)
        {
            if (!connectedNodes.Contains(n) && origin)
            {
                connectedNodes.Add(n);
                n.LinkTo(this, false);
            }
            else if (!connectedNodes.Contains(n))
            {
                connectedNodes.Add(n);
            }
        }

        public bool IsLinkedTo(Node n) { return connectedNodes.Contains(n); }

        public void SetState(STATE s) { state = s; }

        public STATE GetState() { return state; }

        public ReadOnlyCollection<Node> GetNeighbors() { return connectedNodes.AsReadOnly(); }

        public string GetStateString()
        {
            switch (state)
            {
                case STATE.RED:
                    return "r";
                case STATE.BLACK:
                    return "b";
                case STATE.KING_RED:
                    return "R";
                case STATE.KING_BLACK:
                    return "B";
                case STATE.EMPTY:
                    return "o";
                case STATE.NULL:
                    return "_";
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}
