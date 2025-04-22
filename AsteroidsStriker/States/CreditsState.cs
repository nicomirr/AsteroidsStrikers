using System;
using SFML.System;
using SFML.Graphics;

namespace SpaceShipGame3
{
    public class CreditsState : LoopState
    {
        private string language = "en";

        private Font textsFont;
        private Text titleText;

        private Entity background;

        private Text developerNameText;        
        private Text imagesTitleText;
        private Text arrowsAssetText;
        private Text shootingStarAssetText;
        private Text spaceshipAssetText;
        private Text fontsTitleText;
        private Text mastengFontAssetText;
        private Text smokindFontAssetText;
        private Text musicTitleText;
        private Text pixelPerfectAssetText;
        private Text pixelatedAdventureAssetText;

        private Button backButton;

        public event Action OnBackPressed;

        public CreditsState(RenderWindow renderWindow) : base(renderWindow) { }

        public string Language { get => language; set => language = value; }

        protected override void Start()
        {
            base.Start();

            Vector2f screenCenter = (Vector2f)renderWindow.Size / 2f;
            textsFont = new Font("Assets/Fonts/SMOKIND.otf");

            string titleLabel = "Credits";
            uint titleSize = 50;

            titleText = new Text(titleLabel, textsFont, titleSize);

            FloatRect titleBounds = titleText.GetGlobalBounds();
            float titleVerticalOffset = 330f;

            titleText.FillColor = Color.Blue;
            titleText.OutlineColor = Color.White;
            titleText.OutlineThickness = 10f;

            titleText.Position = new Vector2f(screenCenter.X - titleBounds.Width / 2f, screenCenter.Y - titleBounds.Height / 2f - titleVerticalOffset);

            background = new Entity("Assets/Images/SpaceBackground.png");
            background.Position = new Vector2f(0f, 0f);

            uint developerCreditsTextSize = 20;
            uint thirdPartyCreditsTextSize = 15;
            Color creditsTextColor = Color.Red;            
            Color titleTextColor = Color.Magenta;            
            Color textOutlineColor = Color.White;
            float textOutlineThickness = 5f;

            developerNameText = new Text("Art & Development: Nicolas Mironoff", textsFont, developerCreditsTextSize);

            FloatRect developerNameTextBounds = developerNameText.GetGlobalBounds();
            float developerNameTextHeigthOffset = 250f;

            developerNameText.FillColor = creditsTextColor;
            developerNameText.OutlineColor = textOutlineColor;
            developerNameText.OutlineThickness = textOutlineThickness;

            developerNameText.Position = new Vector2f(screenCenter.X - developerNameTextBounds.Width / 2f, screenCenter.Y - developerNameTextBounds.Height / 2f - developerNameTextHeigthOffset);

            imagesTitleText = new Text("Third party Assets (Images):", textsFont, thirdPartyCreditsTextSize);

            FloatRect imagesTitleTextBounds = imagesTitleText.GetGlobalBounds();
            float imagesTitleTextHeightOffset = 190f;            

            imagesTitleText.FillColor = titleTextColor;
            imagesTitleText.OutlineColor = textOutlineColor;
            imagesTitleText.OutlineThickness = textOutlineThickness;
            
            imagesTitleText.Position = new Vector2f(screenCenter.X - imagesTitleTextBounds.Width / 2f, screenCenter.Y - imagesTitleTextBounds.Height / 2f - imagesTitleTextHeightOffset);

            arrowsAssetText = new Text("Keyboard arrows asset by JGiver (https://pngtree.com/freepng/keyboard-arrow-keys_7260323.html)", textsFont, thirdPartyCreditsTextSize);

            FloatRect arrowsAssetTextBounds = arrowsAssetText.GetGlobalBounds();
            float arrowsAssetTextHeightOffset = 150f;

            arrowsAssetText.FillColor = creditsTextColor;
            arrowsAssetText.OutlineColor = textOutlineColor;
            arrowsAssetText.OutlineThickness = textOutlineThickness;
            
            arrowsAssetText.Position = new Vector2f(screenCenter.X - arrowsAssetTextBounds.Width / 2f, screenCenter.Y - arrowsAssetTextBounds.Height / 2f - arrowsAssetTextHeightOffset);

            shootingStarAssetText = new Text("Shooting star asset by Cadmium_Red (https://www.shutterstock.com/es/image-vector/shiny-golden-stars-pixel-art-icon-2135699667)", textsFont, thirdPartyCreditsTextSize);

            FloatRect shootingStarAssetTextBounds = shootingStarAssetText.GetGlobalBounds();
            float shootingStarAssetTextHeightOffset = 110f;

            shootingStarAssetText.FillColor = creditsTextColor;
            shootingStarAssetText.OutlineColor = textOutlineColor;
            shootingStarAssetText.OutlineThickness = textOutlineThickness;
             
            shootingStarAssetText.Position = new Vector2f(screenCenter.X - shootingStarAssetTextBounds.Width / 2f, screenCenter.Y - shootingStarAssetTextBounds.Height / 2f - shootingStarAssetTextHeightOffset);
                        
            spaceshipAssetText = new Text("Spaceship asset by BizmasterStudios (https://bizmasterstudios.itch.io/spaceship-creation-kit?download)", textsFont, thirdPartyCreditsTextSize);

            FloatRect spaceshipAssetTextBounds = spaceshipAssetText.GetGlobalBounds();
            float spaceShippAssetTextHeightOffset = 70f;

            spaceshipAssetText.FillColor = creditsTextColor;
            spaceshipAssetText.OutlineColor = textOutlineColor;
            spaceshipAssetText.OutlineThickness = textOutlineThickness;
            
            spaceshipAssetText.Position = new Vector2f(screenCenter.X - spaceshipAssetTextBounds.Width / 2f, screenCenter.Y - spaceshipAssetTextBounds.Height / 2f - spaceShippAssetTextHeightOffset);

            fontsTitleText = new Text("Third party Assets (Fonts):", textsFont, thirdPartyCreditsTextSize);

            FloatRect fontsTitleTextBounds = fontsTitleText.GetGlobalBounds();
            float fontsTitleTextHeightOffset = 10f;

            fontsTitleText.FillColor = titleTextColor;
            fontsTitleText.OutlineColor = textOutlineColor;
            fontsTitleText.OutlineThickness = textOutlineThickness;
            
            fontsTitleText.Position = new Vector2f(screenCenter.X - fontsTitleTextBounds.Width / 2f, screenCenter.Y - fontsTitleTextBounds.Height / 2f - fontsTitleTextHeightOffset);

            mastengFontAssetText = new Text("Masteng font asset by rozi (https://www.1001freefonts.com/es/masteng.font)", textsFont, thirdPartyCreditsTextSize);

            FloatRect mastengFontAssetTextBounds = mastengFontAssetText.GetGlobalBounds();
            float mastengFontAssetTextHeightOffset = 50f;

            mastengFontAssetText.FillColor = creditsTextColor;
            mastengFontAssetText.OutlineColor = textOutlineColor;
            mastengFontAssetText.OutlineThickness = textOutlineThickness;
            
            mastengFontAssetText.Position = new Vector2f(screenCenter.X - mastengFontAssetTextBounds.Width / 2f, screenCenter.Y - mastengFontAssetTextBounds.Height / 2f + mastengFontAssetTextHeightOffset);

            smokindFontAssetText = new Text("Smokind font asset by Storytype Studio (https://www.1001freefonts.com/es/smokind.font)", textsFont, thirdPartyCreditsTextSize);

            FloatRect smokindFontAssetTextBounds = smokindFontAssetText.GetGlobalBounds();
            float smokindFontAssetTextHeightOffset = 80f;

            smokindFontAssetText.FillColor = creditsTextColor;
            smokindFontAssetText.OutlineColor = textOutlineColor;
            smokindFontAssetText.OutlineThickness = textOutlineThickness;

            smokindFontAssetText.Position = new Vector2f(screenCenter.X - smokindFontAssetTextBounds.Width / 2f, screenCenter.Y - smokindFontAssetTextBounds.Height / 2f + smokindFontAssetTextHeightOffset);

            musicTitleText = new Text("Third party Assets (Music):", textsFont, thirdPartyCreditsTextSize);

            FloatRect musicTitleTextBounds = musicTitleText.GetGlobalBounds();
            float musicTitleTextHeightOffset = 140f;

            musicTitleText.FillColor = titleTextColor;
            musicTitleText.OutlineColor = textOutlineColor;
            musicTitleText.OutlineThickness = textOutlineThickness;
            
            musicTitleText.Position = new Vector2f(screenCenter.X - musicTitleTextBounds.Width / 2f, screenCenter.Y - musicTitleTextBounds.Height / 2f + musicTitleTextHeightOffset);

            pixelPerfectAssetText = new Text("Pixel Perfect asset by Lesiakower (https://pixabay.com/es/music/search/pixel%20perfect/?manual_search=1&order=none)", textsFont, thirdPartyCreditsTextSize);

            FloatRect pixelPerfectAssetTextBounds = pixelPerfectAssetText.GetGlobalBounds();
            float pixelPerfectAssetTextHeightOffset = 180f;

            pixelPerfectAssetText.FillColor = creditsTextColor;
            pixelPerfectAssetText.OutlineColor = textOutlineColor;
            pixelPerfectAssetText.OutlineThickness = textOutlineThickness;

            pixelPerfectAssetText.Position = new Vector2f(screenCenter.X - pixelPerfectAssetTextBounds.Width / 2f, screenCenter.Y - pixelPerfectAssetTextBounds.Height / 2f + pixelPerfectAssetTextHeightOffset);

            pixelatedAdventureAssetText = new Text("Pixelated Adventure asset by prazkhanal (https://pixabay.com/es/music/late-pixelated-adventure-122039/)", textsFont, thirdPartyCreditsTextSize);

            FloatRect pixelatedAdventureAssetTextBounds = pixelatedAdventureAssetText.GetGlobalBounds();
            float pixelatedAdventureAssetTextHeightOffset = 220f;

            pixelatedAdventureAssetText.FillColor = creditsTextColor;
            pixelatedAdventureAssetText.OutlineColor = textOutlineColor;
            pixelatedAdventureAssetText.OutlineThickness = textOutlineThickness;
            
            pixelatedAdventureAssetText.Position = new Vector2f(screenCenter.X - pixelatedAdventureAssetTextBounds.Width / 2f, screenCenter.Y - pixelatedAdventureAssetTextBounds.Height / 2f + pixelatedAdventureAssetTextHeightOffset);

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
            if(language == "es")
            {
                backButton.SetText("Atrás", 40);

                titleText.DisplayedString = "Créditos";
                developerNameText.DisplayedString = "Arte & Desarrollo: Nicolas Mironoff";
                imagesTitleText.DisplayedString = "Assets de terceros (Imagenes):";
                arrowsAssetText.DisplayedString = "Asset de flechas de telado por JGiver (https://pngtree.com/freepng/keyboard-arrow-keys_7260323.html)";
                shootingStarAssetText.DisplayedString = "Asset de estrella fugaz por Cadmium_Red (https://www.shutterstock.com/es/image-vector/shiny-golden-stars-pixel-art-icon-2135699667)";
                spaceshipAssetText.DisplayedString = "Assets de naves espaciales por BizmasterStudios (https://bizmasterstudios.itch.io/spaceship-creation-kit?download)";
                fontsTitleText.DisplayedString = "Assets de terceros (Fuentes):";
                mastengFontAssetText.DisplayedString = "Asset fuente Masteng por rozi (https://www.1001freefonts.com/es/masteng.font)";
                smokindFontAssetText.DisplayedString = "Asset fuente Smokind por Storytype Studio (https://www.1001freefonts.com/es/smokind.font)";
                musicTitleText.DisplayedString = "Assets de terceros (Música)";
                pixelPerfectAssetText.DisplayedString = "Asset Pixel Perfect por Lesiakower (https://pixabay.com/es/music/search/pixel%20perfect/?manual_search=1&order=none";
                pixelatedAdventureAssetText.DisplayedString = "Asset Pixelated Adventure por prazkhanal (https://pixabay.com/es/music/late-pixelated-adventure-122039/)";

            }
            else if(language == "en")
            {
                backButton.SetText("Back", 40);

                titleText.DisplayedString = "Credits";
                developerNameText.DisplayedString = "Art & Development: Nicolas Mironoff";
                imagesTitleText.DisplayedString = "Third party Assets (Images):";
                arrowsAssetText.DisplayedString = "Keyboard arrows asset by JGiver (https://pngtree.com/freepng/keyboard-arrow-keys_7260323.html)";
                shootingStarAssetText.DisplayedString = "Shooting star asset by Cadmium_Red (https://www.shutterstock.com/es/image-vector/shiny-golden-stars-pixel-art-icon-2135699667)";
                spaceshipAssetText.DisplayedString = "Spaceship asset by BizmasterStudios (https://bizmasterstudios.itch.io/spaceship-creation-kit?download)";
                fontsTitleText.DisplayedString = "Third party Assets (Fonts):";
                mastengFontAssetText.DisplayedString = "Masteng font asset by rozi (https://www.1001freefonts.com/es/masteng.font)";
                smokindFontAssetText.DisplayedString = "Smokind font asset by Storytype Studio (https://www.1001freefonts.com/es/smokind.font)";
                musicTitleText.DisplayedString = "Third party Assets (Music)";
                pixelPerfectAssetText.DisplayedString = "Pixel Perfect asset by Lesiakower (https://pixabay.com/es/music/search/pixel%20perfect/?manual_search=1&order=none";
                pixelatedAdventureAssetText.DisplayedString = "Pixelated Adventure asset by prazkhanal (https://pixabay.com/es/music/late-pixelated-adventure-122039/)";

            }
        }

        protected override void Draw()
        {
            renderWindow.Draw(background.Graphic);

            renderWindow.Draw(titleText);
            renderWindow.Draw(developerNameText);
            renderWindow.Draw(imagesTitleText);
            renderWindow.Draw(arrowsAssetText);                        
            renderWindow.Draw(shootingStarAssetText);                        
            renderWindow.Draw(spaceshipAssetText);                        
            renderWindow.Draw(fontsTitleText);
            renderWindow.Draw(mastengFontAssetText);
            renderWindow.Draw(smokindFontAssetText); 
            renderWindow.Draw(musicTitleText); 
            renderWindow.Draw(pixelPerfectAssetText); 
            renderWindow.Draw(pixelatedAdventureAssetText);

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
