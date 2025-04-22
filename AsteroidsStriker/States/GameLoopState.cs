using System;
using SFML.System;
using SFML.Graphics;
using SFML.Audio;
using SFML.Window;
using System.IO;
using System.Collections.Generic;
using System.Text;

namespace SpaceShipGame3
{
    public class GameLoopState : LoopState
    {
        private HighScoreState highScoreState;

        private FileStream highScores;
        private byte[] highScoresData = new byte[115];
        private bool scoreScreen;

        private string language = "en";

        private Entity background;

        private Player player;

        private Pickable gasCan;
        private Pickable plasma;
        private Pickable life;

        private bool meteorShower;

        private Asteroid[] smallAsteroids;
        private Asteroid[] mediumAsteroids;
        private Asteroid[] bigAsteroids;

        private float asteroidsTimer;
        private float asteroidWaveDuration;
        private float asteroidNextWave;

        private int actualSmallAsteroids;
        private int actualMediumAsteroids;
        private int actualBigAsteroids;

        private int totalSmallAsteroids = 14;
        private int totalMediumAsteroids = 9;
        private int totalBigAsteroids = 7;

        private SmallSpaceShip smallSpaceShipOne;
        private SmallSpaceShip smallSpaceShipTwo;
        private SmallSpaceShip smallSpaceShipThree;
        private SmallSpaceShip smallSpaceShipFour;
        private SmallSpaceShip smallSpaceShipFive;
        private SmallSpaceShip smallSpaceShipSix;
        private SmallSpaceShip smallSpaceShipSeven;
        private SmallSpaceShip smallSpaceShipEight;
        private SmallSpaceShip smallSpaceShipNine;
        private SmallSpaceShip smallSpaceShipTen;
        private SmallSpaceShip smallSpaceShipEleven;
        private SmallSpaceShip smallSpaceShipTwelve;
        private SmallSpaceShip smallSpaceShipThirteen;
        private SmallSpaceShip smallSpaceShipFourteen;
        private SmallSpaceShip smallSpaceShipFifteen;
        private SmallSpaceShip smallSpaceShipSixteen;
        private SmallSpaceShip smallSpaceShipSeventeen;
        private SmallSpaceShip smallSpaceShipEighteen;
        private SmallSpaceShip smallSpaceShipNineteen;
        private SmallSpaceShip smallSpaceShipTwenty;
        private SmallSpaceShip smallSpaceShipTwentyone;
        private SmallSpaceShip smallSpaceShipTwentytwo;
        private SmallSpaceShip smallSpaceShipTwentythree;

        private MediumSpaceShip mediumSpaceShipOne;
        private MediumSpaceShip mediumSpaceShipTwo;
        private MediumSpaceShip mediumSpaceShipThree;
        private MediumSpaceShip mediumSpaceShipFour;
        private MediumSpaceShip mediumSpaceShipFive;
        private MediumSpaceShip mediumSpaceShipSix;
        private MediumSpaceShip mediumSpaceShipSeven;
        private MediumSpaceShip mediumSpaceShipEight;
        private MediumSpaceShip mediumSpaceShipNine;
        private MediumSpaceShip mediumSpaceShipTen;

        private BigSpaceShip bigSpaceShipOne;
        private BigSpaceShip bigSpaceShipTwo;
        private BigSpaceShip bigSpaceShipThree;
        private BigSpaceShip bigSpaceShipFour;
        private BigSpaceShip bigSpaceShipFive;
        private BigSpaceShip bigSpaceShipSix;
        private BigSpaceShip bigSpaceShipSeven;
        private BigSpaceShip bigSpaceShipEight;
        private BigSpaceShip bigSpaceShipNine;
        private BigSpaceShip bigSpaceShipTen;

        private List<Bullet> playerBullets = new List<Bullet>();

        private int wave;
        private string waveText;

        private float travelTimer;
        private float travelTime;

        private ShootingStar shootingStar;

        private HUD hud;
        private Entity hudbar;

        private Music backgroundMusic;

        private bool pauseGameplay;

        private Entity pauseMenu;
        private Entity looseMenu;

        private Font textFont;
        private Text gamePausedText;
        private Text looseText;
        private Text finalScoreText;

        private bool nameInputed;
        private Text scoreInputNameText;
        private Text scoreInputNameInstructionsText;

        private char[] scoreName;
        private int charactersInputed;
        private Text scoreNameText;

        private bool newHighScore;
        private Text newHighScoreText;

        private Button continueButton;
        private Button mainMenuButton;        
        private Button restartButton;
                
        public event Action OnMainMenuPressed;
        public event Action OnRestartPressed;

        public GameLoopState(RenderWindow renderWindow, FileStream highscores, HighScoreState highScoreState) : base(renderWindow) 
        {
            this.highScoreState = highScoreState;
            this.highScores = highscores;
            scoreScreen = false;

            float playerLives = 3;
            string playerImageFilePath = "Assets/Images/Spaceship.png";
            Vector2i playerFrameSize = new Vector2i(45, 44);
            float playerReducedSpeed = 130f;
            float playerNormalSpeed = 200f;
            float playerRotationSpeed = 180f;
            player = new Player(renderWindow, playerBullets, playerLives, playerImageFilePath, playerFrameSize, playerRotationSpeed, playerNormalSpeed, playerReducedSpeed);

            float bulletsSpeed = 400f;
            string bulletsImageFilePath = "Assets/Images/Bullet.png";

            for (int i = 0; i < 100; i++)
                playerBullets.Add(new Bullet(renderWindow, bulletsSpeed, bulletsImageFilePath));

            for (int i = 0; i < playerBullets.Count; i++)
            {
                CollisionsHandler.AddEntity(playerBullets[i]);
                playerBullets[i].IsBullet = true;
            }                       
          
            smallAsteroids = new SmallAsteroid[totalSmallAsteroids];
            mediumAsteroids = new MediumAsteroid[totalMediumAsteroids];
            bigAsteroids = new BigAsteroid[totalBigAsteroids];

            int smallAsteroidScorePoints = 10;
            int smallAsteroidLives = 1;
            float smallAsteroidSpeed = 200f;
            Vector2i smallAsteroidSize = new Vector2i(30, 30);
            string smallAsteroidImageFilePath = "Assets/Images/SmallAsteroid.png";
            SoundEffect smallAsteroidDestroyed = new SoundEffect("Assets/Sounds/SmallAsteroidExplosion.wav");

            int mediumAsteroidScorePoints = 20;
            int mediumAsteroidLives = 2;
            float mediumAsteroidSpeed = 170f;
            Vector2i mediumAsteroidSize = new Vector2i(45, 45);
            string mediumAsteroidImageFilePath = "Assets/Images/MediumAsteroid.png";
            SoundEffect mediumAsteroidDestroyed = new SoundEffect("Assets/Sounds/MediumAsteroidExplosion.wav");

            int bigAsteroidScorePoints = 40;
            int bigAsteroidLives = 4;
            float bigAsteroidSpeed = 140f;
            Vector2i bigAsteroidSize = new Vector2i(60, 60);
            string bigAsteroidImageFilePath = "Assets/Images/BigAsteroid.png";
            SoundEffect bigAsteroidDestroyed = new SoundEffect("Assets/Sounds/BigAsteroidExplosion.wav");
                      
            for (int i = 0; i < totalSmallAsteroids; i++)
            {
                smallAsteroids[i] = new SmallAsteroid(player, smallAsteroidScorePoints, renderWindow, smallAsteroidLives, smallAsteroidSpeed, smallAsteroidSize, smallAsteroidImageFilePath, smallAsteroidDestroyed);                          
            }

            for (int i = 0; i < totalMediumAsteroids; i++)
            {
                mediumAsteroids[i] = new MediumAsteroid(player, mediumAsteroidScorePoints, renderWindow, mediumAsteroidLives, mediumAsteroidSpeed, mediumAsteroidSize, mediumAsteroidImageFilePath, mediumAsteroidDestroyed);                           
            }

            for (int i = 0; i < totalBigAsteroids; i++)
            {
                bigAsteroids[i] = new BigAsteroid(player, bigAsteroidScorePoints, renderWindow, bigAsteroidLives, bigAsteroidSpeed, bigAsteroidSize, bigAsteroidImageFilePath, bigAsteroidDestroyed);                              
            }
                       
            int smallSpaceShipScorePoints = 5;
            float smallSpaceShipLives = 1;
            float smallSpaceShipSpeed = 500;
            float smallSpaceShipOneSpawnTime = 2;
            float smallSpaceShipTwoSpawnTime = 3.3f;
            float smallSpaceShipThreeSpawnTime = 3.7f;
            float smallSpaceShipFourSpawnTime = 4f;
            float smallSpaceShipFiveSpawnTime = 4.3f;
            float smallSpaceShipSixSpawnTime = 4.5f;
            float smallSpaceShipSevenSpawnTime = 4.9f;
            float smallSpaceShipEightSpawnTime = 5.1f;
            float smallSpaceShipNineSpawnTime = 5.3f;
            float smallSpaceShipTenSpawnTime = 5.6f;
            float smallSpaceShipElevenSpawnTime = 5.9f;
            float smallSpaceShipTwelveSpawnTime = 6.2f;
            float smallSpaceShipThirteenSpawnTime = 6.5f;
            float smallSpaceShipFourteenSpawnTime = 6.8f;
            float smallSpaceShipFifteenSpawnTime = 7.1f;
            float smallSpaceShipSixteenSpawnTime = 7.4f;
            float smallSpaceShipSeventeenSpawnTime = 7.7f;
            float smallSpaceShipEighteenSpawnTime = 8f;
            float smallSpaceShipNineteenSpawnTime = 8.3f;
            float smallSpaceShipTwentySpawnTime = 8.6f;
            float smallSpaceShipTwentyoneSpawnTime = 8.9f;
            float smallSpaceShipTwentytwoSpawnTime = 9.2f;
            float smallSpaceShipTwentythreeSpawnTime = 9.5f;

            Vector2i smallSpaceShipFrameSize = new Vector2i(44, 45);
            string smallSpaceShipImageFilePath = "Assets/Images/Enemy-small.png";

            int mediumSpaceShipScorePoints = 10;
            float mediumSpaceShipLives = 2;
            float mediumSpaceShipSpeed = 350;
            float mediumSpaceShipOneSpawnTime = 4;
            float mediumSpaceShipTwoSpawnTime = 4.7f;
            float mediumSpaceShipThreeSpawnTime = 5.4f;
            float mediumSpaceShipFourSpawnTime = 6.1f;
            float mediumSpaceShipFiveSpawnTime = 6.8f;
            float mediumSpaceShipSixSpawnTime = 7.5f;
            float mediumSpaceShipSevenSpawnTime = 8.2f;
            float mediumSpaceShipEightSpawnTime = 8.9f;
            float mediumSpaceShipNineSpawnTime = 9.6f;
            float mediumSpaceShipTenSpawnTime = 10.3f;

            Vector2i mediumSpaceShipFrameSize = new Vector2i(100, 40);
            string mediumSpaceShipImageFilePath = "Assets/Images/Enemy-medium.png";

            int bigSpaceShipScorePoints = 15;
            float bigSpaceShipLives = 3;
            float bigSpaceShipSpeed = 250;
            float bigSpaceShipOneSpawnTime = 5;
            float bigSpaceShipTwoSpawnTime = 8f;
            float bigSpaceShipThreeSpawnTime = 11f;
            float bigSpaceShipFourSpawnTime = 14f;
            float bigSpaceShipFiveSpawnTime = 17f;
            float bigSpaceShipSixSpawnTime = 20f;
            float bigSpaceShipSevenSpawnTime = 23f;
            float bigSpaceShipEightSpawnTime = 26f;
            float bigSpaceShipNineSpawnTime = 29f;
            float bigSpaceShipTenSpawnTime = 32f;

            Vector2i bigSpaceShipFrameSize = new Vector2i(150, 154);
            string bigSpaceShipImageFilePath = "Assets/Images/Enemy-big.png";

            smallSpaceShipOne = new SmallSpaceShip(player, smallSpaceShipScorePoints, smallSpaceShipLives, smallSpaceShipSpeed, smallSpaceShipFrameSize, smallSpaceShipOneSpawnTime, smallSpaceShipImageFilePath);
            smallSpaceShipTwo = new SmallSpaceShip(player, smallSpaceShipScorePoints, smallSpaceShipLives, smallSpaceShipSpeed, smallSpaceShipFrameSize, smallSpaceShipTwoSpawnTime, smallSpaceShipImageFilePath);
            smallSpaceShipThree = new SmallSpaceShip(player, smallSpaceShipScorePoints, smallSpaceShipLives, smallSpaceShipSpeed, smallSpaceShipFrameSize, smallSpaceShipThreeSpawnTime, smallSpaceShipImageFilePath);
            smallSpaceShipFour = new SmallSpaceShip(player, smallSpaceShipScorePoints, smallSpaceShipLives, smallSpaceShipSpeed, smallSpaceShipFrameSize, smallSpaceShipFourSpawnTime, smallSpaceShipImageFilePath);
            smallSpaceShipFive = new SmallSpaceShip(player, smallSpaceShipScorePoints, smallSpaceShipLives, smallSpaceShipSpeed, smallSpaceShipFrameSize, smallSpaceShipFiveSpawnTime, smallSpaceShipImageFilePath);
            smallSpaceShipSix = new SmallSpaceShip(player, smallSpaceShipScorePoints, smallSpaceShipLives, smallSpaceShipSpeed, smallSpaceShipFrameSize, smallSpaceShipSixSpawnTime, smallSpaceShipImageFilePath);
            smallSpaceShipSeven = new SmallSpaceShip(player, smallSpaceShipScorePoints, smallSpaceShipLives, smallSpaceShipSpeed, smallSpaceShipFrameSize, smallSpaceShipSevenSpawnTime, smallSpaceShipImageFilePath);
            smallSpaceShipEight = new SmallSpaceShip(player, smallSpaceShipScorePoints, smallSpaceShipLives, smallSpaceShipSpeed, smallSpaceShipFrameSize, smallSpaceShipEightSpawnTime, smallSpaceShipImageFilePath);
            smallSpaceShipNine = new SmallSpaceShip(player, smallSpaceShipScorePoints, smallSpaceShipLives, smallSpaceShipSpeed, smallSpaceShipFrameSize, smallSpaceShipNineSpawnTime, smallSpaceShipImageFilePath);
            smallSpaceShipTen = new SmallSpaceShip(player, smallSpaceShipScorePoints, smallSpaceShipLives, smallSpaceShipSpeed, smallSpaceShipFrameSize, smallSpaceShipTenSpawnTime, smallSpaceShipImageFilePath);
            smallSpaceShipEleven = new SmallSpaceShip(player, smallSpaceShipScorePoints, smallSpaceShipLives, smallSpaceShipSpeed, smallSpaceShipFrameSize, smallSpaceShipElevenSpawnTime, smallSpaceShipImageFilePath);
            smallSpaceShipTwelve = new SmallSpaceShip(player, smallSpaceShipScorePoints, smallSpaceShipLives, smallSpaceShipSpeed, smallSpaceShipFrameSize, smallSpaceShipTwelveSpawnTime, smallSpaceShipImageFilePath);
            smallSpaceShipThirteen = new SmallSpaceShip(player, smallSpaceShipScorePoints, smallSpaceShipLives, smallSpaceShipSpeed, smallSpaceShipFrameSize, smallSpaceShipThirteenSpawnTime, smallSpaceShipImageFilePath);
            smallSpaceShipFourteen = new SmallSpaceShip(player, smallSpaceShipScorePoints, smallSpaceShipLives, smallSpaceShipSpeed, smallSpaceShipFrameSize, smallSpaceShipFourteenSpawnTime, smallSpaceShipImageFilePath);
            smallSpaceShipFifteen = new SmallSpaceShip(player, smallSpaceShipScorePoints, smallSpaceShipLives, smallSpaceShipSpeed, smallSpaceShipFrameSize, smallSpaceShipFifteenSpawnTime, smallSpaceShipImageFilePath);
            smallSpaceShipSixteen = new SmallSpaceShip(player, smallSpaceShipScorePoints, smallSpaceShipLives, smallSpaceShipSpeed, smallSpaceShipFrameSize, smallSpaceShipSixteenSpawnTime, smallSpaceShipImageFilePath);
            smallSpaceShipSeventeen = new SmallSpaceShip(player, smallSpaceShipScorePoints, smallSpaceShipLives, smallSpaceShipSpeed, smallSpaceShipFrameSize, smallSpaceShipSeventeenSpawnTime, smallSpaceShipImageFilePath);
            smallSpaceShipEighteen = new SmallSpaceShip(player, smallSpaceShipScorePoints, smallSpaceShipLives, smallSpaceShipSpeed, smallSpaceShipFrameSize, smallSpaceShipEighteenSpawnTime, smallSpaceShipImageFilePath);
            smallSpaceShipNineteen = new SmallSpaceShip(player, smallSpaceShipScorePoints, smallSpaceShipLives, smallSpaceShipSpeed, smallSpaceShipFrameSize, smallSpaceShipNineteenSpawnTime, smallSpaceShipImageFilePath);
            smallSpaceShipTwenty = new SmallSpaceShip(player, smallSpaceShipScorePoints, smallSpaceShipLives, smallSpaceShipSpeed, smallSpaceShipFrameSize, smallSpaceShipTwentySpawnTime, smallSpaceShipImageFilePath);
            smallSpaceShipTwentyone = new SmallSpaceShip(player, smallSpaceShipScorePoints, smallSpaceShipLives, smallSpaceShipSpeed, smallSpaceShipFrameSize, smallSpaceShipTwentyoneSpawnTime, smallSpaceShipImageFilePath);
            smallSpaceShipTwentytwo = new SmallSpaceShip(player, smallSpaceShipScorePoints, smallSpaceShipLives, smallSpaceShipSpeed, smallSpaceShipFrameSize, smallSpaceShipTwentytwoSpawnTime, smallSpaceShipImageFilePath);
            smallSpaceShipTwentythree = new SmallSpaceShip(player, smallSpaceShipScorePoints, smallSpaceShipLives, smallSpaceShipSpeed, smallSpaceShipFrameSize, smallSpaceShipTwentythreeSpawnTime, smallSpaceShipImageFilePath);

            mediumSpaceShipOne = new MediumSpaceShip(player, mediumSpaceShipScorePoints, mediumSpaceShipLives, mediumSpaceShipSpeed, mediumSpaceShipFrameSize, mediumSpaceShipOneSpawnTime, mediumSpaceShipImageFilePath);
            mediumSpaceShipTwo = new MediumSpaceShip(player, mediumSpaceShipScorePoints, mediumSpaceShipLives, mediumSpaceShipSpeed, mediumSpaceShipFrameSize, mediumSpaceShipTwoSpawnTime, mediumSpaceShipImageFilePath);
            mediumSpaceShipThree = new MediumSpaceShip(player, mediumSpaceShipScorePoints, mediumSpaceShipLives, mediumSpaceShipSpeed, mediumSpaceShipFrameSize, mediumSpaceShipThreeSpawnTime, mediumSpaceShipImageFilePath);
            mediumSpaceShipFour = new MediumSpaceShip(player, mediumSpaceShipScorePoints, mediumSpaceShipLives, mediumSpaceShipSpeed, mediumSpaceShipFrameSize, mediumSpaceShipFourSpawnTime, mediumSpaceShipImageFilePath);
            mediumSpaceShipFive = new MediumSpaceShip(player, mediumSpaceShipScorePoints, mediumSpaceShipLives, mediumSpaceShipSpeed, mediumSpaceShipFrameSize, mediumSpaceShipFiveSpawnTime, mediumSpaceShipImageFilePath);
            mediumSpaceShipSix = new MediumSpaceShip(player, mediumSpaceShipScorePoints, mediumSpaceShipLives, mediumSpaceShipSpeed, mediumSpaceShipFrameSize, mediumSpaceShipSixSpawnTime, mediumSpaceShipImageFilePath);
            mediumSpaceShipSeven = new MediumSpaceShip(player, mediumSpaceShipScorePoints, mediumSpaceShipLives, mediumSpaceShipSpeed, mediumSpaceShipFrameSize, mediumSpaceShipSevenSpawnTime, mediumSpaceShipImageFilePath);
            mediumSpaceShipEight = new MediumSpaceShip(player, mediumSpaceShipScorePoints, mediumSpaceShipLives, mediumSpaceShipSpeed, mediumSpaceShipFrameSize, mediumSpaceShipEightSpawnTime, mediumSpaceShipImageFilePath);
            mediumSpaceShipNine = new MediumSpaceShip(player, mediumSpaceShipScorePoints, mediumSpaceShipLives, mediumSpaceShipSpeed, mediumSpaceShipFrameSize, mediumSpaceShipNineSpawnTime, mediumSpaceShipImageFilePath);
            mediumSpaceShipTen = new MediumSpaceShip(player, mediumSpaceShipScorePoints, mediumSpaceShipLives, mediumSpaceShipSpeed, mediumSpaceShipFrameSize, mediumSpaceShipTenSpawnTime, mediumSpaceShipImageFilePath);

            bigSpaceShipOne = new BigSpaceShip(player, bigSpaceShipScorePoints, bigSpaceShipLives, bigSpaceShipSpeed, bigSpaceShipFrameSize, bigSpaceShipOneSpawnTime, bigSpaceShipImageFilePath);
            bigSpaceShipTwo = new BigSpaceShip(player, bigSpaceShipScorePoints, bigSpaceShipLives, bigSpaceShipSpeed, bigSpaceShipFrameSize, bigSpaceShipTwoSpawnTime, bigSpaceShipImageFilePath);
            bigSpaceShipThree = new BigSpaceShip(player, bigSpaceShipScorePoints, bigSpaceShipLives, bigSpaceShipSpeed, bigSpaceShipFrameSize, bigSpaceShipThreeSpawnTime, bigSpaceShipImageFilePath);
            bigSpaceShipFour = new BigSpaceShip(player, bigSpaceShipScorePoints, bigSpaceShipLives, bigSpaceShipSpeed, bigSpaceShipFrameSize, bigSpaceShipFourSpawnTime, bigSpaceShipImageFilePath);
            bigSpaceShipFive = new BigSpaceShip(player, bigSpaceShipScorePoints, bigSpaceShipLives, bigSpaceShipSpeed, bigSpaceShipFrameSize, bigSpaceShipFiveSpawnTime, bigSpaceShipImageFilePath);
            bigSpaceShipSix = new BigSpaceShip(player, bigSpaceShipScorePoints, bigSpaceShipLives, bigSpaceShipSpeed, bigSpaceShipFrameSize, bigSpaceShipSixSpawnTime, bigSpaceShipImageFilePath);
            bigSpaceShipSeven = new BigSpaceShip(player, bigSpaceShipScorePoints, bigSpaceShipLives, bigSpaceShipSpeed, bigSpaceShipFrameSize, bigSpaceShipSevenSpawnTime, bigSpaceShipImageFilePath);
            bigSpaceShipEight = new BigSpaceShip(player, bigSpaceShipScorePoints, bigSpaceShipLives, bigSpaceShipSpeed, bigSpaceShipFrameSize, bigSpaceShipEightSpawnTime, bigSpaceShipImageFilePath);
            bigSpaceShipNine = new BigSpaceShip(player, bigSpaceShipScorePoints, bigSpaceShipLives, bigSpaceShipSpeed, bigSpaceShipFrameSize, bigSpaceShipNineSpawnTime, bigSpaceShipImageFilePath);
            bigSpaceShipTen = new BigSpaceShip(player, bigSpaceShipScorePoints, bigSpaceShipLives, bigSpaceShipSpeed, bigSpaceShipFrameSize, bigSpaceShipTenSpawnTime, bigSpaceShipImageFilePath);

            actualSmallAsteroids = 4;
            
            actualMediumAsteroids = 1;
            
            actualBigAsteroids = 1;
            

            meteorShower = false;
            
        }
           
        public string Language { get => language; set => language = value; }

        public void OncloseWindow(object sender, EventArgs e)
        {
            renderWindow.Close();
        }

        protected override void Start()
        {
            base.Start();

            player.Graphic.Scale = new Vector2f(0.8f, 0.8f);

            smallSpaceShipOne.Graphic.Scale = new Vector2f(0.8f, 0.8f);
            smallSpaceShipTwo.Graphic.Scale = new Vector2f(0.8f, 0.8f);
            smallSpaceShipThree.Graphic.Scale = new Vector2f(0.8f, 0.8f);
            smallSpaceShipFour.Graphic.Scale = new Vector2f(0.8f, 0.8f);
            smallSpaceShipFive.Graphic.Scale = new Vector2f(0.8f, 0.8f);
            smallSpaceShipSix.Graphic.Scale = new Vector2f(0.8f, 0.8f);
            smallSpaceShipSeven.Graphic.Scale = new Vector2f(0.8f, 0.8f);
            smallSpaceShipEight.Graphic.Scale = new Vector2f(0.8f, 0.8f);
            smallSpaceShipNine.Graphic.Scale = new Vector2f(0.8f, 0.8f);
            smallSpaceShipTen.Graphic.Scale = new Vector2f(0.8f, 0.8f);
            smallSpaceShipEleven.Graphic.Scale = new Vector2f(0.8f, 0.8f);
            smallSpaceShipTwelve.Graphic.Scale = new Vector2f(0.8f, 0.8f);
            smallSpaceShipThirteen.Graphic.Scale = new Vector2f(0.8f, 0.8f);
            smallSpaceShipFourteen.Graphic.Scale = new Vector2f(0.8f, 0.8f);
            smallSpaceShipFifteen.Graphic.Scale = new Vector2f(0.8f, 0.8f);
            smallSpaceShipSixteen.Graphic.Scale = new Vector2f(0.8f, 0.8f);
            smallSpaceShipSeventeen.Graphic.Scale = new Vector2f(0.8f, 0.8f);
            smallSpaceShipEighteen.Graphic.Scale = new Vector2f(0.8f, 0.8f);
            smallSpaceShipNineteen.Graphic.Scale = new Vector2f(0.8f, 0.8f);
            smallSpaceShipTwenty.Graphic.Scale = new Vector2f(0.8f, 0.8f);
            smallSpaceShipTwentyone.Graphic.Scale = new Vector2f(0.8f, 0.8f);
            smallSpaceShipTwentytwo.Graphic.Scale = new Vector2f(0.8f, 0.8f);
            smallSpaceShipTwentythree.Graphic.Scale = new Vector2f(0.8f, 0.8f);

            mediumSpaceShipOne.Graphic.Scale = new Vector2f(0.8f, 0.8f);
            mediumSpaceShipTwo.Graphic.Scale = new Vector2f(0.8f, 0.8f);
            mediumSpaceShipThree.Graphic.Scale = new Vector2f(0.8f, 0.8f);
            mediumSpaceShipFour.Graphic.Scale = new Vector2f(0.8f, 0.8f);
            mediumSpaceShipFive.Graphic.Scale = new Vector2f(0.8f, 0.8f);
            mediumSpaceShipSix.Graphic.Scale = new Vector2f(0.8f, 0.8f);
            mediumSpaceShipSeven.Graphic.Scale = new Vector2f(0.8f, 0.8f);
            mediumSpaceShipEight.Graphic.Scale = new Vector2f(0.8f, 0.8f);
            mediumSpaceShipNine.Graphic.Scale = new Vector2f(0.8f, 0.8f);
            mediumSpaceShipTen.Graphic.Scale = new Vector2f(0.8f, 0.8f);

            bigSpaceShipOne.Graphic.Scale = new Vector2f(0.8f, 0.8f);
            bigSpaceShipTwo.Graphic.Scale = new Vector2f(0.8f, 0.8f);
            bigSpaceShipThree.Graphic.Scale = new Vector2f(0.8f, 0.8f);
            bigSpaceShipFour.Graphic.Scale = new Vector2f(0.8f, 0.8f);
            bigSpaceShipFive.Graphic.Scale = new Vector2f(0.8f, 0.8f);
            bigSpaceShipSix.Graphic.Scale = new Vector2f(0.8f, 0.8f);
            bigSpaceShipSeven.Graphic.Scale = new Vector2f(0.8f, 0.8f);
            bigSpaceShipEight.Graphic.Scale = new Vector2f(0.8f, 0.8f);
            bigSpaceShipNine.Graphic.Scale = new Vector2f(0.8f, 0.8f);
            bigSpaceShipTen.Graphic.Scale = new Vector2f(0.8f, 0.8f);

            string backgroundImageFilePath = "Assets/Images/SpaceBackground.png";
            background = new Entity(backgroundImageFilePath);
            background.Graphic.Origin = new Vector2f(0, 2159);
            background.Position = new Vector2f(0f, 0f);          
                                
            Vector2f playerPosition = new Vector2f(renderWindow.Size.X / 2, renderWindow.Size.Y / 2);
            player.Position = playerPosition;
            CollisionsHandler.AddEntity(player);                        
            player.IsPlayer = true;

            float addFuelPickableEffect = 100f;
            float fuelSpawnTimeRate = 45;
            float fuelSpawnDuration = 8;
            string gasCanImageFilePath = "Assets/Images/GasCan.png";
            gasCan = new GasCan(renderWindow, addFuelPickableEffect, fuelSpawnTimeRate, fuelSpawnDuration, gasCanImageFilePath);
            gasCan.Position = new Vector2f(-50f, -50f);
            CollisionsHandler.AddEntity(gasCan);
            gasCan.IsGasCan = true;

            float addPlasmaPickableEffect = 60f;
            float plasmaSpawnTimeRate = 30;
            float plasmaSpawnDuration = 8;
            string plasmaImageFilePath = "Assets/Images/Plasma.png";
            plasma = new Plasma(renderWindow, addPlasmaPickableEffect, plasmaSpawnTimeRate, plasmaSpawnDuration, plasmaImageFilePath);
            plasma.Position = new Vector2f(-50f, -50f);
            CollisionsHandler.AddEntity(plasma);
            plasma.IsPlasma = true;

            float addLifePickableEffect = 1f;
            float lifeSpawnTimeRate = 100;
            float lifeSpawnDuration = 8;
            string lifeImageFilePath = "Assets/Images/Life.png";
            life = new Life(renderWindow, addLifePickableEffect, lifeSpawnTimeRate, lifeSpawnDuration, lifeImageFilePath);
            life.Position = new Vector2f(-50f, -50f);
            CollisionsHandler.AddEntity(life);
            life.IsLife = true;
                        
            wave = 1;
            waveText = wave.ToString();

            for (int i = 0; i < totalSmallAsteroids; i++)
            {                
                CollisionsHandler.AddEntity(smallAsteroids[i]);
                smallAsteroids[i].IsSmallAsteroid = true;
                smallAsteroids[i].IsAsteroid = true;
                smallAsteroids[i].IsDestroyed = false;

            }

            for (int i = 0; i < totalMediumAsteroids; i++)
            {               
                CollisionsHandler.AddEntity(mediumAsteroids[i]);
                mediumAsteroids[i].IsMediumAsteroid = true;
                mediumAsteroids[i].IsAsteroid = true;
                mediumAsteroids[i].IsDestroyed = false;

            }

            for (int i = 0; i < totalBigAsteroids; i++)
            {                
                CollisionsHandler.AddEntity(bigAsteroids[i]);
                bigAsteroids[i].IsBigAsteroid = true;
                bigAsteroids[i].IsAsteroid = true;
                bigAsteroids[i].IsDestroyed = false;

            }

            asteroidsTimer = 0;
            asteroidWaveDuration = 45;
            asteroidNextWave = 60;
                        
            CollisionsHandler.AddEntity(smallSpaceShipOne);
            CollisionsHandler.AddEntity(smallSpaceShipTwo);
            CollisionsHandler.AddEntity(smallSpaceShipThree);
            CollisionsHandler.AddEntity(smallSpaceShipFour);
            CollisionsHandler.AddEntity(smallSpaceShipFive);
            CollisionsHandler.AddEntity(smallSpaceShipSix);
            CollisionsHandler.AddEntity(smallSpaceShipSeven);
            CollisionsHandler.AddEntity(smallSpaceShipEight);
            CollisionsHandler.AddEntity(smallSpaceShipNine);
            CollisionsHandler.AddEntity(smallSpaceShipTen);
            CollisionsHandler.AddEntity(smallSpaceShipEleven);
            CollisionsHandler.AddEntity(smallSpaceShipTwelve);
            CollisionsHandler.AddEntity(smallSpaceShipThirteen);
            CollisionsHandler.AddEntity(smallSpaceShipFourteen);
            CollisionsHandler.AddEntity(smallSpaceShipFifteen);
            CollisionsHandler.AddEntity(smallSpaceShipSixteen);
            CollisionsHandler.AddEntity(smallSpaceShipSeventeen);
            CollisionsHandler.AddEntity(smallSpaceShipEighteen);
            CollisionsHandler.AddEntity(smallSpaceShipNineteen);
            CollisionsHandler.AddEntity(smallSpaceShipTwenty);
            CollisionsHandler.AddEntity(smallSpaceShipTwentyone);
            CollisionsHandler.AddEntity(smallSpaceShipTwentytwo);
            CollisionsHandler.AddEntity(smallSpaceShipTwentythree);

            CollisionsHandler.AddEntity(mediumSpaceShipOne);
            CollisionsHandler.AddEntity(mediumSpaceShipTwo);
            CollisionsHandler.AddEntity(mediumSpaceShipThree);
            CollisionsHandler.AddEntity(mediumSpaceShipFour);
            CollisionsHandler.AddEntity(mediumSpaceShipFive);
            CollisionsHandler.AddEntity(mediumSpaceShipSix);
            CollisionsHandler.AddEntity(mediumSpaceShipSeven);
            CollisionsHandler.AddEntity(mediumSpaceShipEight);
            CollisionsHandler.AddEntity(mediumSpaceShipNine);
            CollisionsHandler.AddEntity(mediumSpaceShipTen);

            CollisionsHandler.AddEntity(bigSpaceShipOne);
            CollisionsHandler.AddEntity(bigSpaceShipTwo);
            CollisionsHandler.AddEntity(bigSpaceShipThree);
            CollisionsHandler.AddEntity(bigSpaceShipFour);
            CollisionsHandler.AddEntity(bigSpaceShipFive);
            CollisionsHandler.AddEntity(bigSpaceShipSix);
            CollisionsHandler.AddEntity(bigSpaceShipSeven);
            CollisionsHandler.AddEntity(bigSpaceShipEight);
            CollisionsHandler.AddEntity(bigSpaceShipNine);
            CollisionsHandler.AddEntity(bigSpaceShipTen);

            travelTime = 40;
            travelTimer = 0;

            float shootingStarSpeed = 300f;
            int shootingStarScorePoints = 350;
            string shootingStarImageFilePath = "Assets/Images/ShootingStar.png";
            shootingStar = new ShootingStar(renderWindow, player, shootingStarSpeed, shootingStarScorePoints, shootingStarImageFilePath);
            CollisionsHandler.AddEntity(shootingStar);
            shootingStar.IsShootingStar = true;            
            shootingStar.Position = new Vector2f(-200f, -200f);

            string hudFontFilePath = "Assets/Fonts/SMOKIND.otf";
            hud = new HUD(renderWindow, player, hudFontFilePath);

            string hudbarImageFilePath = "Assets/Images/Hudbar.png";
            hudbar = new Entity(hudbarImageFilePath);
            hudbar.Position = new Vector2f(0f, 0f);

            backgroundMusic = new Music("Assets/Sounds/PixelPerfect.wav");
            backgroundMusic.Loop = true;
            backgroundMusic.Play();

            pauseGameplay = false;

            pauseMenu = new Entity("Assets/Images/PauseMenu.png");
            pauseMenu.Position = new Vector2f(-2000f, -2000f);
                       
            textFont = new Font("Assets/Fonts/SMOKIND.otf");
            uint textSize = 30;

            gamePausedText = new Text("GAME PAUSED", textFont, textSize);
            gamePausedText.FillColor = Color.Blue;
            gamePausedText.OutlineColor = Color.White;
            gamePausedText.OutlineThickness = 5;

            gamePausedText.Position = new Vector2f(-200f, -200f);

            Color buttonsBackgroundColor = new Color(185, 185, 185);
            Color buttonsTextColor = Color.Red;
            Color buttonsOutlineColor = Color.White;
            string buttonsFontPath = "Assets/Fonts/SMOKIND.otf";
            string buttonsBackgroundPath = "Assets/Images/Button.png";
            float buttonOutlineThickness = 2f;            
            uint buttonsTextSize = 20;

            continueButton = new Button(renderWindow, buttonsFontPath, buttonsBackgroundPath);
            continueButton.Background.Scale = new Vector2f(0.45f, 0.45f);
            continueButton.SetText("Continue", buttonsTextSize);
            continueButton.SetColor(buttonsBackgroundColor);
            continueButton.FormatText(buttonsTextColor, buttonsOutlineColor, outline: true, buttonOutlineThickness);
            continueButton.SetPosition(new Vector2f(- 200f, -200f));

            mainMenuButton = new Button(renderWindow, buttonsFontPath, buttonsBackgroundPath);
            mainMenuButton.Background.Scale = new Vector2f(0.45f, 0.45f);
            mainMenuButton.SetText("Main menu", buttonsTextSize);
            mainMenuButton.SetColor(buttonsBackgroundColor);
            mainMenuButton.FormatText(buttonsTextColor, buttonsOutlineColor, outline: true, buttonOutlineThickness);
            mainMenuButton.SetPosition(new Vector2f(-200f, -200f));

            looseMenu = new Entity("Assets/Images/LooseMenu.png");
            looseMenu.Position = new Vector2f(-2000f, -2000f);

            looseText = new Text("YOUR SPACESHIP EXPLODED!", textFont, textSize);
            looseText.FillColor = Color.Blue;
            looseText.OutlineColor = Color.White;
            looseText.OutlineThickness = 5;
            looseText.Position = new Vector2f(-200f, -200f);

            finalScoreText = new Text($"FINAL SCORE: {player.Score}", textFont, textSize);            
            finalScoreText.Position = new Vector2f(-200f, -200f);

            nameInputed = false;          

            scoreInputNameText = new Text("INPUT SCORE NAME: ", textFont, 26);
            scoreInputNameText.FillColor = Color.Red;
            scoreInputNameText.OutlineColor = Color.White;
            scoreInputNameText.OutlineThickness = 5;
            scoreInputNameText.Position = new Vector2f(-400f, -400f);

            scoreInputNameInstructionsText = new Text("Press enter to finish input", textFont, 25);
            scoreInputNameInstructionsText.FillColor = Color.Green;
            scoreInputNameInstructionsText.OutlineColor = Color.Black;
            scoreInputNameInstructionsText.OutlineThickness = 5;
            scoreInputNameInstructionsText.Position = new Vector2f(-400f, -400f);

            scoreName = new char[3];
            charactersInputed = 0;

            scoreNameText = new Text("", textFont, textSize + 10);
            scoreNameText.FillColor = Color.Black;
            scoreNameText.OutlineColor = Color.White;
            scoreNameText.OutlineThickness = 5;
            scoreNameText.Position = new Vector2f(-400f, -400f);            
            
            newHighScore = false;

            newHighScoreText = new Text("", textFont, textSize + 10);
            newHighScoreText.FillColor = Color.Black;
            newHighScoreText.OutlineColor = Color.White;
            newHighScoreText.OutlineThickness = 5;
            newHighScoreText.Position = new Vector2f(-400f, -400f);


            restartButton = new Button(renderWindow, buttonsFontPath, buttonsBackgroundPath);
            restartButton.Background.Scale = new Vector2f(0.45f, 0.45f);
            restartButton.SetText("Restart", buttonsTextSize);
            restartButton.SetColor(buttonsBackgroundColor);
            restartButton.FormatText(buttonsTextColor, buttonsOutlineColor, outline: true, buttonOutlineThickness);
            restartButton.SetPosition(new Vector2f(-200f, -200f));

            renderWindow.KeyPressed += OnPressPauseKey;
            continueButton.OnPressed += OnPressContinueKey;
            mainMenuButton.OnPressed += OnPressMainMenuKey;
            restartButton.OnPressed += OnPressRestartKey;

            continueButton.OnTouched += OnTouchedContinue;
            mainMenuButton.OnTouched += OnTouchedMainMenu;
            restartButton.OnTouched += OnTouchedRestart;

            continueButton.OnNotTouched += OnNotTouchedContinue;
            mainMenuButton.OnNotTouched += OnNotTouchedMainMenu;
            restartButton.OnNotTouched += OnNotTouchedRestart;

            renderWindow.LostFocus += OnMinimizedWindow;
            renderWindow.GainedFocus += OnMaximizedWindow;
        }
       
        private void OnPressPauseKey(object sender, KeyEventArgs args)
        {
            if (args.Code == Keyboard.Key.Escape)
            {
                pauseGameplay = true;

                FloatRect pauseMenuBounds = pauseMenu.Graphic.GetGlobalBounds();
                pauseMenu.Graphic.Origin = new Vector2f(pauseMenuBounds.Width / 2, pauseMenuBounds.Height / 2);

                pauseMenu.Position = new Vector2f(renderWindow.Size.X / 2, renderWindow.Size.Y / 2);

                FloatRect gamePausedTextBounds = gamePausedText.GetGlobalBounds();
                gamePausedText.Origin = new Vector2f(gamePausedTextBounds.Width / 2, gamePausedTextBounds.Height / 2);
                float gamePausedTextHeightOffset = 50f;

                gamePausedText.Position = new Vector2f(renderWindow.Size.X / 2, renderWindow.Size.Y / 2 - gamePausedTextHeightOffset);

                float ButtonsWidthOffset = 90f;
                float ButtonsHeightOffset = 55f;

                float ButtonsTextHeigthOffset = 5f;

                continueButton.SetPosition(new Vector2f(renderWindow.Size.X / 2 - ButtonsWidthOffset, renderWindow.Size.Y / 2 + ButtonsHeightOffset));
                continueButton.Text.Position = new Vector2f(continueButton.Background.Position.X, continueButton.Background.Position.Y - ButtonsTextHeigthOffset);

                mainMenuButton.SetPosition(new Vector2f(renderWindow.Size.X / 2 + ButtonsWidthOffset, renderWindow.Size.Y / 2 + ButtonsHeightOffset));
                mainMenuButton.Text.Position = new Vector2f(mainMenuButton.Background.Position.X, mainMenuButton.Background.Position.Y - ButtonsTextHeigthOffset);

            }                
        }

        private void OnPressContinueKey()
        {
            pauseGameplay = false;

            pauseMenu.Position = new Vector2f(-2000f, -2000f);           
            gamePausedText.Position = new Vector2f(-200f, -200f);
            continueButton.SetPosition (new Vector2f (-200f, -200f));            
            mainMenuButton.SetPosition (new Vector2f(-200f, -200f));
        }

        private void OnPressMainMenuKey() => OnMainMenuPressed?.Invoke();
        private void OnPressRestartKey() => OnRestartPressed?.Invoke();

        private void OnTouchedContinue() => continueButton.Background.Color = new Color(145, 145, 145);
        private void OnTouchedMainMenu() => mainMenuButton.Background.Color = new Color(145, 145, 145);
        private void OnTouchedRestart() => restartButton.Background.Color = new Color(145, 145, 145);

        private void OnNotTouchedContinue() => continueButton.Background.Color = new Color(185, 185, 185);
        private void OnNotTouchedMainMenu() => mainMenuButton.Background.Color = new Color(185, 185, 185);
        private void OnNotTouchedRestart() => restartButton.Background.Color = new Color(185, 185, 185);

        private void OnMinimizedWindow(object sender, EventArgs eventArgs) => backgroundMusic.Pause();
        private void OnMaximizedWindow(object sender, EventArgs eventArgs) => backgroundMusic.Play();

       
        private void PlayerDeath()
        {
            if (player.Lives == 0)
            {                                  
                renderWindow.KeyPressed -= OnPressPauseKey;

                FloatRect looseMenuBounds = looseMenu.Graphic.GetGlobalBounds();
                looseMenu.Graphic.Origin = new Vector2f(looseMenuBounds.Width / 2, looseMenuBounds.Height / 2);

                looseMenu.Position = new Vector2f(renderWindow.Size.X / 2, renderWindow.Size.Y / 2);

                float ButtonsWidthOffset = 100f;
                float ButtonsHeightOffset = 75f;

                float ButtonsTextHeigthOffset = 5f;

                if(nameInputed)
                {
                    mainMenuButton.SetPosition(new Vector2f(renderWindow.Size.X / 2 + ButtonsWidthOffset, renderWindow.Size.Y / 2 + ButtonsHeightOffset));
                    mainMenuButton.Text.Position = new Vector2f(mainMenuButton.Background.Position.X, mainMenuButton.Background.Position.Y - ButtonsTextHeigthOffset);

                    restartButton.SetPosition(new Vector2f(renderWindow.Size.X / 2 - ButtonsWidthOffset, renderWindow.Size.Y / 2 + ButtonsHeightOffset));
                    restartButton.Text.Position = new Vector2f(restartButton.Background.Position.X, restartButton.Background.Position.Y - ButtonsTextHeigthOffset);

                    scoreInputNameText.Position = new Vector2f(-400f, -400f);
                    scoreInputNameInstructionsText.Position = new Vector2f(-400f, -400f);

                    scoreNameText.Position = new Vector2f(-400f, -400f);
                    scoreNameText.DisplayedString = "";

                    newHighScoreText.Position = new Vector2f(-400f, -400f);
                }               

                FloatRect looseTextBounds = looseText.GetGlobalBounds();
                looseText.Origin = new Vector2f(looseTextBounds.Width / 2, looseTextBounds.Height / 2);
                float looseTextHeightOffset = 75f;

                looseText.Position = new Vector2f(renderWindow.Size.X / 2, renderWindow.Size.Y / 2 - looseTextHeightOffset);

                uint textSize = 30;

                if (language == "es")
                {
                    finalScoreText = new Text($"PUNTUACION FINAL: {player.Score}", textFont, textSize);
                    scoreInputNameText.DisplayedString = "NOMBRE PUNTUACIÓN: ";
                    scoreInputNameInstructionsText.DisplayedString = "Presionar enter para terminar";
                    newHighScoreText.DisplayedString = "¡NUEVA MEJOR PUNTUACIÓN!";
                }
                    

                else if (language == "en")
                {
                    finalScoreText = new Text($"FINAL SCORE: {player.Score}", textFont, textSize);
                    scoreInputNameText.DisplayedString = "INPUT SCORE NAME: ";
                    scoreInputNameInstructionsText.DisplayedString = "Press enter to finish input";
                    newHighScoreText.DisplayedString = "NEW HIGHSCORE!";
                }

                finalScoreText.FillColor = Color.Blue;
                finalScoreText.OutlineColor = Color.White;
                finalScoreText.OutlineThickness = 5;

                FloatRect finalScoreTextBounds = finalScoreText.GetGlobalBounds();
                finalScoreText.Origin = new Vector2f(finalScoreTextBounds.Width / 2, finalScoreTextBounds.Height / 2);
                float finalScoreTextHeightOffset = 20f;

                finalScoreText.Position = new Vector2f(renderWindow.Size.X / 2, renderWindow.Size.Y / 2 - finalScoreTextHeightOffset);

                FloatRect scoreInputNameTextBounds = finalScoreText.GetGlobalBounds();
                scoreInputNameText.Origin = new Vector2f(scoreInputNameTextBounds.Width / 2, scoreInputNameTextBounds.Height / 2);
                float scoreInputNameTextWidthOffset = 100;
                float scoreInputNameTextHeightOffset = 70;

                FloatRect scoreInputNameInstructionsTextBounds = scoreInputNameInstructionsText.GetGlobalBounds();
                scoreInputNameInstructionsText.Origin = new Vector2f(scoreInputNameInstructionsTextBounds.Width / 2, scoreInputNameInstructionsTextBounds.Height / 2);
                float scoreInputNameInstructionsTextHeightOffset = 200;

                FloatRect scoreNameTextBounds = scoreNameText.GetGlobalBounds();
                scoreNameText.Origin = new Vector2f(scoreNameTextBounds.Width / 2, scoreNameTextBounds.Height / 2);
                float scoreNameTextWidthOffset = 120;
                float scoreNameTextHeightOffset = 60;

                FloatRect newHighScoreTextBounds = newHighScoreText.GetGlobalBounds();
                newHighScoreText.Origin = new Vector2f(newHighScoreTextBounds.Width / 2, newHighScoreTextBounds.Height / 2);
                float newHighScoreTextHeightOffset = 270;
                               

                if(scoreScreen)
                {
                    char[] scoreChar = new char[90];                                       

                    string score1Chars;
                    string score2Chars;
                    string score3Chars;
                    string score4Chars;
                    string score5Chars;
                    string score6Chars;
                    string score7Chars;
                    string score8Chars;
                    string score9Chars;
                    string score10Chars;

                    int score1;
                    int score2;
                    int score3;
                    int score4;
                    int score5;
                    int score6;
                    int score7;
                    int score8;
                    int score9;
                    int score10;

                    highScores.Seek(0, SeekOrigin.Begin);

                    highScores.Read(highScoresData, 0, (int)highScoresData.Length);              
                                            
                    highScores.Seek(0, SeekOrigin.End);
                                                                                                    
                    highScores.Seek(0, SeekOrigin.Begin);

                    highScores.Read(highScoresData, 0, (int)highScoresData.Length);

                    scoreChar = ASCIIEncoding.ASCII.GetChars(highScoresData);

                    score1Chars = Convert.ToString(scoreChar[0]) + Convert.ToString(scoreChar[1]) + Convert.ToString(scoreChar[2]) + Convert.ToString(scoreChar[3]) + Convert.ToString(scoreChar[4]) 
                        + Convert.ToString(scoreChar[5]) + Convert.ToString(scoreChar[6]) + Convert.ToString(scoreChar[7]);

                    score2Chars = Convert.ToString(scoreChar[9]) + Convert.ToString(scoreChar[10]) + Convert.ToString(scoreChar[11]) + Convert.ToString(scoreChar[12]) + Convert.ToString(scoreChar[13]) 
                        + Convert.ToString(scoreChar[14]) + Convert.ToString(scoreChar[15]) + Convert.ToString(scoreChar[16]);

                    score3Chars = Convert.ToString(scoreChar[18]) + Convert.ToString(scoreChar[19]) + Convert.ToString(scoreChar[20]) + Convert.ToString(scoreChar[21]) + Convert.ToString(scoreChar[22]) 
                        + Convert.ToString(scoreChar[23]) + Convert.ToString(scoreChar[24]) + Convert.ToString(scoreChar[25]);

                    score4Chars = Convert.ToString(scoreChar[27]) + Convert.ToString(scoreChar[28]) + Convert.ToString(scoreChar[29]) + Convert.ToString(scoreChar[30]) + Convert.ToString(scoreChar[31])
                        + Convert.ToString(scoreChar[32] + Convert.ToString(scoreChar[33]) + Convert.ToString(scoreChar[34]) + Convert.ToString(scoreChar[35]));

                    score5Chars = Convert.ToString(scoreChar[36]) + Convert.ToString(scoreChar[37]) + Convert.ToString(scoreChar[38]) + Convert.ToString(scoreChar[39]) + Convert.ToString(scoreChar[40]) 
                        + Convert.ToString(scoreChar[41]) + Convert.ToString(scoreChar[42]) + Convert.ToString(scoreChar[43]);

                    score6Chars = Convert.ToString(scoreChar[45]) + Convert.ToString(scoreChar[46]) + Convert.ToString(scoreChar[47]) + Convert.ToString(scoreChar[48]) + Convert.ToString(scoreChar[49]) 
                        + Convert.ToString(scoreChar[50]) + Convert.ToString(scoreChar[51]) + Convert.ToString(scoreChar[52]);

                    score7Chars = Convert.ToString(scoreChar[54]) + Convert.ToString(scoreChar[55]) + Convert.ToString(scoreChar[56]) + Convert.ToString(scoreChar[57]) + Convert.ToString(scoreChar[58]) 
                        + Convert.ToString(scoreChar[59]) + Convert.ToString(scoreChar[60]) + Convert.ToString(scoreChar[61]);

                    score8Chars = Convert.ToString(scoreChar[63]) + Convert.ToString(scoreChar[64]) + Convert.ToString(scoreChar[65]) + Convert.ToString(scoreChar[66]) + Convert.ToString(scoreChar[67])
                        + Convert.ToString(scoreChar[68]) + Convert.ToString(scoreChar[69]) + Convert.ToString(scoreChar[70]);

                    score9Chars = Convert.ToString(scoreChar[72]) + Convert.ToString(scoreChar[73]) + Convert.ToString(scoreChar[74]) + Convert.ToString(scoreChar[75]) + Convert.ToString(scoreChar[76])
                        + Convert.ToString(scoreChar[77]) + Convert.ToString(scoreChar[78]) + Convert.ToString(scoreChar[79]);

                    score10Chars = Convert.ToString(scoreChar[81]) + Convert.ToString(scoreChar[82]) + Convert.ToString(scoreChar[83]) + Convert.ToString(scoreChar[84]) 
                        + Convert.ToString(scoreChar[85]) + Convert.ToString(scoreChar[86]) + Convert.ToString(scoreChar[87]) + Convert.ToString(scoreChar[88]);

                    string score1Nums = Convert.ToString(scoreChar[4]) + Convert.ToString(scoreChar[5]) + Convert.ToString(scoreChar[6]) + Convert.ToString(scoreChar[7]);
                    string score2Nums = Convert.ToString(scoreChar[13]) + Convert.ToString(scoreChar[14]) + Convert.ToString(scoreChar[15]) + Convert.ToString(scoreChar[16]);
                    string score3Nums = Convert.ToString(scoreChar[22]) + Convert.ToString(scoreChar[23]) + Convert.ToString(scoreChar[24]) + Convert.ToString(scoreChar[25]);
                    string score4Nums = Convert.ToString(scoreChar[31]) + Convert.ToString(scoreChar[32]) + Convert.ToString(scoreChar[33]) + Convert.ToString(scoreChar[34]);
                    string score5Nums = Convert.ToString(scoreChar[40]) + Convert.ToString(scoreChar[41]) + Convert.ToString(scoreChar[42]) + Convert.ToString(scoreChar[43]);
                    string score6Nums = Convert.ToString(scoreChar[49]) + Convert.ToString(scoreChar[50]) + Convert.ToString(scoreChar[51]) + Convert.ToString(scoreChar[52]);
                    string score7Nums = Convert.ToString(scoreChar[58]) + Convert.ToString(scoreChar[59]) + Convert.ToString(scoreChar[60]) + Convert.ToString(scoreChar[61]);
                    string score8Nums = Convert.ToString(scoreChar[67]) + Convert.ToString(scoreChar[68]) + Convert.ToString(scoreChar[69]) + Convert.ToString(scoreChar[70]);
                    string score9Nums = Convert.ToString(scoreChar[76]) + Convert.ToString(scoreChar[77]) + Convert.ToString(scoreChar[78]) + Convert.ToString(scoreChar[79]);
                    string score10Nums = Convert.ToString(scoreChar[85]) + Convert.ToString(scoreChar[86]) + Convert.ToString(scoreChar[87]) + Convert.ToString(scoreChar[88]);

                    score1 = Convert.ToInt32(score1Nums);
                    score2 = Convert.ToInt32(score2Nums);
                    score3 = Convert.ToInt32(score3Nums);
                    score4 = Convert.ToInt32(score4Nums);
                    score5 = Convert.ToInt32(score5Nums);
                    score6 = Convert.ToInt32(score6Nums);
                    score7 = Convert.ToInt32(score7Nums);
                    score8 = Convert.ToInt32(score8Nums);
                    score9 = Convert.ToInt32(score9Nums);
                    score10 = Convert.ToInt32(score10Nums);

                    if (!nameInputed)
                    {
                        if(player.Score > score1 || player.Score > score2 || player.Score > score3 || player.Score > score4 || player.Score > score5 || player.Score > score6 || player.Score > score7
                        || player.Score > score8 || player.Score > score9 || player.Score > score10)
                        {
                            int sleepTime = 60;

                            if (language == "es")
                            {
                                scoreInputNameText.Position = new Vector2f(renderWindow.Size.X / 2 - scoreInputNameTextWidthOffset + 50, renderWindow.Size.Y / 2 + scoreInputNameTextHeightOffset);
                                scoreNameText.Position = new Vector2f(renderWindow.Size.X / 2 + scoreNameTextWidthOffset + 30, renderWindow.Size.Y / 2 + scoreNameTextHeightOffset);
                            }
                            else if (language == "en")
                            {
                                scoreInputNameText.Position = new Vector2f(renderWindow.Size.X / 2 - scoreInputNameTextWidthOffset, renderWindow.Size.Y / 2 + scoreInputNameTextHeightOffset);
                                scoreNameText.Position = new Vector2f(renderWindow.Size.X / 2 + scoreNameTextWidthOffset, renderWindow.Size.Y / 2 + scoreNameTextHeightOffset);
                            }

                            scoreInputNameInstructionsText.Position = new Vector2f(renderWindow.Size.X / 2, renderWindow.Size.Y / 2 + scoreInputNameInstructionsTextHeightOffset);
                            newHighScoreText.Position = new Vector2f(renderWindow.Size.X / 2, renderWindow.Size.Y / 2 - newHighScoreTextHeightOffset);
                            
                            
                            if (Keyboard.IsKeyPressed(Keyboard.Key.A) && charactersInputed < 3)
                            {
                                
                                if (charactersInputed == 0)
                                    scoreName[0] = 'A';

                                if (charactersInputed == 1)
                                    scoreName[1] = 'A';

                                if (charactersInputed == 2)
                                    scoreName[2] = 'A';

                                charactersInputed++;

                                System.Threading.Thread.Sleep(sleepTime);

                            }
                            if (Keyboard.IsKeyPressed(Keyboard.Key.B) && charactersInputed < 3)
                            {
                                if (charactersInputed == 0)
                                    scoreName[0] = 'B';

                                if (charactersInputed == 1)
                                    scoreName[1] = 'B';

                                if (charactersInputed == 2)
                                    scoreName[2] = 'B';

                                charactersInputed++;

                                System.Threading.Thread.Sleep(sleepTime);

                            }
                            if (Keyboard.IsKeyPressed(Keyboard.Key.C) && charactersInputed < 3)
                            {
                                if (charactersInputed == 0)
                                    scoreName[0] = 'C';

                                if (charactersInputed == 1)
                                    scoreName[1] = 'C';

                                if (charactersInputed == 2)
                                    scoreName[2] = 'C';

                                charactersInputed++;

                                System.Threading.Thread.Sleep(sleepTime);

                            }
                            if (Keyboard.IsKeyPressed(Keyboard.Key.D) && charactersInputed < 3)
                            {
                                if (charactersInputed == 0)
                                    scoreName[0] = 'D';

                                if (charactersInputed == 1)
                                    scoreName[1] = 'D';

                                if (charactersInputed == 2)
                                    scoreName[2] = 'D';

                                charactersInputed++;

                                System.Threading.Thread.Sleep(sleepTime);

                            }
                            if (Keyboard.IsKeyPressed(Keyboard.Key.E) && charactersInputed < 3)
                            {
                                if (charactersInputed == 0)
                                    scoreName[0] = 'E';

                                if (charactersInputed == 1)
                                    scoreName[1] = 'E';

                                if (charactersInputed == 2)
                                    scoreName[2] = 'E';

                                charactersInputed++;

                                System.Threading.Thread.Sleep(sleepTime);

                            }
                            if (Keyboard.IsKeyPressed(Keyboard.Key.F) && charactersInputed < 3)
                            {
                                if (charactersInputed == 0)
                                    scoreName[0] = 'F';

                                if (charactersInputed == 1)
                                    scoreName[1] = 'F';

                                if (charactersInputed == 2)
                                    scoreName[2] = 'F';

                                charactersInputed++;

                                System.Threading.Thread.Sleep(sleepTime);

                            }
                            if (Keyboard.IsKeyPressed(Keyboard.Key.G) && charactersInputed < 3)
                            {
                                if (charactersInputed == 0)
                                    scoreName[0] = 'G';

                                if (charactersInputed == 1)
                                    scoreName[1] = 'G';

                                if (charactersInputed == 2)
                                    scoreName[2] = 'G';

                                charactersInputed++;

                                System.Threading.Thread.Sleep(sleepTime);

                            }
                            if (Keyboard.IsKeyPressed(Keyboard.Key.H) && charactersInputed < 3)
                            {
                                if (charactersInputed == 0)
                                    scoreName[0] = 'H';

                                if (charactersInputed == 1)
                                    scoreName[1] = 'H';

                                if (charactersInputed == 2)
                                    scoreName[2] = 'H';

                                charactersInputed++;

                                System.Threading.Thread.Sleep(sleepTime);

                            }
                            if (Keyboard.IsKeyPressed(Keyboard.Key.I) && charactersInputed < 3)
                            {
                                if (charactersInputed == 0)
                                    scoreName[0] = 'I';

                                if (charactersInputed == 1)
                                    scoreName[1] = 'I';

                                if (charactersInputed == 2)
                                    scoreName[2] = 'I';

                                charactersInputed++;

                                System.Threading.Thread.Sleep(sleepTime);

                            }
                            if (Keyboard.IsKeyPressed(Keyboard.Key.J) && charactersInputed < 3)
                            {
                                if (charactersInputed == 0)
                                    scoreName[0] = 'J';

                                if (charactersInputed == 1)
                                    scoreName[1] = 'J';

                                if (charactersInputed == 2)
                                    scoreName[2] = 'J';

                                charactersInputed++;

                                System.Threading.Thread.Sleep(sleepTime);

                            }
                            if (Keyboard.IsKeyPressed(Keyboard.Key.K) && charactersInputed < 3)
                            {
                                if (charactersInputed == 0)
                                    scoreName[0] = 'K';

                                if (charactersInputed == 1)
                                    scoreName[1] = 'K';

                                if (charactersInputed == 2)
                                    scoreName[2] = 'K';

                                charactersInputed++;

                                System.Threading.Thread.Sleep(sleepTime);

                            }
                            if (Keyboard.IsKeyPressed(Keyboard.Key.L) && charactersInputed < 3)
                            {
                                if (charactersInputed == 0)
                                    scoreName[0] = 'L';

                                if (charactersInputed == 1)
                                    scoreName[1] = 'L';

                                if (charactersInputed == 2)
                                    scoreName[2] = 'L';

                                charactersInputed++;

                                System.Threading.Thread.Sleep(sleepTime);

                            }
                            if (Keyboard.IsKeyPressed(Keyboard.Key.M) && charactersInputed < 3)
                            {
                                if (charactersInputed == 0)
                                    scoreName[0] = 'M';

                                if (charactersInputed == 1)
                                    scoreName[1] = 'M';

                                if (charactersInputed == 2)
                                    scoreName[2] = 'M';

                                charactersInputed++;

                                System.Threading.Thread.Sleep(sleepTime);

                            }
                            if (Keyboard.IsKeyPressed(Keyboard.Key.N) && charactersInputed < 3)
                            {
                                if (charactersInputed == 0)
                                    scoreName[0] = 'N';

                                if (charactersInputed == 1)
                                    scoreName[1] = 'N';

                                if (charactersInputed == 2)
                                    scoreName[2] = 'N';

                                charactersInputed++;

                                System.Threading.Thread.Sleep(sleepTime);

                            }
                            if (Keyboard.IsKeyPressed(Keyboard.Key.O) && charactersInputed < 3)
                            {
                                if (charactersInputed == 0)
                                    scoreName[0] = 'O';

                                if (charactersInputed == 1)
                                    scoreName[1] = 'O';

                                if (charactersInputed == 2)
                                    scoreName[2] = 'O';

                                charactersInputed++;

                                System.Threading.Thread.Sleep(sleepTime);

                            }
                            if (Keyboard.IsKeyPressed(Keyboard.Key.P) && charactersInputed < 3)
                            {
                                if (charactersInputed == 0)
                                    scoreName[0] = 'P';

                                if (charactersInputed == 1)
                                    scoreName[1] = 'P';

                                if (charactersInputed == 2)
                                    scoreName[2] = 'P';

                                charactersInputed++;

                                System.Threading.Thread.Sleep(sleepTime);

                            }
                            if (Keyboard.IsKeyPressed(Keyboard.Key.Q) && charactersInputed < 3)
                            {
                                if (charactersInputed == 0)
                                    scoreName[0] = 'Q';

                                if (charactersInputed == 1)
                                    scoreName[1] = 'Q';

                                if (charactersInputed == 2)
                                    scoreName[2] = 'Q';

                                charactersInputed++;

                                System.Threading.Thread.Sleep(sleepTime);

                            }
                            if (Keyboard.IsKeyPressed(Keyboard.Key.R) && charactersInputed < 3)
                            {
                                if (charactersInputed == 0)
                                    scoreName[0] = 'R';

                                if (charactersInputed == 1)
                                    scoreName[1] = 'R';

                                if (charactersInputed == 2)
                                    scoreName[2] = 'R';

                                charactersInputed++;

                                System.Threading.Thread.Sleep(sleepTime);

                            }
                            if (Keyboard.IsKeyPressed(Keyboard.Key.S) && charactersInputed < 3)
                            {
                                if (charactersInputed == 0)
                                    scoreName[0] = 'S';

                                if (charactersInputed == 1)
                                    scoreName[1] = 'S';

                                if (charactersInputed == 2)
                                    scoreName[2] = 'S';

                                charactersInputed++;

                                System.Threading.Thread.Sleep(sleepTime);

                            }
                            if (Keyboard.IsKeyPressed(Keyboard.Key.T) && charactersInputed < 3)
                            {
                                if (charactersInputed == 0)
                                    scoreName[0] = 'T';

                                if (charactersInputed == 1)
                                    scoreName[1] = 'T';

                                if (charactersInputed == 2)
                                    scoreName[2] = 'T';

                                charactersInputed++;

                                System.Threading.Thread.Sleep(sleepTime);

                            }
                            if (Keyboard.IsKeyPressed(Keyboard.Key.U) && charactersInputed < 3)
                            {
                                if (charactersInputed == 0)
                                    scoreName[0] = 'U';

                                if (charactersInputed == 1)
                                    scoreName[1] = 'U';

                                if (charactersInputed == 2)
                                    scoreName[2] = 'U';

                                charactersInputed++;

                                System.Threading.Thread.Sleep(sleepTime);

                            }
                            if (Keyboard.IsKeyPressed(Keyboard.Key.V) && charactersInputed < 3)
                            {
                                if (charactersInputed == 0)
                                    scoreName[0] = 'V';

                                if (charactersInputed == 1)
                                    scoreName[1] = 'V';

                                if (charactersInputed == 2)
                                    scoreName[2] = 'V';

                                charactersInputed++;

                                System.Threading.Thread.Sleep(sleepTime);

                            }
                            if (Keyboard.IsKeyPressed(Keyboard.Key.W) && charactersInputed < 3)
                            {
                                if (charactersInputed == 0)
                                    scoreName[0] = 'W';

                                if (charactersInputed == 1)
                                    scoreName[1] = 'W';

                                if (charactersInputed == 2)
                                    scoreName[2] = 'W';

                                charactersInputed++;

                                System.Threading.Thread.Sleep(sleepTime);

                            }
                            if (Keyboard.IsKeyPressed(Keyboard.Key.X) && charactersInputed < 3)
                            {
                                if (charactersInputed == 0)
                                    scoreName[0] = 'X';

                                if (charactersInputed == 1)
                                    scoreName[1] = 'X';

                                if (charactersInputed == 2)
                                    scoreName[2] = 'X';

                                charactersInputed++;

                                System.Threading.Thread.Sleep(sleepTime);

                            }
                            if (Keyboard.IsKeyPressed(Keyboard.Key.Y) && charactersInputed < 3)
                            {                               
                                if (charactersInputed == 0)
                                    scoreName[0] = 'Y';

                                if (charactersInputed == 1)
                                    scoreName[1] = 'Y';

                                if (charactersInputed == 2)
                                    scoreName[2] = 'Y';

                                charactersInputed++;

                                System.Threading.Thread.Sleep(sleepTime);

                            }
                            if (Keyboard.IsKeyPressed(Keyboard.Key.Z) && charactersInputed < 3)
                            {                               
                                if (charactersInputed == 0)
                                    scoreName[0] = 'Z';

                                if (charactersInputed == 1)
                                    scoreName[1] = 'Z';

                                if (charactersInputed == 2)
                                    scoreName[2] = 'Z';

                                charactersInputed++;

                                System.Threading.Thread.Sleep(sleepTime);

                            }

                            if (charactersInputed == 1)
                                scoreNameText.DisplayedString = Convert.ToString(scoreName[0]);

                            else if (charactersInputed == 2)
                                scoreNameText.DisplayedString = Convert.ToString(scoreName[0]) + Convert.ToString(scoreName[1]);

                            else if (charactersInputed == 3)
                                scoreNameText.DisplayedString = Convert.ToString(scoreName[0]) + Convert.ToString(scoreName[1]) + Convert.ToString(scoreName[2]);


                            if (charactersInputed >= 3 && Keyboard.IsKeyPressed(Keyboard.Key.Enter))
                            {
                                nameInputed = true;
                                charactersInputed = 0;
                            }


                        }
                        else
                            nameInputed = true;
                    }                                          
                    
                    string playerFinalScore = player.Score.ToString();

                    if (playerFinalScore.Length == 1)
                        playerFinalScore = "000" + playerFinalScore;

                    else if (playerFinalScore.Length == 2)
                        playerFinalScore = "00" + playerFinalScore;

                    else if (playerFinalScore.Length == 3)
                        playerFinalScore = "0" + playerFinalScore;


                    if (nameInputed)
                    {
                        string scoreNameText = Convert.ToString(scoreName[0]) + Convert.ToString(scoreName[1]) + Convert.ToString(scoreName[2]) + " " + playerFinalScore;
                        Console.WriteLine(scoreNameText);

                        if (Convert.ToInt32(playerFinalScore) > score1)
                        {
                            highScores.Seek(0, SeekOrigin.Begin);
                            highScores.Write(ASCIIEncoding.ASCII.GetBytes(scoreNameText), 0, scoreNameText.Length);

                            highScores.Seek(8, SeekOrigin.Begin);
                            highScores.Write(ASCIIEncoding.ASCII.GetBytes("\n" + score1Chars), 0, score1Chars.Length + 1);

                            highScores.Seek(8, SeekOrigin.Begin);
                            highScores.Write(ASCIIEncoding.ASCII.GetBytes("\n", 0, 0));
                            highScores.Seek(9, SeekOrigin.Current);
                            highScores.Write(ASCIIEncoding.ASCII.GetBytes("\n" + score2Chars), 0, score2Chars.Length + 1);

                            highScores.Seek(8, SeekOrigin.Begin);
                            highScores.Write(ASCIIEncoding.ASCII.GetBytes("\n", 0, 0));
                            highScores.Seek(9, SeekOrigin.Current);
                            highScores.Write(ASCIIEncoding.ASCII.GetBytes("\n", 0, 0));
                            highScores.Seek(9, SeekOrigin.Current);
                            highScores.Write(ASCIIEncoding.ASCII.GetBytes("\n" + score3Chars), 0, score3Chars.Length + 1);

                            highScores.Seek(8, SeekOrigin.Begin);
                            highScores.Write(ASCIIEncoding.ASCII.GetBytes("\n", 0, 0));
                            highScores.Seek(9, SeekOrigin.Current);
                            highScores.Write(ASCIIEncoding.ASCII.GetBytes("\n", 0, 0));
                            highScores.Seek(9, SeekOrigin.Current);
                            highScores.Write(ASCIIEncoding.ASCII.GetBytes("\n", 0, 0));
                            highScores.Seek(9, SeekOrigin.Current);
                            highScores.Write(ASCIIEncoding.ASCII.GetBytes("\n" + score4Chars), 0, score4Chars.Length + 1);

                            highScores.Seek(8, SeekOrigin.Begin);
                            highScores.Write(ASCIIEncoding.ASCII.GetBytes("\n", 0, 0));
                            highScores.Seek(9, SeekOrigin.Current);
                            highScores.Write(ASCIIEncoding.ASCII.GetBytes("\n", 0, 0));
                            highScores.Seek(9, SeekOrigin.Current);
                            highScores.Write(ASCIIEncoding.ASCII.GetBytes("\n", 0, 0));
                            highScores.Seek(9, SeekOrigin.Current);
                            highScores.Write(ASCIIEncoding.ASCII.GetBytes("\n", 0, 0));
                            highScores.Seek(9, SeekOrigin.Current);
                            highScores.Write(ASCIIEncoding.ASCII.GetBytes("\n" + score5Chars), 0, score5Chars.Length + 1);

                            highScores.Seek(8, SeekOrigin.Begin);
                            highScores.Write(ASCIIEncoding.ASCII.GetBytes("\n", 0, 0));
                            highScores.Seek(9, SeekOrigin.Current);
                            highScores.Write(ASCIIEncoding.ASCII.GetBytes("\n", 0, 0));
                            highScores.Seek(9, SeekOrigin.Current);
                            highScores.Write(ASCIIEncoding.ASCII.GetBytes("\n", 0, 0));
                            highScores.Seek(9, SeekOrigin.Current);
                            highScores.Write(ASCIIEncoding.ASCII.GetBytes("\n", 0, 0));
                            highScores.Seek(9, SeekOrigin.Current);
                            highScores.Write(ASCIIEncoding.ASCII.GetBytes("\n", 0, 0));
                            highScores.Seek(9, SeekOrigin.Current);
                            highScores.Write(ASCIIEncoding.ASCII.GetBytes("\n" + score6Chars), 0, score6Chars.Length + 1);

                            highScores.Seek(8, SeekOrigin.Begin);
                            highScores.Write(ASCIIEncoding.ASCII.GetBytes("\n", 0, 0));
                            highScores.Seek(9, SeekOrigin.Current);
                            highScores.Write(ASCIIEncoding.ASCII.GetBytes("\n", 0, 0));
                            highScores.Seek(9, SeekOrigin.Current);
                            highScores.Write(ASCIIEncoding.ASCII.GetBytes("\n", 0, 0));
                            highScores.Seek(9, SeekOrigin.Current);
                            highScores.Write(ASCIIEncoding.ASCII.GetBytes("\n", 0, 0));
                            highScores.Seek(9, SeekOrigin.Current);
                            highScores.Write(ASCIIEncoding.ASCII.GetBytes("\n", 0, 0));
                            highScores.Seek(9, SeekOrigin.Current);
                            highScores.Write(ASCIIEncoding.ASCII.GetBytes("\n", 0, 0));
                            highScores.Seek(9, SeekOrigin.Current);
                            highScores.Write(ASCIIEncoding.ASCII.GetBytes("\n" + score7Chars), 0, score7Chars.Length + 1);

                            highScores.Seek(8, SeekOrigin.Begin);
                            highScores.Write(ASCIIEncoding.ASCII.GetBytes("\n", 0, 0));
                            highScores.Seek(9, SeekOrigin.Current);
                            highScores.Write(ASCIIEncoding.ASCII.GetBytes("\n", 0, 0));
                            highScores.Seek(9, SeekOrigin.Current);
                            highScores.Write(ASCIIEncoding.ASCII.GetBytes("\n", 0, 0));
                            highScores.Seek(9, SeekOrigin.Current);
                            highScores.Write(ASCIIEncoding.ASCII.GetBytes("\n", 0, 0));
                            highScores.Seek(9, SeekOrigin.Current);
                            highScores.Write(ASCIIEncoding.ASCII.GetBytes("\n", 0, 0));
                            highScores.Seek(9, SeekOrigin.Current);
                            highScores.Write(ASCIIEncoding.ASCII.GetBytes("\n", 0, 0));
                            highScores.Seek(9, SeekOrigin.Current);
                            highScores.Write(ASCIIEncoding.ASCII.GetBytes("\n", 0, 0));
                            highScores.Seek(9, SeekOrigin.Current);
                            highScores.Write(ASCIIEncoding.ASCII.GetBytes("\n" + score8Chars), 0, score8Chars.Length + 1);

                            highScores.Seek(8, SeekOrigin.Begin);
                            highScores.Write(ASCIIEncoding.ASCII.GetBytes("\n", 0, 0));
                            highScores.Seek(9, SeekOrigin.Current);
                            highScores.Write(ASCIIEncoding.ASCII.GetBytes("\n", 0, 0));
                            highScores.Seek(9, SeekOrigin.Current);
                            highScores.Write(ASCIIEncoding.ASCII.GetBytes("\n", 0, 0));
                            highScores.Seek(9, SeekOrigin.Current);
                            highScores.Write(ASCIIEncoding.ASCII.GetBytes("\n", 0, 0));
                            highScores.Seek(9, SeekOrigin.Current);
                            highScores.Write(ASCIIEncoding.ASCII.GetBytes("\n", 0, 0));
                            highScores.Seek(9, SeekOrigin.Current);
                            highScores.Write(ASCIIEncoding.ASCII.GetBytes("\n", 0, 0));
                            highScores.Seek(9, SeekOrigin.Current);
                            highScores.Write(ASCIIEncoding.ASCII.GetBytes("\n", 0, 0));
                            highScores.Seek(9, SeekOrigin.Current);
                            highScores.Write(ASCIIEncoding.ASCII.GetBytes("\n", 0, 0));
                            highScores.Seek(9, SeekOrigin.Current);
                            highScores.Write(ASCIIEncoding.ASCII.GetBytes("\n" + score9Chars), 0, score9Chars.Length + 1);

                        }
                        else if (Convert.ToInt32(playerFinalScore) > score2)
                        {
                            highScores.Seek(8, SeekOrigin.Begin);
                            highScores.Write(ASCIIEncoding.ASCII.GetBytes("\n" + scoreNameText), 0, scoreNameText.Length + 1);

                            highScores.Seek(8, SeekOrigin.Begin);
                            highScores.Write(ASCIIEncoding.ASCII.GetBytes("\n", 0, 0));
                            highScores.Seek(9, SeekOrigin.Current);
                            highScores.Write(ASCIIEncoding.ASCII.GetBytes("\n" + score2Chars), 0, score2Chars.Length + 1);

                            highScores.Seek(8, SeekOrigin.Begin);
                            highScores.Write(ASCIIEncoding.ASCII.GetBytes("\n", 0, 0));
                            highScores.Seek(9, SeekOrigin.Current);
                            highScores.Write(ASCIIEncoding.ASCII.GetBytes("\n", 0, 0));
                            highScores.Seek(9, SeekOrigin.Current);
                            highScores.Write(ASCIIEncoding.ASCII.GetBytes("\n" + score3Chars), 0, score3Chars.Length + 1);

                            highScores.Seek(8, SeekOrigin.Begin);
                            highScores.Write(ASCIIEncoding.ASCII.GetBytes("\n", 0, 0));
                            highScores.Seek(9, SeekOrigin.Current);
                            highScores.Write(ASCIIEncoding.ASCII.GetBytes("\n", 0, 0));
                            highScores.Seek(9, SeekOrigin.Current);
                            highScores.Write(ASCIIEncoding.ASCII.GetBytes("\n", 0, 0));
                            highScores.Seek(9, SeekOrigin.Current);
                            highScores.Write(ASCIIEncoding.ASCII.GetBytes("\n" + score4Chars), 0, score4Chars.Length + 1);

                            highScores.Seek(8, SeekOrigin.Begin);
                            highScores.Write(ASCIIEncoding.ASCII.GetBytes("\n", 0, 0));
                            highScores.Seek(9, SeekOrigin.Current);
                            highScores.Write(ASCIIEncoding.ASCII.GetBytes("\n", 0, 0));
                            highScores.Seek(9, SeekOrigin.Current);
                            highScores.Write(ASCIIEncoding.ASCII.GetBytes("\n", 0, 0));
                            highScores.Seek(9, SeekOrigin.Current);
                            highScores.Write(ASCIIEncoding.ASCII.GetBytes("\n", 0, 0));
                            highScores.Seek(9, SeekOrigin.Current);
                            highScores.Write(ASCIIEncoding.ASCII.GetBytes("\n" + score5Chars), 0, score5Chars.Length + 1);

                            highScores.Seek(8, SeekOrigin.Begin);
                            highScores.Write(ASCIIEncoding.ASCII.GetBytes("\n", 0, 0));
                            highScores.Seek(9, SeekOrigin.Current);
                            highScores.Write(ASCIIEncoding.ASCII.GetBytes("\n", 0, 0));
                            highScores.Seek(9, SeekOrigin.Current);
                            highScores.Write(ASCIIEncoding.ASCII.GetBytes("\n", 0, 0));
                            highScores.Seek(9, SeekOrigin.Current);
                            highScores.Write(ASCIIEncoding.ASCII.GetBytes("\n", 0, 0));
                            highScores.Seek(9, SeekOrigin.Current);
                            highScores.Write(ASCIIEncoding.ASCII.GetBytes("\n", 0, 0));
                            highScores.Seek(9, SeekOrigin.Current);
                            highScores.Write(ASCIIEncoding.ASCII.GetBytes("\n" + score6Chars), 0, score6Chars.Length + 1);

                            highScores.Seek(8, SeekOrigin.Begin);
                            highScores.Write(ASCIIEncoding.ASCII.GetBytes("\n", 0, 0));
                            highScores.Seek(9, SeekOrigin.Current);
                            highScores.Write(ASCIIEncoding.ASCII.GetBytes("\n", 0, 0));
                            highScores.Seek(9, SeekOrigin.Current);
                            highScores.Write(ASCIIEncoding.ASCII.GetBytes("\n", 0, 0));
                            highScores.Seek(9, SeekOrigin.Current);
                            highScores.Write(ASCIIEncoding.ASCII.GetBytes("\n", 0, 0));
                            highScores.Seek(9, SeekOrigin.Current);
                            highScores.Write(ASCIIEncoding.ASCII.GetBytes("\n", 0, 0));
                            highScores.Seek(9, SeekOrigin.Current);
                            highScores.Write(ASCIIEncoding.ASCII.GetBytes("\n", 0, 0));
                            highScores.Seek(9, SeekOrigin.Current);
                            highScores.Write(ASCIIEncoding.ASCII.GetBytes("\n" + score7Chars), 0, score7Chars.Length + 1);

                            highScores.Seek(8, SeekOrigin.Begin);
                            highScores.Write(ASCIIEncoding.ASCII.GetBytes("\n", 0, 0));
                            highScores.Seek(9, SeekOrigin.Current);
                            highScores.Write(ASCIIEncoding.ASCII.GetBytes("\n", 0, 0));
                            highScores.Seek(9, SeekOrigin.Current);
                            highScores.Write(ASCIIEncoding.ASCII.GetBytes("\n", 0, 0));
                            highScores.Seek(9, SeekOrigin.Current);
                            highScores.Write(ASCIIEncoding.ASCII.GetBytes("\n", 0, 0));
                            highScores.Seek(9, SeekOrigin.Current);
                            highScores.Write(ASCIIEncoding.ASCII.GetBytes("\n", 0, 0));
                            highScores.Seek(9, SeekOrigin.Current);
                            highScores.Write(ASCIIEncoding.ASCII.GetBytes("\n", 0, 0));
                            highScores.Seek(9, SeekOrigin.Current);
                            highScores.Write(ASCIIEncoding.ASCII.GetBytes("\n", 0, 0));
                            highScores.Seek(9, SeekOrigin.Current);
                            highScores.Write(ASCIIEncoding.ASCII.GetBytes("\n" + score8Chars), 0, score8Chars.Length + 1);

                            highScores.Seek(8, SeekOrigin.Begin);
                            highScores.Write(ASCIIEncoding.ASCII.GetBytes("\n", 0, 0));
                            highScores.Seek(9, SeekOrigin.Current);
                            highScores.Write(ASCIIEncoding.ASCII.GetBytes("\n", 0, 0));
                            highScores.Seek(9, SeekOrigin.Current);
                            highScores.Write(ASCIIEncoding.ASCII.GetBytes("\n", 0, 0));
                            highScores.Seek(9, SeekOrigin.Current);
                            highScores.Write(ASCIIEncoding.ASCII.GetBytes("\n", 0, 0));
                            highScores.Seek(9, SeekOrigin.Current);
                            highScores.Write(ASCIIEncoding.ASCII.GetBytes("\n", 0, 0));
                            highScores.Seek(9, SeekOrigin.Current);
                            highScores.Write(ASCIIEncoding.ASCII.GetBytes("\n", 0, 0));
                            highScores.Seek(9, SeekOrigin.Current);
                            highScores.Write(ASCIIEncoding.ASCII.GetBytes("\n", 0, 0));
                            highScores.Seek(9, SeekOrigin.Current);
                            highScores.Write(ASCIIEncoding.ASCII.GetBytes("\n", 0, 0));
                            highScores.Seek(9, SeekOrigin.Current);
                            highScores.Write(ASCIIEncoding.ASCII.GetBytes("\n" + score9Chars), 0, score9Chars.Length + 1);

                        }
                        else if (Convert.ToInt32(playerFinalScore) > score3)
                        {
                            highScores.Seek(9, SeekOrigin.Begin);
                            highScores.Write(ASCIIEncoding.ASCII.GetBytes("\n", 0, 0));

                            highScores.Seek(8, SeekOrigin.Current);
                            highScores.Write(ASCIIEncoding.ASCII.GetBytes("\n" + scoreNameText), 0, scoreNameText.Length + 1);

                            highScores.Seek(8, SeekOrigin.Begin);
                            highScores.Write(ASCIIEncoding.ASCII.GetBytes("\n", 0, 0));
                            highScores.Seek(9, SeekOrigin.Current);
                            highScores.Write(ASCIIEncoding.ASCII.GetBytes("\n", 0, 0));
                            highScores.Seek(9, SeekOrigin.Current);
                            highScores.Write(ASCIIEncoding.ASCII.GetBytes("\n" + score3Chars), 0, score3Chars.Length + 1);

                            highScores.Seek(8, SeekOrigin.Begin);
                            highScores.Write(ASCIIEncoding.ASCII.GetBytes("\n", 0, 0));
                            highScores.Seek(9, SeekOrigin.Current);
                            highScores.Write(ASCIIEncoding.ASCII.GetBytes("\n", 0, 0));
                            highScores.Seek(9, SeekOrigin.Current);
                            highScores.Write(ASCIIEncoding.ASCII.GetBytes("\n", 0, 0));
                            highScores.Seek(9, SeekOrigin.Current);
                            highScores.Write(ASCIIEncoding.ASCII.GetBytes("\n" + score4Chars), 0, score4Chars.Length + 1);

                            highScores.Seek(8, SeekOrigin.Begin);
                            highScores.Write(ASCIIEncoding.ASCII.GetBytes("\n", 0, 0));
                            highScores.Seek(9, SeekOrigin.Current);
                            highScores.Write(ASCIIEncoding.ASCII.GetBytes("\n", 0, 0));
                            highScores.Seek(9, SeekOrigin.Current);
                            highScores.Write(ASCIIEncoding.ASCII.GetBytes("\n", 0, 0));
                            highScores.Seek(9, SeekOrigin.Current);
                            highScores.Write(ASCIIEncoding.ASCII.GetBytes("\n", 0, 0));
                            highScores.Seek(9, SeekOrigin.Current);
                            highScores.Write(ASCIIEncoding.ASCII.GetBytes("\n" + score5Chars), 0, score5Chars.Length + 1);

                            highScores.Seek(8, SeekOrigin.Begin);
                            highScores.Write(ASCIIEncoding.ASCII.GetBytes("\n", 0, 0));
                            highScores.Seek(9, SeekOrigin.Current);
                            highScores.Write(ASCIIEncoding.ASCII.GetBytes("\n", 0, 0));
                            highScores.Seek(9, SeekOrigin.Current);
                            highScores.Write(ASCIIEncoding.ASCII.GetBytes("\n", 0, 0));
                            highScores.Seek(9, SeekOrigin.Current);
                            highScores.Write(ASCIIEncoding.ASCII.GetBytes("\n", 0, 0));
                            highScores.Seek(9, SeekOrigin.Current);
                            highScores.Write(ASCIIEncoding.ASCII.GetBytes("\n", 0, 0));
                            highScores.Seek(9, SeekOrigin.Current);
                            highScores.Write(ASCIIEncoding.ASCII.GetBytes("\n" + score6Chars), 0, score6Chars.Length + 1);

                            highScores.Seek(8, SeekOrigin.Begin);
                            highScores.Write(ASCIIEncoding.ASCII.GetBytes("\n", 0, 0));
                            highScores.Seek(9, SeekOrigin.Current);
                            highScores.Write(ASCIIEncoding.ASCII.GetBytes("\n", 0, 0));
                            highScores.Seek(9, SeekOrigin.Current);
                            highScores.Write(ASCIIEncoding.ASCII.GetBytes("\n", 0, 0));
                            highScores.Seek(9, SeekOrigin.Current);
                            highScores.Write(ASCIIEncoding.ASCII.GetBytes("\n", 0, 0));
                            highScores.Seek(9, SeekOrigin.Current);
                            highScores.Write(ASCIIEncoding.ASCII.GetBytes("\n", 0, 0));
                            highScores.Seek(9, SeekOrigin.Current);
                            highScores.Write(ASCIIEncoding.ASCII.GetBytes("\n", 0, 0));
                            highScores.Seek(9, SeekOrigin.Current);
                            highScores.Write(ASCIIEncoding.ASCII.GetBytes("\n" + score7Chars), 0, score7Chars.Length + 1);

                            highScores.Seek(8, SeekOrigin.Begin);
                            highScores.Write(ASCIIEncoding.ASCII.GetBytes("\n", 0, 0));
                            highScores.Seek(9, SeekOrigin.Current);
                            highScores.Write(ASCIIEncoding.ASCII.GetBytes("\n", 0, 0));
                            highScores.Seek(9, SeekOrigin.Current);
                            highScores.Write(ASCIIEncoding.ASCII.GetBytes("\n", 0, 0));
                            highScores.Seek(9, SeekOrigin.Current);
                            highScores.Write(ASCIIEncoding.ASCII.GetBytes("\n", 0, 0));
                            highScores.Seek(9, SeekOrigin.Current);
                            highScores.Write(ASCIIEncoding.ASCII.GetBytes("\n", 0, 0));
                            highScores.Seek(9, SeekOrigin.Current);
                            highScores.Write(ASCIIEncoding.ASCII.GetBytes("\n", 0, 0));
                            highScores.Seek(9, SeekOrigin.Current);
                            highScores.Write(ASCIIEncoding.ASCII.GetBytes("\n", 0, 0));
                            highScores.Seek(9, SeekOrigin.Current);
                            highScores.Write(ASCIIEncoding.ASCII.GetBytes("\n" + score8Chars), 0, score8Chars.Length + 1);

                            highScores.Seek(8, SeekOrigin.Begin);
                            highScores.Write(ASCIIEncoding.ASCII.GetBytes("\n", 0, 0));
                            highScores.Seek(9, SeekOrigin.Current);
                            highScores.Write(ASCIIEncoding.ASCII.GetBytes("\n", 0, 0));
                            highScores.Seek(9, SeekOrigin.Current);
                            highScores.Write(ASCIIEncoding.ASCII.GetBytes("\n", 0, 0));
                            highScores.Seek(9, SeekOrigin.Current);
                            highScores.Write(ASCIIEncoding.ASCII.GetBytes("\n", 0, 0));
                            highScores.Seek(9, SeekOrigin.Current);
                            highScores.Write(ASCIIEncoding.ASCII.GetBytes("\n", 0, 0));
                            highScores.Seek(9, SeekOrigin.Current);
                            highScores.Write(ASCIIEncoding.ASCII.GetBytes("\n", 0, 0));
                            highScores.Seek(9, SeekOrigin.Current);
                            highScores.Write(ASCIIEncoding.ASCII.GetBytes("\n", 0, 0));
                            highScores.Seek(9, SeekOrigin.Current);
                            highScores.Write(ASCIIEncoding.ASCII.GetBytes("\n", 0, 0));
                            highScores.Seek(9, SeekOrigin.Current);
                            highScores.Write(ASCIIEncoding.ASCII.GetBytes("\n" + score9Chars), 0, score9Chars.Length + 1);

                        }
                        else if (Convert.ToInt32(playerFinalScore) > score4)
                        {
                            highScores.Seek(10, SeekOrigin.Begin);
                            highScores.Write(ASCIIEncoding.ASCII.GetBytes("\n", 0, 0));

                            highScores.Seek(8, SeekOrigin.Current);
                            highScores.Write(ASCIIEncoding.ASCII.GetBytes("\n", 0, 0));

                            highScores.Seek(8, SeekOrigin.Current);
                            highScores.Write(ASCIIEncoding.ASCII.GetBytes("\n", 0, 0));

                            highScores.Seek(8, SeekOrigin.Current);
                            highScores.Write(ASCIIEncoding.ASCII.GetBytes("\n", 0, 0));

                            highScores.Seek(-7, SeekOrigin.Current);
                            highScores.Write(ASCIIEncoding.ASCII.GetBytes(scoreNameText), 0, scoreNameText.Length);

                            highScores.Seek(8, SeekOrigin.Begin);
                            highScores.Write(ASCIIEncoding.ASCII.GetBytes("\n", 0, 0));
                            highScores.Seek(9, SeekOrigin.Current);
                            highScores.Write(ASCIIEncoding.ASCII.GetBytes("\n", 0, 0));
                            highScores.Seek(9, SeekOrigin.Current);
                            highScores.Write(ASCIIEncoding.ASCII.GetBytes("\n", 0, 0));
                            highScores.Seek(9, SeekOrigin.Current);
                            highScores.Write(ASCIIEncoding.ASCII.GetBytes("\n" + score4Chars), 0, score4Chars.Length + 1);

                            highScores.Seek(8, SeekOrigin.Begin);
                            highScores.Write(ASCIIEncoding.ASCII.GetBytes("\n", 0, 0));
                            highScores.Seek(9, SeekOrigin.Current);
                            highScores.Write(ASCIIEncoding.ASCII.GetBytes("\n", 0, 0));
                            highScores.Seek(9, SeekOrigin.Current);
                            highScores.Write(ASCIIEncoding.ASCII.GetBytes("\n", 0, 0));
                            highScores.Seek(9, SeekOrigin.Current);
                            highScores.Write(ASCIIEncoding.ASCII.GetBytes("\n", 0, 0));
                            highScores.Seek(9, SeekOrigin.Current);
                            highScores.Write(ASCIIEncoding.ASCII.GetBytes("\n" + score5Chars), 0, score5Chars.Length + 1);

                            highScores.Seek(8, SeekOrigin.Begin);
                            highScores.Write(ASCIIEncoding.ASCII.GetBytes("\n", 0, 0));
                            highScores.Seek(9, SeekOrigin.Current);
                            highScores.Write(ASCIIEncoding.ASCII.GetBytes("\n", 0, 0));
                            highScores.Seek(9, SeekOrigin.Current);
                            highScores.Write(ASCIIEncoding.ASCII.GetBytes("\n", 0, 0));
                            highScores.Seek(9, SeekOrigin.Current);
                            highScores.Write(ASCIIEncoding.ASCII.GetBytes("\n", 0, 0));
                            highScores.Seek(9, SeekOrigin.Current);
                            highScores.Write(ASCIIEncoding.ASCII.GetBytes("\n", 0, 0));
                            highScores.Seek(9, SeekOrigin.Current);
                            highScores.Write(ASCIIEncoding.ASCII.GetBytes("\n" + score6Chars), 0, score6Chars.Length + 1);

                            highScores.Seek(8, SeekOrigin.Begin);
                            highScores.Write(ASCIIEncoding.ASCII.GetBytes("\n", 0, 0));
                            highScores.Seek(9, SeekOrigin.Current);
                            highScores.Write(ASCIIEncoding.ASCII.GetBytes("\n", 0, 0));
                            highScores.Seek(9, SeekOrigin.Current);
                            highScores.Write(ASCIIEncoding.ASCII.GetBytes("\n", 0, 0));
                            highScores.Seek(9, SeekOrigin.Current);
                            highScores.Write(ASCIIEncoding.ASCII.GetBytes("\n", 0, 0));
                            highScores.Seek(9, SeekOrigin.Current);
                            highScores.Write(ASCIIEncoding.ASCII.GetBytes("\n", 0, 0));
                            highScores.Seek(9, SeekOrigin.Current);
                            highScores.Write(ASCIIEncoding.ASCII.GetBytes("\n", 0, 0));
                            highScores.Seek(9, SeekOrigin.Current);
                            highScores.Write(ASCIIEncoding.ASCII.GetBytes("\n" + score7Chars), 0, score7Chars.Length + 1);

                            highScores.Seek(8, SeekOrigin.Begin);
                            highScores.Write(ASCIIEncoding.ASCII.GetBytes("\n", 0, 0));
                            highScores.Seek(9, SeekOrigin.Current);
                            highScores.Write(ASCIIEncoding.ASCII.GetBytes("\n", 0, 0));
                            highScores.Seek(9, SeekOrigin.Current);
                            highScores.Write(ASCIIEncoding.ASCII.GetBytes("\n", 0, 0));
                            highScores.Seek(9, SeekOrigin.Current);
                            highScores.Write(ASCIIEncoding.ASCII.GetBytes("\n", 0, 0));
                            highScores.Seek(9, SeekOrigin.Current);
                            highScores.Write(ASCIIEncoding.ASCII.GetBytes("\n", 0, 0));
                            highScores.Seek(9, SeekOrigin.Current);
                            highScores.Write(ASCIIEncoding.ASCII.GetBytes("\n", 0, 0));
                            highScores.Seek(9, SeekOrigin.Current);
                            highScores.Write(ASCIIEncoding.ASCII.GetBytes("\n", 0, 0));
                            highScores.Seek(9, SeekOrigin.Current);
                            highScores.Write(ASCIIEncoding.ASCII.GetBytes("\n" + score8Chars), 0, score8Chars.Length + 1);

                            highScores.Seek(8, SeekOrigin.Begin);
                            highScores.Write(ASCIIEncoding.ASCII.GetBytes("\n", 0, 0));
                            highScores.Seek(9, SeekOrigin.Current);
                            highScores.Write(ASCIIEncoding.ASCII.GetBytes("\n", 0, 0));
                            highScores.Seek(9, SeekOrigin.Current);
                            highScores.Write(ASCIIEncoding.ASCII.GetBytes("\n", 0, 0));
                            highScores.Seek(9, SeekOrigin.Current);
                            highScores.Write(ASCIIEncoding.ASCII.GetBytes("\n", 0, 0));
                            highScores.Seek(9, SeekOrigin.Current);
                            highScores.Write(ASCIIEncoding.ASCII.GetBytes("\n", 0, 0));
                            highScores.Seek(9, SeekOrigin.Current);
                            highScores.Write(ASCIIEncoding.ASCII.GetBytes("\n", 0, 0));
                            highScores.Seek(9, SeekOrigin.Current);
                            highScores.Write(ASCIIEncoding.ASCII.GetBytes("\n", 0, 0));
                            highScores.Seek(9, SeekOrigin.Current);
                            highScores.Write(ASCIIEncoding.ASCII.GetBytes("\n", 0, 0));
                            highScores.Seek(9, SeekOrigin.Current);
                            highScores.Write(ASCIIEncoding.ASCII.GetBytes("\n" + score9Chars), 0, score9Chars.Length + 1);

                        }
                        else if (Convert.ToInt32(playerFinalScore) > score5)
                        {
                            highScores.Seek(8, SeekOrigin.Begin);
                            highScores.Write(ASCIIEncoding.ASCII.GetBytes("\n", 0, 0));

                            highScores.Seek(0, SeekOrigin.Current);
                            highScores.Write(ASCIIEncoding.ASCII.GetBytes("\n", 0, 0));

                            highScores.Seek(9, SeekOrigin.Current);
                            highScores.Write(ASCIIEncoding.ASCII.GetBytes("\n", 0, 0));

                            highScores.Seek(9, SeekOrigin.Current);
                            highScores.Write(ASCIIEncoding.ASCII.GetBytes("\n", 0, 0));

                            highScores.Seek(9, SeekOrigin.Current);
                            highScores.Write(ASCIIEncoding.ASCII.GetBytes("\n", 0, 0));

                            highScores.Seek(9, SeekOrigin.Current);
                            highScores.Write(ASCIIEncoding.ASCII.GetBytes("\n", 0, 0));

                            highScores.Seek(-8, SeekOrigin.Current);
                            highScores.Write(ASCIIEncoding.ASCII.GetBytes(scoreNameText), 0, scoreNameText.Length);

                            highScores.Seek(8, SeekOrigin.Begin);
                            highScores.Write(ASCIIEncoding.ASCII.GetBytes("\n", 0, 0));
                            highScores.Seek(9, SeekOrigin.Current);
                            highScores.Write(ASCIIEncoding.ASCII.GetBytes("\n", 0, 0));
                            highScores.Seek(9, SeekOrigin.Current);
                            highScores.Write(ASCIIEncoding.ASCII.GetBytes("\n", 0, 0));
                            highScores.Seek(9, SeekOrigin.Current);
                            highScores.Write(ASCIIEncoding.ASCII.GetBytes("\n", 0, 0));
                            highScores.Seek(9, SeekOrigin.Current);
                            highScores.Write(ASCIIEncoding.ASCII.GetBytes("\n" + score5Chars), 0, score5Chars.Length + 1);

                            highScores.Seek(8, SeekOrigin.Begin);
                            highScores.Write(ASCIIEncoding.ASCII.GetBytes("\n", 0, 0));
                            highScores.Seek(9, SeekOrigin.Current);
                            highScores.Write(ASCIIEncoding.ASCII.GetBytes("\n", 0, 0));
                            highScores.Seek(9, SeekOrigin.Current);
                            highScores.Write(ASCIIEncoding.ASCII.GetBytes("\n", 0, 0));
                            highScores.Seek(9, SeekOrigin.Current);
                            highScores.Write(ASCIIEncoding.ASCII.GetBytes("\n", 0, 0));
                            highScores.Seek(9, SeekOrigin.Current);
                            highScores.Write(ASCIIEncoding.ASCII.GetBytes("\n", 0, 0));
                            highScores.Seek(9, SeekOrigin.Current);
                            highScores.Write(ASCIIEncoding.ASCII.GetBytes("\n" + score6Chars), 0, score6Chars.Length + 1);

                            highScores.Seek(8, SeekOrigin.Begin);
                            highScores.Write(ASCIIEncoding.ASCII.GetBytes("\n", 0, 0));
                            highScores.Seek(9, SeekOrigin.Current);
                            highScores.Write(ASCIIEncoding.ASCII.GetBytes("\n", 0, 0));
                            highScores.Seek(9, SeekOrigin.Current);
                            highScores.Write(ASCIIEncoding.ASCII.GetBytes("\n", 0, 0));
                            highScores.Seek(9, SeekOrigin.Current);
                            highScores.Write(ASCIIEncoding.ASCII.GetBytes("\n", 0, 0));
                            highScores.Seek(9, SeekOrigin.Current);
                            highScores.Write(ASCIIEncoding.ASCII.GetBytes("\n", 0, 0));
                            highScores.Seek(9, SeekOrigin.Current);
                            highScores.Write(ASCIIEncoding.ASCII.GetBytes("\n", 0, 0));
                            highScores.Seek(9, SeekOrigin.Current);
                            highScores.Write(ASCIIEncoding.ASCII.GetBytes("\n" + score7Chars), 0, score7Chars.Length + 1);

                            highScores.Seek(8, SeekOrigin.Begin);
                            highScores.Write(ASCIIEncoding.ASCII.GetBytes("\n", 0, 0));
                            highScores.Seek(9, SeekOrigin.Current);
                            highScores.Write(ASCIIEncoding.ASCII.GetBytes("\n", 0, 0));
                            highScores.Seek(9, SeekOrigin.Current);
                            highScores.Write(ASCIIEncoding.ASCII.GetBytes("\n", 0, 0));
                            highScores.Seek(9, SeekOrigin.Current);
                            highScores.Write(ASCIIEncoding.ASCII.GetBytes("\n", 0, 0));
                            highScores.Seek(9, SeekOrigin.Current);
                            highScores.Write(ASCIIEncoding.ASCII.GetBytes("\n", 0, 0));
                            highScores.Seek(9, SeekOrigin.Current);
                            highScores.Write(ASCIIEncoding.ASCII.GetBytes("\n", 0, 0));
                            highScores.Seek(9, SeekOrigin.Current);
                            highScores.Write(ASCIIEncoding.ASCII.GetBytes("\n", 0, 0));
                            highScores.Seek(9, SeekOrigin.Current);
                            highScores.Write(ASCIIEncoding.ASCII.GetBytes("\n" + score8Chars), 0, score8Chars.Length + 1);

                            highScores.Seek(8, SeekOrigin.Begin);
                            highScores.Write(ASCIIEncoding.ASCII.GetBytes("\n", 0, 0));
                            highScores.Seek(9, SeekOrigin.Current);
                            highScores.Write(ASCIIEncoding.ASCII.GetBytes("\n", 0, 0));
                            highScores.Seek(9, SeekOrigin.Current);
                            highScores.Write(ASCIIEncoding.ASCII.GetBytes("\n", 0, 0));
                            highScores.Seek(9, SeekOrigin.Current);
                            highScores.Write(ASCIIEncoding.ASCII.GetBytes("\n", 0, 0));
                            highScores.Seek(9, SeekOrigin.Current);
                            highScores.Write(ASCIIEncoding.ASCII.GetBytes("\n", 0, 0));
                            highScores.Seek(9, SeekOrigin.Current);
                            highScores.Write(ASCIIEncoding.ASCII.GetBytes("\n", 0, 0));
                            highScores.Seek(9, SeekOrigin.Current);
                            highScores.Write(ASCIIEncoding.ASCII.GetBytes("\n", 0, 0));
                            highScores.Seek(9, SeekOrigin.Current);
                            highScores.Write(ASCIIEncoding.ASCII.GetBytes("\n", 0, 0));
                            highScores.Seek(9, SeekOrigin.Current);
                            highScores.Write(ASCIIEncoding.ASCII.GetBytes("\n" + score9Chars), 0, score9Chars.Length + 1);


                        }
                        else if (Convert.ToInt32(playerFinalScore) > score6)
                        {
                            highScores.Seek(8, SeekOrigin.Begin);
                            highScores.Write(ASCIIEncoding.ASCII.GetBytes("\n", 0, 0));

                            highScores.Seek(0, SeekOrigin.Current);
                            highScores.Write(ASCIIEncoding.ASCII.GetBytes("\n", 0, 0));

                            highScores.Seek(9, SeekOrigin.Current);
                            highScores.Write(ASCIIEncoding.ASCII.GetBytes("\n", 0, 0));

                            highScores.Seek(9, SeekOrigin.Current);
                            highScores.Write(ASCIIEncoding.ASCII.GetBytes("\n", 0, 0));

                            highScores.Seek(9, SeekOrigin.Current);
                            highScores.Write(ASCIIEncoding.ASCII.GetBytes("\n", 0, 0));

                            highScores.Seek(9, SeekOrigin.Current);
                            highScores.Write(ASCIIEncoding.ASCII.GetBytes("\n", 0, 0));

                            highScores.Seek(9, SeekOrigin.Current);
                            highScores.Write(ASCIIEncoding.ASCII.GetBytes("\n", 0, 0));

                            highScores.Seek(-8, SeekOrigin.Current);
                            highScores.Write(ASCIIEncoding.ASCII.GetBytes(scoreNameText), 0, scoreNameText.Length);

                            highScores.Seek(8, SeekOrigin.Begin);
                            highScores.Write(ASCIIEncoding.ASCII.GetBytes("\n", 0, 0));
                            highScores.Seek(9, SeekOrigin.Current);
                            highScores.Write(ASCIIEncoding.ASCII.GetBytes("\n", 0, 0));
                            highScores.Seek(9, SeekOrigin.Current);
                            highScores.Write(ASCIIEncoding.ASCII.GetBytes("\n", 0, 0));
                            highScores.Seek(9, SeekOrigin.Current);
                            highScores.Write(ASCIIEncoding.ASCII.GetBytes("\n", 0, 0));
                            highScores.Seek(9, SeekOrigin.Current);
                            highScores.Write(ASCIIEncoding.ASCII.GetBytes("\n", 0, 0));
                            highScores.Seek(9, SeekOrigin.Current);
                            highScores.Write(ASCIIEncoding.ASCII.GetBytes("\n" + score6Chars), 0, score6Chars.Length + 1);

                            highScores.Seek(8, SeekOrigin.Begin);
                            highScores.Write(ASCIIEncoding.ASCII.GetBytes("\n", 0, 0));
                            highScores.Seek(9, SeekOrigin.Current);
                            highScores.Write(ASCIIEncoding.ASCII.GetBytes("\n", 0, 0));
                            highScores.Seek(9, SeekOrigin.Current);
                            highScores.Write(ASCIIEncoding.ASCII.GetBytes("\n", 0, 0));
                            highScores.Seek(9, SeekOrigin.Current);
                            highScores.Write(ASCIIEncoding.ASCII.GetBytes("\n", 0, 0));
                            highScores.Seek(9, SeekOrigin.Current);
                            highScores.Write(ASCIIEncoding.ASCII.GetBytes("\n", 0, 0));
                            highScores.Seek(9, SeekOrigin.Current);
                            highScores.Write(ASCIIEncoding.ASCII.GetBytes("\n", 0, 0));
                            highScores.Seek(9, SeekOrigin.Current);
                            highScores.Write(ASCIIEncoding.ASCII.GetBytes("\n" + score7Chars), 0, score7Chars.Length + 1);

                            highScores.Seek(8, SeekOrigin.Begin);
                            highScores.Write(ASCIIEncoding.ASCII.GetBytes("\n", 0, 0));
                            highScores.Seek(9, SeekOrigin.Current);
                            highScores.Write(ASCIIEncoding.ASCII.GetBytes("\n", 0, 0));
                            highScores.Seek(9, SeekOrigin.Current);
                            highScores.Write(ASCIIEncoding.ASCII.GetBytes("\n", 0, 0));
                            highScores.Seek(9, SeekOrigin.Current);
                            highScores.Write(ASCIIEncoding.ASCII.GetBytes("\n", 0, 0));
                            highScores.Seek(9, SeekOrigin.Current);
                            highScores.Write(ASCIIEncoding.ASCII.GetBytes("\n", 0, 0));
                            highScores.Seek(9, SeekOrigin.Current);
                            highScores.Write(ASCIIEncoding.ASCII.GetBytes("\n", 0, 0));
                            highScores.Seek(9, SeekOrigin.Current);
                            highScores.Write(ASCIIEncoding.ASCII.GetBytes("\n", 0, 0));
                            highScores.Seek(9, SeekOrigin.Current);
                            highScores.Write(ASCIIEncoding.ASCII.GetBytes("\n" + score8Chars), 0, score8Chars.Length + 1);

                            highScores.Seek(8, SeekOrigin.Begin);
                            highScores.Write(ASCIIEncoding.ASCII.GetBytes("\n", 0, 0));
                            highScores.Seek(9, SeekOrigin.Current);
                            highScores.Write(ASCIIEncoding.ASCII.GetBytes("\n", 0, 0));
                            highScores.Seek(9, SeekOrigin.Current);
                            highScores.Write(ASCIIEncoding.ASCII.GetBytes("\n", 0, 0));
                            highScores.Seek(9, SeekOrigin.Current);
                            highScores.Write(ASCIIEncoding.ASCII.GetBytes("\n", 0, 0));
                            highScores.Seek(9, SeekOrigin.Current);
                            highScores.Write(ASCIIEncoding.ASCII.GetBytes("\n", 0, 0));
                            highScores.Seek(9, SeekOrigin.Current);
                            highScores.Write(ASCIIEncoding.ASCII.GetBytes("\n", 0, 0));
                            highScores.Seek(9, SeekOrigin.Current);
                            highScores.Write(ASCIIEncoding.ASCII.GetBytes("\n", 0, 0));
                            highScores.Seek(9, SeekOrigin.Current);
                            highScores.Write(ASCIIEncoding.ASCII.GetBytes("\n", 0, 0));
                            highScores.Seek(9, SeekOrigin.Current);
                            highScores.Write(ASCIIEncoding.ASCII.GetBytes("\n" + score9Chars), 0, score9Chars.Length + 1);

                        }
                        else if (Convert.ToInt32(playerFinalScore) > score7)
                        {
                            highScores.Seek(8, SeekOrigin.Begin);
                            highScores.Write(ASCIIEncoding.ASCII.GetBytes("\n", 0, 0));

                            highScores.Seek(0, SeekOrigin.Current);
                            highScores.Write(ASCIIEncoding.ASCII.GetBytes("\n", 0, 0));

                            highScores.Seek(9, SeekOrigin.Current);
                            highScores.Write(ASCIIEncoding.ASCII.GetBytes("\n", 0, 0));

                            highScores.Seek(9, SeekOrigin.Current);
                            highScores.Write(ASCIIEncoding.ASCII.GetBytes("\n", 0, 0));

                            highScores.Seek(9, SeekOrigin.Current);
                            highScores.Write(ASCIIEncoding.ASCII.GetBytes("\n", 0, 0));

                            highScores.Seek(9, SeekOrigin.Current);
                            highScores.Write(ASCIIEncoding.ASCII.GetBytes("\n", 0, 0));

                            highScores.Seek(9, SeekOrigin.Current);
                            highScores.Write(ASCIIEncoding.ASCII.GetBytes("\n", 0, 0));

                            highScores.Seek(9, SeekOrigin.Current);
                            highScores.Write(ASCIIEncoding.ASCII.GetBytes("\n", 0, 0));

                            highScores.Seek(-8, SeekOrigin.Current);
                            highScores.Write(ASCIIEncoding.ASCII.GetBytes(scoreNameText), 0, scoreNameText.Length);

                            highScores.Seek(8, SeekOrigin.Begin);
                            highScores.Write(ASCIIEncoding.ASCII.GetBytes("\n", 0, 0));
                            highScores.Seek(9, SeekOrigin.Current);
                            highScores.Write(ASCIIEncoding.ASCII.GetBytes("\n", 0, 0));
                            highScores.Seek(9, SeekOrigin.Current);
                            highScores.Write(ASCIIEncoding.ASCII.GetBytes("\n", 0, 0));
                            highScores.Seek(9, SeekOrigin.Current);
                            highScores.Write(ASCIIEncoding.ASCII.GetBytes("\n", 0, 0));
                            highScores.Seek(9, SeekOrigin.Current);
                            highScores.Write(ASCIIEncoding.ASCII.GetBytes("\n", 0, 0));
                            highScores.Seek(9, SeekOrigin.Current);
                            highScores.Write(ASCIIEncoding.ASCII.GetBytes("\n", 0, 0));
                            highScores.Seek(9, SeekOrigin.Current);
                            highScores.Write(ASCIIEncoding.ASCII.GetBytes("\n" + score7Chars), 0, score7Chars.Length + 1);

                            highScores.Seek(8, SeekOrigin.Begin);
                            highScores.Write(ASCIIEncoding.ASCII.GetBytes("\n", 0, 0));
                            highScores.Seek(9, SeekOrigin.Current);
                            highScores.Write(ASCIIEncoding.ASCII.GetBytes("\n", 0, 0));
                            highScores.Seek(9, SeekOrigin.Current);
                            highScores.Write(ASCIIEncoding.ASCII.GetBytes("\n", 0, 0));
                            highScores.Seek(9, SeekOrigin.Current);
                            highScores.Write(ASCIIEncoding.ASCII.GetBytes("\n", 0, 0));
                            highScores.Seek(9, SeekOrigin.Current);
                            highScores.Write(ASCIIEncoding.ASCII.GetBytes("\n", 0, 0));
                            highScores.Seek(9, SeekOrigin.Current);
                            highScores.Write(ASCIIEncoding.ASCII.GetBytes("\n", 0, 0));
                            highScores.Seek(9, SeekOrigin.Current);
                            highScores.Write(ASCIIEncoding.ASCII.GetBytes("\n", 0, 0));
                            highScores.Seek(9, SeekOrigin.Current);
                            highScores.Write(ASCIIEncoding.ASCII.GetBytes("\n" + score8Chars), 0, score8Chars.Length + 1);

                            highScores.Seek(8, SeekOrigin.Begin);
                            highScores.Write(ASCIIEncoding.ASCII.GetBytes("\n", 0, 0));
                            highScores.Seek(9, SeekOrigin.Current);
                            highScores.Write(ASCIIEncoding.ASCII.GetBytes("\n", 0, 0));
                            highScores.Seek(9, SeekOrigin.Current);
                            highScores.Write(ASCIIEncoding.ASCII.GetBytes("\n", 0, 0));
                            highScores.Seek(9, SeekOrigin.Current);
                            highScores.Write(ASCIIEncoding.ASCII.GetBytes("\n", 0, 0));
                            highScores.Seek(9, SeekOrigin.Current);
                            highScores.Write(ASCIIEncoding.ASCII.GetBytes("\n", 0, 0));
                            highScores.Seek(9, SeekOrigin.Current);
                            highScores.Write(ASCIIEncoding.ASCII.GetBytes("\n", 0, 0));
                            highScores.Seek(9, SeekOrigin.Current);
                            highScores.Write(ASCIIEncoding.ASCII.GetBytes("\n", 0, 0));
                            highScores.Seek(9, SeekOrigin.Current);
                            highScores.Write(ASCIIEncoding.ASCII.GetBytes("\n", 0, 0));
                            highScores.Seek(9, SeekOrigin.Current);
                            highScores.Write(ASCIIEncoding.ASCII.GetBytes("\n" + score9Chars), 0, score9Chars.Length + 1);

                        }
                        else if (Convert.ToInt32(playerFinalScore) > score8)
                        {
                            highScores.Seek(8, SeekOrigin.Begin);
                            highScores.Write(ASCIIEncoding.ASCII.GetBytes("\n", 0, 0));

                            highScores.Seek(0, SeekOrigin.Current);
                            highScores.Write(ASCIIEncoding.ASCII.GetBytes("\n", 0, 0));

                            highScores.Seek(9, SeekOrigin.Current);
                            highScores.Write(ASCIIEncoding.ASCII.GetBytes("\n", 0, 0));

                            highScores.Seek(9, SeekOrigin.Current);
                            highScores.Write(ASCIIEncoding.ASCII.GetBytes("\n", 0, 0));

                            highScores.Seek(9, SeekOrigin.Current);
                            highScores.Write(ASCIIEncoding.ASCII.GetBytes("\n", 0, 0));

                            highScores.Seek(9, SeekOrigin.Current);
                            highScores.Write(ASCIIEncoding.ASCII.GetBytes("\n", 0, 0));

                            highScores.Seek(9, SeekOrigin.Current);
                            highScores.Write(ASCIIEncoding.ASCII.GetBytes("\n", 0, 0));

                            highScores.Seek(9, SeekOrigin.Current);
                            highScores.Write(ASCIIEncoding.ASCII.GetBytes("\n", 0, 0));

                            highScores.Seek(9, SeekOrigin.Current);
                            highScores.Write(ASCIIEncoding.ASCII.GetBytes("\n", 0, 0));

                            highScores.Seek(-8, SeekOrigin.Current);
                            highScores.Write(ASCIIEncoding.ASCII.GetBytes(scoreNameText), 0, scoreNameText.Length);

                            highScores.Seek(8, SeekOrigin.Begin);
                            highScores.Write(ASCIIEncoding.ASCII.GetBytes("\n", 0, 0));
                            highScores.Seek(9, SeekOrigin.Current);
                            highScores.Write(ASCIIEncoding.ASCII.GetBytes("\n", 0, 0));
                            highScores.Seek(9, SeekOrigin.Current);
                            highScores.Write(ASCIIEncoding.ASCII.GetBytes("\n", 0, 0));
                            highScores.Seek(9, SeekOrigin.Current);
                            highScores.Write(ASCIIEncoding.ASCII.GetBytes("\n", 0, 0));
                            highScores.Seek(9, SeekOrigin.Current);
                            highScores.Write(ASCIIEncoding.ASCII.GetBytes("\n", 0, 0));
                            highScores.Seek(9, SeekOrigin.Current);
                            highScores.Write(ASCIIEncoding.ASCII.GetBytes("\n", 0, 0));
                            highScores.Seek(9, SeekOrigin.Current);
                            highScores.Write(ASCIIEncoding.ASCII.GetBytes("\n", 0, 0));
                            highScores.Seek(9, SeekOrigin.Current);
                            highScores.Write(ASCIIEncoding.ASCII.GetBytes("\n" + score8Chars), 0, score8Chars.Length + 1);

                            highScores.Seek(8, SeekOrigin.Begin);
                            highScores.Write(ASCIIEncoding.ASCII.GetBytes("\n", 0, 0));
                            highScores.Seek(9, SeekOrigin.Current);
                            highScores.Write(ASCIIEncoding.ASCII.GetBytes("\n", 0, 0));
                            highScores.Seek(9, SeekOrigin.Current);
                            highScores.Write(ASCIIEncoding.ASCII.GetBytes("\n", 0, 0));
                            highScores.Seek(9, SeekOrigin.Current);
                            highScores.Write(ASCIIEncoding.ASCII.GetBytes("\n", 0, 0));
                            highScores.Seek(9, SeekOrigin.Current);
                            highScores.Write(ASCIIEncoding.ASCII.GetBytes("\n", 0, 0));
                            highScores.Seek(9, SeekOrigin.Current);
                            highScores.Write(ASCIIEncoding.ASCII.GetBytes("\n", 0, 0));
                            highScores.Seek(9, SeekOrigin.Current);
                            highScores.Write(ASCIIEncoding.ASCII.GetBytes("\n", 0, 0));
                            highScores.Seek(9, SeekOrigin.Current);
                            highScores.Write(ASCIIEncoding.ASCII.GetBytes("\n", 0, 0));
                            highScores.Seek(9, SeekOrigin.Current);
                            highScores.Write(ASCIIEncoding.ASCII.GetBytes("\n" + score9Chars), 0, score9Chars.Length + 1);

                        }
                        else if (Convert.ToInt32(playerFinalScore) > score9)
                        {
                            highScores.Seek(8, SeekOrigin.Begin);
                            highScores.Write(ASCIIEncoding.ASCII.GetBytes("\n", 0, 0));

                            highScores.Seek(0, SeekOrigin.Current);
                            highScores.Write(ASCIIEncoding.ASCII.GetBytes("\n", 0, 0));

                            highScores.Seek(9, SeekOrigin.Current);
                            highScores.Write(ASCIIEncoding.ASCII.GetBytes("\n", 0, 0));

                            highScores.Seek(9, SeekOrigin.Current);
                            highScores.Write(ASCIIEncoding.ASCII.GetBytes("\n", 0, 0));

                            highScores.Seek(9, SeekOrigin.Current);
                            highScores.Write(ASCIIEncoding.ASCII.GetBytes("\n", 0, 0));

                            highScores.Seek(9, SeekOrigin.Current);
                            highScores.Write(ASCIIEncoding.ASCII.GetBytes("\n", 0, 0));

                            highScores.Seek(9, SeekOrigin.Current);
                            highScores.Write(ASCIIEncoding.ASCII.GetBytes("\n", 0, 0));

                            highScores.Seek(9, SeekOrigin.Current);
                            highScores.Write(ASCIIEncoding.ASCII.GetBytes("\n", 0, 0));

                            highScores.Seek(9, SeekOrigin.Current);
                            highScores.Write(ASCIIEncoding.ASCII.GetBytes("\n", 0, 0));

                            highScores.Seek(9, SeekOrigin.Current);
                            highScores.Write(ASCIIEncoding.ASCII.GetBytes("\n", 0, 0));

                            highScores.Seek(-8, SeekOrigin.Current);
                            highScores.Write(ASCIIEncoding.ASCII.GetBytes(scoreNameText), 0, scoreNameText.Length);

                            highScores.Seek(8, SeekOrigin.Begin);
                            highScores.Write(ASCIIEncoding.ASCII.GetBytes("\n", 0, 0));
                            highScores.Seek(9, SeekOrigin.Current);
                            highScores.Write(ASCIIEncoding.ASCII.GetBytes("\n", 0, 0));
                            highScores.Seek(9, SeekOrigin.Current);
                            highScores.Write(ASCIIEncoding.ASCII.GetBytes("\n", 0, 0));
                            highScores.Seek(9, SeekOrigin.Current);
                            highScores.Write(ASCIIEncoding.ASCII.GetBytes("\n", 0, 0));
                            highScores.Seek(9, SeekOrigin.Current);
                            highScores.Write(ASCIIEncoding.ASCII.GetBytes("\n", 0, 0));
                            highScores.Seek(9, SeekOrigin.Current);
                            highScores.Write(ASCIIEncoding.ASCII.GetBytes("\n", 0, 0));
                            highScores.Seek(9, SeekOrigin.Current);
                            highScores.Write(ASCIIEncoding.ASCII.GetBytes("\n", 0, 0));
                            highScores.Seek(9, SeekOrigin.Current);
                            highScores.Write(ASCIIEncoding.ASCII.GetBytes("\n", 0, 0));
                            highScores.Seek(9, SeekOrigin.Current);
                            highScores.Write(ASCIIEncoding.ASCII.GetBytes("\n" + score9Chars), 0, score9Chars.Length + 1);

                        }
                        else if (Convert.ToInt32(playerFinalScore) > score10)
                        {
                            highScores.Seek(9, SeekOrigin.Begin);
                            highScores.Write(ASCIIEncoding.ASCII.GetBytes("\n", 0, 0));

                            highScores.Seek(9, SeekOrigin.Current);
                            highScores.Write(ASCIIEncoding.ASCII.GetBytes("\n", 0, 0));

                            highScores.Seek(9, SeekOrigin.Current);
                            highScores.Write(ASCIIEncoding.ASCII.GetBytes("\n", 0, 0));

                            highScores.Seek(9, SeekOrigin.Current);
                            highScores.Write(ASCIIEncoding.ASCII.GetBytes("\n", 0, 0));

                            highScores.Seek(9, SeekOrigin.Current);
                            highScores.Write(ASCIIEncoding.ASCII.GetBytes("\n", 0, 0));

                            highScores.Seek(9, SeekOrigin.Current);
                            highScores.Write(ASCIIEncoding.ASCII.GetBytes("\n", 0, 0));

                            highScores.Seek(9, SeekOrigin.Current);
                            highScores.Write(ASCIIEncoding.ASCII.GetBytes("\n", 0, 0));

                            highScores.Seek(9, SeekOrigin.Current);
                            highScores.Write(ASCIIEncoding.ASCII.GetBytes("\n", 0, 0));

                            highScores.Seek(9, SeekOrigin.Current);
                            highScores.Write(ASCIIEncoding.ASCII.GetBytes(scoreNameText), 0, scoreNameText.Length);

                        }

                        highScores.Seek(0, SeekOrigin.Begin);
                        highScores.Read(highScoresData, 0, (int)highScoresData.Length);

                        highScoreState.HighScoresData = highScoresData;

                        Console.WriteLine(ASCIIEncoding.ASCII.GetString(highScoresData));

                        scoreScreen = false;
                    }
                    
                }
                
            }
            else
                scoreScreen = true;

        }

        private void AsteroidSpawner(float deltaTime)
        {
            if(meteorShower)
            {
                if (actualSmallAsteroids < totalSmallAsteroids && actualMediumAsteroids < totalMediumAsteroids && actualBigAsteroids < totalBigAsteroids)
                {
                    asteroidsTimer += deltaTime;

                    if (asteroidsTimer >= asteroidNextWave - 1)
                    {
                        asteroidWaveDuration += 30;
                        asteroidNextWave += 30;

                        wave++;
                        waveText = wave.ToString();

                        if (actualSmallAsteroids < totalSmallAsteroids)
                            actualSmallAsteroids += 2;

                        if (actualMediumAsteroids < totalMediumAsteroids)
                            actualMediumAsteroids += 2;

                        if (actualBigAsteroids < totalBigAsteroids)
                            actualBigAsteroids += 1;                     

                        asteroidsTimer = 0;

                        meteorShower = false;
                    }
                }
                else
                {
                    if (language == "es")
                        waveText = "OLEADA SIN FIN";

                    else if (language == "en")
                        waveText = "ENDLESS WAVE";

                }
            }
            
        }

        public void ProcessInput()
        {
            renderWindow.DispatchEvents();
        }
        
        protected override void Update(float deltaTime)
        {                     
            uint buttonsTextSize = 20;

            if (language == "es")
            {
                gamePausedText.DisplayedString = "JUEGO PAUSADO";
                continueButton.SetText("Continuar", buttonsTextSize);
                mainMenuButton.SetText("Menu", buttonsTextSize);
                looseText.DisplayedString = "TU NAVE HA SIDO DESTRUIDA!";
                finalScoreText.DisplayedString = "PUNTUACION FINAL: ";
                restartButton.SetText("Reiniciar", buttonsTextSize);

                hud.Language = language;                              
            }
            else if(language == "en")
            {
                gamePausedText.DisplayedString = "GAME PAUSED";
                continueButton.SetText("Continue", buttonsTextSize);
                mainMenuButton.SetText("Main menu", buttonsTextSize);
                looseText.DisplayedString = "YOUR SPACESHIP EXPLODED!";
                finalScoreText.DisplayedString = "FINAL SCORE: ";
                restartButton.SetText("Restart", buttonsTextSize);

                hud.Language = language;                                
            }

            if(!pauseGameplay)
            {
                if(!meteorShower)
                {
                    background.Translate(new Vector2f(0, 1) * 100 * deltaTime);

                    if (background.Position.Y > 1078)
                        background.Position = new Vector2f(0f, 0f);

                    travelTimer += deltaTime;

                    if (travelTimer >= travelTime)
                    {
                        travelTimer = 0;
                        travelTime += 20;

                        meteorShower = true;
                    }
                                       
                }

                AsteroidSpawner(deltaTime);

                CollisionsHandler.Update(deltaTime);

                player.Update(deltaTime);

                player.MeteorShower(meteorShower);

                gasCan.Update(deltaTime, meteorShower);

                plasma.Update(deltaTime, meteorShower);

                life.Update(deltaTime, meteorShower);


                for (int i = 0; i < actualSmallAsteroids; i++)
                {
                    smallAsteroids[i].Update(deltaTime, asteroidsTimer, asteroidWaveDuration, meteorShower);
                }

                for (int i = 0; i < actualMediumAsteroids; i++)
                {
                    mediumAsteroids[i].Update(deltaTime, asteroidsTimer, asteroidWaveDuration, meteorShower);
                }

                for (int i = 0; i < actualBigAsteroids; i++)
                {
                    bigAsteroids[i].Update(deltaTime, asteroidsTimer, asteroidWaveDuration, meteorShower);
                }

                smallSpaceShipOne.MeteorShower(meteorShower);
                smallSpaceShipTwo.MeteorShower(meteorShower);
                smallSpaceShipThree.MeteorShower(meteorShower);
                smallSpaceShipFour.MeteorShower(meteorShower);
                smallSpaceShipFive.MeteorShower(meteorShower);
                smallSpaceShipSix.MeteorShower(meteorShower);
                smallSpaceShipSeven.MeteorShower(meteorShower);
                smallSpaceShipEight.MeteorShower(meteorShower);
                smallSpaceShipNine.MeteorShower(meteorShower);
                smallSpaceShipTen.MeteorShower(meteorShower);
                smallSpaceShipEleven.MeteorShower(meteorShower);
                smallSpaceShipTwelve.MeteorShower(meteorShower);
                smallSpaceShipThirteen.MeteorShower(meteorShower);
                smallSpaceShipFourteen.MeteorShower(meteorShower);
                smallSpaceShipFifteen.MeteorShower(meteorShower);
                smallSpaceShipSixteen.MeteorShower(meteorShower);
                smallSpaceShipSeventeen.MeteorShower(meteorShower);
                smallSpaceShipEighteen.MeteorShower(meteorShower);
                smallSpaceShipNineteen.MeteorShower(meteorShower);
                smallSpaceShipTwenty.MeteorShower(meteorShower);
                smallSpaceShipTwentyone.MeteorShower(meteorShower);
                smallSpaceShipTwentytwo.MeteorShower(meteorShower);
                smallSpaceShipTwentythree.MeteorShower(meteorShower);

                smallSpaceShipOne.TravelTime(travelTime);
                smallSpaceShipTwo.TravelTime(travelTime);
                smallSpaceShipThree.TravelTime(travelTime);
                smallSpaceShipFour.TravelTime(travelTime);
                smallSpaceShipFive.TravelTime(travelTime);
                smallSpaceShipSix.TravelTime(travelTime);
                smallSpaceShipSeven.TravelTime(travelTime);
                smallSpaceShipEight.TravelTime(travelTime);
                smallSpaceShipNine.TravelTime(travelTime);
                smallSpaceShipTen.TravelTime(travelTime);
                smallSpaceShipEleven.TravelTime(travelTime);
                smallSpaceShipTwelve.TravelTime(travelTime);
                smallSpaceShipThirteen.TravelTime(travelTime);
                smallSpaceShipFourteen.TravelTime(travelTime);
                smallSpaceShipFifteen.TravelTime(travelTime);
                smallSpaceShipSixteen.TravelTime(travelTime);
                smallSpaceShipSeventeen.TravelTime(travelTime);
                smallSpaceShipEighteen.TravelTime(travelTime);
                smallSpaceShipNineteen.TravelTime(travelTime);
                smallSpaceShipTwenty.TravelTime(travelTime);
                smallSpaceShipTwentyone.TravelTime(travelTime);
                smallSpaceShipTwentytwo.TravelTime(travelTime);
                smallSpaceShipTwentythree.TravelTime(travelTime);

                smallSpaceShipOne.TravelTimer(travelTimer);
                smallSpaceShipTwo.TravelTimer(travelTimer);
                smallSpaceShipThree.TravelTimer(travelTimer);
                smallSpaceShipFour.TravelTimer(travelTimer);
                smallSpaceShipFive.TravelTimer(travelTimer);
                smallSpaceShipSix.TravelTimer(travelTimer);
                smallSpaceShipSeven.TravelTimer(travelTimer);
                smallSpaceShipEight.TravelTimer(travelTimer);
                smallSpaceShipNine.TravelTimer(travelTimer);
                smallSpaceShipTen.TravelTimer(travelTimer);
                smallSpaceShipEleven.TravelTimer(travelTimer);
                smallSpaceShipTwelve.TravelTimer(travelTimer);
                smallSpaceShipThirteen.TravelTimer(travelTimer);
                smallSpaceShipFourteen.TravelTimer(travelTimer);
                smallSpaceShipFifteen.TravelTimer(travelTimer);
                smallSpaceShipSixteen.TravelTimer(travelTimer);
                smallSpaceShipSeventeen.TravelTimer(travelTimer);
                smallSpaceShipEighteen.TravelTimer(travelTimer);
                smallSpaceShipNineteen.TravelTimer(travelTimer);
                smallSpaceShipTwenty.TravelTimer(travelTimer);
                smallSpaceShipTwentyone.TravelTimer(travelTimer);
                smallSpaceShipTwentytwo.TravelTimer(travelTimer);
                smallSpaceShipTwentythree.TravelTimer(travelTimer);

                smallSpaceShipOne.Update(deltaTime);
                smallSpaceShipTwo.Update(deltaTime);
                smallSpaceShipThree.Update(deltaTime);
                smallSpaceShipFour.Update(deltaTime);
                smallSpaceShipFive.Update(deltaTime);
                smallSpaceShipSix.Update(deltaTime);
                smallSpaceShipSeven.Update(deltaTime);
                smallSpaceShipEight.Update(deltaTime);
                smallSpaceShipNine.Update(deltaTime);
                smallSpaceShipTen.Update(deltaTime);
                smallSpaceShipEleven.Update(deltaTime);
                smallSpaceShipTwelve.Update(deltaTime);
                smallSpaceShipThirteen.Update(deltaTime);
                smallSpaceShipFourteen.Update(deltaTime);
                smallSpaceShipFifteen.Update(deltaTime);
                smallSpaceShipSixteen.Update(deltaTime);
                smallSpaceShipSeventeen.Update(deltaTime);
                smallSpaceShipEighteen.Update(deltaTime);
                smallSpaceShipNineteen.Update(deltaTime);
                smallSpaceShipTwenty.Update(deltaTime);
                smallSpaceShipTwentyone.Update(deltaTime);
                smallSpaceShipTwentytwo.Update(deltaTime);
                smallSpaceShipTwentythree.Update(deltaTime);

                mediumSpaceShipOne.MeteorShower(meteorShower);
                mediumSpaceShipTwo.MeteorShower(meteorShower);
                mediumSpaceShipThree.MeteorShower(meteorShower);
                mediumSpaceShipFour.MeteorShower(meteorShower);
                mediumSpaceShipFive.MeteorShower(meteorShower);
                mediumSpaceShipSix.MeteorShower(meteorShower);
                mediumSpaceShipSeven.MeteorShower(meteorShower);
                mediumSpaceShipEight.MeteorShower(meteorShower);
                mediumSpaceShipNine.MeteorShower(meteorShower);
                mediumSpaceShipTen.MeteorShower(meteorShower);

                mediumSpaceShipOne.TravelTime(travelTime);
                mediumSpaceShipTwo.TravelTime(travelTime);
                mediumSpaceShipThree.TravelTime(travelTime);
                mediumSpaceShipFour.TravelTime(travelTime);
                mediumSpaceShipFive.TravelTime(travelTime);
                mediumSpaceShipSix.TravelTime(travelTime);
                mediumSpaceShipSeven.TravelTime(travelTime);
                mediumSpaceShipEight.TravelTime(travelTime);
                mediumSpaceShipNine.TravelTime(travelTime);
                mediumSpaceShipTen.TravelTime(travelTime);

                mediumSpaceShipOne.TravelTimer(travelTimer);
                mediumSpaceShipTwo.TravelTimer(travelTimer);
                mediumSpaceShipThree.TravelTimer(travelTimer);
                mediumSpaceShipFour.TravelTimer(travelTimer);
                mediumSpaceShipFive.TravelTimer(travelTimer);
                mediumSpaceShipSix.TravelTimer(travelTimer);
                mediumSpaceShipSeven.TravelTimer(travelTimer);
                mediumSpaceShipEight.TravelTimer(travelTimer);
                mediumSpaceShipNine.TravelTimer(travelTimer);
                mediumSpaceShipTen.TravelTimer(travelTimer);

                mediumSpaceShipOne.CurrentWave(wave);
                mediumSpaceShipTwo.CurrentWave(wave);
                mediumSpaceShipThree.CurrentWave(wave);
                mediumSpaceShipFour.CurrentWave(wave);
                mediumSpaceShipFive.CurrentWave(wave);
                mediumSpaceShipSix.CurrentWave(wave);
                mediumSpaceShipSeven.CurrentWave(wave);
                mediumSpaceShipEight.CurrentWave(wave);
                mediumSpaceShipNine.CurrentWave(wave);
                mediumSpaceShipTen.CurrentWave(wave);

                mediumSpaceShipOne.Update(deltaTime);
                mediumSpaceShipTwo.Update(deltaTime);
                mediumSpaceShipThree.Update(deltaTime);
                mediumSpaceShipFour.Update(deltaTime);
                mediumSpaceShipFive.Update(deltaTime);
                mediumSpaceShipSix.Update(deltaTime);
                mediumSpaceShipSeven.Update(deltaTime);
                mediumSpaceShipEight.Update(deltaTime);
                mediumSpaceShipNine.Update(deltaTime);
                mediumSpaceShipTen.Update(deltaTime);

                bigSpaceShipOne.MeteorShower(meteorShower);
                bigSpaceShipTwo.MeteorShower(meteorShower);
                bigSpaceShipThree.MeteorShower(meteorShower);
                bigSpaceShipFour.MeteorShower(meteorShower);
                bigSpaceShipFive.MeteorShower(meteorShower);
                bigSpaceShipSix.MeteorShower(meteorShower);
                bigSpaceShipSeven.MeteorShower(meteorShower);
                bigSpaceShipEight.MeteorShower(meteorShower);
                bigSpaceShipNine.MeteorShower(meteorShower);
                bigSpaceShipTen.MeteorShower(meteorShower);

                bigSpaceShipOne.TravelTime(travelTime);
                bigSpaceShipTwo.TravelTime(travelTime);
                bigSpaceShipThree.TravelTime(travelTime);
                bigSpaceShipFour.TravelTime(travelTime);
                bigSpaceShipFive.TravelTime(travelTime);
                bigSpaceShipSix.TravelTime(travelTime);
                bigSpaceShipSeven.TravelTime(travelTime);
                bigSpaceShipEight.TravelTime(travelTime);
                bigSpaceShipNine.TravelTime(travelTime);
                bigSpaceShipTen.TravelTime(travelTime);

                bigSpaceShipOne.TravelTimer(travelTimer);
                bigSpaceShipTwo.TravelTimer(travelTimer);
                bigSpaceShipThree.TravelTimer(travelTimer);
                bigSpaceShipFour.TravelTimer(travelTimer);
                bigSpaceShipFive.TravelTimer(travelTimer);
                bigSpaceShipSix.TravelTimer(travelTimer);
                bigSpaceShipSeven.TravelTimer(travelTimer);
                bigSpaceShipEight.TravelTimer(travelTimer);
                bigSpaceShipNine.TravelTimer(travelTimer);
                bigSpaceShipTen.TravelTimer(travelTimer);

                bigSpaceShipOne.CurrentWave(wave);
                bigSpaceShipTwo.CurrentWave(wave);
                bigSpaceShipThree.CurrentWave(wave);
                bigSpaceShipFour.CurrentWave(wave);
                bigSpaceShipFive.CurrentWave(wave);
                bigSpaceShipSix.CurrentWave(wave);
                bigSpaceShipSeven.CurrentWave(wave);
                bigSpaceShipEight.CurrentWave(wave);
                bigSpaceShipNine.CurrentWave(wave);
                bigSpaceShipTen.CurrentWave(wave);

                bigSpaceShipOne.Update(deltaTime);
                bigSpaceShipTwo.Update(deltaTime);
                bigSpaceShipThree.Update(deltaTime);
                bigSpaceShipFour.Update(deltaTime);
                bigSpaceShipFive.Update(deltaTime);
                bigSpaceShipSix.Update(deltaTime);
                bigSpaceShipSeven.Update(deltaTime);
                bigSpaceShipEight.Update(deltaTime);
                bigSpaceShipNine.Update(deltaTime);
                bigSpaceShipTen.Update(deltaTime);

                shootingStar.Update(deltaTime, meteorShower);

                hud.Update(waveText);
            }

            PlayerDeath();
                       
        }

        protected override void Draw()
        {
            List<Bullet> bullets = player.GetActiveBullets();
                       
            renderWindow.Draw(background.Graphic);

            renderWindow.Draw(gasCan.Graphic);

            renderWindow.Draw(plasma.Graphic);

            renderWindow.Draw(life.Graphic);
                       
            renderWindow.Draw(player.Graphic);    

            for(int i = 0; i < bullets.Count; i++)
            {
                renderWindow.Draw(bullets[i].Graphic);
            }
            
            for (int i = 0; i < actualSmallAsteroids; i++)
            {
                renderWindow.Draw(smallAsteroids[i].Graphic);                
            }

            for (int i = 0; i < actualMediumAsteroids; i++)
            {
                renderWindow.Draw(mediumAsteroids[i].Graphic);
            }

            for (int i = 0; i < actualBigAsteroids; i++)
            {
                renderWindow.Draw(bigAsteroids[i].Graphic);
            }

            renderWindow.Draw(smallSpaceShipOne.Graphic);
            renderWindow.Draw(smallSpaceShipTwo.Graphic);
            renderWindow.Draw(smallSpaceShipThree.Graphic);
            renderWindow.Draw(smallSpaceShipFour.Graphic);
            renderWindow.Draw(smallSpaceShipFive.Graphic);
            renderWindow.Draw(smallSpaceShipSix.Graphic);
            renderWindow.Draw(smallSpaceShipSeven.Graphic);
            renderWindow.Draw(smallSpaceShipEight.Graphic);
            renderWindow.Draw(smallSpaceShipNine.Graphic);
            renderWindow.Draw(smallSpaceShipTen.Graphic);
            renderWindow.Draw(smallSpaceShipEleven.Graphic);
            renderWindow.Draw(smallSpaceShipTwelve.Graphic);
            renderWindow.Draw(smallSpaceShipThirteen.Graphic);
            renderWindow.Draw(smallSpaceShipFourteen.Graphic);
            renderWindow.Draw(smallSpaceShipFifteen.Graphic);
            renderWindow.Draw(smallSpaceShipSixteen.Graphic);
            renderWindow.Draw(smallSpaceShipSeventeen.Graphic);
            renderWindow.Draw(smallSpaceShipEighteen.Graphic);
            renderWindow.Draw(smallSpaceShipNineteen.Graphic);
            renderWindow.Draw(smallSpaceShipTwenty.Graphic);
            renderWindow.Draw(smallSpaceShipTwentyone.Graphic);
            renderWindow.Draw(smallSpaceShipTwentytwo.Graphic);
            renderWindow.Draw(smallSpaceShipTwentythree.Graphic);
           
            renderWindow.Draw(mediumSpaceShipOne.Graphic);
            renderWindow.Draw(mediumSpaceShipTwo.Graphic);
            renderWindow.Draw(mediumSpaceShipThree.Graphic);
            renderWindow.Draw(mediumSpaceShipFour.Graphic);
            renderWindow.Draw(mediumSpaceShipFive.Graphic);
            renderWindow.Draw(mediumSpaceShipSix.Graphic);
            renderWindow.Draw(mediumSpaceShipSeven.Graphic);
            renderWindow.Draw(mediumSpaceShipEight.Graphic);
            renderWindow.Draw(mediumSpaceShipNine.Graphic);
            renderWindow.Draw(mediumSpaceShipTen.Graphic);

            renderWindow.Draw(bigSpaceShipOne.Graphic);
            renderWindow.Draw(bigSpaceShipTwo.Graphic);
            renderWindow.Draw(bigSpaceShipThree.Graphic);
            renderWindow.Draw(bigSpaceShipFour.Graphic);
            renderWindow.Draw(bigSpaceShipFive.Graphic);
            renderWindow.Draw(bigSpaceShipSix.Graphic);
            renderWindow.Draw(bigSpaceShipSeven.Graphic);
            renderWindow.Draw(bigSpaceShipEight.Graphic);
            renderWindow.Draw(bigSpaceShipNine.Graphic);
            renderWindow.Draw(bigSpaceShipTen.Graphic);

            renderWindow.Draw(shootingStar.Graphic);

            renderWindow.Draw(hudbar.Graphic);

            hud.Draw();

            renderWindow.Draw(pauseMenu.Graphic);
            renderWindow.Draw(looseMenu.Graphic);

            renderWindow.Draw(gamePausedText);
            renderWindow.Draw(looseText);
            renderWindow.Draw(finalScoreText);
            renderWindow.Draw(scoreInputNameText);
            renderWindow.Draw(scoreInputNameInstructionsText);
            renderWindow.Draw(scoreNameText);
            renderWindow.Draw(newHighScoreText);

            continueButton.Draw();
            mainMenuButton.Draw();
            restartButton.Draw();
                                              
        }

        protected override void Finish()
        {
            base.Finish();            

            backgroundMusic.Stop();

            CollisionsHandler.RemoveEntity(player);

            CollisionsHandler.RemoveEntity(gasCan);

            CollisionsHandler.RemoveEntity(plasma);

            CollisionsHandler.RemoveEntity(life);

            CollisionsHandler.RemoveEntity(shootingStar);

            CollisionsHandler.RemoveEntity(smallSpaceShipOne);
            CollisionsHandler.RemoveEntity(smallSpaceShipTwo);
            CollisionsHandler.RemoveEntity(smallSpaceShipThree);
            CollisionsHandler.RemoveEntity(smallSpaceShipFour);
            CollisionsHandler.RemoveEntity(smallSpaceShipFive);
            CollisionsHandler.RemoveEntity(smallSpaceShipSix);
            CollisionsHandler.RemoveEntity(smallSpaceShipSeven);
            CollisionsHandler.RemoveEntity(smallSpaceShipEight);
            CollisionsHandler.RemoveEntity(smallSpaceShipNine);
            CollisionsHandler.RemoveEntity(smallSpaceShipTen);
            CollisionsHandler.RemoveEntity(smallSpaceShipEleven);
            CollisionsHandler.RemoveEntity(smallSpaceShipTwelve);
            CollisionsHandler.RemoveEntity(smallSpaceShipThirteen);
            CollisionsHandler.RemoveEntity(smallSpaceShipFourteen);
            CollisionsHandler.RemoveEntity(smallSpaceShipFifteen);
            CollisionsHandler.RemoveEntity(smallSpaceShipSixteen);
            CollisionsHandler.RemoveEntity(smallSpaceShipSeventeen);
            CollisionsHandler.RemoveEntity(smallSpaceShipEighteen);
            CollisionsHandler.RemoveEntity(smallSpaceShipNineteen);
            CollisionsHandler.RemoveEntity(smallSpaceShipTwenty);
            CollisionsHandler.RemoveEntity(smallSpaceShipTwentyone);
            CollisionsHandler.RemoveEntity(smallSpaceShipTwentytwo);
            CollisionsHandler.RemoveEntity(smallSpaceShipTwentythree);

            CollisionsHandler.RemoveEntity(mediumSpaceShipOne);
            CollisionsHandler.RemoveEntity(mediumSpaceShipTwo);
            CollisionsHandler.RemoveEntity(mediumSpaceShipThree);
            CollisionsHandler.RemoveEntity(mediumSpaceShipFour);
            CollisionsHandler.RemoveEntity(mediumSpaceShipFive);
            CollisionsHandler.RemoveEntity(mediumSpaceShipSix);
            CollisionsHandler.RemoveEntity(mediumSpaceShipSeven);
            CollisionsHandler.RemoveEntity(mediumSpaceShipEight);
            CollisionsHandler.RemoveEntity(mediumSpaceShipNine);
            CollisionsHandler.RemoveEntity(mediumSpaceShipTen);

            CollisionsHandler.RemoveEntity(bigSpaceShipOne);
            CollisionsHandler.RemoveEntity(bigSpaceShipTwo);
            CollisionsHandler.RemoveEntity(bigSpaceShipThree);
            CollisionsHandler.RemoveEntity(bigSpaceShipFour);
            CollisionsHandler.RemoveEntity(bigSpaceShipFive);
            CollisionsHandler.RemoveEntity(bigSpaceShipSix);
            CollisionsHandler.RemoveEntity(bigSpaceShipSeven);
            CollisionsHandler.RemoveEntity(bigSpaceShipEight);
            CollisionsHandler.RemoveEntity(bigSpaceShipNine);
            CollisionsHandler.RemoveEntity(bigSpaceShipTen);

            smallSpaceShipOne.Finish();
            smallSpaceShipTwo.Finish();
            smallSpaceShipThree.Finish();
            smallSpaceShipFour.Finish();
            smallSpaceShipFive.Finish();
            smallSpaceShipSix.Finish();
            smallSpaceShipSeven.Finish();
            smallSpaceShipEight.Finish();
            smallSpaceShipNine.Finish();
            smallSpaceShipTen.Finish();
            smallSpaceShipEleven.Finish();
            smallSpaceShipTwelve.Finish();
            smallSpaceShipThirteen.Finish();
            smallSpaceShipFourteen.Finish();
            smallSpaceShipFifteen.Finish();
            smallSpaceShipSixteen.Finish();
            smallSpaceShipSeventeen.Finish();
            smallSpaceShipEighteen.Finish();
            smallSpaceShipNineteen.Finish();
            smallSpaceShipTwenty.Finish();
            smallSpaceShipTwentyone.Finish();
            smallSpaceShipTwentytwo.Finish();
            smallSpaceShipTwentythree.Finish();

            mediumSpaceShipOne.Finish();
            mediumSpaceShipTwo.Finish();
            mediumSpaceShipThree.Finish();
            mediumSpaceShipFour.Finish();
            mediumSpaceShipFive.Finish();
            mediumSpaceShipSix.Finish();
            mediumSpaceShipSeven.Finish();
            mediumSpaceShipEight.Finish();
            mediumSpaceShipNine.Finish();
            mediumSpaceShipTen.Finish();

            bigSpaceShipOne.Finish();
            bigSpaceShipTwo.Finish();
            bigSpaceShipThree.Finish();
            bigSpaceShipFour.Finish();
            bigSpaceShipFive.Finish();
            bigSpaceShipSix.Finish();
            bigSpaceShipSeven.Finish();
            bigSpaceShipEight.Finish();
            bigSpaceShipNine.Finish();
            bigSpaceShipTen.Finish();

            player.Finish();

            for (int i = 0; i < actualSmallAsteroids; i++)
            {               
                CollisionsHandler.RemoveEntity(smallAsteroids[i]);
                smallAsteroids[i].Finish();
            }

            for (int i = 0; i < actualMediumAsteroids; i++)
            {                
                CollisionsHandler.RemoveEntity(mediumAsteroids[i]);
                mediumAsteroids[i].Finish();
            }

            for (int i = 0; i < actualBigAsteroids; i++)
            {
                CollisionsHandler.RemoveEntity(bigAsteroids[i]);
                bigAsteroids[i].Finish();
            }

            for (int i = 0; i < 100; i++ )
            {
                playerBullets[i].Finish();
            }

            renderWindow.KeyPressed -= OnPressPauseKey;
            continueButton.OnPressed -= OnPressContinueKey;
            mainMenuButton.OnPressed -= OnPressMainMenuKey;
            restartButton.OnPressed -= OnPressRestartKey;

            continueButton.OnTouched -= OnTouchedContinue;
            mainMenuButton.OnTouched -= OnTouchedMainMenu;
            restartButton.OnTouched -= OnTouchedRestart;

            continueButton.OnNotTouched -= OnNotTouchedContinue;
            mainMenuButton.OnNotTouched -= OnNotTouchedMainMenu;
            restartButton.OnNotTouched -= OnNotTouchedRestart;

            renderWindow.LostFocus -= OnMinimizedWindow;
            renderWindow.GainedFocus -= OnMaximizedWindow;

            meteorShower = false;
        }
               
    }
      
}
