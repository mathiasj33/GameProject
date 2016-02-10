using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using Utils;

namespace GameLogic
{
    class Pathfinder
    {
        private GameField field;
        private List<Node> openList = new List<Node>();
        private PriorityQueue<Node> queue = new PriorityQueue<Node>();
        private HashSet<Node> closedList = new HashSet<Node>();

        public Pathfinder(GameField field)
        {
            this.field = field;
        }

        public List<Vector2> FindPath(Vector2 from, Vector2 to)
        {
            SetupOpenList(to);
            Node startNode = GetNodeAt(from);
            startNode.G = 0;
            queue.Enqueue(startNode);

            Node targetNode = null;

            while (queue.Count > 0)
            {
                Node currentNode = queue.Dequeue();
                if(currentNode.Position == to)
                {
                    targetNode = currentNode;
                    break;
                }
                List<Field> neighbors = field.GetNeighbors(currentNode.Position);
                foreach (Node n in FieldsToNodes(neighbors))
                {
                    if (closedList.Contains(n)) continue;
                    int newG = currentNode.G + 1;
                    if (!queue.Contains(n))
                    {
                        n.G = newG;
                        n.Predecessor = currentNode;
                        openList.Add(n);
                        queue.Enqueue(n);
                    }
                    else if (queue.Contains(n) && newG < n.G)
                    {
                        n.G = newG;
                        n.Predecessor = currentNode;
                        queue.PriorityChanged();
                    }
                }
                closedList.Add(currentNode);
            }

            if(targetNode != null)
            {
                Debug.Log("Found");
                Stack<Node> stack = new Stack<Node>();
                stack.Push(targetNode);
                Node parent = targetNode.Predecessor;
                while(parent != null)
                {
                    stack.Push(parent);
                    parent = parent.Predecessor;
                }
                List<Vector2> path = new List<Vector2>();
                while(stack.Count > 0)
                {
                    path.Add(stack.Pop().Position);
                }
                return path;
            }
            Debug.Log("Empty");
            return null;
        }

        private void SetupOpenList(Vector2 target)
        {
            field.Fields.ForEach(f =>
            {
                Node n = new Node();
                n.H = (int)(target - f.Position).sqrMagnitude;
                n.Position = f.Position;
                openList.Add(n);
            });
        }

        private Node GetNodeAt(Vector2 position)
        {
            return openList.First(n => n.Position == position);
        }

        private List<Node> FieldsToNodes(List<Field> fields)
        {
            List<Node> nodes = new List<Node>();
            foreach (Field field in fields)
            {
                nodes.Add(GetNodeAt(field.Position));
            }
            return nodes;
        }
    }
}
