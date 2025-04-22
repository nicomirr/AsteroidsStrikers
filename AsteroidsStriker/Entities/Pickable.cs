using System;
using SFML.System;
using SFML.Graphics;

namespace SpaceShipGame3
{
    public abstract class Pickable : Entity
    {
        private RenderWindow renderWindow;

        private float addPickableEffect;

        private float spawnTimeRate;
        private float spawnTimer;

        private float spawnDuration;
        private float spawnDurationTimer;
              
        private float spawnLocationX; 
        private float spawnLocationY; 
        private float previouSpawnLocationX; 
        private float previousSpawnLocationY; 
        
        

        public Pickable(RenderWindow renderWindow, float addPickableEffect, float spawnTimeRate, float spawnDuration, string imageFilePath) : base(imageFilePath)
        {
            this.renderWindow = renderWindow;

            this.addPickableEffect = addPickableEffect;

            this.spawnTimeRate = spawnTimeRate;
            spawnTimer = 0;

            this.spawnDuration = spawnDuration;
            spawnDurationTimer = 0;

        }

        public float AddPickableEffect()
        {
            return addPickableEffect;
        }

        public void Update(float deltaTime, bool meteorShower)
        {
            if(meteorShower)
            {
                Random random = new Random();

                if (Graphic.Position == new Vector2f(-50, -50))
                {
                    spawnTimer += deltaTime;
                    spawnDurationTimer = 0;
                }

                else
                    spawnDurationTimer += deltaTime;

                if (spawnDurationTimer >= spawnDuration)
                {
                    Graphic.Position = new Vector2f(-50, -50);
                    spawnDurationTimer = 0;
                }

                if (spawnTimer >= spawnTimeRate)
                {
                    int maxX = (int)renderWindow.Size.X - 30;
                    int maxY = (int)renderWindow.Size.Y - 80;

                    do
                    {
                        spawnLocationX = random.Next(15, maxX);
                        spawnLocationY = random.Next(60, maxY);
                    }
                    while (spawnLocationX == previouSpawnLocationX && spawnLocationY == previousSpawnLocationY);

                    previouSpawnLocationX = spawnLocationX;
                    previousSpawnLocationY = spawnLocationY;


                    spawnTimer -= spawnTimeRate;

                    Graphic.Position = new Vector2f(random.Next(15, maxX), random.Next(60, maxY));
                }
            }
                        
        }
    }
}
