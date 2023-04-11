using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        GameManager.instance.Pause();
    }
    
} 
