using SFML.Graphics;


namespace SpaceShipGame3
{
    public class Life : Pickable
    {        
        public Life(RenderWindow renderWindow, float addPickableEffect, float spawnTime, float spawnDuration, string imageFilePath) : base(renderWindow, addPickableEffect, spawnTime, spawnDuration, imageFilePath)
        {
            
        }               
    }
}
