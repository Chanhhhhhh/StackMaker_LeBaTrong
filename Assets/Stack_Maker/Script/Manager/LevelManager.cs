using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelManager : Singleton<LevelManager>
{
    public List<Level> levels = new List<Level>();
    [SerializeField] Player player;
    public GameObject BrickFill;
    public int totalBrick = 0;
    public int CountLevel = 10;
    public int currentLevel;
    public Vector3 playerTF;
    public Level CurrentLevel;
    private void Awake()
    {

    }
    internal void OnPlayGame(int level)
    {
        SimplePool.CollectAll();
        currentLevel = level;
        totalBrick = 0;
        if(CurrentLevel != null)
        {
            Destroy(CurrentLevel.gameObject);

        }
        CurrentLevel = Instantiate(levels[level], Vector3.zero, Quaternion.identity);
        player.TF.position = CurrentLevel.StartPoint.position;
        player.OnInit();
        player.gameObject.SetActive(true);
    }

    internal void FinishLevel()
    {
        CurrentLevel.WinGame();
        if(currentLevel == SaveManager.Instance.UnlockLevel)
        {
            SaveManager.Instance.UnlockLevel++;
        }
    }

    public void CloseLevel()
    {
        SimplePool.CollectAll();
        Destroy(CurrentLevel.gameObject);
        player.OnDespawn();
    }
}
