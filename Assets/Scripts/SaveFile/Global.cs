using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Global : MonoBehaviour
{
    public static SaveFile Save;
    public static KeyBinds Keys;

    public static Camera MainCamera;
    public static CameraMover CameraMover;
    public static SceneSwitcher SceneSwitcher;

    public static GameObject Player;
    public static Movement PlayerMovement;
    public static HealthManager PlayerHealth;
    public static PlayerAnimator PlayerAnimator;
    public static SpriteRenderer PlayerRenderer;

    public static GameObject ActiveRoom;

    public GameObject PlayerPrefab;

	// Use this for initialization
	private void OnEnable()
    {
        //Load saved data
        DontDestroyOnLoad(gameObject);
        LoadKeys();
        LoadData();

        //Assign camera
        MainCamera = Camera.main;
        CameraMover = MainCamera.GetComponent<CameraMover>();     
          
        //Assign player
        Player = Instantiate(PlayerPrefab);
        PlayerMovement = Player.GetComponent<Movement>();
        PlayerHealth = Player.GetComponentInChildren<HealthManager>();
        PlayerAnimator = Player.GetComponent<PlayerAnimator>();
        PlayerRenderer = Player.GetComponent<SpriteRenderer>();

        //Link player to HUD
        LinkHUD();

        //Assign scene switching
        SceneSwitcher = MainCamera.GetComponent<SceneSwitcher>();
        SceneSwitcher.SwitchScene(Save.ActiveScene, Save.ActiveRoom, Save.PlayerLocation, false, true);
	}

    //Links the player to the HUD
    private void LinkHUD()
    {
        StatBar Healthbar = transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<StatBar>();
        PlayerHealth.OnValueChange += Healthbar.UpdateUI;
    }

    //Saves the game when the application is closed
    private void OnApplicationQuit()
    {
        SaveData();
        SaveKeys();
    }

    //Loads the game progress. Creates a new save if it can't find an existing one
    private void LoadData()
    {
        Save = ClassToXmlFileIO.Load<SaveFile>("Project_SOUL", "Save");
        if (Save == null)
        {
            Save = new SaveFile();
            ClassToXmlFileIO.Save("Project_SOUL", "Save", Save);
        }
    }

    //Saves the game progress
    public static void SaveData()
    {
        if (Save != null)
        {
            ClassToXmlFileIO.Save("Project_SOUL", "Save", Save);
        }
    }

    //Loads the keybinds. Creates a new file if none exists
    private void LoadKeys()
    {
        Keys = ClassToXmlFileIO.Load<KeyBinds>("Project_SOUL", "Keybindings");
        if (Keys == null)
        {
            Keys = new KeyBinds();
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
}