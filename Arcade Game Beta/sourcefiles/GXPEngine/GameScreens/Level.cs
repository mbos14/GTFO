using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Drawing;

namespace GXPEngine
{
    public class Level : GameObject
    {
        //Lists
        public List<SolidObject> solidList = new List<SolidObject>();
        public List<DamageBlock> damageList = new List<DamageBlock>();
        public List<Enemy> enemyList = new List<Enemy>();

        //Data
        public Player player;
        public MyGame thisgame;
        private string _fileName;
        private Drawer drawer = new Drawer(1024, 100);

        //Variables for tile- and levelsize
        const int TILESY = 50; //Amount of tiles vertical
        const int TILESX = 100; //Amount of tiles horizontal
        const int TILESIZE = 32;

        const int BETWEENLAYERS = 4;
        const int TILEDINFO = 13;

        //Pivots
        private Pivot _backgroundLayer = new Pivot();
        private Pivot _midgroundLayer = new Pivot();
        private Pivot _foregroundLayer = new Pivot();
        public Pivot hudLayer = new Pivot();
        public Level(MyGame pGame, string pFileName)
        {
            _fileName = pFileName;
            thisgame = pGame;
            thisgame.playerScore = 0;

            addPivots();
            drawAll();

            Camera cam1 = new Camera(player, this);
            AddChild(cam1);
        }
        void Update()
        {
            drawHUD();
        }
        //Drawfunctions
        private void drawAll()
        {
            drawBackGroundLayer();
            drawbackGroundObjectLayer();
            drawSolidLayer();
            drawDamageLayer();
            drawPreDefinedLayer();
            drawPlayer();
            drawForeGroundLayer();
        }
        private void addPivots()
        {
            AddChild(_backgroundLayer);
            AddChild(_midgroundLayer);
            AddChild(_foregroundLayer);
            AddChild(hudLayer);
        }
        private void drawPlayer()
        {
            player = new Player(this);
            _midgroundLayer.AddChild(player);
            player.spawnX = 100;
            player.spawnY = 540;
            player.SetXY(player.spawnX, player.spawnY);
        }
        private void drawHUD()
        {
            string message = "Score: " + thisgame.playerScore;
            string message2 = "Lives: " + player.lives;
            string message3 = "FPS: " + thisgame.currentFps;

            hudLayer.AddChild(drawer);
            PointF pos1 = new PointF(0, 20);
            PointF pos2 = new PointF(thisgame.width - 150, 20);
            PointF pos3 = new PointF(thisgame.width - 150, 60);

            drawer.DrawText(message, pos1);
            drawer.DrawText(message2, pos2);
            drawer.DrawText(message3, pos3);
        }
        //Layers
        private void drawBackGroundLayer()
        {
            int startNr = TILEDINFO;
            int endNr = startNr + TILESY;

            int[,] levelData = new int[TILESY, TILESX];
            //------------------------------------------READ FILE----------------------------------------------
            StreamReader reader1 = new StreamReader(_fileName);
            string fileData = reader1.ReadToEnd();
            reader1.Close();
            //--------------------------------------SPLIT AND CONVERT------------------------------------------
            string[] lines = fileData.Split('\n');
            for (int j = 13; j < 63; j++)
            {
                string[] columns = lines[j].Split(',');

                for (int i = 0; i < TILESX; i++)
                {
                    string column = columns[i];
                    levelData[j - 13, i] = int.Parse(column);
                }
            }

            //-------------------------------------READ THE NUMBERS, PLACE IN GAME-----------------------------
            for (int i = 0; i < TILESY; i++)
            {
                for (int j = 0; j < TILESX; j++)
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
            int startNr = TILEDINFO + BETWEENLAYERS + TILESY;
            int endNr = startNr + TILESY;

            int[,] levelData = new int[TILESY, TILESX];
            //------------------------------------------READ FILE----------------------------------------------
            StreamReader reader1 = new StreamReader(_fileName);
            string fileData = reader1.ReadToEnd();
            reader1.Close();
            //--------------------------------------SPLIT AND CONVERT------------------------------------------
            string[] lines = fileData.Split('\n');
            for (int j = startNr; j < endNr; j++)
            {
                string[] columns = lines[j].Split(',');

                for (int i = 0; i < TILESX; i++)
                {
                    string column = columns[i];
                    levelData[j - startNr, i] = int.Parse(column);
                }
            }

            //-------------------------------------READ THE NUMBERS, PLACE IN GAME-----------------------------
            for (int i = 0; i < TILESY; i++)
            {
                for (int j = 0; j < TILESX; j++)
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
        private void drawPreDefinedLayer()
        {
            int startNr = TILEDINFO + BETWEENLAYERS * 2 + TILESY * 2;
            int endNr = startNr + TILESY;

            int[,] levelData = new int[TILESY, TILESX];
            //------------------------------------------READ FILE----------------------------------------------
            StreamReader reader1 = new StreamReader(_fileName);
            string fileData = reader1.ReadToEnd();
            reader1.Close();
            //--------------------------------------SPLIT AND CONVERT------------------------------------------
            string[] lines = fileData.Split('\n');
            for (int j = startNr; j < endNr; j++)
            {
                string[] columns = lines[j].Split(',');

                for (int i = 0; i < TILESX; i++)
                {
                    string column = columns[i];
                    levelData[j - startNr, i] = int.Parse(column);
                }
            }

            //-------------------------------------READ THE NUMBERS, PLACE IN GAME-----------------------------
            for (int i = 0; i < TILESY; i++)
            {
                for (int j = 0; j < TILESX; j++)
                {
                    int tile = levelData[i, j];

                    switch (tile)
                    {
                        case 1: //Reload
                            {
                                PickUpReload thisobject = new PickUpReload(this);
                                _midgroundLayer.AddChild(thisobject);
                                thisobject.SetXY(j * TILESIZE, i * TILESIZE);
                                break;
                            }
                        case 2: //Life
                            {
                                PickUpLife thisobject = new PickUpLife(this);
                                _midgroundLayer.AddChild(thisobject);
                                thisobject.SetXY(j * TILESIZE, i * TILESIZE);
                                break;
                            }
                        case 3: //Coin
                            {
                                PickUpCoin thisobject = new PickUpCoin(this);
                                _midgroundLayer.AddChild(thisobject);
                                thisobject.SetXY(j * TILESIZE, i * TILESIZE);
                                break;
                            }
                        case 4: //EnemySpider
                            {
                                EnemySpider thisenemy = new EnemySpider(this);
                                _midgroundLayer.AddChild(thisenemy);
                                thisenemy.SetXY(j * TILESIZE, i * TILESIZE);
                                enemyList.Add(thisenemy);
                                break;
                            }
                        case 5: //Enemy bug Horizontal
                            {
                                EnemyBugHorizontal thisenemy = new EnemyBugHorizontal(this);
                                _midgroundLayer.AddChild(thisenemy);
                                thisenemy.SetXY(j * TILESIZE, i * TILESIZE);
                                enemyList.Add(thisenemy);
                                break;
                            }
                        case 6: //Enemy bug Vertical
                            {
                                EnemyBugVertical thisenemy = new EnemyBugVertical(this);
                                _midgroundLayer.AddChild(thisenemy);
                                thisenemy.SetXY(j * TILESIZE, i * TILESIZE);
                                enemyList.Add(thisenemy);
                                break;
                            }
                        case 7: //EnemyFloater
                            {
                                EnemyFloater thisenemy = new EnemyFloater(this);
                                _midgroundLayer.AddChild(thisenemy);
                                thisenemy.SetXY(j * TILESIZE, i * TILESIZE);
                                enemyList.Add(thisenemy);
                                break;
                            }
                        case 8: //Weapon
                            {
                                PickUpWeapon thisobject = new PickUpWeapon(this);
                                _midgroundLayer.AddChild(thisobject);
                                thisobject.SetXY(j * TILESIZE, i * TILESIZE);
                                break;
                            }

                        case 9: //Invisible block of doom
                            {
                                InvisBlock thisobject = new InvisBlock(this);
                                _midgroundLayer.AddChild(thisobject);
                                thisobject.SetXY(j * TILESIZE, i * TILESIZE);
                                break;
                            }
                    }

                }
            }
        }
        private void drawSolidLayer()
        {
            int startNr = TILEDINFO + BETWEENLAYERS * 3+ TILESY * 3;
            int endNr = startNr + TILESY;

            int[,] levelData = new int[TILESY, TILESX];
            //------------------------------------------READ FILE----------------------------------------------
            StreamReader reader1 = new StreamReader(_fileName);
            string fileData = reader1.ReadToEnd();
            reader1.Close();
            //--------------------------------------SPLIT AND CONVERT------------------------------------------
            string[] lines = fileData.Split('\n');
            for (int j = startNr; j < endNr; j++)
            {
                string[] columns = lines[j].Split(',');

                for (int i = 0; i < TILESX; i++)
                {
                    string column = columns[i];
                    levelData[j - startNr, i] = int.Parse(column);
                }
            }

            //-------------------------------------READ THE NUMBERS, PLACE IN GAME-----------------------------
            for (int i = 0; i < TILESY; i++)
            {
                for (int j = 0; j < TILESX; j++)
                {
                    if (levelData[i, j] != 0)
                    {
                        SolidObject thisobject = new SolidObject();
                        _midgroundLayer.AddChild(thisobject);
                        thisobject.SetXY(j * TILESIZE, i * TILESIZE);
                        thisobject.SetFrame(levelData[i, j] - 1);
                        solidList.Add(thisobject);
                    }
                }
            }
        }
        private void drawDamageLayer()
        {
            int startNr = TILEDINFO + BETWEENLAYERS * 4 + TILESY * 4;
            int endNr = startNr + TILESY;

            int[,] levelData = new int[TILESY, TILESX];
            //------------------------------------------READ FILE----------------------------------------------
            StreamReader reader1 = new StreamReader(_fileName);
            string fileData = reader1.ReadToEnd();
            reader1.Close();
            //--------------------------------------SPLIT AND CONVERT------------------------------------------
            string[] lines = fileData.Split('\n');
            for (int j = startNr; j < endNr; j++)
            {
                string[] columns = lines[j].Split(',');

                for (int i = 0; i < TILESX; i++)
                {
                    string column = columns[i];
                    levelData[j - startNr, i] = int.Parse(column);
                }
            }

            //-------------------------------------READ THE NUMBERS, PLACE IN GAME-----------------------------
            for (int i = 0; i < TILESY; i++)
            {
                for (int j = 0; j < TILESX; j++)
                {
                    if (levelData[i, j] != 0)
                    {
                        DamageBlock thisobject = new DamageBlock();
                        _midgroundLayer.AddChild(thisobject);
                        thisobject.SetXY(j * TILESIZE, i * TILESIZE);
                        thisobject.SetFrame(levelData[i, j] - 1);
                        damageList.Add(thisobject);
                    }
                }
            }
        }
        private void drawForeGroundLayer()
        {
            int startNr = TILEDINFO + BETWEENLAYERS * 5 + TILESY * 5;
            int endNr = startNr + TILESY;

            int[,] levelData = new int[TILESY, TILESX];
            //------------------------------------------READ FILE----------------------------------------------
            StreamReader reader1 = new StreamReader(_fileName);
            string fileData = reader1.ReadToEnd();
            reader1.Close();
            //--------------------------------------SPLIT AND CONVERT------------------------------------------
            string[] lines = fileData.Split('\n');
            for (int j = startNr; j < endNr; j++)
            {
                string[] columns = lines[j].Split(',');

                for (int i = 0; i < TILESX; i++)
                {
                    string column = columns[i];
                    levelData[j - startNr, i] = int.Parse(column);
                }
            }

            //-------------------------------------READ THE NUMBERS, PLACE IN GAME-----------------------------
            for (int i = 0; i < TILESY; i++)
            {
                for (int j = 0; j < TILESX; j++)
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
        //Collisions
        public bool CheckCollision(Sprite pSprite)
        {
            //Solid objects            
            foreach (Sprite other in solidList)
            {
                if (pSprite.HitTest(other))
                {
                    return true;
                }
            }
            //Damage blocks
            foreach (Sprite other in damageList)
            {
                if (pSprite.HitTest(other))
                {
                    if (pSprite is Player)
                    {
                        Player thisplayer = (Player)pSprite;
                        thisplayer.playerDIE();
                    }
                    else if (pSprite is PlayerBullet)
                    {
                        pSprite.Destroy();
                    }
                }
            }
            //Enemies
            foreach (Sprite other in enemyList)
            {
                if (pSprite.HitTest(other))
                {
                    if (pSprite is Player)
                    {
                        Player thisplayer = (Player)pSprite;
                        thisplayer.playerDIE();
                    }
                    else if (pSprite is PlayerBullet)
                    {
                        //Pass through data: bullet damage, 
                        pSprite.Destroy();
                    }
                }
            }
            return false;
        }
    }
}