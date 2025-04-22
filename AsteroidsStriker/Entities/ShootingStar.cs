using System;
using SFML.System;
using SFML.Graphics;

namespace SpaceShipGame3
{
    public class ShootingStar : Entity
    {
        RenderWindow renderWindow;

        private Player player;

        private float scoreTimer;

        private float speed;
        private float shootingStarTimer;

        private int scorePoints;

        private int spawn;
        private int previousSpawn;

        private int shootingStarSpawnTime = 50;

        private Vector2f direction;

        private SoundEffect starSpawn;
        private SoundEffect starDestroyed;

        public ShootingStar(RenderWindow renderWindow, Player player, float speed, int scorePoints, string imageFilePath) : base(imageFilePath)
        {
            this.renderWindow = renderWindow;

            this.player = player;
            this.speed = speed;
            this.scorePoints = scorePoints;
                       
            starSpawn = new SoundEffect("Assets/Sounds/StarSpawn.wav");
            starDestroyed = new SoundEffect("Assets/Sounds/StarDestroyed.wav");
        }

        public bool Destroyed { get; set; }

        public void AddPoints(float deltaTime)
        {
            scoreTimer += deltaTime;

            if (scoreTimer == deltaTime)
            {
                player.AddScore(scorePoints);
                starDestroyed.Play();
            }

            scoreTimer = 0;
                        
        }
        
        public void Update(float deltaTime, bool meteorShower)
        {
            if(meteorShower)
            {
                if (Graphic.Position.X < 0 || Graphic.Position.X > renderWindow.Size.X || Graphic.Position.Y < 30 || Graphic.Position.Y > renderWindow.Size.Y)
                {
                    IsShootingStar = false;
                }
                else
                    IsShootingStar = true;

                if (Destroyed)
                {
                    direction = new Vector2f(0f, 0f);
                    AddPoints(deltaTime);

                    Destroyed = false;
                }


                shootingStarTimer += deltaTime;

                if (shootingStarTimer > shootingStarSpawnTime)
                {
                    Random random = new Random();

                    do
                    {
                        spawn = random.Next(1, 16);

                    } while (spawn == previousSpawn);

                    previousSpawn = spawn;

                    switch (spawn)
                    {
                        case 1:

                            Graphic.Position = new Vector2f(0f, 50f);
                            direction = new Vector2f(1f, 1f);
                            shootingStarTimer = 0;
                            starSpawn.Play();

                            break;

                        case 2:

                            Graphic.Position = new Vector2f(0f, renderWindow.Size.Y * 0.25f);
                            direction = new Vector2f(1f, 1f);
                            shootingStarTimer = 0;
                            starSpawn.Play();

                            break;

                        case 3:

                            Graphic.Position = new Vector2f(0f, renderWindow.Size.Y * 0.5f);
                            direction = new Vector2f(1f, 0f);
                            shootingStarTimer = 0;
                            starSpawn.Play();

                            break;

                        case 4:

                            Graphic.Position = new Vector2f(0f, renderWindow.Size.Y * 0.75f);
                            direction = new Vector2f(1f, -1f);
                            shootingStarTimer = 0;
                            starSpawn.Play();

                            break;

                        case 5:

                            Graphic.Position = new Vector2f(0f, renderWindow.Size.Y);
                            direction = new Vector2f(1f, -1f);
                            shootingStarTimer = 0;
                            starSpawn.Play();

                            break;

                        case 6:

                            Graphic.Position = new Vector2f(renderWindow.Size.X * 0.25f, renderWindow.Size.Y);
                            direction = new Vector2f(1f, -1f);
                            shootingStarTimer = 0;
                            starSpawn.Play();

                            break;

                        case 7:

                            Graphic.Position = new Vector2f(renderWindow.Size.X * 0.5f, renderWindow.Size.Y);
                            direction = new Vector2f(0f, -1f);
                            shootingStarTimer = 0;
                            starSpawn.Play();

                            break;

                        case 8:

                            Graphic.Position = new Vector2f(renderWindow.Size.X * 0.75f, renderWindow.Size.Y);
                            direction = new Vector2f(-1f, -1f);
                            shootingStarTimer = 0;
                            starSpawn.Play();

                            break;

                        case 9:

                            Graphic.Position = new Vector2f(renderWindow.Size.X, renderWindow.Size.Y);
                            direction = new Vector2f(-1f, -1f);
                            shootingStarTimer = 0;
                            starSpawn.Play();

                            break;

                        case 10:

                            Graphic.Position = new Vector2f(renderWindow.Size.X, renderWindow.Size.Y * 0.75f);
                            direction = new Vector2f(-1f, -1f);
                            shootingStarTimer = 0;
                            starSpawn.Play();

                            break;

                        case 11:

                            Graphic.Position = new Vector2f(renderWindow.Size.X, renderWindow.Size.Y * 0.5f);
                            direction = new Vector2f(-1f, 0f);
                            shootingStarTimer = 0;
                            starSpawn.Play();

                            break;

                        case 12:

                            Graphic.Position = new Vector2f(renderWindow.Size.X, renderWindow.Size.Y * 0.25f);
                            direction = new Vector2f(-1f, 1f);
                            shootingStarTimer = 0;
                            starSpawn.Play();

                            break;

                        case 13:

                            Graphic.Position = new Vector2f(renderWindow.Size.X, 50f);
                            direction = new Vector2f(-1f, 1f);
                            shootingStarTimer = 0;
                            starSpawn.Play();

                            break;

                        case 14:

                            Graphic.Position = new Vector2f(renderWindow.Size.X * 0.75f, 50f);
                            direction = new Vector2f(-1f, 1f);
                            shootingStarTimer = 0;
                            starSpawn.Play();

                            break;

                        case 15:

                            Graphic.Position = new Vector2f(renderWindow.Size.X * 0.5f, 50f);
                            direction = new Vector2f(0f, 1f);
                            shootingStarTimer = 0;
                            starSpawn.Play();

                            break;

                        case 16:

                            Graphic.Position = new Vector2f(renderWindow.Size.X * 0.25f, 50f);
                            direction = new Vector2f(1f, 1f);
                            shootingStarTimer = 0;
                            starSpawn.Play();

                            break;

                    }

                }

                Vector2f translation = VectorUtility.Normalize(direction) * speed * deltaTime;
                Translate(translation);
            }
        }
                   
    }
}
