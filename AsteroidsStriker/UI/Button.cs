using System;
using SFML.System;
using SFML.Graphics;
using SFML.Window;

namespace SpaceShipGame3
{
    public class Button
    {
        private RenderWindow renderWindow;

        private Texture texture;
        private Font font;

        private Sprite background;
        private Text text;

        public event Action OnPressed;   
        public event Action OnTouched;   
        public event Action OnNotTouched;   

        public Button(RenderWindow renderWindow, string fontPath, string backgroundPath)
        {
            this.renderWindow = renderWindow;

            texture = new Texture(backgroundPath);
            font = new Font(fontPath);

            background = new Sprite(texture);
            text = new Text("", font);
                        
            FloatRect backgroundRect = background.GetGlobalBounds();  
            background.Origin = new Vector2f(backgroundRect.Width / 2, backgroundRect.Height / 2);  

            renderWindow.MouseButtonReleased += OnReleaseMouseButton;
            renderWindow.MouseMoved += OnMovedOverButton;
            renderWindow.MouseMoved += OnMovedOutsideButton;           
        }

        ~Button()
        {
            renderWindow.MouseButtonReleased -= OnReleaseMouseButton;
        }

        public Sprite Background { get => background; set => background = value; }
        public Text Text { get => text; set => text = value; }

        private void OnReleaseMouseButton(object sender, MouseButtonEventArgs eventArgs)  
        {
            if (eventArgs.Button != Mouse.Button.Left)  
                return;

            FloatRect bounds = background.GetGlobalBounds();

            if (bounds.Contains(eventArgs.X, eventArgs.Y))
                OnPressed?.Invoke();
        }

        private void OnMovedOverButton(object sender, MouseMoveEventArgs eventArgs)
        {         
            FloatRect bounds = background.GetGlobalBounds();

            if (bounds.Contains(eventArgs.X, eventArgs.Y))
                OnTouched?.Invoke();
        }

        private void OnMovedOutsideButton(object sender, MouseMoveEventArgs eventArgs)
        {
            FloatRect bounds = background.GetGlobalBounds();

            if (!bounds.Contains(eventArgs.X, eventArgs.Y))
                OnNotTouched?.Invoke();
        }

        public void Draw()
        {
            renderWindow.Draw(background);
            renderWindow.Draw(text);
        }

        public void SetText(string newText, uint textSize) 
        {
            text.DisplayedString = newText;
            text.CharacterSize = textSize;

            FloatRect textRect = text.GetGlobalBounds();
            text.Origin = new Vector2f(textRect.Width / 2f, textRect.Height / 2f);
        }

        public void SetPosition(Vector2f position) 
        {
            text.Position = position;
            background.Position = position;
        }

        public void SetColor(Color color)
        {
            background.Color = color;
        }

        public void FormatText(Color fillColor, Color outlineColor, bool outline, float outlineThickness)
        {
            text.FillColor = fillColor;

            if (outline)
            {
                text.OutlineColor = outlineColor;
                text.OutlineThickness = outlineThickness;
            }
            else
            {
                text.OutlineColor = Color.Transparent;
                text.OutlineThickness = 0f;
            }
        }
    }
}
