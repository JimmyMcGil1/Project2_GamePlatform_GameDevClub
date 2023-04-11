using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance { get; private set; }
    private void Awake()
    {
        if (instance != null && instance != this) Destroy(this);
        else instance = this;
    }
    private void Start()
    {
        DontDestroyOnLoad(gameObject);
    }
    public void StartGame()
    {
        DataPersistenceManager.instance.LoadGame();
    }
    public void Continue()
    {
        Time.timeScale = 1; 
    }
    public void Pause()
    {
        Time.timeScale = 0;
    }
    public void GameOver()
    {
        Time.timeScale = 0;
    }
    public void Quit()
    {

        DataPersistenceManager.instance.SaveGame();
        Application.Quit();
    }
}
