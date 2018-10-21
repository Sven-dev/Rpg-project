using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class CameraMover : MonoBehaviour {

    public Movement Target;
    public float Speed = 1f;
    Camera Cam;

    public bool bounds;
    public Vector2 minCameraPos;
    public Vector2 maxCameraPos;

    public SpriteRenderer Blackout;

    // Use this for initialization
    void Start()
    {
        Target = GameObject.FindWithTag("Player").GetComponent<Movement>();
        DontDestroyOnLoad(Target);
        DontDestroyOnLoad(gameObject);
        Cam = GetComponent<Camera>();

        Blackout.color = new Color(1, 1, 1, 0);
        Blackout.enabled = true;
    }

    // Update is called once per frame
    void Update()
    {
        //Cam.orthographicSize = (Screen.height / 100);
        UpdateCameraPos();

        if (bounds)
        {
            ClampBounds();
        }

        transform.Translate(new Vector3(0, 0, -100));
    }



    // Update camera position
    void UpdateCameraPos()
    {
        if (Target != null)
        {
            transform.position = Vector2.Lerp(transform.position, Target.transform.position, Speed);
        }
    }

    //function that ensures the camera stays within the bounds of the map
    void ClampBounds()
    {
        transform.position = new Vector2(
            Mathf.Clamp(transform.position.x, minCameraPos.x, maxCameraPos.x),
            Mathf.Clamp(transform.position.y, minCameraPos.y, maxCameraPos.y)
            );
    }

    public void SwitchScene(string scene, Vector2 spawnposition)
    {
        StartCoroutine(_Switch(scene, spawnposition));
    }

    IEnumerator _Switch(string scene, Vector2 spawnposition)
    {
        Target.Immobile = true;
        float Alpha = Blackout.color.a; //should be 0

        //Turn the screen black over time
        while (Alpha < 1)
        {
            Alpha += 0.1f;
            Blackout.color = new Color(1, 1, 1, Alpha);

            yield return new WaitForSeconds(0.025f);
        }

        Switch(scene, spawnposition);
        yield return new WaitForSeconds(0.25f);

        while (Alpha > 0)
        {
            Alpha -= 0.1f;
            Blackout.color = new Color(1, 1, 1, Alpha);

            yield return new WaitForSeconds(0.025f);
        }

        Target.Immobile = false;
    }

    //Switches active scenes, and sets object positions
    void Switch(string scene, Vector2 spawn)
    {
        SceneManager.LoadScene(scene);
        Target.transform.position = spawn;
    }
}