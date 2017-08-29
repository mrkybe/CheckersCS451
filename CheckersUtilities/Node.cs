using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;

namespace CheckersUtilities
{
	[Serializable]
	public class Node
	{
		private readonly List<Node> connectedNodes = new List<Node>();
		private Point normalCoords;
		private Point sparseCoords;
		private State state = State.EMPTY;

		public Node(Point normalCoords, Point sparseCoords)
		{
			this.normalCoords = normalCoords;
			this.sparseCoords = sparseCoords;
		}

		public Point GetSparsePosition()
		{
			return new Point(sparseCoords.X, sparseCoords.Y);
		}

		public Point GetNormalPosition()
		{
			return new Point(normalCoords.X, normalCoords.Y);
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

		public void SetState(State s) { state = s; }

		public State GetState() { return state; }

		public ReadOnlyCollection<Node> GetNeighbors() { return connectedNodes.AsReadOnly(); }

		public string GetStateString()
		{
			switch (state)
			{
				case State.RED:
					return "r";
				case State.BLACK:
					return "b";
				case State.KING_RED:
					return "R";
				case State.KING_BLACK:
					return "B";
				case State.EMPTY:
					return ".";
				case State.NULL:
					return "_";
				default:
					throw new ArgumentOutOfRangeException();
			}
		}
	}
}
