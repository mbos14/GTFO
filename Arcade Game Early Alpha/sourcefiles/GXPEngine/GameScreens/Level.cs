using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace GXPEngine
{
    public class Level : GameObject
    {
        //Data
        public List<GameObject> objectList = new List<GameObject>();
        private Player _player;
        private MyGame _game;
        private string _fileName;
        
        //Variables for tile- and levelsize
        const int HEIGHT = 26;
        const int WIDTH = 60;
        const int TILESIZE = 32;

        //Pivots
        private Pivot _backgroundLayer = new Pivot();
        private Pivot _midgroundLayer = new Pivot();
        private Pivot _foregroundLayer = new Pivot();
        public Level(MyGame pGame, string pFileName)
        {
            _fileName = pFileName;
            _game = pGame;

            addPivots();
            drawAll();

            Camera cam1 = new Camera(_player, this);
            AddChild(cam1);
        }
        //Drawfunctions
        private void drawAll()
        {
            drawBackGroundLayer();
            drawbackGroundObjectLayer();
            drawSolidLayer();
            drawDamageLayer();
            drawPickUpLayer();
            drawForeGroundLayer();
            drawPlayer();
        }
        private void addPivots()
        {
            AddChild(_backgroundLayer);
            AddChild(_midgroundLayer);
            AddChild(_foregroundLayer);
        }
        private void drawPlayer()
        {
            _player = new Player(this);
            _midgroundLayer.AddChild(_player);
            _player.spawnX = 100;
            _player.spawnY = 530;
            _player.SetXY(_player.spawnX, _player.spawnY);
        }
        //Layers
        private void drawBackGroundLayer()
        {
            int[,] levelData = new int[HEIGHT, WIDTH];
            //------------------------------------------READ FILE----------------------------------------------
            StreamReader reader1 = new StreamReader(_fileName);
            string fileData = reader1.ReadToEnd();
            reader1.Close();
            //--------------------------------------SPLIT AND CONVERT------------------------------------------
            string[] lines = fileData.Split('\n');
            for (int j = 14; j < 40; j++)
            {
                string[] columns = lines[j].Split(',');

                for (int i = 0; i < WIDTH; i++)
                {
                    string column = columns[i];
                    levelData[j - 14, i] = int.Parse(column);
                }
            }

            //-------------------------------------READ THE NUMBERS, PLACE IN GAME-----------------------------
            for (int i = 0; i < HEIGHT; i++)
            {
                for (int j = 0; j < WIDTH; j++)
                {
                    if (levelData[i, j] != 0)
                    {
                        BackgroundObject thisobject = new BackgroundObject();
                        _backgroundLayer.AddChild(thisobject);
                        thisobject.SetXY(j * TILESIZE, i * TILESIZE);
                        thisobject.SetFrame(levelData[i, j] - 1);
                    }
                }
            }
        }
        private void drawbackGroundObjectLayer()
        {
            int[,] levelData = new int[HEIGHT, WIDTH];
            //------------------------------------------READ FILE----------------------------------------------
            StreamReader reader1 = new StreamReader(_fileName);
            string fileData = reader1.ReadToEnd();
            reader1.Close();
            //--------------------------------------SPLIT AND CONVERT------------------------------------------
            string[] lines = fileData.Split('\n');
            for (int j = 44; j < 70; j++)
            {
                string[] columns = lines[j].Split(',');

                for (int i = 0; i < WIDTH; i++)
                {
                    string column = columns[i];
                    levelData[j - 44, i] = int.Parse(column);
                }
            }

            //-------------------------------------READ THE NUMBERS, PLACE IN GAME-----------------------------
            for (int i = 0; i < HEIGHT; i++)
            {
                for (int j = 0; j < WIDTH; j++)
                {
                    if (levelData[i, j] != 0)
                    {
                        BackgroundObject thisobject = new BackgroundObject();
                        _backgroundLayer.AddChild(thisobject);
                        thisobject.SetXY(j * TILESIZE, i * TILESIZE);
                        thisobject.SetFrame(levelData[i, j] - 1);
                    }
                }
            }
        }
        private void drawPickUpLayer()
        {
            int[,] levelData = new int[HEIGHT, WIDTH];
            //------------------------------------------READ FILE----------------------------------------------
            StreamReader reader1 = new StreamReader(_fileName);
            string fileData = reader1.ReadToEnd();
            reader1.Close();
            //--------------------------------------SPLIT AND CONVERT------------------------------------------
            string[] lines = fileData.Split('\n');
            for (int j = 74; j < 100; j++)
            {
                string[] columns = lines[j].Split(',');

                for (int i = 0; i < WIDTH; i++)
                {
                    string column = columns[i];
                    levelData[j - 74, i] = int.Parse(column);
                }
            }

            //-------------------------------------READ THE NUMBERS, PLACE IN GAME-----------------------------
            for (int i = 0; i < HEIGHT; i++)
            {
                for (int j = 0; j < WIDTH; j++)
                {
                    switch (levelData[i, j])
                    {
                        case 270: //Coin
                            {
                                PickUpCoin thisobject = new PickUpCoin();
                                _midgroundLayer.AddChild(thisobject);
                                thisobject.SetXY(j * TILESIZE, i * TILESIZE);
                                thisobject.SetFrame(levelData[i, j] - 1);
                                objectList.Add(thisobject);
                                break;
                            }
                        case 269: //Reload
                            {
                                PickUpReload thisobject = new PickUpReload();
                                _midgroundLayer.AddChild(thisobject);
                                thisobject.SetXY(j * TILESIZE, i * TILESIZE);
                                thisobject.SetFrame(levelData[i, j] - 1);
                                objectList.Add(thisobject);
                                break;
                            }
                        case 267: //Weapon
                            {
                                PickUpWeapon thisobject = new PickUpWeapon();
                                _midgroundLayer.AddChild(thisobject);
                                thisobject.SetXY(j * TILESIZE, i * TILESIZE);
                                thisobject.SetFrame(levelData[i, j] - 1);
                                objectList.Add(thisobject);
                                break;
                            }
                    }
                   
                }
            }
        }
        private void drawSolidLayer()
        {
            int[,] levelData = new int[HEIGHT, WIDTH];
            //------------------------------------------READ FILE----------------------------------------------
            StreamReader reader1 = new StreamReader(_fileName);
            string fileData = reader1.ReadToEnd();
            reader1.Close();
            //--------------------------------------SPLIT AND CONVERT------------------------------------------
            string[] lines = fileData.Split('\n');
            for (int j = 104; j < 130; j++)
            {
                string[] columns = lines[j].Split(',');

                for (int i = 0; i < WIDTH; i++)
                {
                    string column = columns[i];
                    levelData[j - 104, i] = int.Parse(column);
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
                        _midgroundLayer.AddChild(thisobject);
                        thisobject.SetXY(j * TILESIZE, i * TILESIZE);
                        thisobject.SetFrame(levelData[i, j] - 1);
                        objectList.Add(thisobject);
                    }
                }
            }
        }
        private void drawDamageLayer()
        {
            int[,] levelData = new int[HEIGHT, WIDTH];
            //------------------------------------------READ FILE----------------------------------------------
            StreamReader reader1 = new StreamReader(_fileName);
            string fileData = reader1.ReadToEnd();
            reader1.Close();
            //--------------------------------------SPLIT AND CONVERT------------------------------------------
            string[] lines = fileData.Split('\n');
            for (int j = 134; j < 160; j++)
            {
                string[] columns = lines[j].Split(',');

                for (int i = 0; i < WIDTH; i++)
                {
                    string column = columns[i];
                    levelData[j - 134, i] = int.Parse(column);
                }
            }

            //-------------------------------------READ THE NUMBERS, PLACE IN GAME-----------------------------
            for (int i = 0; i < HEIGHT; i++)
            {
                for (int j = 0; j < WIDTH; j++)
                {
                    if (levelData[i, j] != 0)
                    {
                        DamageBlock thisobject = new DamageBlock();
                        _midgroundLayer.AddChild(thisobject);
                        thisobject.SetXY(j * TILESIZE, i * TILESIZE);
                        thisobject.SetFrame(levelData[i, j] - 1);
                        objectList.Add(thisobject);
                    }
                }
            }
        }
        private void drawForeGroundLayer()
        {
            int[,] levelData = new int[HEIGHT, WIDTH];
            //------------------------------------------READ FILE----------------------------------------------
            StreamReader reader1 = new StreamReader(_fileName);
            string fileData = reader1.ReadToEnd();
            reader1.Close();
            //--------------------------------------SPLIT AND CONVERT------------------------------------------
            string[] lines = fileData.Split('\n');
            for (int j = 164; j < 190; j++)
            {
                string[] columns = lines[j].Split(',');

                for (int i = 0; i < WIDTH; i++)
                {
                    string column = columns[i];
                    levelData[j - 164, i] = int.Parse(column);
                }
            }

            //-------------------------------------READ THE NUMBERS, PLACE IN GAME-----------------------------
            for (int i = 0; i < HEIGHT; i++)
            {
                for (int j = 0; j < WIDTH; j++)
                {
                    if (levelData[i, j] != 0)
                    {
                        BackgroundObject thisobject = new BackgroundObject();
                        _foregroundLayer.AddChild(thisobject);
                        thisobject.SetXY(j * TILESIZE, i * TILESIZE);
                        thisobject.SetFrame(levelData[i, j] - 1);
                    }
                }
            }
        }

    }
}
