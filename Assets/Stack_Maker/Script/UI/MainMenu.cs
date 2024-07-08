using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : UICanvas
{
    public void OpenLevel() 
    {
        this.CloseDirectly();
        GameManager.ChangeState(GameState.Level);
    }
}
