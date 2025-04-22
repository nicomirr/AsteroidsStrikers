using System;
using SFML.System;
using SFML.Graphics;

namespace SpaceShipGame3
{
    public abstract class LoopState
    {
        protected RenderWindow renderWindow;
        protected bool isRunning;

        public LoopState(RenderWindow renderWindow)
        {
            this.renderWindow = renderWindow;
            isRunning = false;
        }

        private void OnCloseWindow(object sender, EventArgs e)
        {
            renderWindow.Close();
            Environment.Exit(0);
        }

        protected virtual void Start() => renderWindow.Closed += OnCloseWindow;
        private void ProcessInput() => renderWindow.DispatchEvents();
        protected abstract void Update(float deltaTime);


        protected abstract void Draw();

        protected virtual void Finish() => renderWindow.Closed -= OnCloseWindow;

        public void Play()  
        {
            Clock clock = new Clock();

            Start();

            isRunning = true;

            while (isRunning)
            {
                Time deltaTime = clock.Restart();

                ProcessInput();

                Update(deltaTime.AsSeconds());

                renderWindow.Clear();
                Draw();
                renderWindow.Display();
            }

            Finish();
        }

        public void Stop()    
        {
            if (!isRunning)
            {
                Console.WriteLine("Cannot stop a state that is not running.");
                return;
            }

            isRunning = false;

            Finish();
        }
    }
}
