using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CanvasFinish : UICanvas
{
    [SerializeField] TextMeshProUGUI txt_Score;
    public override void Setup()
    {
        base.Setup();
        txt_Score.text = "Score : " + LevelManager.Instance.Score.ToString();
    }


    public void Retry()
    {
        this.CloseDirectly();
        GameManager.Instance.SelectLevel(LevelManager.Instance.currentLevel);
    }

    public  void Next()
    {
        this.CloseDirectly();
        GameManager.Instance.SelectLevel(LevelManager.Instance.currentLevel+1);

    }

    public void BackMainMenu()
    {
        this.CloseDirectly();
        GameManager.Instance.CloseLevel();
    }
}
