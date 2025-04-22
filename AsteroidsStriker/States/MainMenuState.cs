using System;
using SFML.Audio;
using SFML.System;
using SFML.Graphics;
using System.Security.Cryptography;

namespace SpaceShipGame3
{
    public class MainMenuState : LoopState
    {       
        public string language = "en";

        private Music backgroundMusic;

        private Font titleFont;
        private Text titleText;

        private Entity background;

        private Button playButton;
        private Button instructionsButton;
        private Button controlsButton;
        private Button creditsButton;
        private Button quitButton;
        private Button changeLanguageButton;
        private Button highScoreButton;

        public event Action OnPlayPressed;
        public event Action OnInstructionsPressed;
        public event Action OnControlsPressed;
        public event Action OnCreditsPressed;
        public event Action OnQuitPressed;
        public event Action OnChangeLanguagePressed;
        public event Action OnHighScorePressed;
               
        public MainMenuState(RenderWindow renderWindow) : base(renderWindow) 
        {
            backgroundMusic = new Music("Assets/Sounds/PixelatedAdventure.wav");
            backgroundMusic.Loop = true;
        }

        public string Language { get => language; set => language = value; }
        public Music BackgroundMusic { get => backgroundMusic; set => backgroundMusic = value; }
        
        protected override void Start()
        {            
            base.Start();                       

            backgroundMusic.Play();

            string titleLabel = "ASTEROIDS STRIKER";
            uint titleSize = 80;

            titleFont = new Font("Assets/Fonts/MASTENG.ttf");
            titleText = new Text(titleLabel, titleFont, titleSize);

            Vector2f screenCenter = (Vector2f)renderWindow.Size / 2f;

            FloatRect titleBounds = titleText.GetGlobalBounds();    
            float verticalOffset = 370f;
                        
            titleText.FillColor = Color.Blue;
            titleText.OutlineColor = Color.White;
            titleText.OutlineThickness = 10f;

            titleText.Position = new Vector2f(screenCenter.X - titleBounds.Width / 2f, screenCenter.Y - titleBounds.Height / 2f - verticalOffset + 60);

            background = new Entity("Assets/Images/SpaceBackground.png");
            background.Position = new Vector2f(0f, 0f);

            Color buttonsBackgroundColor = Color.Green;
            Color buttonsTextColor = Color.Black;
            Color buttonsOutlineColor = Color.White;
            string buttonsFontPath = "Assets/Fonts/SMOKIND.otf";
            string buttonsBackgroundPath = "Assets/Images/Button.png";  
            float buttonOutlineThickness = 2f;
            float buttonsSpacing = 0f;
            float buttonsHeight = 110f;
            float buttonsTextHeightOffset = 11f;
            uint buttonsTextSize = 40;     
                      
            playButton = new Button(renderWindow, buttonsFontPath, buttonsBackgroundPath);
            instructionsButton = new Button(renderWindow, buttonsFontPath, buttonsBackgroundPath);
            controlsButton = new Button(renderWindow, buttonsFontPath, buttonsBackgroundPath);
            creditsButton = new Button(renderWindow, buttonsFontPath, buttonsBackgroundPath);
            quitButton = new Button(renderWindow, buttonsFontPath, buttonsBackgroundPath);
            changeLanguageButton = new Button(renderWindow, buttonsFontPath, buttonsBackgroundPath);
            highScoreButton = new Button(renderWindow, buttonsFontPath, buttonsBackgroundPath);

            playButton.SetText("Play", buttonsTextSize);
            instructionsButton.SetText("Instructions", buttonsTextSize);
            controlsButton.SetText("Controls", buttonsTextSize);
            creditsButton.SetText("Credits", buttonsTextSize);
            quitButton.SetText("Quit", buttonsTextSize);
            changeLanguageButton.SetText("Español", buttonsTextSize);
            highScoreButton.SetText("High Scores", buttonsTextSize);
                        
            playButton.SetColor(buttonsBackgroundColor);
            instructionsButton.SetColor(buttonsBackgroundColor);
            controlsButton.SetColor(buttonsBackgroundColor);
            creditsButton.SetColor(buttonsBackgroundColor);
            quitButton.SetColor(buttonsBackgroundColor);
            changeLanguageButton.SetColor(buttonsBackgroundColor);
            highScoreButton.SetColor(buttonsBackgroundColor);

            playButton.FormatText(buttonsTextColor, buttonsOutlineColor, outline: true, buttonOutlineThickness);
            instructionsButton.FormatText(buttonsTextColor, buttonsOutlineColor, outline: true, buttonOutlineThickness);
            controlsButton.FormatText(buttonsTextColor, buttonsOutlineColor, outline: true, buttonOutlineThickness);
            creditsButton.FormatText(buttonsTextColor, buttonsOutlineColor, outline: true, buttonOutlineThickness);
            quitButton.FormatText(buttonsTextColor, buttonsOutlineColor, outline: true, buttonOutlineThickness);
            changeLanguageButton.FormatText(buttonsTextColor, buttonsOutlineColor, outline: true, buttonOutlineThickness);
            highScoreButton.FormatText(buttonsTextColor, buttonsOutlineColor, outline: true, buttonOutlineThickness);
           
            playButton.SetPosition(new Vector2f(screenCenter.X, (screenCenter.Y - 155f)));
            instructionsButton.SetPosition(new Vector2f(screenCenter.X, (screenCenter.Y - 155f) + buttonsHeight + buttonsSpacing));
            controlsButton.SetPosition(new Vector2f(screenCenter.X, (screenCenter.Y - 155f) + buttonsHeight * 2 + buttonsSpacing * 2));
            creditsButton.SetPosition(new Vector2f(screenCenter.X, (screenCenter.Y - 155f) + buttonsHeight * 3 + buttonsSpacing * 3));
            quitButton.SetPosition(new Vector2f(screenCenter.X, (screenCenter.Y - 155f) + buttonsHeight * 4 + buttonsSpacing * 4));
            changeLanguageButton.SetPosition(new Vector2f(screenCenter.X + 450, (screenCenter.Y - 130f) + buttonsHeight * 4 + buttonsSpacing * 4));
            highScoreButton.SetPosition(new Vector2f(screenCenter.X + 450, (screenCenter.Y - 230f) + buttonsHeight * 4 + buttonsSpacing * 4));

            playButton.Text.Position = new Vector2f(playButton.Background.Position.X, playButton.Background.Position.Y - buttonsTextHeightOffset);
            instructionsButton.Text.Position = new Vector2f(instructionsButton.Background.Position.X, instructionsButton.Background.Position.Y - buttonsTextHeightOffset);
            controlsButton.Text.Position = new Vector2f(controlsButton.Background.Position.X, controlsButton.Background.Position.Y - buttonsTextHeightOffset);
            creditsButton.Text.Position = new Vector2f(creditsButton.Background.Position.X, creditsButton.Background.Position.Y - buttonsTextHeightOffset);
            quitButton.Text.Position = new Vector2f(quitButton.Background.Position.X, quitButton.Background.Position.Y - buttonsTextHeightOffset);        
            changeLanguageButton.Text.Position = new Vector2f(changeLanguageButton.Background.Position.X, changeLanguageButton.Background.Position.Y - buttonsTextHeightOffset);        
            highScoreButton.Text.Position = new Vector2f(highScoreButton.Background.Position.X, highScoreButton.Background.Position.Y - buttonsTextHeightOffset);

            playButton.Background.Scale = new Vector2f(1f, 0.8f);
            instructionsButton.Background.Scale = new Vector2f(1f, 0.8f);
            controlsButton.Background.Scale = new Vector2f(1f, 0.8f);
            creditsButton.Background.Scale = new Vector2f(1f, 0.8f);
            quitButton.Background.Scale = new Vector2f(1f, 0.8f);
            changeLanguageButton.Background.Scale = new Vector2f(1f, 0.8f);
            highScoreButton.Background.Scale = new Vector2f(1f, 0.8f);

            playButton.OnPressed += OnPressPlay;
            instructionsButton.OnPressed += OnPressInstructions;
            controlsButton.OnPressed += OnPressControls;
            creditsButton.OnPressed += OnPressCredits;
            quitButton.OnPressed += OnPressQuit;
            changeLanguageButton.OnPressed += OnPressLanguageButton;
            highScoreButton.OnPressed += OnPressHighScoreButton;

            playButton.OnTouched += OnTouchedPlay;
            instructionsButton.OnTouched += OnTouchedInstructions;
            controlsButton.OnTouched += OnTouchedControls;
            creditsButton.OnTouched += OnTouchedCredits;
            quitButton.OnTouched += OnTouchedQuit;
            changeLanguageButton.OnTouched += OnTouchedChangeLanguage;
            highScoreButton.OnTouched += OnTouchedHighScore;

            playButton.OnNotTouched += OnNotTouchedPlay;
            instructionsButton.OnNotTouched += OnNotTouchedInstructions;
            controlsButton.OnNotTouched += OnNotTouchedControls;
            creditsButton.OnNotTouched += OnNotTouchedCredits;
            quitButton.OnNotTouched += OnNotTouchedQuit;
            changeLanguageButton.OnNotTouched += OnNotTouchedChangeLanguage;
            highScoreButton.OnNotTouched += OnNotTouchedHighScore;


        }

        private void OnPressPlay() => OnPlayPressed?.Invoke();
        private void OnPressInstructions() => OnInstructionsPressed?.Invoke();
        private void OnPressControls() => OnControlsPressed?.Invoke();
        private void OnPressCredits() => OnCreditsPressed?.Invoke();
        private void OnPressQuit() => OnQuitPressed?.Invoke();
        private void OnPressLanguageButton() => OnChangeLanguagePressed?.Invoke();
        private void OnPressHighScoreButton() => OnHighScorePressed?.Invoke();

        private void OnTouchedPlay() => playButton.Background.Color = new Color(45, 200, 44);
        private void OnTouchedInstructions() => instructionsButton.Background.Color = new Color(45, 200, 44);
        private void OnTouchedControls() => controlsButton.Background.Color = new Color(45, 200, 44);
        private void OnTouchedCredits() => creditsButton.Background.Color = new Color(45, 200, 44);
        private void OnTouchedQuit() => quitButton.Background.Color = new Color(45, 200, 44);
        private void OnTouchedChangeLanguage() => changeLanguageButton.Background.Color = new Color(45, 200, 44);
        private void OnTouchedHighScore() => highScoreButton.Background.Color = new Color(45, 200, 44);

        private void OnNotTouchedPlay() => playButton.Background.Color = Color.Green;
        private void OnNotTouchedInstructions() => instructionsButton.Background.Color = Color.Green;
        private void OnNotTouchedControls() => controlsButton.Background.Color = Color.Green;
        private void OnNotTouchedCredits() => creditsButton.Background.Color = Color.Green;
        private void OnNotTouchedQuit() => quitButton.Background.Color = Color.Green;
        private void OnNotTouchedChangeLanguage() => changeLanguageButton.Background.Color = Color.Green;
        private void OnNotTouchedHighScore() => highScoreButton.Background.Color = Color.Green;

        protected override void Update(float deltaTime) 
        {
            uint buttonsTextSize = 40;

            if (language == "en")
            {                
                playButton.SetText("Play", buttonsTextSize);
                instructionsButton.SetText("Instructions", buttonsTextSize);
                controlsButton.SetText("Controls", buttonsTextSize);
                creditsButton.SetText("Credits", buttonsTextSize);
                quitButton.SetText("Quit", buttonsTextSize);
                changeLanguageButton.SetText("Español", buttonsTextSize);
                highScoreButton.SetText("High Score", buttonsTextSize);
            }
            else if (language == "es")
            {                
                playButton.SetText("Jugar", buttonsTextSize);
                instructionsButton.SetText("Instrucciones", buttonsTextSize);
                controlsButton.SetText("Controles", buttonsTextSize);
                creditsButton.SetText("Créditos", buttonsTextSize);
                quitButton.SetText("Salir", buttonsTextSize);
                changeLanguageButton.SetText("English", buttonsTextSize);
                highScoreButton.SetText("Puntuación", buttonsTextSize);
            }

        }

        protected override void Draw()
        {
            renderWindow.Draw(background.Graphic);

            renderWindow.Draw(titleText);

            playButton.Draw();

            instructionsButton.Draw();

            controlsButton.Draw();

            creditsButton.Draw();

            quitButton.Draw();

            changeLanguageButton.Draw();

            highScoreButton.Draw();
        }

        protected override void Finish()
        {
            base.Finish();

            playButton.OnPressed -= OnPressPlay;
            instructionsButton.OnPressed -= OnPressInstructions;
            controlsButton.OnPressed -= OnPressControls;
            creditsButton.OnPressed -= OnPressCredits;
            quitButton.OnPressed -= OnPressQuit;
            changeLanguageButton.OnPressed -= OnPressLanguageButton;
            highScoreButton.OnPressed -= OnPressHighScoreButton;

            playButton.OnTouched -= OnTouchedPlay;
            instructionsButton.OnPressed -= OnTouchedInstructions;
            controlsButton.OnPressed -= OnTouchedControls;
            creditsButton.OnPressed -= OnTouchedCredits;
            quitButton.OnPressed -= OnTouchedQuit;
            changeLanguageButton.OnPressed -= OnTouchedChangeLanguage;
            highScoreButton.OnPressed -= OnTouchedHighScore;

            backgroundMusic.Pause();                        

        }
    }
}
