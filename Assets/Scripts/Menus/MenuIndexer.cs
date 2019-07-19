using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuIndexer : MonoBehaviour
{
    private MenuItem[] Items;
    private int SelectedIndex;

	// Use this for initialization
	void Start ()
    {
        Items = GetComponentsInChildren<MenuItem>(true);
        Items[0].ToggleItem(true);
        SelectedIndex = 0;
	}

    //Update the selected item. direction is either 1 or -1
    public void UpdateIndex(int direction)
    {
        int newIndex = SelectedIndex + direction;
        if (newIndex >= 0 && newIndex <= Items.Length -1)
        {
            Items[SelectedIndex].ToggleItem();
            Items[newIndex].ToggleItem();
            SelectedIndex = newIndex;
        }
    }

    public void SelectIndex()
    {
        Items[SelectedIndex].SelectItem();
    }

    public void ToggleMenu()
    {
        GameObject menu = transform.parent.gameObject;
        menu.SetActive(!menu.activeSelf);
    }

    public void ToggleMenu(bool state)
    {
        transform.parent.gameObject.SetActive(state);
    }

    public MenuBar GetActiveBar()
    {
        return Items[SelectedIndex].GetComponent<MenuBar>();
    }
}