using UnityEngine;

namespace GameLogic
{
    struct Field
    {
        public Vector2 position;
        public bool used;

        public Field(Vector2 position, bool used)
        {
            this.position = position;
            this.used = used;
        }
    }
}