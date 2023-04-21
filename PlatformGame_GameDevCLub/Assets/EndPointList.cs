using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndPointList : MonoBehaviour
{
    public static EndPointList instance { get; set; }
    [SerializeField] Vector3 posLevel2;
    [SerializeField] Vector3 posLevel3;

    private void Awake()
    {
        if (instance != null && instance != this) Destroy(this);
        else instance = this;
    }
    public void LoadNextScene(int scene)
    {
       if (scene < 3) SceneManager.LoadScene($"Map{scene+1}");
       else SceneManager.LoadScene($"Map3");

        DataPersistenceManager.instance.gameData.loadNewScene = 1;
        if (scene == 1) DataPersistenceManager.instance.gameData.currentMap = 2;
        else DataPersistenceManager.instance.gameData.currentMap = 3;
        DataPersistenceManager.instance.SaveGame();
    }
}
