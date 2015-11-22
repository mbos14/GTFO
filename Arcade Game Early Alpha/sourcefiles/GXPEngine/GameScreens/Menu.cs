using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace GXPEngine
{
    public class Menu : GameObject
    {
        private string[] _highScoreNames = new string[10];
        private int[] _highScores = new int[10];
        private MyGame _game;
        private bool _coinInserted = false;

        public Menu(MyGame pGame)
        {
            _game = pGame;
            drawBackGround();
            getHighScore();
        }
        private void checkButtons()
        {
            if (Input.GetAnyKeyDown() && _coinInserted)
            {
                _game.setGameState(GameStates.level);
            }
            if (Input.GetKeyDown((int)PlayerButtons.shoot))
            {
                _coinInserted = true;
            }
        }
        void Update()
        {
            checkButtons();
        }
        private void drawText()
        {
            //Title
            //Insert coin
            //Press any button to start the game
        }
        private void drawBackGround()
        {
            Canvas menuBG = new Canvas("menubackground.png");
            menuBG.SetScaleXY(4, 4);
            AddChild(menuBG);
        }
        private void drawHighScore()
        {
            
        }
        //HIGHSCORES
        private void getHighScore()
        {
            StreamReader streamReader = new StreamReader("highscoredata.txt");
            string fileData = streamReader.ReadToEnd();
            streamReader.Close();

            string[] lines = fileData.Split('\n');
            for (int i = 0; i < lines.Length; i++) 
            {
                string[] columns = lines[i].Split(',');
                //Save in arrays
                _highScoreNames[i] = columns[1];
                int.TryParse(columns[0], out _highScores[i]);
            }
                Array.Sort(_highScores, _highScoreNames);

            for (int i = 0; i < 10; i++)
            {
                Console.WriteLine(_highScores[i] + " " + _highScoreNames[i]);
            }
        }
        private void writeHighScore()
        {
            StreamWriter streamWriter = new StreamWriter("highscoredata.txt");
            
            for (int i = 0; i < 10; i++)
            {
                streamWriter.WriteLine(_highScoreNames[i] + "," + _highScores[i]);
            }
            streamWriter.Close();
        }
        private void addScore(string pName, int pScore)
        {
            if (pScore >= _highScores[10])
            {
                _highScores[10] = pScore;
                _highScoreNames[10] = pName;
                Array.Sort(_highScores, _highScoreNames);
                writeHighScore();
            }
        }
    }
}
