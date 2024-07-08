using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public enum GameState { MainMenu, Play, Level, Shop, Finish}
public class GameManager : Singleton<GameManager>
{
    public Camera CameraCanvas;
    private static GameState gameState;
    public static UnityEvent<GameState> OnGameStateChange;

    public int currentLevel;
    private void Awake()
    {
        Application.targetFrameRate = 60;
        ChangeState(GameState.MainMenu);
        UIManager.Instance.OpenUI<CanvasItem>();
    }
    public static void ChangeState(GameState state)
    {
        gameState = state;
        switch (gameState)
        {          
            case GameState.MainMenu:
                UIManager.Instance.OpenUI<MainMenu>();
                 break;
            case GameState.Level:
                UIManager.Instance.OpenUI<CanvasLevel>();
                break;
            case GameState.Play:                
                break;
            case GameState.Finish:
                UIManager.Instance.OpenUI<CanvasTreasure>(3f);
                LevelManager.Instance.FinishLevel();
                break;
            default: 
                break;
        }

        OnGameStateChange?.Invoke(state);
    }
    public static bool IsState(GameState state)
    {
        return gameState == state;
    }
    public void SelectLevel(int level)
    {
        UIManager.Instance.CloseUI<CanvasLevel>();
        UIManager.Instance.CloseUI<CanvasFinish>();
        ChangeState(GameState.Play);
        LevelManager.Instance.OnPlayGame(level);

    }

    public void CloseLevel()
    {
        ChangeState(GameState.MainMenu);
        LevelManager.Instance.CloseLevel();
    }
}
