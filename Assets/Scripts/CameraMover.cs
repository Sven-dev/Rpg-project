using UnityEngine;
using System.Collections;

public class CameraMover : MonoBehaviour {

    public Transform Target;
    public float Speed = 1f;
    Camera Cam;

    public bool bounds;
    public Vector2 minCameraPos;
    public Vector2 maxCameraPos;

    // Use this for initialization
    void Start()
    {
        Target = GameObject.FindWithTag("Player").transform;
        Cam = GetComponent<Camera>();
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
}
