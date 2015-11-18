using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace GXPEngine
{
    public class Level001 : GameObject
    {
        public List<GameObject> objectList = new List<GameObject>();
        private Player _player;

        const int HEIGHT = 8;
        const int WIDTH = 150;
        private string _fileName;

        private MyGame _game;
        const int TILESIZE = 69;
        public Level001(MyGame pGame, string pFileName)
        {
            _fileName = pFileName;
            _game = pGame;
            drawSolidObjects();
            drawPickUps();
            drawDamageBlocks();
            drawPlayer();

            Camera cam1 = new Camera(_player, this);
            AddChild(cam1);


            PickUpWeapon weapon = new PickUpWeapon();
            AddChild(weapon);
            weapon.SetXY(_player.x + 200, _player.y - 50);
            objectList.Add(weapon);

            EnemySpider spider = new EnemySpider(this);
            AddChild(spider);
            spider.SetXY(_player.x + 300, _player.y);
            objectList.Add(spider);
        }
        void Update()
        {
            updateGS();
        }
        private void drawPlayer()
        {
            _player = new Player(this);
            AddChild(_player);
            _player.spawnX = 20;
            _player.spawnY = 460;
            _player.SetXY(_player.spawnX, _player.spawnY);
        }
        private void drawSolidObjects()
        {
            int[,] levelData = new int[HEIGHT, WIDTH];
            //------------------------------------------READ FILE----------------------------------------------
            StreamReader reader1 = new StreamReader(_fileName);
            string fileData = reader1.ReadToEnd();
            reader1.Close();
            //--------------------------------------SPLIT AND CONVERT------------------------------------------
            string[] lines = fileData.Split('\n');
            for (int j = 13; j < 21; j++)
            {
                string[] columns = lines[j].Split(',');

                for (int i = 0; i < 63; i++)
                {
                    string column = columns[i];
                    levelData[j - 13, i] = int.Parse(column);
                }
            }

            //-------------------------------------READ THE NUMBERS, PLACE IN GAME-----------------------------
            for (int i = 0; i < HEIGHT; i++)
            {
                for (int j = 0; j < WIDTH; j++)
                {
                    if (levelData[i, j] != 0)
                    {
                        SolidObject thisobject = new SolidObject();
                        AddChild(thisobject);
                        thisobject.SetXY(j * TILESIZE, i * TILESIZE);
                        thisobject.SetFrame(levelData[i, j] - 1);
                        objectList.Add(thisobject);
                    }
                }
            }
        }
        private void drawPickUps()
        {
            int[,] levelData = new int[HEIGHT, WIDTH];

            //------------------------------------------READ FILE----------------------------------------------
            StreamReader reader1 = new StreamReader(_fileName);
            string fileData = reader1.ReadToEnd();
            reader1.Close();
            //--------------------------------------SPLIT AND CONVERT------------------------------------------
            string[] lines = fileData.Split('\n');
            for (int j = 25; j < 33; j++)
            {
                string[] columns = lines[j].Split(',');

                for (int i = 0; i < 63; i++)
                {
                    string column = columns[i];
                    levelData[j - 25, i] = int.Parse(column);
                }
            }

            //-------------------------------------READ THE NUMBERS, PLACE IN GAME-----------------------------
            for (int i = 0; i < HEIGHT; i++)
            {
                for (int j = 0; j < WIDTH; j++)
                {
                    if (levelData[i, j] != 0)
                    {
                        PickUpCoin pickup = new PickUpCoin();
                        AddChild(pickup);
                        pickup.SetXY(j * TILESIZE, i * TILESIZE);
                        pickup.SetFrame(levelData[i, j] - 1);
                        objectList.Add(pickup);
                    }
                }
            }            
        }
        private void drawDamageBlocks()
        {
            int[,] levelData = new int[HEIGHT, WIDTH];

            //------------------------------------------READ FILE----------------------------------------------
            StreamReader reader1 = new StreamReader(_fileName);
            string fileData = reader1.ReadToEnd();
            reader1.Close();
            //--------------------------------------SPLIT AND CONVERT------------------------------------------
            string[] lines = fileData.Split('\n');
            for (int j = 37; j < lines.Count() - 2; j++)
            {
                string[] columns = lines[j].Split(',');

                for (int i = 0; i < 63; i++)
                {
                    string column = columns[i];
                    levelData[j - 37, i] = int.Parse(column);
                }
            }
            //-------------------------------------READ THE NUMBERS, PLACE IN GAME-----------------------------
            for (int i = 0; i < HEIGHT; i++)
            {
                for (int j = 0; j < WIDTH; j++)
                {
                    if (levelData[i, j] != 0)
                    {
                        DamageBlock dmgBlock = new DamageBlock();
                        AddChild(dmgBlock);
                        dmgBlock.SetXY(j * TILESIZE, i * TILESIZE);
                        dmgBlock.SetFrame(levelData[i, j] - 1);
                        objectList.Add(dmgBlock);
                    }
                }
            }
        }
        private void updateGS()
        {
            //Update game state, go to next level
            const int LEVEL2 = 2;

            if (_player.x > 3440)
            {
                _game.setGameState(LEVEL2);
            }
        }
    }
}
