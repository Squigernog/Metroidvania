using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This is just an example of how I implempted IDataPersistence for MSDraft
public class CollectionData : MonoBehaviour, IDataPersistence
{

    public static CollectionData instance;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(gameObject);
    }


    public string saveSlot1Name { get; set; }
    public string saveSlot2Name { get; set; }
    public string saveSlot3Name { get; set; }

    public void LoadData(GameData data)
    {
        //example of how you would load in game data
        /*
        this.saveSlot1Name = data.saveSlot1Name;
        this.saveSlot2Name = data.saveSlot2Name;
        this.saveSlot3Name = data.saveSlot3Name;
        */
    }

    public void SaveData(GameData data)
    {
        //example of how you would save in game data
        /*
        data.saveSlot1Name = this.saveSlot1Name;
        data.saveSlot2Name = this.saveSlot2Name;
        data.saveSlot3Name = this.saveSlot3Name;
        */
    }
}
