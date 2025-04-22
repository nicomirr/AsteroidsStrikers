using System;
using SFML.System;

namespace SpaceShipGame3
{
    public class SmallSpaceShip : AnimatedEntity
    {
        private Player player;
        private int scorePoints;

        private float lives;
        private float speed;

        private string flyingName = "Fly";
        private string destroyedName = "Destroyed";

        private float spawnTimer = 0;
        private float spawnTime;

        private float destroyedTimer;

        private int spaceShipNumber = 0;        //DEJARLO POR AHORA PERO EVALUAR SI SACARLO
        private int currentSpawnLocation;
        private int previousSpawnLocation;

        private bool meteorShower;

        private float travelTime;
        private float travelTimer;

        private SoundEffect smallSpaceShipExplosion;

        public SmallSpaceShip(Player player, int scorePoints, float lives, float speed, Vector2i frameSize, float spawnTime, string imageFilePath) : base(frameSize, imageFilePath)
        {
            this.player = player;
            this.scorePoints = scorePoints;

            Position = new Vector2f(-200f, -200f);

            this.lives = lives;
            this.speed = speed;
            this.spawnTime = spawnTime;

            AnimationData fly = new AnimationData()
            {
                frameRate = 1f,
                rowIndex = 0,
                columnsCount = 2,
                loops = true
            };

            AnimationData destroyed = new AnimationData()
            {
                frameRate = 0.5f,
                rowIndex = 1,
                columnsCount = 2,
                loops = false
            };

            Graphic.Origin = (Vector2f)frameSize / 2;

            AddAnimation(flyingName, fly);
            AddAnimation(destroyedName, destroyed);

            SetCurrentAnimation(flyingName);

            smallSpaceShipExplosion = new SoundEffect("Assets/Sounds/SpaceShipExplosion.wav");
        }

        public bool IsDestroyed { get; set; }
        public int SpaceShipNumer => spaceShipNumber;
        public float Lives => lives;

        public void ReceiveDamage()
        {
            lives--;
        }

        public void MeteorShower(bool meteorShower)
        {
            this.meteorShower = meteorShower;
        }

        public void TravelTime(float travelTime)
        {
            this.travelTime = travelTime;
        }

        public void TravelTimer(float travelTimer)
        {
            this.travelTimer = travelTimer;
        }

        public override void Update(float deltaTime)
        {
            base.Update(deltaTime);

            if (!meteorShower)
            {
                if (lives == 0)
                {
                    IsDestroyed = true;
                }


                if (IsDestroyed)
                {
                    destroyedTimer += deltaTime;

                    IsSmallSpaceShip = false;
                    IsEnemySpaceShip = false;

                    if (destroyedTimer == deltaTime)
                    {
                        smallSpaceShipExplosion.Play();
                        player.AddScore(scorePoints);
                    }


                    if (destroyedTimer <= 3f)
                    {
                        SetCurrentAnimation(destroyedName);
                        Translate(new Vector2f(0f, 0f));

                    }
                    else
                    {
                        IsDestroyed = false;

                        lives = 1;

                        SetCurrentAnimation(flyingName);
                        IsSmallSpaceShip = true;
                        Graphic.Position = new Vector2f(-50f, -50f);
                        destroyedTimer = 0;

                    }
                }

                if (travelTimer < travelTime - 6)
                {
                    if (spawnTimer < spawnTime)
                        spawnTimer += deltaTime;

                    if (spawnTimer >= spawnTime)
                    {
                        spawnTimer -= spawnTime;

                        if (spaceShipNumber < 4)
                            spaceShipNumber++;
                        else
                            spaceShipNumber = 0;

                        Random random = new Random();

                        do
                        {
                            currentSpawnLocation = random.Next(1, 19);

                        } while (currentSpawnLocation == previousSpawnLocation);

                        previousSpawnLocation = currentSpawnLocation;

                        switch (currentSpawnLocation)
                        {
                            case 1:
                                Graphic.Position = new Vector2f(60, 20);
                                break;

                            case 2:
                                Graphic.Position = new Vector2f(120, 20);
                                break;

                            case 3:
                                Graphic.Position = new Vector2f(180, 20);
                                break;

                            case 4:
                                Graphic.Position = new Vector2f(240, 20);
                                break;

                            case 5:
                                Graphic.Position = new Vector2f(300, 20);
                                break;

                            case 6:
                                Graphic.Position = new Vector2f(360, 20);
                                break;

                            case 7:
                                Graphic.Position = new Vector2f(420, 20);
                                break;

                            case 8:
                                Graphic.Position = new Vector2f(480, 20);
                                break;

                            case 9:
                                Graphic.Position = new Vector2f(540, 20);
                                break;

                            case 10:
                                Graphic.Position = new Vector2f(600, 20);
                                break;

                            case 11:
                                Graphic.Position = new Vector2f(660, 20);
                                break;

                            case 12:
                                Graphic.Position = new Vector2f(720, 20);
                                break;

                            case 13:
                                Graphic.Position = new Vector2f(780, 20);
                                break;

                            case 14:
                                Graphic.Position = new Vector2f(840, 20);
                                break;

                            case 15:
                                Graphic.Position = new Vector2f(900, 20);
                                break;

                            case 16:
                                Graphic.Position = new Vector2f(960, 20);
                                break;

                            case 17:
                                Graphic.Position = new Vector2f(1120, 20);
                                break;

                            case 18:
                                Graphic.Position = new Vector2f(1180, 20);
                                break;

                            case 19:
                                Graphic.Position = new Vector2f(1240, 20);
                                break;                           

                        }

                    }
                }
                else
                    spawnTimer = 0;
                

                if (Position.X >= 0 || Position.X <= 1920 || Position.Y >= 0 || Position.Y <= 1080)
                {
                    if (!IsDestroyed)
                    {
                        IsSmallSpaceShip = true;
                        IsEnemySpaceShip = true;
                        Translate(new Vector2f(0, 1) * speed * deltaTime);
                    }

                }
                else
                {
                    IsSmallSpaceShip = false;
                    IsEnemySpaceShip = false;
                    Position = new Vector2f(-200f, -200f);
                }

            }
            else
                Position = new Vector2f(-200f, -200f);
        }

        public void Finish()
        {
            Position = new Vector2f(-200f, -200f);
            IsSmallSpaceShip = false;
            IsEnemySpaceShip = false;
            spawnTimer = 0;
            lives = 1;
        }

    }

    
}
