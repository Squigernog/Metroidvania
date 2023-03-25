using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using System.Runtime.InteropServices;


public class FileDataHandler
{
    private string dataDirPath = "";

    private string dataFileName = "";

    public FileDataHandler(string dataDirPath, string dataFileName)
    {
        this.dataDirPath = dataDirPath;
        this.dataFileName = dataFileName;
    }

    public GameData Load()
    {
        // use Path.combine to account for different OS's having different path seperators
        string fullPath = Path.Combine(dataDirPath, dataFileName);
        GameData loadedData = null;

        if (File.Exists(fullPath))
        {
            try
            {
                string dataToLoad = "";
                using (FileStream stream = new FileStream(fullPath, FileMode.Open))
                {
                    using(StreamReader reader = new StreamReader(stream))
                    {
                        dataToLoad = reader.ReadToEnd();
                    }
                }

                // deserialize the data from Json back into C# object
                loadedData = JsonUtility.FromJson<GameData>(dataToLoad);
            }
            catch (Exception ex)
            {
                Debug.LogError("Error occured when trying to load data from file: " + fullPath + "\n" + ex);
            }
        }
        return loadedData;
    }

    public void Save(GameData data)
    {
        // use Path.combine to account for different OS's having different path seperators
        string fullPath = Path.Combine(dataDirPath, dataFileName);

        try
        {
            // create the directory the file will be written to if it doesnt exist yet
            Directory.CreateDirectory(Path.GetDirectoryName(fullPath));

            // serialize the c# game data object into Json
            string dataToStore = JsonUtility.ToJson(data, true);

            //write the serialized data to the file
            using(FileStream stream = new FileStream(fullPath, FileMode.Create))
            {
                using (StreamWriter writer = new StreamWriter(stream))
                {
                    writer.Write(dataToStore);
                }
            }
        }
        catch(Exception ex)
        {
            Debug.LogError("Error occured when trying to save data to file: " + fullPath + "\n" + ex);
        }
    }
}
