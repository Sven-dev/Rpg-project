using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//Fixes any objects not being in a scene
public class CameraFallback : MonoBehaviour {

    private void Awake()
    {
        GetComponent<Camera>().enabled = false;
        GameObject Player = GameObject.FindGameObjectWithTag("Player");

        if (Player == null)
        {
            SceneManager.LoadScene("GamePreload");
        }
    }
}