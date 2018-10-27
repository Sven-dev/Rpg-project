using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//Allows the object to move from oen scene to another
public class SceneSwitcher : MonoBehaviour
{
	// Use this for initialization
	void Start ()
    {
        DontDestroyOnLoad(gameObject);
	}

    //Switches active scenes, and sets the object's position
    public void Switch(string scene, Vector2 spawn)
    {
        SceneManager.LoadScene(scene);
        transform.position = spawn;
    }
}