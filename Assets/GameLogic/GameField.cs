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

        public void AddField(Vector2 position)
        {
            fields.Add(new Field(position, false));
        }

        public bool ContainsField(Vector2 position)
        {
            return fields.Any(f => f.position == position);
        }
    }
}
