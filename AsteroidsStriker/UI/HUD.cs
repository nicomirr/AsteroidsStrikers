using SFML.System;
using SFML.Graphics;
using System;

namespace SpaceShipGame3
{
    public class HUD
    {
        private string language = "en";

        private Font hudFont;                       
        private RenderWindow renderWindow;
        private Player player;

        private Text livesText;
        private Text scoreText;
        private Text fuelText;
        private Text plasmaText;
        private Text waveText;

        private const string LivesLabel = "LIVES X ";
        private const string ScoreLabel = "SCORE: ";
        private const string FuelLabel = "FUEL: ";
        private const string PlasmaLabel = "PLASMA: ";
        private const string WaveLabel = "WAVE: ";

        public HUD(RenderWindow renderWindow, Player player, string fontPath)
        {
            this.renderWindow = renderWindow;
            this.player = player;
            hudFont = new Font(fontPath);       
            livesText = new Text(LivesLabel + "0", hudFont);                        
            scoreText = new Text(ScoreLabel + "0", hudFont);
            fuelText = new Text(FuelLabel + "0", hudFont);
            plasmaText = new Text(PlasmaLabel + "0", hudFont);
            waveText = new Text(WaveLabel + "0", hudFont);

            Color textColor = Color.Green;
            Color outlineColor = Color.Black;
            uint characterSize = 23;
            float outlineThickness = 3;

            livesText.FillColor = textColor;       
            livesText.CharacterSize = characterSize;           
            livesText.OutlineColor = outlineColor;   
            livesText.OutlineThickness = outlineThickness;

            scoreText.FillColor = textColor;
            scoreText.CharacterSize = characterSize;
            scoreText.OutlineColor = outlineColor;
            scoreText.OutlineThickness = outlineThickness;

            fuelText.FillColor = textColor;
            fuelText.CharacterSize = characterSize;
            fuelText.OutlineColor = outlineColor;
            fuelText.OutlineThickness = outlineThickness;

            plasmaText.FillColor = textColor;
            plasmaText.CharacterSize = characterSize;
            plasmaText.OutlineColor = outlineColor;
            plasmaText.OutlineThickness = outlineThickness;

            waveText.FillColor = textColor;
            waveText.CharacterSize = characterSize;
            waveText.OutlineColor = outlineColor;
            waveText.OutlineThickness = outlineThickness;
        }

        public string Language { get => language; set => language = value; }

        public void Update(string waveNumber)    
        {            
            View view = renderWindow.GetView();         

            float halfWindowWidth = renderWindow.Size.X / 2f;
            float halfWindowHeight = renderWindow.Size.Y / 2f;
            float quarterWindowWidth = renderWindow.Size.X / 4f;        
            float marginSizeX = 30;
            float marginSizeY = 4;

            int playerFuel = (int)player.Fuel;

            float scoreWidth = scoreText.GetGlobalBounds().Width;
            float fuelWidth = fuelText.GetGlobalBounds().Width;
            float waveHalfWidth = waveText.GetGlobalBounds().Width / 2;

            Vector2f livesOffset = new Vector2f(-halfWindowWidth + marginSizeX, -halfWindowHeight + marginSizeY);
            Vector2f scoreOffset = new Vector2f(halfWindowWidth - marginSizeX - scoreWidth, -halfWindowHeight + marginSizeY);
            Vector2f fuelOffset = new Vector2f(-quarterWindowWidth - fuelWidth, -halfWindowHeight + marginSizeY);
            Vector2f plasmaOffsetEnglish = new Vector2f(quarterWindowWidth, -halfWindowHeight + marginSizeY);
            Vector2f plasmaOffsetSpanish = new Vector2f(quarterWindowWidth - 70, -halfWindowHeight + marginSizeY);
            Vector2f waveOffset = new Vector2f(-waveHalfWidth, -halfWindowHeight + marginSizeY);

            Vector2f livesPosition = view.Center + livesOffset;
            Vector2f scorePosition = view.Center + scoreOffset;
            Vector2f fuelPosition = view.Center + fuelOffset;
            Vector2f plasmaPositionEnglish = view.Center + plasmaOffsetEnglish;
            Vector2f plasmaPositionSpanish = view.Center + plasmaOffsetSpanish;
            Vector2f wavePosition = view.Center + waveOffset;

            livesText.Position = livesPosition;
            scoreText.Position = scorePosition;
            fuelText.Position = fuelPosition;            
            waveText.Position = wavePosition;

            if (language == "es")
            {
                livesText.DisplayedString = "VIDAS X " + player.Lives.ToString();
                scoreText.DisplayedString = "PUNTUACION: " + player.Score.ToString();
                fuelText.DisplayedString = "NAFTA: " + playerFuel.ToString();
                plasmaText.DisplayedString = "PLASMA: " + player.Plasma.ToString();
                waveText.DisplayedString = "OLEADA: " + waveNumber;

                plasmaText.Position = plasmaPositionSpanish;
            }
            else if (language == "en")
            {
                livesText.DisplayedString = "LIVES X " + player.Lives.ToString();
                scoreText.DisplayedString = "SCORE: " + player.Score.ToString();
                fuelText.DisplayedString = "FUEL: " + playerFuel.ToString();
                plasmaText.DisplayedString = "PLASMA: " + player.Plasma.ToString();
                waveText.DisplayedString = "WAVE: " + waveNumber;

                plasmaText.Position = plasmaPositionEnglish;
            }
        }

        public void Draw()
        {
            renderWindow.Draw(livesText);
            renderWindow.Draw(scoreText);
            renderWindow.Draw(fuelText);
            renderWindow.Draw(plasmaText);
            renderWindow.Draw(waveText);
        }
    }
}
