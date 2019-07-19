using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatBar : MonoBehaviour
{
    private Image Bar;

    private void Awake()
    {
        Bar = transform.GetChild(0).GetComponent<Image>();
        Bar.fillMethod = Image.FillMethod.Horizontal;
    }

    public void UpdateUI(int value)
    {
        Bar.fillAmount = value / 100f;
    }
}
