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
        public List<GameObject> objectList = new List<GameObject>();
        public List<DamageBlock> damageList = new List<DamageBlock>();
        public List<PickUp> pickUpList = new List<PickUp>();
        public List<Enemy> enemyList = new List<Enemy>();

        //Data
        private Player _player;
        public MyGame thisgame;
        private string _fileName;
        private Drawer drawer = new Drawer();
        
        //Variables for tile- and levelsize
        const int HEIGHT = 26;
        const int WIDTH = 60;
        const int TILESIZE = 32;

        //Pivots
        private Pivot _backgroundLayer = new Pivot();
        private Pivot _midgroundLayer = new Pivot();
        public Pivot hudLayer = new Pivot();
        public Level(MyGame pGame, string pFileName)
        {
            _fileName = pFileName;
            thisgame = pGame;

            addPivots();
            drawAll();

            Camera cam1 = new Camera(_player, this);
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
            drawPickUpLayer();
            drawPlayer();
            drawForeGroundLayer();
        }
        private void addPivots()
        {
            AddChild(_backgroundLayer);
            AddChild(_midgroundLayer);
            AddChild(hudLayer);
        }
        private void drawPlayer()
        {
            _player = new Player(this);
            _midgroundLayer.AddChild(_player);
            _player.spawnX = 100;
            _player.spawnY = 540;
            _player.SetXY(_player.spawnX, _player.spawnY);
        }
        private void drawHUD()
        {
            string message = "Score: " + thisgame.playerScore;
            string message2 = "Lives: " + _player.lives;
            AnimationSprite animsprite = new AnimationSprite("chargebar.png", 3, 1);
            
            hudLayer.AddChild(drawer);
            PointF pos1 = new PointF(0, 20);
            PointF pos2 = new PointF(thisgame.width - 150, 20);
            PointF pos3 = new PointF(thisgame.width / 2, 20);

            drawer.DrawText(message, pos1);
            drawer.DrawText(message2, pos2);
            drawer.DrawSprite(animsprite, pos3);
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
                                pickUpList.Add(thisobject);
                                break;
                            }
                        case 269: //Reload
                            {
                                PickUpReload thisobject = new PickUpReload();
                                _midgroundLayer.AddChild(thisobject);
                                thisobject.SetXY(j * TILESIZE, i * TILESIZE);
                                thisobject.SetFrame(levelData[i, j] - 1);
                                pickUpList.Add(thisobject);
                                break;
                            }
                        case 267: //Weapon
                            {
                                PickUpWeapon thisobject = new PickUpWeapon();
                                _midgroundLayer.AddChild(thisobject);
                                thisobject.SetXY(j * TILESIZE, i * TILESIZE);
                                thisobject.SetFrame(levelData[i, j] - 1);
                                pickUpList.Add(thisobject);
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
                        damageList.Add(thisobject);
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
                        _midgroundLayer.AddChild(thisobject);
                        thisobject.SetXY(j * TILESIZE, i * TILESIZE);
                        thisobject.SetFrame(levelData[i, j] - 1);
                    }
                }
            }
        }
        //Collisions
        public bool CheckCollision(Sprite pSprite)
        {
            //Player collisions
            if (pSprite is Player)
            {
                Player thisplayer = (Player)pSprite;
                PlayerPickUps(thisplayer);
            }
            //Solid objects            
            foreach (Sprite other in objectList)
            {
                if (pSprite.HitTest(other))
                {
                    if (other is SolidObject)
                    {
                        return true;
                    }
                }
            }
            //Damage blocks
            foreach (Sprite other in damageList)
            {
                if (pSprite.HitTest(other))
                {
                    if (other is DamageBlock)
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
            }
            return false;
        }
        private void PlayerPickUps(Player pPlayer)
        {
            foreach (Sprite other in pickUpList)
            {
                if (pPlayer.HitTest(other))
                {
                    if (other is PickUpCoin)
                    {
                        other.x = -100;
                        _player.addPoints(10);
                        other.Destroy();
                    }
                    else if (other is PickUpLife)
                    {
                        //Pickuplife
                        other.Destroy();
                    }
                    else if (other is PickUpReload)
                    {
                        if (pPlayer.bulletCounter < 2)
                        {
                            pPlayer.bulletCounter = 2;
                            other.Destroy();
                        }
                        //Pickupreload
                    }
                    else if (other is PickUpWeapon)
                    {
                        MyGame.playerHasWeapon = true;
                        _player.hasWeapon = true;
                        other.Destroy();
                    }
                }
            }
        }
    }
}