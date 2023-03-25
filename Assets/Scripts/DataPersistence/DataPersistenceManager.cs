using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

//Source: https://www.youtube.com/watch?v=aUi9aijvpgs

public class DataPersistenceManager : MonoBehaviour
{
    [Header("File Store Config")]

    [SerializeField] private string fileName;

    private GameData gameData;

    private List<IDataPersistence> dataPersistencesObjects;
    private FileDataHandler dataHandler;
    public static DataPersistenceManager instance { get; private set; }

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
    }

    /*
    private void Start()
    {
        this.dataPersistencesObjects = FindallDataPersistenceObjects();
        LoadGame();
    }
    */

    public void NewGame()
    {
        this.gameData = new GameData();
    }

    public void LoadGame()
    {
        this.dataHandler = new FileDataHandler(Application.persistentDataPath, fileName);
        this.dataPersistencesObjects = FindallDataPersistenceObjects();


        // Load any saved data from a file in the data handler
        this.gameData = dataHandler.Load();


        // if no game data can be loaded, initialize to a new game
        if (this.gameData == null)
        {
            Debug.Log("No data was found. Initializing to default values");
            NewGame();
        }

        // push the loaded data to all other scripts that need it
        foreach(IDataPersistence dataPersistenceObj in dataPersistencesObjects)
        {
            dataPersistenceObj.LoadData(gameData);
        }
        
    }
    
    public void SaveGame()
    {
        // pass the data to other scripts so they can update it 
        foreach(IDataPersistence dataPersistenceObj in dataPersistencesObjects)
        {
            dataPersistenceObj.SaveData(gameData);
        }

        // save that data to a file using the data handler
        dataHandler.Save(gameData);
    }

    private List<IDataPersistence> FindallDataPersistenceObjects()
    {
        IEnumerable<IDataPersistence> dataPersistenceObjects = FindObjectsOfType<MonoBehaviour>().OfType<IDataPersistence>();

        return new List<IDataPersistence>(dataPersistenceObjects);
    }
}
