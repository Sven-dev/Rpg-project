using UnityEngine;
using System.Collections;

public class CameraMovement : MonoBehaviour {

    public int CameraPos; //0 = select screen, 1 = attack screen, 2 = run screen
    Camera cam;

	// Use this for initialization
	void Start ()
    {
        CameraPos = 0;
        cam = GameObject.Find("Main Camera").GetComponent<Camera>();
    }
	
	// Update is called once per frame
	void Update ()
    {
	
	}

    //Changes the cameraposition
    public void ChangeCamera(int pos)
    {
        CameraPos = pos;
        MoveCamera();
    }

    //Moves the camera to the current position
    void MoveCamera()
    {
        Debug.Log("reached");
        if (CameraPos == 2)
        {
            cam.transform.position = new Vector2(11, 0);
        }
        
        else if (CameraPos == 1)
        {
            cam.transform.position = new Vector2(0, 0);
        }

        else //if (CameraPos == 0)
        {
            cam.transform.position = new Vector2(-11, 0);
        }
    }
}
