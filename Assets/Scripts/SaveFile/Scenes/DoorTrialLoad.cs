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
        SaveFile Save = SaveFile.data;

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
        SaveFile.data.TrialDoorStates.Clear();

        foreach (Button b in ButtonList)
        {
            SaveFile.data.TrialDoorStates.Add(b);
        }

        SaveFile.data.Save();
    }

    public void CompleteTrial()
    {
        SaveFile.data.TrialDoorCompleted = true;
    }
}
