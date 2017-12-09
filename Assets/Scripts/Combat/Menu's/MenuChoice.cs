using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class MenuChoice : Choice
{
    protected override void SpawnChoice()
    {
        Text CTA = DialogueBoxClone.transform.Find("Canvas/ChoiceA").GetComponent<Text>();

        if (CTA != null)
        {
            CTA.text = ChoiceList[0];
        }

        Text CTB = DialogueBoxClone.transform.Find("Canvas/ChoiceB").GetComponent<Text>();

        if (CTB != null)
        {
            CTB.text = ChoiceList[1];
        }

        Text CTC = DialogueBoxClone.transform.Find("Canvas/ChoiceC").GetComponent<Text>();

        if (CTC != null)
        {
            CTC.text = ChoiceList[2];
        }

        Text CTD = DialogueBoxClone.transform.Find("Canvas/ChoiceD").GetComponent<Text>();

        if (CTD != null)
        {
            CTD.text = ChoiceList[3];
        }

        CursorClone = Instantiate(CursorPrefab);

        CursorClone.transform.parent = DialogueBoxClone.transform;
        UpdateCursor();
    }

    protected override void UpdateCursor()
    {
        Transform TF;
        Vector3 POS;

        if (SelectedChoice == 0)
        {
            TF = DialogueBoxClone.transform.Find("Canvas/ChoiceA");
        }
        else if(SelectedChoice == 1)
        {
            TF = DialogueBoxClone.transform.Find("Canvas/ChoiceB");
        }
        else //if(SelectedChoice == 2)
        {
            TF = DialogueBoxClone.transform.Find("Canvas/ChoiceC");
        }

        POS = TF.transform.position;
        POS.x -= 0.23f * ChoiceList[SelectedChoice].Length / 2 + 0.6f;
        POS.y += 0.05f;

        CursorClone.transform.position = POS;
    }
}
