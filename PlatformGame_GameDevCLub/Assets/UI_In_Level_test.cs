using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;


public class UI_In_Level_test : MonoBehaviour
{
    public static UI_In_Level_test instance { get; set; }
    private void Awake()
    {
        if (instance != null && instance != this) Destroy(this);
        else instance = this;
        gameObject.SetActive(false);
    }
    public void GameContinue()
    {
        gameObject.SetActive(false);
        GameManager.instance.Continue();
    }
    public void GamePause()
    {
        gameObject.SetActive(true);
        Time.timeScale = 0;
     //   GameManager.instance.Pause();
    }
    private void Start()
    {
        gameObject.SetActive(false);

    }
    
    public void Restart()
    {
        // SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        KnightStatic.instance.currHeal = KnightStatic.instance.maxHeal;
        Time.timeScale = 1;
        RespawnScript.instance.Respawn();
        gameObject.SetActive(false);
    }
   

} 
