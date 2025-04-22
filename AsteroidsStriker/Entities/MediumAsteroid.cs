using SFML.System;
using SFML.Graphics;

namespace SpaceShipGame3
{
    public class MediumAsteroid : Asteroid
    {
        public MediumAsteroid(Player player, int scorePoints, RenderWindow renderWindow, int lives, float speed, Vector2i frameSize, string imageFilePath, SoundEffect mediumAsteroidDestroyed) : base(player, scorePoints, renderWindow, lives, speed, frameSize, imageFilePath, mediumAsteroidDestroyed)
        {
            
        }
              
    }
}
