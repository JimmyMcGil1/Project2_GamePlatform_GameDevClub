using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    private void Awake()
    {
        
    }
    public void NewGame()
    {
       // DataPersistenceManager.instance.fileName = $"data{Random.Range(0, 10000)}.json";
        SceneManager.LoadScene("Map1");
    }
    public void Continue()
    {
        GameManager.instance.StartGame();
        switch (DataPersistenceManager.instance.gameData.currentMap)
        {
            case 1:
                SceneManager.LoadScene("Map1");
                break;
            case 2:
                SceneManager.LoadScene("Map2");
                break;
            case 3: 
                SceneManager.LoadScene("Map3");
                break;
            default:
                break;
        }
    }
    public void Quit()
    {
        Application.Quit();
    }
}
