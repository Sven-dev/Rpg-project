using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class PortraitChoiceManager : DialogueManager
{
    private Image Portrait;

    private List<Text> ChoiceList;
    private List<Image> IndicatorList;
    [HideInInspector]
    public int Choice;

    // Use this for initialization
    protected override void Start()
    {
        base.Start();
        ChoiceList = Holder.GetComponentsInChildren<Text>().ToList();
        ChoiceList.RemoveAt(0);
        IndicatorList = Holder.GetComponentsInChildren<Image>().ToList();
        IndicatorList.RemoveAt(0);
    }

    protected override IEnumerator _Writing()
    {
        Clear();
        Holder.gameObject.SetActive(true);
        while (Dialogue.Writing)
        {
            Write();
            yield return new WaitForSeconds(0.025f);
        }

        FinishWriting();
        ShowChoices();
        while (Dialogue.Active)
        {
            yield return null;
        }

        Choice = SelectedIndex();
        Holder.gameObject.SetActive(false);
    }

    protected override void Clear()
    {
        Dialogue.Active = true;
        Dialogue.Writing = true;

        Label.text = "";
        foreach (Text t in ChoiceList)
        {
            t.text = "";
        }

        foreach (Image i in IndicatorList)
        {
            i.enabled = false;
        }
    }

    private void ShowChoices()
    {
        Choice c = Dialogue as Choice;
        for (int i = 0; i < c.ChoiceList.Count; i++)
        {
            ChoiceList[i].text = c.ChoiceList[i];
        }

        IndicatorList[0].enabled = true;
    }

    public void ChangeIndex(int index)
    {
        int selected = SelectedIndex();
        if (selected + index > -1 && selected + index < IndicatorList.Count)
        {
            IndicatorList[selected].enabled = false;
            IndicatorList[selected + index].enabled = true;
        }
    }

    //Returns the index if the indicator that's enabled
    public int SelectedIndex()
    {
        for (int i = 0; i < IndicatorList.Count; i++)
        {
            if (IndicatorList[i].enabled)
            {
                return i;
            }
        }

        throw new System.Exception("No choice selected (did the indicator break?)");
    }
}
