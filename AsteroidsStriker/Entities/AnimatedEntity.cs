using System;
using SFML.System;
using SFML.Graphics;
using System.Collections.Generic;


namespace SpaceShipGame3
{
    public class AnimatedEntity : Entity
    {
        private Dictionary<string, AnimationData> animations = new Dictionary<string, AnimationData>();
        private Vector2i frameSize;
        private Vector2i imagePosition;
        private string currentAnimationName;
        private float currentFrameTime;
        private float animationTimer;

        public AnimatedEntity(Vector2i frameSize, string imageFilePath) : base(imageFilePath)
        {
            this.frameSize = frameSize;

            Graphic.TextureRect = new IntRect()
            {
                Left = 0,
                Top = 0,
                Width = frameSize.X,
                Height = frameSize.Y
            };
        }

        public void AddAnimation(string animationName, AnimationData animationData)
        {
            if (animations.ContainsKey(animationName))
            {
                Console.WriteLine("Error. An animation with that name is already loaded.");
                return;
            }

            animations.Add(animationName, animationData);
        }

        public void RemoveAnimation(string animationName)
        {
            if (!animations.ContainsKey(animationName))
            {
                Console.WriteLine("Error. There is no animation with that name.");
                return;
            }

            animations.Remove(animationName);
        }

        public void SetCurrentAnimation(string animationName)
        {
            if (!animations.ContainsKey(animationName))
            {
                Console.WriteLine("Error. There is no animation with that name.");
                return;
            }

            if (currentAnimationName != animationName)
            {
                currentAnimationName = animationName;
                currentFrameTime = 1f / animations[currentAnimationName].frameRate;
                imagePosition = new Vector2i(0, animations[currentAnimationName].rowIndex);

                animationTimer = 0;

                Graphic.TextureRect = new IntRect()
                {
                    Left = imagePosition.X * frameSize.X,
                    Top = imagePosition.Y * frameSize.Y,
                    Width = frameSize.X,
                    Height = frameSize.Y
                };
                                
            }
        }

        public virtual void Update(float deltaTime)
        {
            if (currentAnimationName == null)
                return;

            animationTimer += deltaTime;

            if (animationTimer >= currentFrameTime)
            {
                animationTimer -= currentFrameTime;

                if (imagePosition.X < animations[currentAnimationName].columnsCount - 1)
                    imagePosition.X++;

                else if (animations[currentAnimationName].loops)
                    imagePosition.X = 0;

                Graphic.TextureRect = new IntRect()
                {
                    Left = imagePosition.X * frameSize.X,
                    Top = imagePosition.Y * frameSize.Y,
                    Width = frameSize.X,
                    Height = frameSize.Y
                };
            }
        }
    }
}
