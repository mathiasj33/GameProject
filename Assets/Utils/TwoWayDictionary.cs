using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Utils
{
    class TwoWayDictionary<K, V>
    {
        private List<K> keys = new List<K>();
        private List<V> values = new List<V>();

        public List<K> Keys { get { return keys; } }
        public List<V> Values { get { return values; } }
        public int Count { get { return keys.Count; } }

        public void Add(K key, V value)
        {
            keys.Add(key);
            values.Add(value);
        }

        public V GetValue(K key)
        {
            int index = keys.IndexOf(key);
            if (index == -1) throw new ArgumentException("key not in dictionary");
            return values[index];
        }

        public K GetKey(V value)
        {
            int index = values.IndexOf(value);
            if (index == -1) throw new ArgumentException("value not in dictionary");
            return keys[index];
        }

        public K GetKeyAt(int index)
        {
            return keys[index];
        }

        public V GetValueAt(int index)
        {
            return values[index];
        }

        public void RemoveKeyAndValue(K key)
        {
            int index = keys.IndexOf(key);
            keys.Remove(key);
            values.RemoveAt(index);
        }

        public void RemoveAll(Predicate<K> predicate)
        {
            for(int i = keys.Count - 1; i >= 0; i--)
            {
                K key = keys[i];
                if(predicate.Invoke(key))
                {
                    RemoveKeyAndValue(key);
                }
            }
        }
    }
}
