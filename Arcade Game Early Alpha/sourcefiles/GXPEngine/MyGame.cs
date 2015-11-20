using System;
using GXPEngine;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class MyGame : Game
{
    private GameStates _state;
    public GameStates oldGameState;
    public static bool playerHasWeapon = false;

    Menu _menu;
    Level _level;
    //private EndScreen _endscreen;
    public MyGame() : base(1024, 768, false)
    {
        setGameState(GameStates.level1);
    }
    public void setGameState(GameStates pState)
    {
        if (pState == _state) return;
        destroyGameState(_state);
        oldGameState = _state;
        _state = pState;
        newGameState(pState);
    }
    private void newGameState(GameStates pState)
    {
        switch (pState)
        {
            case GameStates.menu:
                {
                    _menu = new Menu();
                    AddChild(_menu);
                    break;
                }
            case GameStates.level1:
                {
                    _level = new Level(this, "level1prototype.txt");
                    AddChild(_level);
                    break;
                }
            case GameStates.endscreen:
                {
                    //_endscreen = new EndScreen();
                    //AddChild(_endscreen);
                    break;
                }
        }
    }
    private void destroyGameState(GameStates pState)
    {
        switch (pState)
        {
            case GameStates.menu:
                {
                    if (_menu != null) { _menu.Destroy(); }
                    break;
                }
            case GameStates.level1:
                {
                    if (_level != null) { _level.Destroy(); }
                    break;
                }
            case GameStates.endscreen:
                {
                    //if (_endscreen != null) { _endscreen.Destroy(); }
                    break;
                }
        }
    }
    static void Main()
    {
        new MyGame().Start();
    }
}