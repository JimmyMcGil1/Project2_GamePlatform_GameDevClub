using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance { get; private set; }
    GameObject UI_In_Level;
    [SerializeField] GameObject printer;
    private void Awake()
    {
        if (instance != null && instance != this) Destroy(this);
        else instance = this;
        UI_In_Level = gameObject.transform.Find("UI_In_Level").gameObject;
        printer.SetActive(false);
        GameObject[] objs = GameObject.FindGameObjectsWithTag("GameController");
     // if (objs.Length > 1) Destroy(gameObject);
     //   DontDestroyOnLoad(gameObject);

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
        gameObject.SetActive(true);
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
    public void PrintMessage(string msg, float duration)
    {
        printer.SetActive(true);
        printer.GetComponent<Print_Text>().PrintMessage(msg);
        StartCoroutine(DestroyPrinter(duration));
       
    }
    IEnumerator DestroyPrinter(float sec)
    {
        for (int i = 0; i < 1; i++)
        {
            yield return new WaitForSeconds(sec);
        }
        printer.SetActive(false);
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            UI_In_Level_test.instance.GamePause();
        }
    }
    public void GameOverFame()
    {
        GameManager.instance.PrintMessage("Dead", 2);
        StartCoroutine(StartCountDownRespawn(2));

    }
    IEnumerator StartCountDownRespawn(float sec)
    {
        for (int i = 0; i < 1; i++)
        {
            yield return new WaitForSeconds(sec);
        }

        RespawnScript.instance.Respawn();
        
        
    }
}
