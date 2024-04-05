using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : UICanvas
{
    
    public void PlayButton()
    {
        CloseDirectly();
        //manager.Goto(1);
        GameManager.instance.IsGame = true;
    }
}
