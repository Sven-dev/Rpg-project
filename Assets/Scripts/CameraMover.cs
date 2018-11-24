using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class CameraMover : MonoBehaviour
{
    public Movement Target;
    public float Speed = 1f;
    [Space]
    public Transform Bounds;
    private bool Bounded;
    private Transform BottomLeft;
    private Transform TopRight;

    private SceneSwitcher SceneSwitcher;
    private Camera Cam;
    public SpriteRenderer Blackout;
    private bool Fading;

    // Use this for initialization
    void Start()
    {
        Target = GameObject.FindWithTag("Player").GetComponent<Movement>();
        SceneSwitcher = Target.GetComponent<SceneSwitcher>();

        Bounded = false;
        if (Bounds != null)
        {
            BottomLeft = Bounds.GetChild(0);
            TopRight = Bounds.GetChild(1);
            Bounded = true;
        }

        Cam = GetComponent<Camera>();
        StartCoroutine(_FadeIn());
    }

    // Update is called once per frame
    void Update()
    {
        UpdateCameraPosition();

        if (Bounded)
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
            Mathf.Clamp(transform.position.x, BottomLeft.position.x, TopRight.position.x),
            Mathf.Clamp(transform.position.y, BottomLeft.position.y, TopRight.position.y)
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
        Fading = true;
        StartCoroutine(_FadeOut());
        while (Fading)
        {
            yield return null;
        }

        SceneSwitcher.Switch(scene, spawnposition);
    }

    //Fades out the black screen
    IEnumerator _FadeIn()
    {
        if (Blackout != null)
        {
            Blackout.color = new Color(
                Blackout.color.r,
                Blackout.color.g,
                Blackout.color.b,
                1);
            while (Blackout.color.a > 0)
            {
                Blackout.color = new Color(
                    Blackout.color.r,
                    Blackout.color.g,
                    Blackout.color.b,
                    Blackout.color.a - 0.1f);
                yield return new WaitForSeconds(0.025f);
            }

            Target.Immobile = false;
        }
    }

    //Fades in the black screen
    IEnumerator _FadeOut()
    {
        if (Blackout != null)
        {
            Target.Immobile = true;
            Blackout.color = new Color(
                Blackout.color.r,
                Blackout.color.g,
                Blackout.color.b,
                0);
            while (Blackout.color.a < 1)
            {
                Blackout.color = new Color(
                    Blackout.color.r,
                    Blackout.color.g,
                    Blackout.color.b,
                    Blackout.color.a + 0.1f);
                yield return new WaitForSeconds(0.025f);
            }
        }
            Fading = false;
    }
    #endregion
}