using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class SaveFile : MonoBehaviour {

    public static SaveFile Instance;
    private SerializableData data;

	// Use this for initialization
	void Awake ()
    {
        if (Instance == null)
        {
            DontDestroyOnLoad(gameObject);
            Instance = this;
            Load();
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
	}

    public void Save()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/Playerinfo.dat");

        bf.Serialize(file, this.data);
        file.Close();
    }

    public void Load()
    {
        if (File.Exists(Application.persistentDataPath + "/Playerinfo.dat"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/Playerinfo.dat", FileMode.Open);
            this.data = (SerializableData)bf.Deserialize(file);
            file.Close();
        }
    }

    void NewSave()
    {

    }

    public SerializableData getData()
    {
        return this.data;
    }
}

[System.Serializable]
public class SerializableData
{
    //Player info
    public Vector3 PlayerPos;
    public int PlayerScene;

    public bool SwordInStone;

    #region Trials
    //Trial 1
    public int ActiveButton;
    public bool TrialDoorCompleted;

    //Trial 2

    //Trial 3

    //Trial 4

    #endregion
}