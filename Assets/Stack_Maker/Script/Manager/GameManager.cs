using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public enum GameState { MainMenu, GamePlay, Finish, Lose }
public class GameManager : Singleton<GameManager>
{
    private static GameState gameState;
    public static UnityEvent<GameState> OnGameStateChange;
    private void Awake()
    {
        Application.targetFrameRate = 60;        
    }
    public static void ChangeState(GameState state)
    {
        gameState = state;
        switch (gameState)
        {           
            default:
                break;
        }

        OnGameStateChange?.Invoke(state);
    }
    public static bool IsState(GameState state)
    {
        return gameState == state;
    }

    //public void Increase()
    //{
    //    Diamond += Random.Range(100, 500);
    //    PlayerPrefs.SetInt("Diamond", Diamond);
    //    PlayerPrefs.Save();
    //}

}
