using System;
using SFML.System;
using SFML.Window;
using SFML.Graphics;
using System.Collections.Generic;



namespace SpaceShipGame3
{
    public class Player : AnimatedEntity
    {
        RenderWindow renderWindow;

        private int score;
        private float lives;

        private bool shipIsAlive;
        private bool damaged;

        private float fuel;
        private float fuelTimer;
        private float fuelTime;
        private float fuelConsumption;

        private float plasma;

        private bool engineOn;
        private float turnOnEngineTime = 0.5f;
        private float turnOffEngineTime = 2f;
        private float engineStartTimer;
        private float engineStopTimer;

        private List<Bullet> bullets = new List<Bullet>();

        private Vector2f forward;
        private Vector2f lastDirection;

        private float movementSpeed;
        private float normalSpeed;
        private float reducedSpeed;

        private float rotationSpeed;

        private string idleName = "Idle";
        private string flyName = "Fly";
        private string explosionName = "Explosion";

        private float shootTimer;
        private float fireCoolDown = 0.6f; 

        private float damageTakenTimer;

        private Color damagedColor = new Color(255, 255, 255, 0);
        private Color notDamagedColor = new Color(255, 255, 255, 255);

        private float invincibilityTimer;

        private bool meteorShower;

        private SoundEffect playerExplosion;
        private SoundEffect playerTakeDamage;
        private SoundEffect playerPickupLife;
        private SoundEffect playerPickupFuel;
        private SoundEffect playerPickupPlasma;


        public Player(RenderWindow renderWindow, List<Bullet> bullets, float lives, string imageFilePath, Vector2i frameSize, float rotationSpeed, float normalSpeed, float reducedSpeed) : base(frameSize, imageFilePath)
        {
            this.renderWindow = renderWindow;

            score = 0;

            this.bullets = bullets;

            this.lives = lives;

            shipIsAlive = true;
            damaged = false;

            plasma = 60;

            fuelTime = 1;
            fuel = 100;
            fuelConsumption = 1.5f;

            engineOn = false;

            this.reducedSpeed = reducedSpeed;
            this.normalSpeed = normalSpeed;
            movementSpeed = normalSpeed;

            this.rotationSpeed = rotationSpeed;

            Graphic.Origin = (Vector2f)frameSize / 2f;

            AnimationData idle = new AnimationData()
            {
                frameRate = 10f,
                rowIndex = 0,
                columnsCount = 1,
                loops = false
            };

            AnimationData fly = new AnimationData()
            {
                frameRate = 10f,
                rowIndex = 1,
                columnsCount = 1,
                loops = false
            };

            AnimationData explosion = new AnimationData()
            {
                frameRate = 0.5f,
                rowIndex = 2,
                columnsCount = 2,
                loops = false
            };


            AddAnimation(idleName, idle);
            AddAnimation(flyName, fly);
            AddAnimation(explosionName, explosion);            

            SetCurrentAnimation(idleName);

            playerExplosion = new SoundEffect("Assets/Sounds/SpaceShipExplosion.wav");
            playerTakeDamage = new SoundEffect("Assets/Sounds/AsteroidShipCollision.wav");
            playerPickupLife = new SoundEffect("Assets/Sounds/PickupLife.wav");
            playerPickupPlasma = new SoundEffect("Assets/Sounds/PickupPlasma.wav");
            playerPickupFuel = new SoundEffect("Assets/Sounds/PickupFuel.wav");

            
        }               
              
        public Vector2f Forward => forward;
        public float Lives => lives;
        public float Fuel => fuel;
        public float Plasma => plasma;
        public int Score => score;

        public bool Damaged { get => damaged; set => damaged = value; }
        public bool Invincibility { get; set; }
        public float InvincibilityTimer => invincibilityTimer;
                            
        public void AddScore(int points)
        {
            score += points;
        }
        public void AddLife(float life)
        {
            playerPickupLife.Play();
            lives = Math.Clamp(lives + life, 0f, 5f);
           
        }
        
        public void AddFuel(float refuel)
        {
            playerPickupFuel.Play();
            fuel = Math.Clamp(fuel + refuel, 0f, 100f);
           
        }

        public void AddPlasma(float newPlasma)
        {
            playerPickupPlasma.Play();
            plasma = Math.Clamp(plasma + newPlasma, 0f, 60f);
            
        }

        public void ReceiveDamage()
        {
            playerTakeDamage.Play();

            lives--;
            
            damaged = true;
        }

        public void DamageTaken(float deltaTime)
        {
            damageTakenTimer += deltaTime;

            if (damageTakenTimer == deltaTime)
                Graphic.Color = damagedColor;

            else if (damageTakenTimer >= 0.2f && damageTakenTimer < 0.35f)
                Graphic.Color = notDamagedColor;

            else if (damageTakenTimer >= 0.35f && damageTakenTimer < 0.5f)
                Graphic.Color = damagedColor;

            else if (damageTakenTimer >= 0.5f && damageTakenTimer < 0.65f)
                Graphic.Color = notDamagedColor;

            else if (damageTakenTimer >= 0.65f && damageTakenTimer < 0.8f)
                Graphic.Color = damagedColor;            

            else if (damageTakenTimer >= 0.8f)
            {
                Graphic.Color = notDamagedColor;
                damageTakenTimer = 0;

                Invincibility = false;
            }
        }

        private void Explosion()
        {
            if(shipIsAlive)
            {
                playerExplosion.Play();

                Invincibility = false;
                Graphic.Color = notDamagedColor;
                shipIsAlive = false;
                SetCurrentAnimation(explosionName);                                
            }                   
        }

        public List<Bullet> GetActiveBullets()
        {
            return bullets.FindAll(b => b.IsActive);
        }

        public void MeteorShower(bool meteorShower)
        {
            this.meteorShower = meteorShower;
        }

        public override void Update(float deltaTime)
        {
            base.Update(deltaTime);

            if (Graphic.Rotation < 0)
                Graphic.Rotation = 359.9f;

            else if (Graphic.Rotation > 359.9f)
                Graphic.Rotation = 0f;

            if (damaged)
                invincibilityTimer += deltaTime;

            if (!damaged)
                invincibilityTimer = 0;
                        
            if (Invincibility)
                DamageTaken(deltaTime);

            if (lives == 0)
                Explosion();

            for(int i = 0; i < bullets.Count; i++)
            {
                bullets[i].Update(deltaTime);
            }
            
            if(shipIsAlive)
            {
                if (meteorShower)
                {
                    float rotationMultiplier = 0f;

                    if (Keyboard.IsKeyPressed(Keyboard.Key.Left))
                        rotationMultiplier = -1f;

                    if (Keyboard.IsKeyPressed(Keyboard.Key.Right))
                        rotationMultiplier = 1f;

                    Rotation += rotationMultiplier * rotationSpeed * deltaTime;

                    float angleInRadians = Rotation * MathF.PI / 180f;
                    float sin = MathF.Sin(angleInRadians);
                    float cos = MathF.Cos(angleInRadians);

                    forward = new Vector2f(VectorUtility.Up.X * cos - VectorUtility.Up.Y * sin,
                                                    VectorUtility.Up.X * sin + VectorUtility.Up.Y * cos);

                    bool moveForward = Keyboard.IsKeyPressed(Keyboard.Key.Up);

                    if (fuel > 0)
                    {
                        if (moveForward)
                        {
                            engineStopTimer = 0;

                            if (!engineOn)
                            {
                                movementSpeed = reducedSpeed;

                                engineStartTimer += deltaTime;

                                if (engineStartTimer >= turnOnEngineTime)
                                {
                                    engineStartTimer = 0;
                                    movementSpeed = normalSpeed;

                                    engineOn = true;
                                }
                            }

                            fuelTimer += deltaTime;

                            if (fuelTimer >= fuelTime)
                            {
                                fuelTimer -= fuelTime;

                                fuel -= fuelConsumption;
                            }

                            SetCurrentAnimation(flyName);

                            angleInRadians = Rotation * MathF.PI / 180f;
                            sin = MathF.Sin(angleInRadians);
                            cos = MathF.Cos(angleInRadians);

                            forward = new Vector2f(VectorUtility.Up.X * cos - VectorUtility.Up.Y * sin,
                                                            VectorUtility.Up.X * sin + VectorUtility.Up.Y * cos);

                            Translate(forward * movementSpeed * deltaTime);

                            lastDirection = forward;

                        }
                        else
                        {
                            SetCurrentAnimation(idleName);

                            movementSpeed = reducedSpeed;
                            engineStopTimer += deltaTime;

                            Translate(lastDirection * movementSpeed * deltaTime);
                            engineOn = false;

                            if (engineStopTimer >= turnOffEngineTime)
                            {
                                engineStopTimer = 0;
                                lastDirection = new Vector2f(0f, 0f);
                            }
                        }

                    }
                    else
                    {
                        SetCurrentAnimation(idleName);

                        movementSpeed = reducedSpeed;
                        engineStopTimer += deltaTime;

                        Translate(lastDirection * movementSpeed * deltaTime);
                        engineOn = false;

                        if (engineStopTimer >= turnOffEngineTime)
                        {
                            engineStopTimer = 0;
                            lastDirection = new Vector2f(0f, 0f);

                        }
                    }

                }
                else
                {
                    if (Position.Y > 695)
                        Translate(new Vector2f(0f, -1f) * movementSpeed * deltaTime);

                    else if (Position.Y < 690)
                        Translate(new Vector2f(0f, 1f) * movementSpeed * deltaTime);

                    if(Graphic.Rotation != 0)
                    {
                        if (Graphic.Rotation > 180)
                            Rotation += 1 * rotationSpeed * deltaTime;

                        else if (Graphic.Rotation < 180)
                            Rotation -= 1 * rotationSpeed * deltaTime;

                        if (Graphic.Rotation > 358f || Graphic.Rotation < 2f)
                            Graphic.Rotation = 0;
                    }


                    SetCurrentAnimation(flyName);
                    movementSpeed = normalSpeed;
                    forward = new Vector2f(0f, -1f);

                    fuel -= 0.1f * deltaTime;

                    Vector2f sideMovement = new Vector2f(0f, 0f);

                    if (Keyboard.IsKeyPressed(Keyboard.Key.Left))
                    {
                        sideMovement = new Vector2f(-1f, 0f);
                        fuel -= 0.5f * deltaTime;
                    }                        

                    else if (Keyboard.IsKeyPressed(Keyboard.Key.Right))
                    {
                        sideMovement = new Vector2f(1f, 0f);
                        fuel -= 0.5f * deltaTime;
                    }
                        
                    Translate(sideMovement * movementSpeed * deltaTime);

                }

                shootTimer += deltaTime;

                if (plasma > 0)
                {
                    if (Keyboard.IsKeyPressed(Keyboard.Key.X))
                    {
                        if (shootTimer >= fireCoolDown)
                        {
                            shootTimer = 0;

                            plasma--;

                            Bullet bullet = bullets.Find(b => !b.IsActive);

                            if (bullet != null)
                                bullet.Shoot(forward, Graphic.Position);
                        }
                    }
                }
            }
            


            if (Graphic.Position.X > renderWindow.Size.X + 30)
            {
                Graphic.Position = new Vector2f(0, Position.Y);
            }

            if (Graphic.Position.X < -30)
            {
                Graphic.Position = new Vector2f(renderWindow.Size.X, Position.Y);
            }

            if (Graphic.Position.Y > renderWindow.Size.Y + 40)
            {
                Graphic.Position = new Vector2f(Position.X, 30);
            }

            if (Graphic.Position.Y < 20)
            {
                Graphic.Position = new Vector2f(Position.X, renderWindow.Size.Y);
            }


        }

        public void Finish()
        {
            shipIsAlive = true;            
            Position = new Vector2f(renderWindow.Size.X / 2, renderWindow.Size.Y / 2);
            lives = 3;
            score = 0;
            plasma = 60;
            fuel = 100;
            Graphic.Rotation = 0;
        }
    }
}
