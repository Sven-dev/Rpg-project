using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class DoorTrigger : Trigger
{
    public string Scene;
    public Vector2 position;
    public CameraMover Camera;

    // Use this for initialization
    new void Start ()
    {
        base.Start();
        Camera = GameObject.FindWithTag("MainCamera").GetComponent<CameraMover>();
    }

    void OnApplicationQuit()
    {
        //Debug.Log("This works");
    }

    public override void ExecuteTrigger()
    {
        Camera.SwitchScene(Scene, position);
    }
}