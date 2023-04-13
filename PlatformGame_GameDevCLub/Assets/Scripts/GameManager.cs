using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance { get; private set; }
    [SerializeField] GameObject printer;
    private void Awake()
    {
        if (instance != null && instance != this) Destroy(this);
        else instance = this;
        printer.SetActive(false);
    }
    private void Start()
    {
      //  DontDestroyOnLoad(gameObject);
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
    public void PrintMessage(string msg)
    {
        printer.SetActive(true);
        printer.GetComponent<Print_Text>().PrintMessage(msg);
        StartCoroutine(DestroyPrinter());
       
    }
    IEnumerator DestroyPrinter()
    {
        for (int i = 0; i < 6; i++)
        {
            yield return new WaitForSeconds(1);
        }
        printer.SetActive(false);
    }
}
