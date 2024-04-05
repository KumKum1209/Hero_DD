using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Setting : UICanvas
{
    public void ContinueGame()
    {
        
        CloseDirectly();
    }
    public void OpenHTP()
    {
        UIManager.Ins.OpenUI<HTP>();
        UIManager.Ins.CloseUI<Setting>();
    }
}
