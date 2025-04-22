using System;
using SFML.System;
using SFML.Graphics;

namespace SpaceShipGame3
{
    class InstructionsState : LoopState
    {
        private string language = "en";

        private Font textsFont;
        private Text titleText;

        private Entity background;
        private Entity fuel;
        private Entity plasma;
        private Entity shootingStar;
        private Entity life;
        private Entity smallAsteroid;
        private Entity mediumAsteroid;
        private Entity bigAsteroid;
        private Entity smallEnemy;
        private Entity mediumEnemy;
        private Entity bigEnemy;

        private Text fuelText;
        private Text plasmaText;
        private Text shootingStarText;
        private Text lifeText;
        private Text smallAsteroidText;
        private Text mediumAsteroidText;
        private Text bigAsteroidText;
        private Text smallEnemyText;
        private Text mediumEnemyText;
        private Text bigEnemyText;
        private Text destroyAsteroidAndSurviveText;

        private Button backButton;

        public event Action OnBackPressed;

        public InstructionsState(RenderWindow renderWindow) : base(renderWindow) { }

        public string Language {get => language; set => language = value; }

        protected override void Start()
        {
            base.Start();

            Vector2f screenCenter = (Vector2f)renderWindow.Size / 2f;
            textsFont = new Font("Assets/Fonts/SMOKIND.otf");

            string titleLabel = "Instructions";
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

            Vector2f imagesScale = new Vector2f(0.6f, 0.6f);

            fuel = new Entity("Assets/Images/GasCan.png");
            float fuelWidthOffset = 420f;
            float fuelHeigthOffset = 255f;
            fuel.Graphic.Scale = imagesScale;
            FloatRect fuelBounds = fuel.Graphic.GetGlobalBounds();
            fuel.Graphic.Origin = new Vector2f(fuelBounds.Width / 2, fuelBounds.Height / 2);
            fuel.Position = new Vector2f(screenCenter.X - fuelWidthOffset, screenCenter.Y - fuelHeigthOffset);

            plasma = new Entity("Assets/Images/Plasma.png");
            float plasmaWidthOffset = 420f;
            float plasmaHeigthOffset = 215f;
            plasma.Graphic.Scale = imagesScale;
            FloatRect plasmaBounds = plasma.Graphic.GetGlobalBounds();
            plasma.Graphic.Origin = new Vector2f(plasmaBounds.Width / 2, plasmaBounds.Height / 2);
            plasma.Position = new Vector2f(screenCenter.X - plasmaWidthOffset, screenCenter.Y - plasmaHeigthOffset);

            shootingStar = new Entity("Assets/Images/ShootingStar.png");
            float shootingStarWidthOffset = 420f;
            float shootingStarHeigthOffset = 175f;
            shootingStar.Graphic.Scale = imagesScale;
            FloatRect shootingStarBounds = shootingStar.Graphic.GetGlobalBounds();
            shootingStar.Graphic.Origin = new Vector2f(shootingStarBounds.Width / 2, shootingStarBounds.Height / 2);
            shootingStar.Position = new Vector2f(screenCenter.X - shootingStarWidthOffset, screenCenter.Y - shootingStarHeigthOffset);

            life = new Entity("Assets/Images/Life.png");
            float lifeWidthOffset = 420f;
            float lifeHeigthOffset = 135f;
            life.Graphic.Scale = imagesScale;
            FloatRect lifeBounds = life.Graphic.GetGlobalBounds();
            life.Graphic.Origin = new Vector2f(lifeBounds.Width / 2, lifeBounds.Height / 2);
            life.Position = new Vector2f(screenCenter.X - lifeWidthOffset, screenCenter.Y - lifeHeigthOffset);

            smallAsteroid = new Entity("Assets/Images/SmallAsteroidItem.png");
            float smallAsteroidWidthOffset = 420f;
            float smallAsteroidHeigthOffset = 95f;
            smallAsteroid.Graphic.Scale = imagesScale;
            FloatRect smallAsteroidBounds = smallAsteroid.Graphic.GetGlobalBounds();
            smallAsteroid.Graphic.Origin = new Vector2f(smallAsteroidBounds.Width / 2, smallAsteroidBounds.Height / 2);
            smallAsteroid.Position = new Vector2f(screenCenter.X - smallAsteroidWidthOffset, screenCenter.Y - smallAsteroidHeigthOffset);

            mediumAsteroid = new Entity("Assets/Images/MediumAsteroidItem.png");
            float mediumAsteroidWidthOffset = 420f;
            float mediumAsteroidHeigthOffset = 55f;
            mediumAsteroid.Graphic.Scale = imagesScale;
            FloatRect mediumAsteroidBounds = mediumAsteroid.Graphic.GetGlobalBounds();
            mediumAsteroid.Graphic.Origin = new Vector2f(mediumAsteroidBounds.Width / 2, mediumAsteroidBounds.Height / 2);
            mediumAsteroid.Position = new Vector2f(screenCenter.X - mediumAsteroidWidthOffset, screenCenter.Y - mediumAsteroidHeigthOffset);

            bigAsteroid = new Entity("Assets/Images/BigAsteroidItem.png");
            float bigAsteroidWidthOffset = 420f;
            float bigAsteroidHeigthOffset = 5f;
            bigAsteroid.Graphic.Scale = imagesScale;
            FloatRect bigAsteroidBounds = bigAsteroid.Graphic.GetGlobalBounds();
            bigAsteroid.Graphic.Origin = new Vector2f(bigAsteroidBounds.Width / 2, bigAsteroidBounds.Height / 2);
            bigAsteroid.Position = new Vector2f(screenCenter.X - bigAsteroidWidthOffset, screenCenter.Y - bigAsteroidHeigthOffset);

            smallEnemy = new Entity("Assets/Images/Enemy-small-Item.png");
            float smallEnemyWidthOffset = 423f;
            float smallEnemyHeigthOffset = 43f;
            smallEnemy.Graphic.Scale = imagesScale;
            FloatRect smallEnemyBounds = smallEnemy.Graphic.GetGlobalBounds();
            smallEnemy.Graphic.Origin = new Vector2f(smallEnemyBounds.Width / 2, smallEnemyBounds.Height / 2);
            smallEnemy.Position = new Vector2f(screenCenter.X - smallEnemyWidthOffset, screenCenter.Y + smallEnemyHeigthOffset);

            mediumEnemy = new Entity("Assets/Images/Enemy-medium-Item.png");
            float mediumEnemyWidthOffset = 435f;
            float mediumEnemyHeigthOffset = 93f;
            mediumEnemy.Graphic.Scale = imagesScale;
            FloatRect mediumEnemyBounds = mediumEnemy.Graphic.GetGlobalBounds();
            mediumEnemy.Graphic.Origin = new Vector2f(mediumEnemyBounds.Width / 2, mediumEnemyBounds.Height / 2);
            mediumEnemy.Position = new Vector2f(screenCenter.X - mediumEnemyWidthOffset, screenCenter.Y + mediumEnemyHeigthOffset);

            bigEnemy = new Entity("Assets/Images/Enemy-big-Item.png");
            float bigEnemyWidthOffset = 447f;
            float bigEnemyHeigthOffset = 146f;
            bigEnemy.Graphic.Scale = imagesScale;
            FloatRect bigEnemyBounds = bigEnemy.Graphic.GetGlobalBounds();
            bigEnemy.Graphic.Origin = new Vector2f(bigEnemyBounds.Width / 2, bigEnemyBounds.Height / 2);
            bigEnemy.Position = new Vector2f(screenCenter.X - bigEnemyWidthOffset, screenCenter.Y + bigEnemyHeigthOffset);

            uint itemsTextSize = 17;
            Color itemsTextColor = Color.Black;
            Color itemsTextOutlineColor = Color.White;
            float itemsTextOutlineThickness = 2f;

            fuelText = new Text("FUEL: needed to fuel up spaceship. Don't miss it when it spawns!", textsFont, itemsTextSize);

            FloatRect fuelTextBounds = fuelText.GetGlobalBounds();
            float fuelTextWidthOffset = 0f;
            float fuelTextHeigthOffset = 255f;

            fuelText.FillColor = itemsTextColor;
            fuelText.OutlineColor = itemsTextOutlineColor;
            fuelText.OutlineThickness = itemsTextOutlineThickness;

            fuelText.Position = new Vector2f(screenCenter.X - fuelTextBounds.Width / 2f + fuelTextWidthOffset, screenCenter.Y - fuelTextBounds.Height / 2f - fuelTextHeigthOffset);

            plasmaText = new Text("PLASMA: without this you can't shoot. When you see it be sure to grab it.", textsFont, itemsTextSize);

            FloatRect plasmaTextBounds = plasmaText.GetGlobalBounds();
            float plasmaTextWidthOffset = 0f;
            float plasmaTextHeigthOffset = 215f;

            plasmaText.FillColor = itemsTextColor;
            plasmaText.OutlineColor = itemsTextOutlineColor;
            plasmaText.OutlineThickness = itemsTextOutlineThickness;

            plasmaText.Position = new Vector2f(screenCenter.X - plasmaTextBounds.Width / 2f + plasmaTextWidthOffset, screenCenter.Y - plasmaTextBounds.Height / 2f - plasmaTextHeigthOffset);

            shootingStarText = new Text("SHOOTING STAR: you should shoot at it if you want lot of points.", textsFont, itemsTextSize);

            FloatRect shootingStarTextBounds = shootingStarText.GetGlobalBounds();
            float shootingStarTextWidthOffset = 0f;
            float shootingStarTextHeigthOffset = 175f;

            shootingStarText.FillColor = itemsTextColor;
            shootingStarText.OutlineColor = itemsTextOutlineColor;
            shootingStarText.OutlineThickness = itemsTextOutlineThickness;
            
            shootingStarText.Position = new Vector2f(screenCenter.X - shootingStarTextBounds.Width / 2f + shootingStarTextWidthOffset, screenCenter.Y - shootingStarTextBounds.Height / 2f - shootingStarTextHeigthOffset);
                       
            lifeText = new Text("LIFE: if you want to live long you will need this.", textsFont, itemsTextSize);

            FloatRect lifeTextBounds = lifeText.GetGlobalBounds();
            float lifeTextWidthOffset = 0f;
            float lifeTextHeigthOffset = 135f;

            lifeText.FillColor = itemsTextColor;
            lifeText.OutlineColor = itemsTextOutlineColor;
            lifeText.OutlineThickness = itemsTextOutlineThickness;
            
            lifeText.Position = new Vector2f(screenCenter.X - lifeTextBounds.Width / 2f + lifeTextWidthOffset, screenCenter.Y - lifeTextBounds.Height / 2f - lifeTextHeigthOffset);

            smallAsteroidText = new Text("SMALL ASTEROID: beware not to get hit. Easy to destroy but fast.", textsFont, itemsTextSize);

            FloatRect smallAsteroidTextBounds = smallAsteroidText.GetGlobalBounds();
            float smallAsteroidTextWidthOffset = 0f;
            float smallAsteroidTextHeigthOffset = 95f;

            smallAsteroidText.FillColor = itemsTextColor;
            smallAsteroidText.OutlineColor = itemsTextOutlineColor;
            smallAsteroidText.OutlineThickness = itemsTextOutlineThickness;
            
            smallAsteroidText.Position = new Vector2f(screenCenter.X - smallAsteroidTextBounds.Width / 2f + smallAsteroidTextWidthOffset, screenCenter.Y - smallAsteroidTextBounds.Height / 2f - smallAsteroidTextHeigthOffset);

            mediumAsteroidText = new Text("MEDIUM ASTEROID: it hurts if you touch it. Slower but harder to destroy.", textsFont, itemsTextSize);

            FloatRect mediumAsteroidTextBounds = mediumAsteroidText.GetGlobalBounds();
            float mediumAsteroidTextWidthOffset = 0f;
            float mediumAsteroidTextHeigthOffset = 55f;

            mediumAsteroidText.FillColor = itemsTextColor;
            mediumAsteroidText.OutlineColor = itemsTextOutlineColor;
            mediumAsteroidText.OutlineThickness = itemsTextOutlineThickness;
            
            mediumAsteroidText.Position = new Vector2f(screenCenter.X - mediumAsteroidTextBounds.Width / 2f + mediumAsteroidTextWidthOffset, screenCenter.Y - mediumAsteroidTextBounds.Height / 2f - mediumAsteroidTextHeigthOffset);

            bigAsteroidText = new Text("BIG ASTEROID: will crush you. Very slow but really hard.", textsFont, itemsTextSize);

            FloatRect bigAsteroidTextBounds = bigAsteroidText.GetGlobalBounds();
            float bigAsteroidTextWidthOffset = 5f;
            float bigAsteroidTextHeigthOffset = 5f;

            bigAsteroidText.FillColor = itemsTextColor;
            bigAsteroidText.OutlineColor = itemsTextOutlineColor;
            bigAsteroidText.OutlineThickness = itemsTextOutlineThickness;
           
            bigAsteroidText.Position = new Vector2f(screenCenter.X - bigAsteroidTextBounds.Width / 2f + bigAsteroidTextWidthOffset, screenCenter.Y - bigAsteroidTextBounds.Height / 2f - bigAsteroidTextHeigthOffset);

            smallEnemyText = new Text("SMALL ENEMY: weak against bullets, but really fast.", textsFont, itemsTextSize);

            FloatRect smallEnemyTextBounds = smallEnemyText.GetGlobalBounds();
            float smallEnemyTextWidthOffset = 0f;
            float smallEnemyTextHeigthOffset = 43f;

            smallEnemyText.FillColor = itemsTextColor;
            smallEnemyText.OutlineColor= itemsTextOutlineColor;
            smallEnemyText.OutlineThickness= itemsTextOutlineThickness;

            smallEnemyText.Position = new Vector2f(screenCenter.X - smallEnemyTextBounds.Width / 2f + smallEnemyTextWidthOffset, screenCenter.Y - smallEnemyTextBounds.Height / 2f + smallEnemyTextHeigthOffset);

            mediumEnemyText = new Text("MEDIUM ENEMY: more resistant against bullets, medium speed.", textsFont, itemsTextSize);

            FloatRect mediumEnemyTextBounds = mediumEnemyText.GetGlobalBounds();
            float mediumEnemyTextWidthOffset = 0f;
            float mediumEnemyTextHeigthOffset = 93f;

            mediumEnemyText.FillColor = itemsTextColor;
            mediumEnemyText.OutlineColor = itemsTextOutlineColor;
            mediumEnemyText.OutlineThickness = itemsTextOutlineThickness;

            mediumEnemyText.Position = new Vector2f(screenCenter.X - mediumEnemyTextBounds.Width / 2f + mediumEnemyTextWidthOffset, screenCenter.Y - mediumEnemyTextBounds.Height / 2f + mediumEnemyTextHeigthOffset);

            bigEnemyText = new Text("BIG ENEMY:  strong against bullets, but quite slow.", textsFont, itemsTextSize);

            FloatRect bigEnemyTextBounds = mediumEnemyText.GetGlobalBounds();
            float bigEnemyTextWidthOffset = 50f;
            float bigEnemyTextHeigthOffset = 170f;

            bigEnemyText.FillColor = itemsTextColor;
            bigEnemyText.OutlineColor = itemsTextOutlineColor;
            bigEnemyText.OutlineThickness = itemsTextOutlineThickness;

            bigEnemyText.Position = new Vector2f(screenCenter.X - bigEnemyTextBounds.Width / 2f + bigEnemyTextWidthOffset, screenCenter.Y - bigEnemyTextBounds.Height / 2f + bigEnemyTextHeigthOffset);


            destroyAsteroidAndSurviveText = new Text("Destroy asteroids and enemies. Earn points. Survive as long as you can!", textsFont, characterSize: 20);

            FloatRect destroyAsteroidsTextBounds = destroyAsteroidAndSurviveText.GetGlobalBounds();
            float destroyAsteroidsTextWidthOffset = 0f;
            float destroyAsteroidsTextHeigthOffset = 235f;

            destroyAsteroidAndSurviveText.FillColor = Color.Red;
            destroyAsteroidAndSurviveText.OutlineColor = itemsTextOutlineColor;
            destroyAsteroidAndSurviveText.OutlineThickness = 5;
            
            destroyAsteroidAndSurviveText.Position = new Vector2f(screenCenter.X - destroyAsteroidsTextBounds.Width / 2f + destroyAsteroidsTextWidthOffset, screenCenter.Y - destroyAsteroidsTextBounds.Height / 2f + destroyAsteroidsTextHeigthOffset);

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
            backButton.SetPosition(new Vector2f(screenCenter.X, (screenCenter.Y + 310f)));
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

                titleText.DisplayedString = "Instrucciones";
                fuelText.DisplayedString = "NAFTA: necesaria para el funcionamiento de la nave. No te la pierdas cuando aparezca!";
                plasmaText.DisplayedString = "PLASMA: sin esto no puedes disparar. Cuando lo veas asegurate de agarrarlo.";
                shootingStarText.DisplayedString = "ESTRELLA FUGAZ: deberías dispararle si quieres muchos puntos.";
                lifeText.DisplayedString = "VIDA: si quieres vivir mucho, las necesitarás.";
                smallAsteroidText.DisplayedString = "ASTEROIDE PEQUEÑO: cuidado con ser golpeado. Facil de destruir, pero veloz.";
                mediumAsteroidText.DisplayedString = "ASTEROIDE MEDIANO: duele cuando lo tocas. Más lento pero más dificil de destruir.";
                bigAsteroidText.DisplayedString = "ASTEROIDE GRANDE: te aplastará. Muy lento pero muy duro.";
                smallEnemyText.DisplayedString = "ENEMIGO PEQUEÑO: debil frente a las balas, pero muy veloz.";
                mediumEnemyText.DisplayedString = "ENEMIGO MEDIANO: más resistente a las balas, velocidad media.";
                bigEnemyText.DisplayedString = "ENEMIGO GRANDE:  resistente frente a las balas, pero bastante lento.";
                destroyAsteroidAndSurviveText.DisplayedString = "Destruye asteroides y enemigos. Gana puntos. Sobrevive tanto como puedas!";

                Vector2f screenCenter = (Vector2f)renderWindow.Size / 2f;

                FloatRect fuelTextBounds = fuelText.GetGlobalBounds();
                float fuelTextWidthOffset = 0f;
                float fuelTextHeigthOffset = 255f;

                fuelText.Position = new Vector2f(screenCenter.X - fuelTextBounds.Width / 2f + fuelTextWidthOffset, screenCenter.Y - fuelTextBounds.Height / 2f - fuelTextHeigthOffset);

                FloatRect bigEnemyTextBounds = mediumEnemyText.GetGlobalBounds();
                float bigEnemyTextWidthOffset = -10f;
                float bigEnemyTextHeigthOffset = 170f;

                bigEnemyText.Position = new Vector2f(screenCenter.X - bigEnemyTextBounds.Width / 2f + bigEnemyTextWidthOffset, screenCenter.Y - bigEnemyTextBounds.Height / 2f + bigEnemyTextHeigthOffset);
            }
            else if(language == "en")
            {
                backButton.SetText("Back", 40);

                titleText.DisplayedString = "Instructions";
                fuelText.DisplayedString = "FUEL: needed to fuel up spaceship. Don't miss it when it spawns!";
                plasmaText.DisplayedString = "PLASMA: without this you can't shoot. When you see it be sure to grab it.";
                shootingStarText.DisplayedString = "SHOOTING STAR: you should shoot at it if you want lot of points.";
                lifeText.DisplayedString = "LIFE: if you want to live long you will need this.";
                smallAsteroidText.DisplayedString = "SMALL ASTEROID: beware not to get hit. Easy to destroy but fast.";
                mediumAsteroidText.DisplayedString = "MEDIUM ASTEROID: it hurts if you touch it. Slower but harder to destroy.";
                bigAsteroidText.DisplayedString = "BIG ASTEROID: will crush you. Very slow but really hard.";
                smallEnemyText.DisplayedString = "SMALL ENEMY: weak against bullets, but really fast.";
                mediumEnemyText.DisplayedString = "MEDIUM ENEMY: more resistant against bullets, medium speed.";
                bigEnemyText.DisplayedString = "BIG ENEMY:  strong against bullets, but quite slow.";
                destroyAsteroidAndSurviveText.DisplayedString = "Destroy asteroids and enemies. Earn points. Survive as long as you can!";

                Vector2f screenCenter = (Vector2f)renderWindow.Size / 2f;

                FloatRect fuelTextBounds = fuelText.GetGlobalBounds();
                float fuelTextWidthOffset = 0f;
                float fuelTextHeigthOffset = 255f;

                fuelText.Position = new Vector2f(screenCenter.X - fuelTextBounds.Width / 2f + fuelTextWidthOffset, screenCenter.Y - fuelTextBounds.Height / 2f - fuelTextHeigthOffset);

                FloatRect bigEnemyTextBounds = mediumEnemyText.GetGlobalBounds();
                float bigEnemyTextWidthOffset = 50f;
                float bigEnemyTextHeigthOffset = 170f;

                bigEnemyText.Position = new Vector2f(screenCenter.X - bigEnemyTextBounds.Width / 2f + bigEnemyTextWidthOffset, screenCenter.Y - bigEnemyTextBounds.Height / 2f + bigEnemyTextHeigthOffset);
            }
        }

        protected override void Draw()
        {
            renderWindow.Draw(background.Graphic);

            renderWindow.Draw(titleText);

            renderWindow.Draw(fuel.Graphic);

            renderWindow.Draw(plasma.Graphic);

            renderWindow.Draw(shootingStar.Graphic);

            renderWindow.Draw(life.Graphic);

            renderWindow.Draw(smallAsteroid.Graphic);

            renderWindow.Draw(mediumAsteroid.Graphic);

            renderWindow.Draw(bigAsteroid.Graphic);

            renderWindow.Draw(smallEnemy.Graphic);

            renderWindow.Draw(mediumEnemy.Graphic);

            renderWindow.Draw(bigEnemy.Graphic);

            renderWindow.Draw(fuelText);

            renderWindow.Draw(plasmaText);

            renderWindow.Draw(shootingStarText);

            renderWindow.Draw(lifeText);

            renderWindow.Draw(smallAsteroidText);

            renderWindow.Draw(mediumAsteroidText);

            renderWindow.Draw(bigAsteroidText);
                       
            renderWindow.Draw(smallEnemyText);

            renderWindow.Draw(mediumEnemyText);

            renderWindow.Draw(bigEnemyText);

            renderWindow.Draw(destroyAsteroidAndSurviveText);

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
