using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : UICanvas
{
    public void OpenShop()
    {
        UIManager.Ins.OpenUI<Shop>();
        UIManager.Ins.CloseUI<MainMenu>();
    }
    public void ReturnMenu()
    {
        CloseDirectly();
        UIManager.Ins.OpenUI<MainMenu>();
    }
}
