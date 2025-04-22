using SFML.System;
using SFML.Graphics;


namespace SpaceShipGame3
{
    public class Entity
    {
        private Texture texture;
        private Sprite sprite;

        public Entity(string imageFilePath)
        {
            texture = new Texture(imageFilePath);
            sprite = new Sprite(texture);
        }

        public Vector2f Position { get => sprite.Position; set => sprite.Position = value; }
        public float Rotation { get => sprite.Rotation; set => sprite.Rotation = value; }
        public Vector2f Scale { get => sprite.Scale; set => sprite.Scale = value; }


        public bool IsAsteroid { get; set; }
        public bool IsSmallAsteroid { get; set; }
        public bool IsMediumAsteroid { get; set; }
        public bool IsBigAsteroid { get; set; }
        public bool IsEnemySpaceShip { get; set; }
        public bool IsSmallSpaceShip { get; set; }
        public bool IsMediumSpaceShip { get; set; }
        public bool IsBigSpaceShip { get; set; }       
        public bool IsGasCan { get; set; }
        public bool IsPlasma { get; set; }
        public bool IsLife { get; set; }
        public bool IsPlayer { get; set; }
        public bool IsBullet { get; set; }
        public bool IsShootingStar { get; set; }


        public Sprite Graphic => sprite;

        public void Translate (Vector2f movement) => Position += movement;
        public void Rotate (float rotation) => Rotation += rotation;

        public bool IsColliding(Entity other)
        {
            FloatRect thisBounds = this.Graphic.GetGlobalBounds();
            FloatRect otherBounds = other.Graphic.GetGlobalBounds();

            return thisBounds.Intersects(otherBounds);
        }

    }
}
