using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class LevelManager : Singleton<LevelManager>
{
    [SerializeField] private Player player;
    [SerializeField] private TextAsset[] textMaps;
    private int[,] Grid;
    private int score;
    public int Score => score;

    public int CountLevel => textMaps.Length;
    public int totalBrick = 0;
    public int currentLevel;
    public WinPos currentWinPos;

    public void OnInit()
    {
        score = 0;
        totalBrick = 0;
        player.TF.position = Generator.Instance.StartPos;
        player.OnInit();
        player.gameObject.SetActive(true);
    }
    internal void FinishLevel()
    {
        if(currentLevel == SaveManager.Instance.UnlockLevel)
        {
            SaveManager.Instance.UnlockLevel++;
        }
    }

    public void CloseLevel()
    {
        SimplePool.CollectAll();
        player.OnDespawn();
    }

    public void CreateLevel(int level)
    {
        SimplePool.CollectAll();
        currentLevel = level;
        string[] lines = textMaps[level].text.Split(new char[] {'\n' }, StringSplitOptions.RemoveEmptyEntries);
        int rowCount = lines.Length;
        int colCount = lines[0].Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries).Length;

        this.Grid = new int[rowCount, colCount];

        for (int i = 0; i < rowCount; i++)
        {
            string[] values = lines[i].Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            for (int j = 0; j < colCount; j++)
            {
                int.TryParse(values[j], out Grid[i, j]);
                //Debug.Log(Grid[i, j]);

            }
        }
        Generator.Instance.InitMap(this.Grid);
        OnInit();
    }

    public void UpDateScore()
    {
        totalBrick++;
        score++;
    }
}

