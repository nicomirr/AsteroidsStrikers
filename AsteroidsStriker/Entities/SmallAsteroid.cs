using SFML.System;
using SFML.Graphics;

namespace SpaceShipGame3
{    
    public class SmallAsteroid : Asteroid
    {       
        public SmallAsteroid(Player player, int scorePoints, RenderWindow renderWindow, int lives, float speed, Vector2i frameSize, string imageFilePath, SoundEffect smallAsteroidDestroyed) : base(player, scorePoints, renderWindow, lives, speed, frameSize, imageFilePath, smallAsteroidDestroyed)
        {
            
        }               

       
    }
}
