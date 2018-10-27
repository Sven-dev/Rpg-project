using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class CameraMover : MonoBehaviour
{
    public Movement Target;
    public float Speed = 1f;

    [Space]
    public bool bounds;
    public Vector2 minCameraPos;
    public Vector2 maxCameraPos;

    private SceneSwitcher SceneSwitcher;
    private Camera Cam;
    private SpriteRenderer Blackout;

    // Use this for initialization
    void Start()
    {
        Target = GameObject.FindWithTag("Player").GetComponent<Movement>();
        SceneSwitcher = Target.GetComponent<SceneSwitcher>();

        Cam = GetComponent<Camera>();
        Blackout = transform.GetChild(4).GetComponent<SpriteRenderer>();

        StartCoroutine(_FadeIn());
    }

    // Update is called once per frame
    void Update()
    {
        UpdateCameraPosition();
        if (bounds)
        {
            ClampBounds();
        }

        //the z-axis of the camera needs to not be 0;
        transform.Translate(new Vector3(0, 0, -100));
    }

    void UpdateCameraPosition()
    {
        if (Target != null)
        {
            transform.position = Vector2.Lerp(transform.position, Target.transform.position, Speed);
        }
    }

    //Ensures the camera stays within the bounds of the map
    void ClampBounds()
    {
        transform.position = new Vector2(
            Mathf.Clamp(transform.position.x, minCameraPos.x, maxCameraPos.x),
            Mathf.Clamp(transform.position.y, minCameraPos.y, maxCameraPos.y)
            );
    }

    #region Scene switching
    public void SwitchScene(string scene, Vector2 spawnposition)
    {
        StartCoroutine(_Switch(scene, spawnposition));
    }

    //Switches to a different scene
    IEnumerator _Switch(string scene, Vector2 spawnposition)
    {
        StartCoroutine(_FadeOut());
        while (Blackout.color.a < 1)
        {
            yield return null;
        }

        SceneSwitcher.Switch(scene, spawnposition);
    }

    //Fades out the black screen
    IEnumerator _FadeIn()
    {
        float Alpha = Blackout.color.a;
        while (Alpha > 0)
        {
            Alpha -= 0.1f;
            Blackout.color = new Color(1, 1, 1, Alpha);

            yield return new WaitForSeconds(0.025f);
        }

        Blackout.color = new Color(1, 1, 1, 0);
        Target.Immobile = false;
    }

    //Fades in the black screen
    IEnumerator _FadeOut()
    {
        Target.Immobile = true;

        float Alpha = Blackout.color.a;
        //Turn the screen black over time
        while (Alpha < 1)
        {
            Alpha += 0.1f;
            Blackout.color = new Color(1, 1, 1, Alpha);

            yield return new WaitForSeconds(0.025f);
        }

        Blackout.color = new Color(1, 1, 1, 1);
    }
    #endregion
}