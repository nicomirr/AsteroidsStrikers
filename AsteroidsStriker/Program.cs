using SFML.Graphics;
using SFML.Window;
using System.IO;

namespace SpaceShipGame3
{
    public class Program
    {
        private static void Main()
        {
            FileStream highScores = new FileStream("highscores.txt", FileMode.OpenOrCreate);
            byte[] highScoresData = new byte[115];

            highScores.Seek(0, SeekOrigin.Begin);
            highScores.Read(highScoresData, 0, (int)highScoresData.Length);

            VideoMode videoMode = new VideoMode(1280, 720);
            string title = "Space game";

            RenderWindow renderWindow = new RenderWindow(videoMode, title, Styles.Close);
            StatesController statesController = new StatesController(renderWindow, highScores, highScoresData);

            statesController.Start();

        }
    }
}
