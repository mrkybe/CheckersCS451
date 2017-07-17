using System;
using System.Collections.Generic;

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
        private Tuple<int, int> normalCoords;
        private Tuple<int, int> sparseCoords;
        private STATE state = STATE.NULL;

        public Node(Tuple<int, int> normalCoords, Tuple<int, int> sparseCoords)
        {
            this.normalCoords = normalCoords;
            this.sparseCoords = sparseCoords;
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

        public string GetStateString()
        {
            switch (state)
            {
                case STATE.RED:
                    return "r";
                    break;
                case STATE.BLACK:
                    return "b";
                    break;
                case STATE.KING_RED:
                    return "R";
                    break;
                case STATE.KING_BLACK:
                    return "B";
                    break;
                case STATE.EMPTY:
                    return "o";
                    break;
                case STATE.NULL:
                    return "_";
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}
