using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator : MonoBehaviour
{
    public float Speed;
    public bool Clockwise;

	// Update is called once per frame
	void Update () {
        int signum = -1;
        if (Clockwise)
            signum = 1;

        transform.Rotate(transform.eulerAngles * Speed * signum * Time.deltaTime);
	}
}
