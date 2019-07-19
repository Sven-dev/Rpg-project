using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class MenuItem : MonoBehaviour
{
    private Image Image;

    private void Awake()
    {
        Image = GetComponent<Image>();
        Image.enabled = false;
    }

    public virtual void SelectItem(){ }

    public void ToggleItem()
    {
        Image.enabled = !Image.enabled;
    }

    public void ToggleItem(bool state)
    {
        Image.enabled = state;
    }
}
