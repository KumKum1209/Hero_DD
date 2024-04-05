using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : GOSingleton<GameManager>
{
    public static GameManager instance;
    public bool IsGame;
    public HeroController hero;
    //public static GameManager gameManager { get; private set; }
    private void Start()
    {
        if (instance == null)
        {
            instance = this;
        }
        UIManager.Ins.OpenUI<MainMenu>();       
    }
    public void Goto(int level)
    {
        SceneManager.LoadScene("Level " + level.ToString());
        
    }
    
    public void QuitGame()
    {
        Application.Quit();
    }
    public HeroController GetHero()
    {
        return hero;
    }
    public EnemyBaseController GetEnemy()
    {
        return FindObjectOfType<EnemyBaseController>();
    }
    public CharacterController GetCharacter()
    {
        return FindObjectOfType<CharacterController>();
    }


}
