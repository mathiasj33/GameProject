using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace GameLogic
{
    class GameField
    {
        private List<Field> fields = new List<Field>();
        public List<Field> Fields { get { return fields; } }

        public GameField(int size)
        {
            for(int i = 0; i < size; i++)
            {
                for(int j = 0; j < size; j++)
                {
                    fields.Add(new Field(new Vector2(i, j), false));
                }
            }
        }

        private Field GetField(Vector2 position)
        {
            return fields.FirstOrDefault(f => f.Position == position);
        }

        public bool ContainsField(Vector2 position)
        {
            return fields.Any(f => f.Position == position);
        }

        public void RemoveField(Vector2 position)
        {
            fields.RemoveAll(f => f.Position == position);
        }

        public List<Vector2> FindPath(Vector2 from, Vector2 to)
        {
            Pathfinder pf = new Pathfinder(this);
            return pf.FindPath(from, to);
        }

        public List<Vector2> FindNodesInRadius(Vector2 position, int radius)
        {
            Wenn zwischen Node und Charakter ein Hindernis ist: Pathfinding. Sonst nicht.
            return null;
        }

        internal List<Field> GetNeighbors(Vector2 position)
        {
            List<Field> neighbors = new List<Field>();
            Field east = GetField(position + new Vector2(1, 0));
            Field south = GetField(position + new Vector2(0, -1));
            Field west = GetField(position + new Vector2(-1, 0));
            Field north = GetField(position + new Vector2(0, 1));
            if(east != null)
            {
                neighbors.Add(east);
            }
            if(south != null)
            {
                neighbors.Add(south);
            }
            if (west != null)
            {
                neighbors.Add(west);
            }
            if (north != null)
            {
                neighbors.Add(north);
            }
            return neighbors;
        }
    }
}
