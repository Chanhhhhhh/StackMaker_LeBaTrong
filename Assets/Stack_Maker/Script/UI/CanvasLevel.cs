using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasLevel : UICanvas
{
    [SerializeField] private LevelBtn levelBtnP;
    [SerializeField] private RectTransform Content;


    public override void Setup()
    {
        base.Setup();
        foreach(Transform child in Content)
        {
            Destroy(child.gameObject);
        }
        for(int i = 0; i < LevelManager.Instance.CountLevel; i++)
        {
            LevelBtn level = Instantiate(levelBtnP, Content);
            level.OnInit(i);
        }
    }

    public void BackMainMenu()
    {
        this.CloseDirectly();
        GameManager.ChangeState(GameState.MainMenu);
    }
}
