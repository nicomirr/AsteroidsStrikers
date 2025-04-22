using SFML.Graphics;
using SFML.System;
using System;
using System.Collections.Generic;
using System.Diagnostics.SymbolStore;
using System.Text;

namespace SpaceShipGame3
{
    public class HighScoreState : LoopState
    {
        private string language = "en";

        private Entity background;

        private Font textsFont;

        private char [] highScoresChars = new char [115];

        private Text titleText;

        private Text score1;
        private Text score1Number;        

        private Text score2;
        private Text score2Number;

        private Text score3;
        private Text score3Number;

        private Text score4;
        private Text score4Number;

        private Text score5;
        private Text score5Number;

        private Text score6;
        private Text score6Number;

        private Text score7;
        private Text score7Number;

        private Text score8;
        private Text score8Number;

        private Text score9;
        private Text score9Number;

        private Text score10;
        private Text score10Number;

        private Button backButton;

        public event Action OnBackPressed;

        private byte[] highScoresData = new byte[115];

        public HighScoreState(RenderWindow renderWindow, byte[] highScoresData) : base(renderWindow)
        {
            this.highScoresData= highScoresData;

        }

        public byte[] HighScoresData { get => highScoresData; set => highScoresData = value; }

        public string Language { get => language; set => language = value; }

        protected override void Start()
        {
            base.Start();

            Vector2f screenCenter = (Vector2f)renderWindow.Size / 2f;                      

            //titleText = new Text(titleLabel, textsFont, titleSize);

            //titleText.FillColor = Color.Blue;
            //titleText.OutlineColor = Color.White;
            //titleText.OutlineThickness = 10f;

            //titleText.Position = new Vector2f(screenCenter.X - titleBounds.Width / 2f, screenCenter.Y - titleBounds.Height / 2f - titleVerticalOffset);

            background = new Entity("Assets/Images/SpaceBackground.png");
            background.Position = new Vector2f(0f, 0f);

            textsFont = new Font("Assets/Fonts/SMOKIND.otf");

            string titleLabel = "High Score";
            uint titleSize = 50;
            float titleVerticalOffset = 330f;

            titleText = new Text("High Score", textsFont, titleSize);

            FloatRect titleBounds = titleText.GetGlobalBounds();

            titleText.FillColor = Color.Blue;
            titleText.OutlineColor = Color.White;
            titleText.OutlineThickness = 10f;

            titleText.Position = new Vector2f(screenCenter.X - titleBounds.Width / 2f, screenCenter.Y - titleBounds.Height / 2f - titleVerticalOffset);            
            
            uint scoresTextSize = 40;
            Color scoreTextColor = Color.Black;
            Color scoreTextOutlineColor = Color.White;
            float scoreTextOutlineThickness = 5f;

            uint scoresNumbersTextSize = 40;
            Color scoreNumbersTextColor = Color.Red;
            Color scoreNumbersTextOutlineColor = Color.White;
            float scoreNumbersTextOutlineThickness = 5f;
                       
            score1Number = new Text("1. ", textsFont, scoresNumbersTextSize);
            score2Number = new Text("2. ", textsFont, scoresNumbersTextSize);
            score3Number = new Text("3. ", textsFont, scoresNumbersTextSize);
            score4Number = new Text("4. ", textsFont, scoresNumbersTextSize);
            score5Number = new Text("5. ", textsFont, scoresNumbersTextSize);
            score6Number = new Text("6. ", textsFont, scoresNumbersTextSize);
            score7Number = new Text("7. ", textsFont, scoresNumbersTextSize);
            score8Number = new Text("8. ", textsFont, scoresNumbersTextSize);
            score9Number = new Text("9. ", textsFont, scoresNumbersTextSize);
            score10Number = new Text("10. ", textsFont, scoresNumbersTextSize);

            score1 = new Text("", textsFont, scoresTextSize);
            score2 = new Text("", textsFont, scoresTextSize);
            score3 = new Text("", textsFont, scoresTextSize);
            score4 = new Text("", textsFont, scoresTextSize);
            score5 = new Text("", textsFont, scoresTextSize);
            score6 = new Text("", textsFont, scoresTextSize);
            score7 = new Text("", textsFont, scoresTextSize);
            score8 = new Text("", textsFont, scoresTextSize);
            score9 = new Text("", textsFont, scoresTextSize);
            score10 = new Text("", textsFont, scoresTextSize);

            score1Number.FillColor = scoreNumbersTextColor;
            score2Number.FillColor = scoreNumbersTextColor;
            score3Number.FillColor = scoreNumbersTextColor;
            score4Number.FillColor = scoreNumbersTextColor;
            score5Number.FillColor = scoreNumbersTextColor;
            score6Number.FillColor = scoreNumbersTextColor;
            score7Number.FillColor = scoreNumbersTextColor;
            score8Number.FillColor = scoreNumbersTextColor;
            score9Number.FillColor = scoreNumbersTextColor;
            score10Number.FillColor = scoreNumbersTextColor;

            score1Number.OutlineColor = scoreNumbersTextOutlineColor;
            score2Number.OutlineColor = scoreNumbersTextOutlineColor;
            score3Number.OutlineColor = scoreNumbersTextOutlineColor;
            score4Number.OutlineColor = scoreNumbersTextOutlineColor;
            score5Number.OutlineColor = scoreNumbersTextOutlineColor;
            score6Number.OutlineColor = scoreNumbersTextOutlineColor;
            score7Number.OutlineColor = scoreNumbersTextOutlineColor;
            score8Number.OutlineColor = scoreNumbersTextOutlineColor;
            score9Number.OutlineColor = scoreNumbersTextOutlineColor;
            score10Number.OutlineColor = scoreNumbersTextOutlineColor;

            score1Number.OutlineThickness = scoreNumbersTextOutlineThickness;
            score2Number.OutlineThickness = scoreNumbersTextOutlineThickness;
            score3Number.OutlineThickness = scoreNumbersTextOutlineThickness;
            score4Number.OutlineThickness = scoreNumbersTextOutlineThickness;
            score5Number.OutlineThickness = scoreNumbersTextOutlineThickness;
            score6Number.OutlineThickness = scoreNumbersTextOutlineThickness;
            score7Number.OutlineThickness = scoreNumbersTextOutlineThickness;
            score8Number.OutlineThickness = scoreNumbersTextOutlineThickness;
            score9Number.OutlineThickness = scoreNumbersTextOutlineThickness;
            score10Number.OutlineThickness = scoreNumbersTextOutlineThickness;

            score1.FillColor = scoreTextColor;
            score2.FillColor = scoreTextColor;
            score3.FillColor = scoreTextColor;
            score4.FillColor = scoreTextColor;
            score5.FillColor = scoreTextColor;
            score6.FillColor = scoreTextColor;
            score7.FillColor = scoreTextColor;
            score8.FillColor = scoreTextColor;
            score9.FillColor = scoreTextColor;
            score10.FillColor = scoreTextColor;

            score1.OutlineColor = scoreTextOutlineColor;
            score2.OutlineColor = scoreTextOutlineColor;
            score3.OutlineColor = scoreTextOutlineColor;
            score4.OutlineColor = scoreTextOutlineColor;
            score5.OutlineColor = scoreTextOutlineColor;
            score6.OutlineColor = scoreTextOutlineColor;
            score7.OutlineColor = scoreTextOutlineColor;
            score8.OutlineColor = scoreTextOutlineColor;
            score9.OutlineColor = scoreTextOutlineColor;
            score10.OutlineColor = scoreTextOutlineColor;

            score1.OutlineThickness = scoreTextOutlineThickness;
            score2.OutlineThickness = scoreTextOutlineThickness;
            score3.OutlineThickness = scoreTextOutlineThickness;
            score4.OutlineThickness = scoreTextOutlineThickness;
            score5.OutlineThickness = scoreTextOutlineThickness;
            score6.OutlineThickness = scoreTextOutlineThickness;
            score7.OutlineThickness = scoreTextOutlineThickness;
            score8.OutlineThickness = scoreTextOutlineThickness;
            score9.OutlineThickness = scoreTextOutlineThickness;
            score10.OutlineThickness = scoreTextOutlineThickness;

            score1.Position = new Vector2f(screenCenter.X - 300, screenCenter.Y - 240);
            score2.Position = new Vector2f(screenCenter.X - 300, screenCenter.Y - 140);
            score3.Position = new Vector2f(screenCenter.X - 300, screenCenter.Y - 40);
            score4.Position = new Vector2f(screenCenter.X - 300, screenCenter.Y + 60);
            score5.Position = new Vector2f(screenCenter.X - 300, screenCenter.Y + 160);
            score6.Position = new Vector2f(screenCenter.X + 170, screenCenter.Y - 240 );
            score7.Position = new Vector2f(screenCenter.X + 170, screenCenter.Y - 140);
            score8.Position = new Vector2f(screenCenter.X + 170, screenCenter.Y - 40);
            score9.Position = new Vector2f(screenCenter.X + 170, screenCenter.Y + 60);
            score10.Position = new Vector2f(screenCenter.X + 170, screenCenter.Y + 160);

            score1Number.Position = new Vector2f(screenCenter.X - 360, screenCenter.Y - 240);
            score2Number.Position = new Vector2f(screenCenter.X - 360, screenCenter.Y - 140);
            score3Number.Position = new Vector2f(screenCenter.X - 360, screenCenter.Y - 40);
            score4Number.Position = new Vector2f(screenCenter.X - 360, screenCenter.Y + 60);
            score5Number.Position = new Vector2f(screenCenter.X - 360, screenCenter.Y + 160);
            score6Number.Position = new Vector2f(screenCenter.X + 110, screenCenter.Y - 240);
            score7Number.Position = new Vector2f(screenCenter.X + 110, screenCenter.Y - 140);
            score8Number.Position = new Vector2f(screenCenter.X + 110, screenCenter.Y - 40);
            score9Number.Position = new Vector2f(screenCenter.X + 110, screenCenter.Y + 60);
            score10Number.Position = new Vector2f(screenCenter.X + 110, screenCenter.Y + 160);

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
            backButton.SetPosition(new Vector2f(screenCenter.X, (screenCenter.Y + 305f)));
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
                titleText.DisplayedString = "Puntuación";
                backButton.SetText("Atrás", 40);
            }                

            else if (language == "en")
            {
                titleText.DisplayedString = "High Score";
                backButton.SetText("Back", 40);
            }
                

            highScoresChars = ASCIIEncoding.ASCII.GetChars(highScoresData, 0, (int)highScoresChars.Length);

            score1.DisplayedString = Convert.ToString(highScoresChars[0]) + Convert.ToString(highScoresChars[1]) + Convert.ToString(highScoresChars[2]) + Convert.ToString(highScoresChars[3]) 
                + Convert.ToString(highScoresChars[4]) + Convert.ToString(highScoresChars[5]) + Convert.ToString(highScoresChars[6]) + Convert.ToString(highScoresChars[7]);

            score2.DisplayedString = Convert.ToString(highScoresChars[9]) + Convert.ToString(highScoresChars[10]) + Convert.ToString(highScoresChars[11]) + Convert.ToString(highScoresChars[12]) 
                + Convert.ToString(highScoresChars[13]) + Convert.ToString(highScoresChars[14]) + Convert.ToString(highScoresChars[15]) + Convert.ToString(highScoresChars[16]) + Convert.ToString(highScoresChars[17]);

            score3.DisplayedString = Convert.ToString(highScoresChars[18]) + Convert.ToString(highScoresChars[19]) + Convert.ToString(highScoresChars[20]) + Convert.ToString(highScoresChars[21]) + Convert.ToString(highScoresChars[22])
                + Convert.ToString(highScoresChars[23]) + Convert.ToString(highScoresChars[24]) + Convert.ToString(highScoresChars[25]) + Convert.ToString(highScoresChars[26]);

            score4.DisplayedString = Convert.ToString(highScoresChars[27]) + Convert.ToString(highScoresChars[28]) + Convert.ToString(highScoresChars[29]) + Convert.ToString(highScoresChars[30]) + Convert.ToString(highScoresChars[31]) 
                + Convert.ToString(highScoresChars[32]) + Convert.ToString(highScoresChars[33]) + Convert.ToString(highScoresChars[34]) + Convert.ToString(highScoresChars[35]); 

            score5.DisplayedString = Convert.ToString(highScoresChars[36]) + Convert.ToString(highScoresChars[37]) + Convert.ToString(highScoresChars[38]) + Convert.ToString(highScoresChars[39]) + Convert.ToString(highScoresChars[40]) 
                + Convert.ToString(highScoresChars[41]) + Convert.ToString(highScoresChars[42]) + Convert.ToString(highScoresChars[43]) + Convert.ToString(highScoresChars[44]);

            score6.DisplayedString = Convert.ToString(highScoresChars[45]) + Convert.ToString(highScoresChars[46]) + Convert.ToString(highScoresChars[47]) + Convert.ToString(highScoresChars[48]) + Convert.ToString(highScoresChars[49]) 
                + Convert.ToString(highScoresChars[50]) + Convert.ToString(highScoresChars[51]) + Convert.ToString(highScoresChars[52]) + Convert.ToString(highScoresChars[53]);

            score7.DisplayedString = Convert.ToString(highScoresChars[54]) + Convert.ToString(highScoresChars[55]) + Convert.ToString(highScoresChars[56]) + Convert.ToString(highScoresChars[57] + Convert.ToString(highScoresChars[58])
                + Convert.ToString(highScoresChars[59]) + Convert.ToString(highScoresChars[60]) + Convert.ToString(highScoresChars[61]) + Convert.ToString(highScoresChars[62]));

            score8.DisplayedString = Convert.ToString(highScoresChars[63]) + Convert.ToString(highScoresChars[64]) + Convert.ToString(highScoresChars[65]) + Convert.ToString(highScoresChars[66] + Convert.ToString(highScoresChars[67])
                + Convert.ToString(highScoresChars[68]) + Convert.ToString(highScoresChars[69]) + Convert.ToString(highScoresChars[70]) + Convert.ToString(highScoresChars[71]));

            score9.DisplayedString = Convert.ToString(highScoresChars[72]) + Convert.ToString(highScoresChars[73]) + Convert.ToString(highScoresChars[74]) + Convert.ToString(highScoresChars[75] + Convert.ToString(highScoresChars[76])
                + Convert.ToString(highScoresChars[77]) + Convert.ToString(highScoresChars[78]) + Convert.ToString(highScoresChars[79]) + Convert.ToString(highScoresChars[80]));

            score10.DisplayedString = Convert.ToString(highScoresChars[81]) + Convert.ToString(highScoresChars[82]) + Convert.ToString(highScoresChars[83]) + Convert.ToString(highScoresChars[84] + Convert.ToString(highScoresChars[85])
                + Convert.ToString(highScoresChars[86]) + Convert.ToString(highScoresChars[87]) + Convert.ToString(highScoresChars[88]) + Convert.ToString(highScoresChars[89]));

        }

        protected override void Draw()
        {
            renderWindow.Draw(background.Graphic);

            renderWindow.Draw(titleText);

            renderWindow.Draw(score1);
            renderWindow.Draw(score2);
            renderWindow.Draw(score3);
            renderWindow.Draw(score4);
            renderWindow.Draw(score5);
            renderWindow.Draw(score6);
            renderWindow.Draw(score7);
            renderWindow.Draw(score8);
            renderWindow.Draw(score9);
            renderWindow.Draw(score10);

            renderWindow.Draw(score1Number);
            renderWindow.Draw(score2Number);
            renderWindow.Draw(score3Number);
            renderWindow.Draw(score4Number);
            renderWindow.Draw(score5Number);
            renderWindow.Draw(score6Number);
            renderWindow.Draw(score7Number);
            renderWindow.Draw(score8Number);
            renderWindow.Draw(score9Number);
            renderWindow.Draw(score10Number);

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
