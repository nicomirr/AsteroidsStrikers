using System;
using SFML.System;
using SFML.Graphics;

namespace SpaceShipGame3
{
    public abstract class Asteroid : AnimatedEntity
    {
        private Player player;
        private int scorePoints;

        private RenderWindow renderWindow;
       
        private float destroyedTimer;
        
        float lives;

        int spawnLocation;
        int previousSpawnLocation;

        private float speed;
        Vector2f direction;

        private string flyingName = "Flying";
        private string destroyedName = "Destroyed";

        private SoundEffect asteroidDamaged;
        private SoundEffect asteroidDestroyed;

        public Asteroid(Player player, int scorePoints, RenderWindow renderWindow, int lives, float speed, Vector2i frameSize, string imageFilePath, SoundEffect asteroidDestroyed) : base(frameSize, imageFilePath)
        {
            this.player = player;
            this.scorePoints = scorePoints;

            this.renderWindow = renderWindow;

            this.lives = lives;
            this.speed = speed;

            spawnLocation = 0;
            previousSpawnLocation = 0;

            AnimationData flying = new AnimationData()
            {
                frameRate = 1f,
                rowIndex = 0,
                columnsCount = 1,
                loops = false
            };

            AnimationData destroyed = new AnimationData()
            {
                frameRate = 1f,
                rowIndex = 1,
                columnsCount = 2,
                loops = false
            };

            AddAnimation(flyingName, flying);
            AddAnimation(destroyedName, destroyed);

            SetCurrentAnimation(flyingName);

            asteroidDamaged = new SoundEffect("Assets/Sounds/AsteroidBulletCollision.wav");
            this.asteroidDestroyed = asteroidDestroyed;

        }
        public bool IsDestroyed { get; set; }
        public float Lives => lives;

        public SoundEffect AsteroidDamaged => asteroidDamaged;
        public SoundEffect AsteroidDestroyed => asteroidDestroyed;

        public void ReceiveDamage()
        {            
            lives--;
        }

        public int AddPoints()
        {
            return scorePoints;
        }

        public void Update(float deltaTime, float asteroidsTimer, float asteroidWaveDuration, bool meteorShower)
        {
            if (meteorShower)
            {
                if (lives == 0)
                {
                    IsDestroyed = true;
                }


                if (IsDestroyed)
                {
                    destroyedTimer += deltaTime;

                    if (destroyedTimer == deltaTime)
                        player.AddScore(AddPoints());

                    else if (destroyedTimer <= 3f)
                    {
                        SetCurrentAnimation(destroyedName);
                        direction = new Vector2f(0f, 0f);
                        IsAsteroid = false;
                    }
                    else
                    {
                        IsDestroyed = false;

                        if (IsSmallAsteroid)
                            lives = 1;
                        else if (IsMediumAsteroid)
                            lives = 2;
                        else if (IsBigAsteroid)
                            lives = 4;

                        SetCurrentAnimation(flyingName);
                        IsAsteroid = true;
                        Graphic.Position = new Vector2f(-50f, -50f);
                        destroyedTimer = 0;

                    }

                }

                if (asteroidsTimer < asteroidWaveDuration)
                {
                    if (Graphic.Position.X <= 0 || Graphic.Position.X >= renderWindow.Size.X || Graphic.Position.Y <= 30 || Graphic.Position.Y >= renderWindow.Size.Y)
                    {
                        Random random = new Random();

                        do
                        {
                            spawnLocation = random.Next(1, 40);

                        } while (spawnLocation == previousSpawnLocation);

                        previousSpawnLocation = spawnLocation;

                        switch (spawnLocation)
                        {
                            case 1:

                                Graphic.Position = new Vector2f(0f, 50f);
                                direction = new Vector2f(1f, 1f);

                                break;

                            case 2:

                                Graphic.Position = new Vector2f(0f, renderWindow.Size.Y * 0.1f);
                                direction = new Vector2f(1f, 1f);

                                break;

                            case 3:

                                Graphic.Position = new Vector2f(0f, renderWindow.Size.Y * 0.2f);
                                direction = new Vector2f(1f, 1f);

                                break;

                            case 4:

                                Graphic.Position = new Vector2f(0f, renderWindow.Size.Y * 0.3f);
                                direction = new Vector2f(1f, 1f);

                                break;

                            case 5:

                                Graphic.Position = new Vector2f(0f, renderWindow.Size.Y * 0.4f);
                                direction = new Vector2f(1f, 1f);

                                break;

                            case 6:

                                Graphic.Position = new Vector2f(0f, renderWindow.Size.Y * 0.5f);
                                direction = new Vector2f(0f, -1f);

                                break;

                            case 7:

                                Graphic.Position = new Vector2f(0f, renderWindow.Size.Y * 0.6f);
                                direction = new Vector2f(1f, -1f);

                                break;

                            case 8:

                                Graphic.Position = new Vector2f(0f, renderWindow.Size.Y * 0.7f);
                                direction = new Vector2f(1f, -1f);

                                break;

                            case 9:

                                Graphic.Position = new Vector2f(0f, renderWindow.Size.Y * 0.8f);
                                direction = new Vector2f(1f, -1f);

                                break;

                            case 10:

                                Graphic.Position = new Vector2f(0f, renderWindow.Size.Y * 0.9f);
                                direction = new Vector2f(1f, -1f);

                                break;

                            case 11:

                                Graphic.Position = new Vector2f(0f, renderWindow.Size.Y);
                                direction = new Vector2f(1f, -1f);

                                break;

                            case 12:

                                Graphic.Position = new Vector2f(renderWindow.Size.X * 0.1f, renderWindow.Size.Y);
                                direction = new Vector2f(1f, -1f);

                                break;

                            case 13:

                                Graphic.Position = new Vector2f(renderWindow.Size.X * 0.2f, renderWindow.Size.Y);
                                direction = new Vector2f(1f, -1f);

                                break;

                            case 14:

                                Graphic.Position = new Vector2f(renderWindow.Size.X * 0.3f, renderWindow.Size.Y);
                                direction = new Vector2f(1f, -1f);

                                break;

                            case 15:

                                Graphic.Position = new Vector2f(renderWindow.Size.X * 0.4f, renderWindow.Size.Y);
                                direction = new Vector2f(1f, -1f);

                                break;

                            case 16:

                                Graphic.Position = new Vector2f(renderWindow.Size.X * 0.5f, renderWindow.Size.Y);
                                direction = new Vector2f(0f, -1f);

                                break;

                            case 17:

                                Graphic.Position = new Vector2f(renderWindow.Size.X * 0.6f, renderWindow.Size.Y);
                                direction = new Vector2f(-1f, -1f);

                                break;

                            case 18:

                                Graphic.Position = new Vector2f(renderWindow.Size.X * 0.7f, renderWindow.Size.Y);
                                direction = new Vector2f(-1f, -1f);

                                break;

                            case 19:

                                Graphic.Position = new Vector2f(renderWindow.Size.X * 0.8f, renderWindow.Size.Y);
                                direction = new Vector2f(-1f, -1f);

                                break;

                            case 20:

                                Graphic.Position = new Vector2f(renderWindow.Size.X * 0.9f, renderWindow.Size.Y);
                                direction = new Vector2f(-1f, -1f);

                                break;

                            case 21:

                                Graphic.Position = new Vector2f(renderWindow.Size.X, renderWindow.Size.Y);
                                direction = new Vector2f(-1f, -1f);

                                break;

                            case 22:

                                Graphic.Position = new Vector2f(renderWindow.Size.X, renderWindow.Size.Y * 0.9f);
                                direction = new Vector2f(-1f, -1f);

                                break;

                            case 23:

                                Graphic.Position = new Vector2f(renderWindow.Size.X, renderWindow.Size.Y * 0.8f);
                                direction = new Vector2f(-1f, -1f);

                                break;

                            case 24:

                                Graphic.Position = new Vector2f(renderWindow.Size.X, renderWindow.Size.Y * 0.7f);
                                direction = new Vector2f(-1f, -1f);

                                break;

                            case 25:

                                Graphic.Position = new Vector2f(renderWindow.Size.X, renderWindow.Size.Y * 0.6f);
                                direction = new Vector2f(-1f, -1f);

                                break;

                            case 26:

                                Graphic.Position = new Vector2f(renderWindow.Size.X, renderWindow.Size.Y * 0.5f);
                                direction = new Vector2f(-1f, 0f);

                                break;

                            case 27:

                                Graphic.Position = new Vector2f(renderWindow.Size.X, renderWindow.Size.Y * 0.4f);
                                direction = new Vector2f(-1f, 1f);

                                break;

                            case 28:

                                Graphic.Position = new Vector2f(renderWindow.Size.X, renderWindow.Size.Y * 0.3f);
                                direction = new Vector2f(-1f, 1f);

                                break;

                            case 29:

                                Graphic.Position = new Vector2f(renderWindow.Size.X, renderWindow.Size.Y * 0.2f);
                                direction = new Vector2f(-1f, 1f);

                                break;

                            case 30:

                                Graphic.Position = new Vector2f(renderWindow.Size.X, renderWindow.Size.Y * 0.1f);
                                direction = new Vector2f(-1f, 1f);

                                break;

                            case 31:

                                Graphic.Position = new Vector2f(renderWindow.Size.X, 50f);
                                direction = new Vector2f(-1f, 1f);

                                break;

                            case 32:

                                Graphic.Position = new Vector2f(renderWindow.Size.X * 0.9f, 50f);
                                direction = new Vector2f(-1f, 1f);

                                break;

                            case 33:

                                Graphic.Position = new Vector2f(renderWindow.Size.X * 0.8f, 50f);
                                direction = new Vector2f(-1f, 1f);

                                break;

                            case 34:

                                Graphic.Position = new Vector2f(renderWindow.Size.X * 0.7f, 50f);
                                direction = new Vector2f(-1f, 1f);

                                break;

                            case 35:

                                Graphic.Position = new Vector2f(renderWindow.Size.X * 0.6f, 50f);
                                direction = new Vector2f(-1f, 1f);

                                break;

                            case 36:

                                Graphic.Position = new Vector2f(renderWindow.Size.X * 0.5f, 50f);
                                direction = new Vector2f(0f, 1f);

                                break;

                            case 37:

                                Graphic.Position = new Vector2f(renderWindow.Size.X * 0.4f, 50f);
                                direction = new Vector2f(1f, 1f);

                                break;

                            case 38:

                                Graphic.Position = new Vector2f(renderWindow.Size.X * 0.3f, 50f);
                                direction = new Vector2f(1f, 1f);

                                break;

                            case 39:

                                Graphic.Position = new Vector2f(renderWindow.Size.X * 0.2f, 50f);
                                direction = new Vector2f(1f, 1f);

                                break;

                            case 40:

                                Graphic.Position = new Vector2f(renderWindow.Size.X * 0.1f, 50f);
                                direction = new Vector2f(1f, 1f);

                                break;


                        }
                    }
                }

                Vector2f translation = VectorUtility.Normalize(direction) * speed * deltaTime;
                Translate(translation);
            }
            else
                Graphic.Position = new Vector2f(-200f, -200f);
        }
        
        public void Finish()
        {
            Position = new Vector2f(-200f, -200f);
            direction = new Vector2f(0f, 0f);

            if (IsSmallAsteroid)
            {
                lives = 1;
                SetCurrentAnimation(flyingName);
            }
                
            else if (IsMediumAsteroid)
            {
                lives = 2;
                SetCurrentAnimation(flyingName);
            }               

            else if (IsBigAsteroid)
            {
                lives = 4;
                SetCurrentAnimation(flyingName);
            }
                
        }
    }
}
