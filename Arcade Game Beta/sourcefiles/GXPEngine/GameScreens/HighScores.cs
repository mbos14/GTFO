using System;
using System.IO;
using System.Drawing;
using GXPEngine.Core;

namespace GXPEngine
{
    public class HighScores : GameObject
    {
        private string[] _highScoreNames = new string[10];
        private int[] _highScores = new int[10];
        public HighScores()
        {
            readHighScore();
        }
        protected override Collider createCollider()
        {
            return null;
        }
        private void readHighScore()
        {
            StreamReader streamReader = new StreamReader("highscoredata.txt");
            string fileData = streamReader.ReadToEnd();
            streamReader.Close();

            string[] lines = fileData.Split('\n');
            for (int i = 0; i < 10; i++)
            {
                string[] columns = lines[i].Split(',');
                //Save in arrays
                _highScoreNames[i] = columns[1];
                _highScores[i] = int.Parse(columns[0]);
            }
            Array.Sort(_highScores, _highScoreNames);
        }
        private void writeHighScore()
        {
            StreamWriter streamWriter = new StreamWriter("highscoredata.txt");

            streamWriter.WriteLine(_highScores[0] + "," + _highScoreNames[0]);
            streamWriter.WriteLine(_highScores[1] + "," + _highScoreNames[1]);
            streamWriter.WriteLine(_highScores[2] + "," + _highScoreNames[2]);
            streamWriter.WriteLine(_highScores[3] + "," + _highScoreNames[3]);
            streamWriter.WriteLine(_highScores[4] + "," + _highScoreNames[4]);
            streamWriter.WriteLine(_highScores[5] + "," + _highScoreNames[5]);
            streamWriter.WriteLine(_highScores[6] + "," + _highScoreNames[6]);
            streamWriter.WriteLine(_highScores[7] + "," + _highScoreNames[7]);
            streamWriter.WriteLine(_highScores[8] + "," + _highScoreNames[8]);
            streamWriter.WriteLine(_highScores[9] + "," + _highScoreNames[9]);

            streamWriter.Close();
        }
        public void AddScore(string pName, int pScore)
        {
            if (pScore >= _highScores[0])
            {
                _highScores[0] = pScore;
                _highScoreNames[0] = pName;
                Array.Sort(_highScores, _highScoreNames);
                writeHighScore();
            }
        }
        public string[] ReturnHighScore(int pI)
        {
            readHighScore();

            string[] highscorelist = new string[10];
            for (int i = 0; i < _highScoreNames.Length; i++)
            {
                string number = (10 - i) + ": ";
                string space = " ";
                highscorelist[i] = number + _highScoreNames[i] + space + _highScores[i];
                Console.WriteLine(highscorelist[i]);
            }
            return highscorelist;
        }
    }
}