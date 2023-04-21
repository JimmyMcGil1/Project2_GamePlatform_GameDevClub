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
        SceneManager.LoadScene($"Map{scene+1}");
        switch (scene)
        {
            case 1:
                DataPersistenceManager.instance.gameData.loadNewScene = 1;
                DataPersistenceManager.instance.SaveGame();
                break;
            case 2:
                DataPersistenceManager.instance.gameData.loadNewScene = 1;
                DataPersistenceManager.instance.SaveGame();
                break;
            default: Debug.Log("Cannot load scene");
                break;
        }
    }
}
