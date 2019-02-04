using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

//Moves the camera with target, as long as target is within bounds
public class CameraMover : MonoBehaviour
{
    public GameObject Target;
    public float Speed = 1f;

    private bool Bounded = false;
    private Transform BottomLeft;
    private Transform TopRight;

    // Use this for initialization
    void Start()
    {
        Target = Global.Player;
        SetBounds();
        SceneManager.activeSceneChanged += SetBounds;
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

    //Finds the bound object, ands sets the clamped values
    private void SetBounds()
    {
        Bounded = false;
        Transform boundbox = GameObject.FindWithTag("BoundBox").transform;
        if (boundbox != null)
        {
            BottomLeft = boundbox.GetChild(0);
            TopRight = boundbox.GetChild(1);
            Bounded = true;
        }
    }

    private void SetBounds(Scene current, Scene next)
    {
        SetBounds();
    }
}