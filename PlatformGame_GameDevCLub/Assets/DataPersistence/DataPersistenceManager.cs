using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
public class DataPersistenceManager: MonoBehaviour
{
    public static DataPersistenceManager instance { get; private set; }
    public GameData gameData;
    private List<IDataPersistence> dataObjects { get; set; }
    private DataHandler _handler { get; set; }
    public string fileName;
    Vector3 startPoint;
    private void Awake()
    {
        if (instance != null && instance != this) Destroy(this);
        else instance = this;
        //fileName = "new_data.json";
        GameObject _startPoint = GameObject.FindGameObjectWithTag("StartPoint");
       if (_startPoint != null) startPoint = _startPoint.transform.position;
    }
    void Start()
    {
        //_handler = new DataHandler("C:\\Users\\LENOVO\\Dropbox\\PC\\Desktop", fileName);
        Debug.Log(fileName);
        _handler = new DataHandler(Application.persistentDataPath, fileName);
        dataObjects = FindAllDataPersistenceObjects();  
        LoadGame();
    }
    public void NewGame()
    {
        gameData = new GameData();
       if (startPoint != Vector3.zero) gameData.playerPos = startPoint;
    }
    public void LoadGame()
    {
        gameData = _handler.Load();
        if (gameData == null)
        {
            Debug.Log("No data was found from json. Initialzing data to defaults");
            NewGame();
        }
        if (gameData.loadNewScene == 1)
        {
            Debug.Log("Load new scene");
            gameData.playerPos = startPoint;
            gameData.loadNewScene = 0;
        }
        foreach (var item in dataObjects) 
        {
            Debug.Log(item);
            item.LoadData(gameData);
        }
    }
    public void SaveGame()
    {
        foreach (var item in dataObjects)
        {
            item.SaveData(ref gameData);
        }
        Debug.Log($"Save Current Hp = {gameData.HP}");
        _handler.Save(gameData);
    }
    private void OnApplicationQuit()
    {
        SaveGame();
    }
    private List<IDataPersistence> FindAllDataPersistenceObjects()
    {
        IEnumerable<IDataPersistence> dataPersistenceObjects = FindObjectsOfType<MonoBehaviour>().OfType<IDataPersistence>();
        return new List<IDataPersistence>(dataPersistenceObjects);
    }
}
