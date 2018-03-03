using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorTrialLoad : SceneLoader {

    public List<Button> ButtonList;
    public GameObject CompletedTrigger;
    public GameObject Wizard;

    public override void LoadData()
    {
        SerializableData Save = SaveFile.Instance.getData();

        for (int i = 0; i < Save.TrialDoorStates.Count; i++)
        {
            if (Save.TrialDoorStates[i] == true)
            {
                ButtonList[i].Press();
            }
        }

        if (Save.TrialDoorCompleted)
        {
            CompletedTrigger.SetActive(false);
            Wizard.SetActive(false);
        }
    }

    public override void SaveData()
    {
        SerializableData Save = SaveFile.Instance.getData();

        Save.TrialDoorStates.Clear();

        foreach (Button b in ButtonList)
        {
            Save.TrialDoorStates.Add(b);
        }

        SaveFile.Instance.Save();
    }

    public void CompleteTrial()
    {
        SerializableData Save = SaveFile.Instance.getData();
        Save.TrialDoorCompleted = true;
    }
}
