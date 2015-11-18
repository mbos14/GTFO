using System;
using GXPEngine;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class MyGame : Game
{
    private int _gameState = -1;
    public int oldGameState;

    Menu _menu;
    Level001 _level1;
    Level001 _level2;

    const int MENU = 0;
    const int LEVEL1 = 1;
    const int LEVEL2 = 2;
    const int LEVEL3 = 3;
    public MyGame() : base(1024, 768, false)
    {
        setGameState(LEVEL1);
    }
    public void setGameState(int pNumber)
    {
        if (pNumber == _gameState) return;
        destroyGameState(_gameState);
        oldGameState = _gameState;
        _gameState = pNumber;
        newGameState(pNumber);
    }
    private void newGameState(int pNumber)
    {
        switch (pNumber)
        {
            case 0:
                {
                    _menu = new Menu();
                    AddChild(_menu);
                    break;
                }
            case 1:
                {
                    _level1 = new Level001(this, "level1.txt");
                    AddChild(_level1);
                    break;
                }
            case 2:
                {
                    _level2 = new Level001(this, "level2.txt");
                    AddChild(_level2);
                    break;
                }
            case 3:
                {
                    break;
                }
        }
    }
    private void destroyGameState(int pNumber)
    {
        switch (pNumber)
        {
            case 0:
                {
                    
                    if (_menu != null) { _menu.Destroy(); }
                    break;
                }
            case 1:
                {
                    if (_level1 != null) { _level1.Destroy(); }
                    break;
                }
            case 2:
                {
                    if (_level2 != null) { _level2.Destroy(); }
                    break;
                }
            case 3:
                {
                    break;
                }
        }
    }
    static void Main()
    {
        new MyGame().Start();
    }
}

