using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class MenuHandler : MonoBehaviour
{
    //Phase 0
    public List<DialogueHandler> DialogueHandlerList;
    public int Phase;

    //Phase 1
    public GameObject MenuboxPrefab;
    protected GameObject MenuboxClone;

    public GameObject CursorPrefab;
    protected GameObject CursorClone;
    int CursorPosition; //0 = top left, 1 = top right, 2 = bottom left, 3 = bottom right

    /// <summary>
    /// combat phases:
    /// 0 = textbox state
    /// 1 = menu state
    /// 2 = fight state
    /// 3 = magic menu state
    /// 4 = magic fight state
    /// 5 = item menu state
    /// 6 = run state
    /// </summary>

    // Use this for initialization
    void Start()
    {
        Phase = 1;
        StartPhase();
    }

    // Update is called once per frame
    void Update()
    {
        CheckPhase();
    }

    void StartPhase()
    {
        if (Phase == 0)
        {
            //start dialogue
            DialogueHandlerList[Phase].StartProcess();
        }
        if (Phase == 1)
        {
            //Spawn main menu
            CursorPosition = 0;
            SpawnMainMenu();
        }
        if (Phase == 2)
        {
            //change camera to fight screen
            //unlock controls
            //engage bullet pattern
        }
        if (Phase == 3)
        {
            //Spawn magic menu
        }
        if (Phase == 4)
        {
            //nothing here yet
        }
        if (Phase == 5)
        {
            //spawn item menu
        }
        if (Phase == 6)
        {
            //change camera to run screen
            //unlock controls
            //engage bullet pattern
        }
    }

    void CheckPhase()
    {
        if (Phase == 0)
        {
            
        }
        if (Phase == 1)
        {
            //Check menu navigation
            if (Input.GetKeyDown(KeyCode.W))
            {
                MainMenuCursorLogic(4, -2);
            }
            if (Input.GetKeyDown(KeyCode.A))
            {
                MainMenuCursorLogic(4, -1);
            }
            if (Input.GetKeyDown(KeyCode.S))
            {
                MainMenuCursorLogic(4, 2);
            }
            if (Input.GetKeyDown(KeyCode.D))
            {
                MainMenuCursorLogic(4, 1);
            }

            //Check option select
            if (Input.GetKeyDown(KeyCode.Z))
            {

            }

            //Check keys for Z, W, A, S, D
            //Update cursor
            //Get new phase
        }
        if (Phase == 2)
        {

        }
        if (Phase == 3)
        {

        }
        if (Phase == 4)
        {

        }
        if (Phase == 5)
        {

        }
        if (Phase == 6)
        {

        }
    }

    void SpawnMainMenu()
    {
        //Gets the cameraposition, and changes the y to get the DialogueBox spawn coöridinates
        Vector3 camerapos = Camera.main.gameObject.transform.position;
        camerapos.y -= 3.25f;
        camerapos.z = 1;

        //Spawns the DialogueBox
        MenuboxClone = GameObject.Instantiate
            (
            MenuboxPrefab,
            camerapos,
            new Quaternion(0, 0, 0, 0)
            ) as GameObject;

        //Gets the text component, and assigns it to a variable
        Text TB = MenuboxClone.transform.Find("Canvas/Text").GetComponent<Text>();

        if (TB != null)
        {
            //DialogueText = TB;
            TB.text = "What will you do?";
        }

        Text CTA = MenuboxClone.transform.Find("Canvas/ChoiceA").GetComponent<Text>();

        if (CTA != null)
        {
            CTA.text = "Fight";
        }

        Text CTB = MenuboxClone.transform.Find("Canvas/ChoiceB").GetComponent<Text>();

        if (CTB != null)
        {
            CTB.text = "Magic";
        }

        Text CTC = MenuboxClone.transform.Find("Canvas/ChoiceC").GetComponent<Text>();

        if (CTC != null)
        {
            CTC.text = "Item";
        }

        Text CTD = MenuboxClone.transform.Find("Canvas/ChoiceD").GetComponent<Text>();

        if (CTD != null)
        {
            CTD.text = "Run";
        }

        CursorClone = Instantiate(CursorPrefab);

        MainMenuUpdateCursor();
    }

    void MainMenuCursorLogic(int amountofOptions, int direction /*2, 1, -1 ,-2*/)
    {

        //Checks if change falls in menu options
        if (CursorPosition + direction >= 0 && CursorPosition + direction < amountofOptions)
        {
            CursorPosition += direction;
            MainMenuUpdateCursor();
        }
    }

    /// <summary>
    /// 
    /// </summary>
    void MainMenuUpdateCursor()
    {
        Vector3 CamPos = Camera.main.gameObject.transform.position;
        CamPos.z = 0.9f;

        if (CursorPosition == 0)
        {
            CamPos.x -= 4;
            CamPos.y -= 3.45f;

            /*
            CamPos.x = -15;
            CamPos.y = -3.45f;      
            */

            CursorClone.transform.position = CamPos;
        }
        if (CursorPosition == 1)
        {
            CamPos.x += 2;
            CamPos.y -= 3.45f;
            /*
            CamPos.x = -9;
            CamPos.y = -3.45f;
            
            */

            CursorClone.transform.position = CamPos;
        }
        if (CursorPosition == 2)
        {
            CamPos.x -= 4;
            CamPos.y -= 4.1f;

            /*
            CamPos.x = -15;
            CamPos.y = -4.1f;
            
            */

            CursorClone.transform.position = CamPos;
        }
        if (CursorPosition == 3)
        {
            CamPos.x += 2;
            CamPos.y -= 4.1f;

            /*
            
            */

            CursorClone.transform.position = CamPos;
        }
    }

    void SpawnMagicMenu()
    {

    }

    void SpawnItemMenu()
    {

    }
}
