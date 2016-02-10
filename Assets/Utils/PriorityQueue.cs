using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Utils
{
    class PriorityQueue<T> where T : IComparable<T>
    {
        private List<T> list = new List<T>();
        public int Count { get { return list.Count; } }

        public void Enqueue(T e)
        {
            list.Add(e);
            list.Sort();
        }

        public T Peek()
        {
            return list[0];
        }

        public T Dequeue()
        {
            T e = list[0];
            list.Remove(e);
            return e;
        }

        public bool Contains(T e)
        {
            return list.Contains(e);
        }

        public void PriorityChanged()
        {
            list.Sort();
        }
    }
}
