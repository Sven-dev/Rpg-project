using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BackgroundManager : MonoBehaviour {

    Camera Camera;
    public List<Color> AreaColours;

    private void Awake()
    {
        Camera = GetComponent<Camera>();
        SceneManager.sceneLoaded += OnSceneLoaded;
        SceneManager.LoadScene("Altar");
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        //scene.path
        string scenepath = SceneManager.GetActiveScene().path;

        if (scenepath.Contains("Forest"))
        {
            Camera.backgroundColor = AreaColours[1];
        }
    }
}