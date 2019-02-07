using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

//Moves the camera with target, as long as target is within bounds
public class CameraMover : MonoBehaviour
{
    public GameObject Target;
    public float Speed = 1f;

    private bool Bounded = false;
    private Transform BottomLeftClamp;
    private Transform TopRightClamp;

    private Vector2 CornerDistance;

    // Use this for initialization
    void Start()
    {
        Target = Global.Player;
        SceneManager.activeSceneChanged += SetBounds;
        SetCornerDistance();
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

    //Calculates the x and y distance between the center and the corners of the camera 
    private void SetCornerDistance()
    {
        Vector2 corner = Global.MainCamera.ViewportToWorldPoint(new Vector2(0, 0));
        CornerDistance = new Vector2(transform.position.x - corner.x, transform.position.y - corner.y);
    }

    //Ensures the camera stays within the bounds of the map
    private void ClampBounds()
    {
        float minxclamp = BottomLeftClamp.position.x + CornerDistance.x;
        float maxxclamp = TopRightClamp.position.x - CornerDistance.x;

        float minyclamp = BottomLeftClamp.position.y + CornerDistance.y;
        float maxyclamp = TopRightClamp.position.y - CornerDistance.y;


        float x;
        if (minxclamp >= maxxclamp)
        {
            x = Mathf.Clamp(transform.position.x, minxclamp, minxclamp);
        }
        else
        {
            x = Mathf.Clamp(transform.position.x, minxclamp, maxxclamp);
        }

        float y;
        if (minyclamp >= maxyclamp)
        {
            y = Mathf.Clamp(transform.position.y, minyclamp, minyclamp);
        }
        else
        {
            y = Mathf.Clamp(transform.position.y, minyclamp, maxyclamp);
        }


        //Clamp the corners of the camera
        transform.position = new Vector2(x, y);
    }

    //Finds the bound object, ands sets the clamped values
    public void SetBounds()
    {
        Bounded = false;
        Transform boundbox = Global.ActiveRoom.transform.GetChild(0);
        if (boundbox != null)
        {
            BottomLeftClamp = boundbox.GetChild(0);
            TopRightClamp = boundbox.GetChild(1);
            Bounded = true;
        }
    }

    private void SetBounds(Scene current, Scene next)
    {
        SetBounds();
    }
}