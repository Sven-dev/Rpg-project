using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DebugWrongRoom : MonoBehaviour {

	// Use this for initialization
	void Awake ()
    {
        if (Application.isEditor)
        {
            if (GameObject.FindWithTag("Player") == null)
            {
                SceneManager.LoadScene(0);
            }
        }
	}
}