using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class DoorTrigger : Trigger
{
    #region Variables
    public string scene;
    public Vector2 position;

    private float FadeAlpha;
    private float FadeTimer;
    #endregion

    #region Unity Logic
    public GameObject BlackBox;
    private GameObject BBClone;
    private GameObject BBClone2;
    private SpriteRenderer BBAlpha;

    private GameObject Player;
    private Player pscript;

    // Use this for initialization
    void Start ()
    {
        DontDestroyOnLoad(transform.gameObject);

        FadeAlpha = 0;
        FadeTimer = 0.01f;
    }

    //Timer for fading in BlackBox
    IEnumerator Fade_In()
    {
        while (FadeAlpha < 1f)
        {
            FadeAlpha += 0.1f;
            BBAlpha.color = new Color(1, 1, 1, FadeAlpha);

            yield return new WaitForSeconds(FadeTimer);
        }

        SwitchScene();
    }

    //Timer for fading out BlackBox
    IEnumerator Fade_Out()
    {
        yield return new WaitForSeconds(0.25f);

        while (FadeAlpha > 0f)
        {
            FadeAlpha -= 0.1f;
            BBAlpha.color = new Color(1, 1, 1, FadeAlpha);

            yield return new WaitForSeconds(FadeTimer);
        }

        FinishSwitch();
    }
    #endregion

    public override void ExecuteTrigger()
    {
        Debug.Log("trigger found: " + scene);

        //Gets the player gameobject and script
        Player = GameObject.FindWithTag("Player");
        pscript = Player.GetComponent<Player>();

        //lock player controls
        pscript.ControlsToggle();

        //spawn blackbox (0% opacity)
        SpawnBlackBox();

        StartCoroutine(Fade_In());
        //make blackbox 100% opacity
        //load next scene
        //set player position
        //set blackbox position
        //make blackbox 0% opacity
        //destroy blackbox
        //unlock player controls
    }

    void SpawnBlackBox()
    {
        Vector3 playerpos = Player.transform.position;
        BBClone = GameObject.Instantiate(BlackBox, playerpos, new Quaternion(0, 0, 0, 0)) as GameObject;

        //DontDestroyOnLoad() blackbox
        DontDestroyOnLoad(BBClone);

        BBAlpha = BBClone.GetComponent<SpriteRenderer>();
        BBAlpha.color = new Color(1, 1, 1, FadeAlpha);
    }

    //Switches active scenes, and sets object positions
    void SwitchScene()
    {
        SceneManager.LoadScene(scene);

        Player.transform.position = position;
        BBClone.transform.position = position;

        StartCoroutine(Fade_Out());
    }

    void FinishSwitch()
    {
        pscript.ControlsToggle();

        Destroy(BBClone);
        Destroy(this.gameObject);

    }
}
