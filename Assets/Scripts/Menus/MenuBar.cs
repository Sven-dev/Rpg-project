using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuBar : MenuItem
{
    private Image Bar;

	// Use this for initialization
	void OnEnable ()
    {
        Bar = transform.GetChild(0).GetComponent<Image>();
	}

    public int Value
    {
        get { return (int)(Bar.fillAmount * 100); }
        set { Bar.fillAmount = value / 100f; }
    }

    //Changes the amount the bar is filled.
    public void ChangeBar(int direction)
    {
        Bar.fillAmount += direction / 100f;
    }
}