using System;
using SFML.System;

namespace SpaceShipGame3
{
    public static class VectorUtility
    {
        public static readonly Vector2f Up = new Vector2f(0f, -1f);
        public static readonly Vector2f Down = new Vector2f(0f, 1f);
        public static readonly Vector2f Left = new Vector2f(-1f, 0f);
        public static readonly Vector2f Right = new Vector2f(1f, 0f);

        public static Vector2f Normalize (Vector2f vector)
        {
            Vector2f normalized;

            float lenght = MathF.Sqrt(vector.X * vector.X + vector.Y * vector.Y);

            normalized = lenght > 0 ? vector / lenght : new Vector2f(0f, 0f);

            return normalized;
        }

    }
}
