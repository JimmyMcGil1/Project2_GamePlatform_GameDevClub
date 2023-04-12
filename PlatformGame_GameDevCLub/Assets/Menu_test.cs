using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Menu_test : MonoBehaviour
{
    public void StartGame()
    {
        SceneManager.LoadScene("Sample0");
    }
    public void Quit()
    {
        Application.Quit();
    }
}
