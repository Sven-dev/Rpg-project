using UnityEngine;
using System.Collections;

public class KeyboardControllerMenu : MonoBehaviour
{
    public MenuIndexer ActiveMenu;

    private bool ControlsEnabled = true;
	
	// Update is called once per frame
	void Update ()
    {
	    CheckKeys();
	}

    void CheckKeys()
    {
        if (ControlsEnabled)
        {
            //If down, move down
            if (Input.GetKeyDown(Prefs.Keys.Down))
            {
                ActiveMenu.UpdateIndex(1);
            }

            //If up, move up
            if (Input.GetKeyDown(Prefs.Keys.Up))
            {
                ActiveMenu.UpdateIndex(-1);
            }

            //If left, move left
            if (Input.GetKey(Prefs.Keys.Left))
            {
                MenuBar bar = ActiveMenu.GetActiveBar();
                if (bar != null)
                {
                    StartCoroutine(_UpdateBar(bar, Prefs.Keys.Left, -1));
                }
            }

            //If right, move right
            if (Input.GetKey(Prefs.Keys.Right))
            {
                MenuBar bar = ActiveMenu.GetActiveBar();
                if (bar != null)
                {
                    StartCoroutine(_UpdateBar(bar, Prefs.Keys.Right, 1));
                }
            }

            //Check if the interact/attack button is held down
            if (Input.GetKeyDown(Prefs.Keys.Attack_Interact))
            {
                ActiveMenu.SelectIndex();
            }
        }
    }

    public void ToggleControls()
    {
        ControlsEnabled = !ControlsEnabled;
    }

    IEnumerator _UpdateBar(MenuBar bar, string key, int direction)
    {
        int i = 0;
        while(Input.GetKey(key) && !Input.GetKey(Prefs.Keys.Up) && !Input.GetKey(Prefs.Keys.Down))
        {
            bar.ChangeBar(direction);
            if (i < 4)
            {
                yield return new WaitForSeconds(0.25f);
            }
            if (i == 4)
            {
                yield return new WaitForSeconds(0.4f);
            }
            else
            {
                yield return new WaitForSeconds(0.1f);
            }
        }
    }
}