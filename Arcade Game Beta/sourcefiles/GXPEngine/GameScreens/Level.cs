using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Drawing;
using GXPEngine.Core;

namespace GXPEngine
{
    public class Level : GameObject
    {
        //Lists
        public List<SolidObject> solidList = new List<SolidObject>();
        public List<DamageBlock> damageList = new List<DamageBlock>();
        public List<Enemy> enemyList = new List<Enemy>();

        public List<EnemyBullet> enemyBulletList = new List<EnemyBullet>();

        //Data
        public Player player;
        public MyGame thisgame;
        private string _fileName;
        public Drawer drawer = new Drawer(1024, 100);
        public int levelPart;

        private bool _levelDrawed = false;

        //Variables for tile- and levelsize
        const int TILESY = 50; //Amount of tiles vertical
        const int TILESX = 100; //Amount of tiles horizontal
        const int TILESIZE = 64;

        const int BETWEENLAYERS = 4;
        const int TILEDINFO = 13;

        //Pivots
        private Pivot _backgroundLayer = new Pivot();
        private Pivot _midgroundLayer = new Pivot();
        private Pivot _foregroundLayer = new Pivot();
        public Pivot hudLayer = new Pivot();
        public Level(MyGame pGame, string pFileName, int pLevelPart)
        {
            levelPart = pLevelPart;
            _fileName = pFileName;
            thisgame = pGame;
            thisgame.playerScore = 0;

            addPivots();
            drawAll();

            Camera cam1 = new Camera(player, this);
            AddChild(cam1);
        }
        protected override Collider createCollider()
        {
            return null;
        }
        //Drawfunctions
        private void drawAll()
        {
            drawBackGroundLayer();
            drawbackGroundObjectLayer();
            drawSolidLayer();
            drawDamageLayer();
            DrawPreDefinedLayer();
            drawPlayer();
            drawForeGroundLayer();

            LevelHUD hud = new LevelHUD(this);
            hudLayer.AddChild(hud);
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
            player.spawnY = 1250;
            player.SetXY(player.spawnX, player.spawnY);
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
        public void DrawPreDefinedLayer()
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
                                if (!_levelDrawed)
                                {
                                    PickUpLife thisobject = new PickUpLife(this);
                                    _midgroundLayer.AddChild(thisobject);
                                    thisobject.SetXY(j * TILESIZE, i * TILESIZE);
                                }
                                break;
                            }
                        case 3: //Coin
                            {
                                if (!_levelDrawed)
                                {
                                    PickUpCoin thisobject = new PickUpCoin(this);
                                    _midgroundLayer.AddChild(thisobject);
                                    thisobject.SetXY(j * TILESIZE, i * TILESIZE);
                                }
                                break;
                            }
                        case 4: //EnemySpider
                            {
                                if (!_levelDrawed)
                                {
                                    EnemySpider thisenemy = new EnemySpider(this);
                                    _midgroundLayer.AddChild(thisenemy);
                                    thisenemy.SetXY(j * TILESIZE, i * TILESIZE);
                                    enemyList.Add(thisenemy);
                                }
                                break;
                            }
                        case 5: //Enemy bug Horizontal
                            {
                                if (!_levelDrawed)
                                {
                                    EnemyBugHorizontal thisenemy = new EnemyBugHorizontal(this, i * TILESIZE);
                                    _midgroundLayer.AddChild(thisenemy);
                                    thisenemy.SetXY(j * TILESIZE, i * TILESIZE);
                                    enemyList.Add(thisenemy);
                                }
                                break;
                            }
                        case 6: //Enemy bug Vertical
                            {
                                if (!_levelDrawed)
                                {
                                    EnemyBugVertical thisenemy = new EnemyBugVertical(this);
                                    _midgroundLayer.AddChild(thisenemy);
                                    thisenemy.SetXY(j * TILESIZE, i * TILESIZE);
                                    enemyList.Add(thisenemy);
                                }
                                break;
                            }
                        case 7: //EnemyFloater
                            {
                                if (!_levelDrawed)
                                {
                                    EnemyFloater thisenemy = new EnemyFloater(this);
                                    _midgroundLayer.AddChild(thisenemy);
                                    thisenemy.SetXY(j * TILESIZE, i * TILESIZE);
                                    enemyList.Add(thisenemy);
                                }
                                break;
                            }
                        case 8: //Weapon
                            {
                                if (!_levelDrawed)
                                {
                                    PickUpWeapon thisobject = new PickUpWeapon(this);
                                    _midgroundLayer.AddChild(thisobject);
                                    thisobject.SetXY(j * TILESIZE, i * TILESIZE);
                                }
                                break;
                            }

                        case 9: //Invisible block of doom
                            {
                                if (!_levelDrawed)
                                {
                                    InvisBlock thisobject = new InvisBlock(this);
                                    _midgroundLayer.AddChild(thisobject);
                                    thisobject.SetXY(j * TILESIZE, i * TILESIZE);
                                    thisobject.SetFrame(193);
                                }
                                break;
                            }
                    }

                }
            }
            _levelDrawed = true;
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
                        DrawPreDefinedLayer();
                    }
                    else if (pSprite is PlayerBullet)
                    {
                        pSprite.Destroy();
                    }
                }
            }
            //Enemies
            foreach (Enemy other in enemyList)
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
                        PlayerBullet bullet = (PlayerBullet)pSprite;
                        other.HitByBullet(bullet.damage / 8, player.aimDirection);
                        pSprite.Destroy();
                    }
                }
            }
            return false;
        }
    }
}