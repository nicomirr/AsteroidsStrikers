using System;
using System.IO;
using SFML.Graphics;

namespace SpaceShipGame3
{
    public class StatesController  
    {
        private FileStream highScores;

        private string language = "en";

        private RenderWindow renderWindow;
        private MainMenuState mainMenu;
        private GameLoopState gameLoop;
        private InstructionsState instructionsScreen;
        private ControlsState controlsScreen;
        private CreditsState creditsScreen;
        private HighScoreState highScoreScreen;

        public StatesController(RenderWindow renderWindow, FileStream highscores, byte[]highscoresData)
        {
            this.highScores = highscores;

            this.renderWindow = renderWindow;
            mainMenu = new MainMenuState(renderWindow);
            highScoreScreen = new HighScoreState(renderWindow, highscoresData);
            gameLoop = new GameLoopState(renderWindow, highscores, highScoreScreen);
            instructionsScreen = new InstructionsState(renderWindow);
            controlsScreen = new ControlsState(renderWindow);
            creditsScreen = new CreditsState(renderWindow);
            

            mainMenu.OnPlayPressed += OnPressPlay;
            mainMenu.OnInstructionsPressed += OnPressInstructions;
            mainMenu.OnControlsPressed += OnPressControls;
            mainMenu.OnCreditsPressed += OnPressCredits;
            mainMenu.OnQuitPressed += OnPressQuit;
            mainMenu.OnChangeLanguagePressed += OnPressChangeLanguage;
            mainMenu.OnHighScorePressed+= OnPressHighScore;

            gameLoop.OnMainMenuPressed += OnPressMainMenu;
            gameLoop.OnRestartPressed += OnPressRestart;

            instructionsScreen.OnBackPressed += InstructionsOnPressBack;

            controlsScreen.OnBackPressed += ControlsOnPressBack;

            creditsScreen.OnBackPressed += CreditsOnPressBack;

            highScoreScreen.OnBackPressed += HighScoreOnPressBack;

            renderWindow.LostFocus += OnMinimizedWindow;
            renderWindow.GainedFocus += OnMaximizedWindow;

            mainMenu.Play();

        }
        ~StatesController()
        {
            mainMenu.OnPlayPressed -= OnPressPlay;
            mainMenu.OnInstructionsPressed -= OnPressInstructions;
            mainMenu.OnControlsPressed -= OnPressControls;
            mainMenu.OnCreditsPressed -= OnPressCredits;
            mainMenu.OnQuitPressed -= OnPressQuit;
            mainMenu.OnChangeLanguagePressed -= OnPressChangeLanguage;
            mainMenu.OnHighScorePressed -= OnPressHighScore;

            gameLoop.OnMainMenuPressed -= OnPressMainMenu;
            gameLoop.OnRestartPressed -= OnPressRestart;

            instructionsScreen.OnBackPressed -= InstructionsOnPressBack;

            controlsScreen.OnBackPressed -= ControlsOnPressBack;

            creditsScreen.OnBackPressed -= CreditsOnPressBack;

            highScoreScreen.OnBackPressed -= HighScoreOnPressBack;

            renderWindow.LostFocus -= OnMinimizedWindow;
            renderWindow.GainedFocus -= OnMaximizedWindow;
        }

        public void Start()
        {
            mainMenu.Play();

        }

        private void OnPressPlay()  
        {
            mainMenu.Stop();

            mainMenu.BackgroundMusic.Stop();

            renderWindow.LostFocus -= OnMinimizedWindow;
            renderWindow.GainedFocus -= OnMaximizedWindow;

            gameLoop.Play();
        }
        private void OnPressInstructions()
        {
            mainMenu.Stop();

            mainMenu.BackgroundMusic.Play();

            instructionsScreen.Play();
        }

        private void InstructionsOnPressBack()
        {
            instructionsScreen.Stop();

            mainMenu.BackgroundMusic.Pause();

            mainMenu.Play();
        }

        private void OnPressControls()
        {
            mainMenu.Stop();

            mainMenu.BackgroundMusic.Play();

            controlsScreen.Play();
        }

        private void ControlsOnPressBack()
        {           
            controlsScreen.Stop();

            mainMenu.BackgroundMusic.Pause();

            mainMenu.Play();
        }

        private void OnPressCredits()
        {
            mainMenu.Stop();

            mainMenu.BackgroundMusic.Play();

            creditsScreen.Play();
        }

        private void CreditsOnPressBack()
        {
            creditsScreen.Stop();

            mainMenu.BackgroundMusic.Pause();

            mainMenu.Play();
        }

        private void OnPressQuit()
        {
            highScores.Close();
            renderWindow.Close();
            Environment.Exit(0);
        }

        private void OnPressChangeLanguage()
        {            

            if (language == "en")
            {
                language = "es";
                mainMenu.Language = language;
                instructionsScreen.Language = language;
                controlsScreen.Language = language;
                creditsScreen.Language = language;
                gameLoop.Language = language;  
                highScoreScreen.Language = language;

            }           
            else if (language == "es")
            {
                language = "en";
                mainMenu.Language = language;
                instructionsScreen.Language = language;
                controlsScreen.Language = language;
                creditsScreen.Language = language;
                gameLoop.Language = language;
                highScoreScreen.Language = language;

            }
                        
        }

        private void OnPressHighScore()
        {
            mainMenu.Stop();

            mainMenu.BackgroundMusic.Play();

            highScoreScreen.Play();
        }

        private void HighScoreOnPressBack()
        {
            highScoreScreen.Stop();

            mainMenu.BackgroundMusic.Pause();

            mainMenu.Play();
        }

        private void OnPressMainMenu()
        {
            gameLoop.Stop();

            renderWindow.LostFocus += OnMinimizedWindow;
            renderWindow.GainedFocus += OnMaximizedWindow;

            mainMenu.Play();
        }

        private void OnPressRestart()
        {
            gameLoop.Stop();
            gameLoop.Play();
        }

        private void OnMinimizedWindow(object sender, EventArgs eventArgs) => mainMenu.BackgroundMusic.Pause();
        private void OnMaximizedWindow(object sender, EventArgs eventArgs) => mainMenu.BackgroundMusic.Play();
    }
}
