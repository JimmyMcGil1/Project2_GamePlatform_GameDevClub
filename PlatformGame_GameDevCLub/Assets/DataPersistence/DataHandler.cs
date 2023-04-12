using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;
public class DataHandler : MonoBehaviour
{
    private string dirPath = "";
    private string fileName = "";
    public DataHandler(string _dirPath, string _fileName)
    {
        dirPath = _dirPath;
        fileName = _fileName;
    }
    public GameData Load()
    {
        string fullPath = Path.Combine(dirPath, fileName);
        GameData loadedData = null;
        if (File.Exists(fullPath))
        {
            try
            {
                string dataToLoad = "";
                using (FileStream stream = new FileStream(fullPath, FileMode.Open))
                {
                    using (StreamReader reader = new StreamReader(stream))
                    {
                        dataToLoad = reader.ReadToEnd();
                    }
                }
                loadedData = JsonUtility.FromJson<GameData>(dataToLoad);
            }
            catch (System.Exception e)
            {
                Debug.LogError("Error occured when trying to load data from file :" + fullPath + "\n" + e);
            }
        }

        return loadedData;
    }
    public void Save(GameData gameData)
    {
        string fullPath = Path.Combine(dirPath, fileName);
        try
        {
            Directory.CreateDirectory(Path.GetDirectoryName(fullPath));
            string dataToStore = JsonUtility.ToJson(gameData, true);
            using (FileStream stream = new FileStream(fullPath, FileMode.Create))
            {
                using (StreamWriter writer = new StreamWriter(stream))
                {
                    writer.Write(dataToStore);
                }
            }
        }
        catch (System.Exception e)
        {
            Debug.Log($"Can not write data to a json file \n {e}");
        }
    }
}
