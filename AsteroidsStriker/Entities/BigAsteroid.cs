using SFML.System;
using SFML.Graphics;

namespace SpaceShipGame3
{
    public class BigAsteroid : Asteroid
    {
        public BigAsteroid(Player player, int scorePoints, RenderWindow renderWindow, int lives, float speed, Vector2i frameSize, string imageFilePath, SoundEffect bigAsteroidDestroyed) : base(player, scorePoints, renderWindow,lives, speed, frameSize, imageFilePath, bigAsteroidDestroyed)
        {

        }

    }
}

