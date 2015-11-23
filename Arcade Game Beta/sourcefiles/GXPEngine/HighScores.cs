using System;
using System.IO;
using System.Drawing;

namespace GXPEngine
{
    public class HighScores : GameObject
    {
        private string[] _highScoreNames = new string[10];
        private int[] _highScores = new int[10];


        /// <summary>
        /// Highscores reads the "highscoredata.txt" file
        /// The HighScores class is used to modify and/or read the highscorelist
        /// </summary>
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
        public void AddScore(string pName, int pScore)
        {
            if (pScore >= _highScores[10])
            {
                _highScores[10] = pScore;
                _highScoreNames[10] = pName;
                Array.Sort(_highScores, _highScoreNames);
                writeHighScore();
            }
        }

        /// <summary>
        /// This draws the highscore and adds it as a child of the gameobject it is called from
        /// </summary>
        /// <param name="pSize">
        /// The pSize represents the size of the drawn highscorelist,
        /// With a value between 1 and 20.
        /// </param>
        public void DrawHighScore(float pSize, GameObject pParent)
        {
            if (pSize < 1) { pSize = 1; }
            if (pSize > 20) { pSize = 20; }

            Canvas highscorecv = new Canvas(1028, (int)pSize * 30);
            pParent.AddChild(highscorecv);

            Font font = new Font("Arial", pSize, FontStyle.Regular);
            Brush brush = new SolidBrush(Color.White);

            int placementValue = 0;
            for (int i = 9; i >= 0; i--)
            {
                placementValue++;
                PointF posI = new PointF(0, placementValue * pSize * 2);
                string message = _highScoreNames[i] + " " + _highScores[i];
                highscorecv.graphics.DrawString(message, font, brush, posI);
            }
            
            
        }
    }
}
