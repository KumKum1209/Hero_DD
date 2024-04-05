using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HTP : UICanvas
{
    public void ReturnMenu()
    {
        CloseDirectly();
        UIManager.Ins.OpenUI<MainMenu>();
    }
    
}
