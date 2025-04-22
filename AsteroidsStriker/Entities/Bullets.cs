using SFML.System;
using SFML.Graphics;

namespace SpaceShipGame3
{
    
    public class Bullet : Entity
    {
        RenderWindow renderWindow;
        private float speed;
        private bool isActive;
        private Vector2f currentDirection;
        private SoundEffect bulletSound;
                
        public Bullet (RenderWindow renderWindow, float speed, string imageFilePath) : base(imageFilePath)
        {
            this.renderWindow = renderWindow;

            Graphic.Position = new Vector2f(-2000, -2000);       

            isActive = false;
            IsBullet = false;
            this.speed = speed;

            bulletSound = new SoundEffect("Assets/Sounds/LaserBlast.wav");
        }

        public bool IsActive { get => isActive; set => isActive = value; }

        public void Shoot(Vector2f forward, Vector2f position)
        {
            bulletSound.Play();

            isActive = true;
            IsBullet = true;

            Graphic.Position = position;
            currentDirection = forward;
                     
        }

        public void Update(float deltaTime)
        {
            if (!isActive)
            {
                IsBullet = false;
                return;
            }
                

            Translate(currentDirection * speed * deltaTime);

            if (Graphic.Position.X == 0)
            {
                isActive = false;
                Graphic.Position = new Vector2f(-2000, -2000);
                currentDirection = new Vector2f(0f, 0f);
            }               

            else if (Graphic.Position.X == renderWindow.Size.X)
            {
                isActive = false;
                Graphic.Position = new Vector2f(-2000, -2000);
                currentDirection = new Vector2f(0f, 0f);
            }
                

            else if (Graphic.Position.Y == 50)
            {
                isActive = false;
                Graphic.Position = new Vector2f(-2000, -2000);
                currentDirection = new Vector2f(0f, 0f);
            }
                

            else if (Graphic.Position.Y == renderWindow.Size.Y)
            {
                isActive = false;
                Graphic.Position = new Vector2f(-2000, -2000);
                currentDirection = new Vector2f(0f, 0f);
            }
               
        }

        public void Finish()
        {
            Graphic.Position = new Vector2f(-2000f, -2000f);
            isActive = false;
            IsBullet = false;
        }
                
    }
}
