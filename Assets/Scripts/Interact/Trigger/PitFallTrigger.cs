using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PitFallTrigger : Trigger
{
    public Vector2 spawnLocation;

    public override void ExecuteTrigger()
    {
        //p.Controls_OFF();
        StartCoroutine(FallDown());
    }

    IEnumerator FallDown()
    {
        //Get player object
        print("Player:" + Player.name);
        Animator a = Player.GetComponent<Animator>();
        CameraMover c = GameObject.FindWithTag("MainCamera").GetComponent<CameraMover>();

        //Change sprite
        c.Target = null;
        //b.enabled = false;
        Player.transform.position += new Vector3(0, -1f, 0);
        a.Play("PitFall");

        yield return null;

        //Fall down
        while (Player.transform.localScale.x > 0)
        {
            Player.transform.localScale -= new Vector3(0.015f, 0.015f, 0);

            yield return new WaitForFixedUpdate();
        }

        yield return new WaitForSeconds(2f);

        //Respawn somewhere
        Player.transform.localScale = new Vector3(1, 1, 1);
        Player.transform.position = spawnLocation;

        //Iframes??
        //b.enabled = true;

        //Enable controls
        c.Target = Player.transform;

        /*
        if (p.Balancing)
        {
            p.Balancing = false;
        }
        */

        //p.Controls_ON();
    }
}
