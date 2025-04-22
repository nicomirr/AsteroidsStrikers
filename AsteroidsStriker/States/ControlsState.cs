using System;
using SFML.System;
using SFML.Graphics;

namespace SpaceShipGame3
{
    public class ControlsState : LoopState
    {
        private string language = "en";

        private Font textsFont;
        private Text titleText;

        private Entity background;
        private Entity movementControls;
        private Entity shootingControls;
        private Entity pauseControls;

        private Text movementControlsText;
        private Text shootingControlsText;
        private Text pauseControlsText;

        private Button backButton;

        public event Action OnBackPressed;

        public ControlsState(RenderWindow renderWindow) : base(renderWindow) { }

        public string Language { get => language; set => language = value; }

        protected override void Start()
        {
            base.Start();
            
            Vector2f screenCenter = (Vector2f)renderWindow.Size / 2f;
            textsFont = new Font("Assets/Fonts/SMOKIND.otf");

            string titleLabel = "Controls";
            uint titleSize = 50;

            titleText = new Text(titleLabel, textsFont, titleSize);

            FloatRect titleBounds = titleText.GetGlobalBounds();
            float titleVerticalOffset = 300f;

            titleText.FillColor = Color.Blue;
            titleText.OutlineColor = Color.White;
            titleText.OutlineThickness = 10f;

            titleText.Position = new Vector2f(screenCenter.X - titleBounds.Width / 2f, screenCenter.Y - titleBounds.Height / 2f - titleVerticalOffset);

            background = new Entity("Assets/Images/SpaceBackground.png");
            background.Position = new Vector2f(0f, 0f);
                       
            movementControls = new Entity("Assets/Images/ArrowKeys.png");
            float movementControlsWidthOffset = 300f;
            float movementControlsHeigthOffset = 140f;
            FloatRect movementControlsBounds = movementControls.Graphic.GetGlobalBounds();
            movementControls.Graphic.Origin = new Vector2f(movementControlsBounds.Width / 2, movementControlsBounds.Height / 2);
            movementControls.Position = new Vector2f(screenCenter.X - movementControlsWidthOffset, screenCenter.Y - movementControlsHeigthOffset);

            shootingControls = new Entity("Assets/Images/XKey.png");
            float shottingControlsWidthOffset = 300f;
            float shottingControlsHeigthOffset = 40f;
            FloatRect shootingControlsBounds = shootingControls.Graphic.GetGlobalBounds();
            shootingControls.Graphic.Origin = new Vector2f(shootingControlsBounds.Width / 2, shootingControlsBounds.Height / 2);
            shootingControls.Position = new Vector2f(screenCenter.X - shottingControlsWidthOffset, screenCenter.Y + shottingControlsHeigthOffset);

            pauseControls = new Entity("Assets/Images/EscKey.png");
            float pauseControlsWidthOffset = 300f;
            float pauseControlsHeigthOffset = 218f;
            FloatRect pauseControlsBounds = pauseControls.Graphic.GetGlobalBounds();
            pauseControls.Graphic.Origin = new Vector2f(pauseControlsBounds.Width / 2, pauseControlsBounds.Height / 2);
            pauseControls.Position = new Vector2f(screenCenter.X - pauseControlsWidthOffset, screenCenter.Y + pauseControlsHeigthOffset);


            uint controlsTextSize = 60;
            Color controlsTextColor = Color.Black;
            Color controlsTextOutlineColor = Color.White;
            float controlsTextOutlineThickness = 10f;

            movementControlsText = new Text("MOVEMENT!", textsFont, controlsTextSize);

            FloatRect movementControlsTextBounds = movementControlsText.GetGlobalBounds();
            float movementControlsTextWidthOffset = 300f;
            float movementControlsTextHeigthOffset = 180f;

            movementControlsText.FillColor = controlsTextColor;
            movementControlsText.OutlineColor = controlsTextOutlineColor;
            movementControlsText.OutlineThickness = controlsTextOutlineThickness;

            movementControlsText.Position = new Vector2f(screenCenter.X - movementControlsTextBounds.Width / 2f + movementControlsTextWidthOffset, screenCenter.Y - movementControlsTextBounds.Height / 2f - movementControlsTextHeigthOffset);

            shootingControlsText = new Text("SHOOTING!", textsFont, controlsTextSize);

            FloatRect shootingControlsTextBounds = shootingControlsText.GetGlobalBounds();
            float shootingControlsTextWidthOffset = 300f;
            float shootingControlsTextHeigthOffset = 20f;

            shootingControlsText.FillColor = controlsTextColor;
            shootingControlsText.OutlineColor = controlsTextOutlineColor;
            shootingControlsText.OutlineThickness = controlsTextOutlineThickness;

            shootingControlsText.Position = new Vector2f(screenCenter.X - shootingControlsTextBounds.Width / 2f + shootingControlsTextWidthOffset, screenCenter.Y - shootingControlsTextBounds.Height / 2f + shootingControlsTextHeigthOffset);

            pauseControlsText = new Text("PAUSE!", textsFont, controlsTextSize);

            FloatRect pauseControlsTextBounds = pauseControlsText.GetGlobalBounds();
            float pauseControlsTextWidthOffset = 300f;
            float pauseControlsTextHeigthOffset = 198f;

            pauseControlsText.FillColor = controlsTextColor;
            pauseControlsText.OutlineColor = controlsTextOutlineColor;
            pauseControlsText.OutlineThickness = controlsTextOutlineThickness;

            pauseControlsText.Position = new Vector2f(screenCenter.X - pauseControlsTextBounds.Width / 2f + pauseControlsTextWidthOffset, screenCenter.Y - pauseControlsTextBounds.Height / 2f + pauseControlsTextHeigthOffset);

            Color buttonBackgroundColor = Color.Green;
            Color buttonTextColor = Color.Black;
            Color buttonOutlineColor = Color.White;
            string buttonFontPath = "Assets/Fonts/SMOKIND.otf";
            string buttonBackgroundPath = "Assets/Images/Button.png";
            float buttonOutlineThickness = 2f;
            float buttonsTextHeightOffset = 12f;
            uint buttonTextSize = 40;
            
            backButton = new Button(renderWindow, buttonFontPath, buttonBackgroundPath);

            backButton.SetText("Back", buttonTextSize);            
            backButton.SetColor(buttonBackgroundColor);           
            backButton.FormatText(buttonTextColor, buttonOutlineColor, outline: true, buttonOutlineThickness);
            backButton.SetPosition(new Vector2f(screenCenter.X, (screenCenter.Y + 300f)));
            backButton.Text.Position = new Vector2f(backButton.Background.Position.X, backButton.Background.Position.Y - buttonsTextHeightOffset);
            backButton.Background.Scale = new Vector2f(1f, 0.8f);

            backButton.OnPressed += OnPressBack;
            backButton.OnTouched += OnTouchedBack;
            backButton.OnNotTouched += OnNotTouchedBack;
        }

        private void OnPressBack() => OnBackPressed?.Invoke();
        private void OnTouchedBack() => backButton.Background.Color = new Color(45, 200, 44);
        private void OnNotTouchedBack() => backButton.Background.Color = Color.Green;

        protected override void Update(float deltaTime)
        {
            if (language == "es")
            {
                backButton.SetText("Atrás", 40);

                titleText.DisplayedString = "Controles";
                movementControlsText.DisplayedString = "MOVIMIENTO!";
                shootingControlsText.DisplayedString = "DISPARO!";
                pauseControlsText.DisplayedString = "PAUSA!";
            }
            else if (language == "en")
            {
                backButton.SetText("Back", 40);

                titleText.DisplayedString = "Controls";
                movementControlsText.DisplayedString = "MOVEMENT!";
                shootingControlsText.DisplayedString = "SHOOTING!";
                pauseControlsText.DisplayedString = "PAUSE!";
            }
        }

        protected override void Draw()
        {
            renderWindow.Draw(background.Graphic);

            renderWindow.Draw(titleText);

            renderWindow.Draw(movementControls.Graphic);

            renderWindow.Draw(shootingControls.Graphic);

            renderWindow.Draw(pauseControls.Graphic);

            renderWindow.Draw(movementControlsText);

            renderWindow.Draw(shootingControlsText);

            renderWindow.Draw(pauseControlsText);

            backButton.Draw();
        }

        protected override void Finish()
        {
            base.Finish();

            backButton.OnPressed -= OnPressBack;
            backButton.OnTouched -= OnTouchedBack;
            backButton.OnNotTouched -= OnNotTouchedBack;

        }

    }
}
