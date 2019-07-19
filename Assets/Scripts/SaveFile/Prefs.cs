using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Prefs : MonoBehaviour
{
    public static Keys Keys;
    public static Settings Settings;

    // Use this for initialization
    private void OnEnable()
    {
        //Load saved data
        DontDestroyOnLoad(gameObject);
        LoadKeys();
        LoadSettings();
    }

    //Saves the game when the application is closed
    private void OnApplicationQuit()
    {
        SaveKeys();
        SaveSettings();
    }

    //Loads the keybinds. Creates a new file if none exists
    private void LoadKeys()
    {
        Keys = ClassToXmlFileIO.Load<Keys>("Project_SOUL", "Keybindings");
        if (Keys == null)
        {
            Keys = new Keys();
            ClassToXmlFileIO.Save("Project_SOUL", "Keybindings", Keys);
        }
    }

    //Saves the keybindings
    public static void SaveKeys()
    {
        if (Keys != null)
        {
            ClassToXmlFileIO.Save("Project_SOUL", "Keybindings", Keys);
        }
    }

    //Loads the settings. Creates a new file if none exists
    private void LoadSettings()
    {
        Settings = ClassToXmlFileIO.Load<Settings>("Project_SOUL", "Settings");
        if (Settings == null)
        {
            Settings = new Settings();
            ClassToXmlFileIO.Save("Project_SOUL", "Settings", Settings);
        }
    }

    //Saves the settings
    public static void SaveSettings()
    {
        if (Settings != null)
        {
            ClassToXmlFileIO.Save("Project_SOUL", "Settings", Settings);
        }
    }
}