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
        int countLevel = LevelManager.Instance.levels.Count;
        for(int i = 0; i < countLevel; i++)
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
