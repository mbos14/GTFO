using System;
using System.IO;
using System.Drawing;

namespace GXPEngine
{
    public class HighScores : GameObject
    {
        private string[] _highScoreNames = new string[10];
        private int[] _highScores = new int[10];
        public HighScores()
        {
            getHighScore();
        }
        private void getHighScore()
        {
            StreamReader streamReader = new StreamReader("highscoredata.txt");
            string fileData = streamReader.ReadToEnd();
            streamReader.Close();

            string[] lines = fileData.Split('\n');
            for (int i = 0; i < 9; i++)
            {
                string[] columns = lines[i].Split(',');
                //Save in arrays
                _highScoreNames[i] = columns[1];
                int.TryParse(columns[0], out _highScores[i]);
            }
            Array.Sort(_highScores, _highScoreNames);
        }
        private void writeHighScore()
        {
            StreamWriter streamWriter = new StreamWriter("highscoredata.txt");

            for (int i = 0; i < 10; i++)
            {
                streamWriter.WriteLine(_highScores[i] + "," + _highScoreNames[i]);
            }
            streamWriter.Close();
        }
        public void AddScore(string pName, int pScore)
        {
            if (pScore > _highScores[0])
            {
                _highScores[0] = pScore;
                _highScoreNames[0] = pName;
                Array.Sort(_highScores, _highScoreNames);
                writeHighScore();
            }
        }
        public string ReturnHighScore(int pI)
        {
            string[] highscorelist = new string[10];
            for (int i = 9; i >= 0; i--)
            {
                highscorelist[i] = _highScoreNames[i] + _highScores[i];
            }
            Console.WriteLine(highscorelist[pI]);
            return highscorelist[pI];
        }
    }
}
