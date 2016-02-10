using UnityEngine;

namespace GameLogic
{
    class Field
    {
        public Vector2 Position { get; set; }
        public bool Used { get; set; }

        public Field(Vector2 position, bool used)
        {
            this.Position = position;
            this.Used = used;
        }

        public override string ToString()
        {
            return Position.ToString();
        }

        public override bool Equals(object obj)
        {
            if (obj == null) return false;
            if (this.GetType() != obj.GetType()) return false;
            Field otherField = obj as Field;
            Vector2 otherPosition = otherField.Position;
            if (otherPosition != Position) return false;
            return true;
        }

        public override int GetHashCode()
        {
            int hash = 17;
            hash = (hash * 23) + Position.GetHashCode();
            return hash;
        }
    }
}