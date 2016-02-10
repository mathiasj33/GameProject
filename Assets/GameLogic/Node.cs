using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace GameLogic
{
    class Node : IComparable<Node>
    {
        public Vector2 Position { get; set; }
        public int G { get; set; }
        public int H { get; set; }
        public int F { get { return G + H; } }

        public Node Predecessor { get; set; }

        public int CompareTo(Node other)
        {
            if (F < other.F) return -1;
            if (F > other.F) return 1;
            return 0;
        }

        public override bool Equals(object obj)
        {
            if (obj == null) return false;
            if (this.GetType() != obj.GetType()) return false;
            Node otherNode = obj as Node;
            Vector2 otherPosition = otherNode.Position;
            if (otherPosition != Position) return false;
            return true;
        }

        public override int GetHashCode()
        {
            int hash = 13;
            hash = (hash * 7) + Position.GetHashCode();
            return hash;
        }

        public override string ToString()
        {
            return Position.ToString();
        }
    }
}
