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
    [SerializeField] string fileName;
    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("Found more than one Data Persistence Manager in the scene");
        }
        instance = this;
    }
    void Start()
    {
        _handler = new DataHandler("C:\\Users\\LENOVO\\Dropbox\\PC\\Desktop", fileName);
        dataObjects = FindAllDataPersistenceObjects();
        LoadGame();
    }
    public void NewGame()
    {
        gameData = new GameData();
    }
    public void LoadGame()
    {
        gameData = _handler.Load();
        if (gameData == null)
        {
            Debug.Log("No data was found from json. Initialzing data to defaults");
            NewGame();
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
