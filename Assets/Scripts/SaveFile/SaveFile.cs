using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class SaveFile : MonoBehaviour {

    public static SaveFile data;

    public Vector3 PlayerPos;
    public int PlayerScene;

    public bool SwordInStone;

    [Header("DoorTrialButtons")]
    public List<bool> TrialDoorStates;
    public bool TrialDoorCompleted;

	// Use this for initialization
	void Awake ()
    {
        if (data == null)
        {
            DontDestroyOnLoad(gameObject);
            data = this;
        }
        else if (data != this)
        {
            Destroy(gameObject);
        }
	}

    public void Save()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/Playerinfo.dat");
        SerializableData data = new SerializableData();

        ConvertToSave();

        bf.Serialize(file, data);
        file.Close();
    }

    public void Load()
    {
        if (File.Exists(Application.persistentDataPath + "/Playerinfo.dat"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/Playerinfo.dat", FileMode.Open);
            SerializableData data = (SerializableData)bf.Deserialize(file);
            file.Close();

            ConvertToGame();
        }
    }

    void NewSave()
    {

    }

    void ConvertToSave()
    {
        data.SwordInStone = SwordInStone;
        data.TrialDoorStates = TrialDoorStates;
        data.TrialDoorCompleted = TrialDoorCompleted;
    }

    void ConvertToGame()
    {
        SwordInStone = data.SwordInStone;
        TrialDoorStates = data.TrialDoorStates;
        TrialDoorCompleted = data.TrialDoorCompleted;
    }
}

[System.Serializable]
class SerializableData
{
    public bool SwordInStone;

    public List<bool> TrialDoorStates;
    public bool TrialDoorCompleted;
}
