using System;
using GXPEngine;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class MyGame : Game
{
    //GameState
    private GameStates _state = GameStates.@default;

    //Gameobjects
    private Menu _menu;
    private Level _part1;
    private Level _part2;
    private Level _part3;
    private Endscreen _endscreen;
    private NameInput _nameinput;

    public static bool playerHasWeapon = false;
    public int playerScore = 0;
    public bool levelWon = false;

    public MyGame() : base(1024, 768, false)
    {
        setGameState(GameStates.menu);
    }
    public void setGameState(GameStates pState)
    {
        if (pState == _state) return;
        destroyGameState(_state);
        _state = pState;
        newGameState(pState);
    }
    private void newGameState(GameStates pState)
    {
        switch (pState)
        {
            case GameStates.menu:
                {
                    _menu = new Menu(this);
                    AddChild(_menu);
                    break;
                }
            case GameStates.part1:
                {
                    _part1 = new Level(this, "part1.txt", 1);
                    AddChild(_part1);
                    break;
                }
            case GameStates.part2:
                {
                    _part2 = new Level(this, "part2.txt", 2);
                    AddChild(_part2);
                    break;
                }
            case GameStates.part3:
                {
                    _part3 = new Level(this, "part3.txt", 3);
                    AddChild(_part3);
                    break;
                }
            case GameStates.nameinput:
                {
                    _nameinput = new NameInput(_part1);
                    AddChild(_nameinput);
                    break;
                }
            case GameStates.endscreen:
                {
                    _endscreen = new Endscreen(this);
                    AddChild(_endscreen);
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
            case GameStates.part1:
                {
                    if (_part1 != null) { _part1.Destroy(); }
                    break;
                }
            case GameStates.part2:
                {
                    if (_part2 != null) { _part2.Destroy(); }
                    break;
                }
            case GameStates.part3:
                {
                    if (_part3 != null) { _part3.Destroy(); }
                    break;
                }
            case GameStates.nameinput:
                {
                    if (_nameinput != null) { _nameinput.Destroy(); }
                    break;
                }
            case GameStates.endscreen:
                {
                    if (_endscreen != null) { _endscreen.Destroy(); }
                    break;
                }

        }
    }
    static void Main()
    {
        new MyGame().Start();
    }
}