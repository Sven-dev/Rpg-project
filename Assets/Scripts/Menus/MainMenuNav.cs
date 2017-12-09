using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class MainMenuNav : MonoBehaviour
{

	// Use this for initialization
	void Start ()
    {
        DontDestroyOnLoad(transform.gameObject);
    }
	
	// Update is called once per frame
	void Update ()
    {
	    
	}

    void FixedUpdate()
    {
        CheckKeys();
    }

    void CheckKeys()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            SceneManager.LoadScene("Cave");
        }
    }
}
