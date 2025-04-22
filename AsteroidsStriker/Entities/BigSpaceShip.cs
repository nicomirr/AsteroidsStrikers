using System;
using SFML.System;

namespace SpaceShipGame3
{
    public class BigSpaceShip : AnimatedEntity
    {
        private Player player;
        private int scorePoints;

        private float lives;
        private float actualLives;

        private float speed;

        private string flyName = "Fly";
        private string explosionName = "Explosion";

        private float spawnTimer = 0;
        private float spawnTime;

        private float destroyedTimer;

        private int spaceShipNumber = 0;
        private int currentSpawnLocation;
        private int previousSpawnLocation;

        private bool meteorShower;

        private int currentWave;

        private float travelTime;
        private float travelTimer;

        private SoundEffect bigSpaceShipExplosion;
        private SoundEffect bigSpaceShipDamaged;

        public BigSpaceShip(Player player, int scorePoints, float lives, float speed, Vector2i frameSize, float spawnTime, string imageFilePath) : base(frameSize, imageFilePath)
        {
            this.player = player;
            this.scorePoints = scorePoints;

            Position = new Vector2f(-6000f, -6000f);

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

            AnimationData explosion = new AnimationData()
            {
                frameRate = 0.5f,
                rowIndex = 1,
                columnsCount = 2,
                loops = false
            };

            Graphic.Origin = (Vector2f)frameSize / 2;

            AddAnimation(flyName, fly);
            AddAnimation(explosionName, explosion);

            SetCurrentAnimation(flyName);

            bigSpaceShipExplosion = new SoundEffect("Assets/Sounds/SpaceShipExplosion.wav");
            bigSpaceShipDamaged = new SoundEffect("Assets/Sounds/AsteroidBulletCollision.wav");
        }

        public bool IsDestroyed { get; set; }
        public int SpaceShipNumer => spaceShipNumber;
        public float Lives => lives;        
        public SoundEffect BigSpaceShipDamaged => bigSpaceShipDamaged;

        public void ReceiveDamage()
        {
            lives--;
        }

        public void MeteorShower(bool meteorShower)
        {
            this.meteorShower = meteorShower;
        }

        public void CurrentWave(int currentWave)
        {
            this.currentWave = currentWave;
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
                      
            if (currentWave == 1)
                actualLives = 3;

            else if (currentWave == 3)
                actualLives = 4;

            else if (currentWave == 4)
                actualLives = 5;

            if (!meteorShower)
            {
                if (lives == 0)
                {
                    IsDestroyed = true;
                }

                if (IsDestroyed)
                {
                    destroyedTimer += deltaTime;

                    IsBigSpaceShip = false;
                    IsEnemySpaceShip = false;

                    if (destroyedTimer == deltaTime)
                    {
                        bigSpaceShipExplosion.Play();
                        player.AddScore(scorePoints);
                    }


                    if (destroyedTimer <= 3f)
                    {
                        SetCurrentAnimation(explosionName);
                        Translate(new Vector2f(0f, 0f));                        
                    }
                    else
                    {
                        IsDestroyed = false;

                        lives = actualLives;

                        SetCurrentAnimation(flyName);
                        IsBigSpaceShip = true;
                        Graphic.Position = new Vector2f(-500f, -500f);
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
                            currentSpawnLocation = random.Next(1, 6);

                        } while (currentSpawnLocation == previousSpawnLocation);

                        previousSpawnLocation = currentSpawnLocation;

                        switch (currentSpawnLocation)
                        {                          
                            case 1:
                                Graphic.Position = new Vector2f(342, 30);
                                lives = actualLives;
                                break;

                            case 2:
                                Graphic.Position = new Vector2f(513, 30);
                                lives = actualLives;
                                break;

                            case 3:
                                Graphic.Position = new Vector2f(684, 30);
                                lives = actualLives;
                                break;

                            case 4:
                                Graphic.Position = new Vector2f(855, 30);
                                lives = actualLives;
                                break;

                            case 5:
                                Graphic.Position = new Vector2f(1026, 30);
                                lives = actualLives;
                                break;

                            case 6:
                                Graphic.Position = new Vector2f(1197, 30);
                                lives = actualLives;
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
                        IsBigSpaceShip = true;
                        IsEnemySpaceShip = true;
                        Translate(new Vector2f(0, 1) * speed * deltaTime);
                    }
                }
                else
                {
                    IsBigSpaceShip = false;
                    IsEnemySpaceShip = false;
                    Position = new Vector2f(-6000f, -6000f);
                    lives = actualLives;
                }
                   
            }
            else
            {
                Position = new Vector2f(-6000f, -6000f);
                lives = actualLives;
            }
                
        }

        public void Finish()
        {
            Position = new Vector2f(-6000f, -6000f);
            IsBigSpaceShip = false;
            IsEnemySpaceShip = false;
            spawnTimer = 0;
            lives = 3;

        }
    }
}
