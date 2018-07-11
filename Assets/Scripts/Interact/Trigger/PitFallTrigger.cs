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
        GameObject g = GameObject.FindWithTag("Player");
        BoxCollider b = g.GetComponent<BoxCollider>();
        Player p = g.GetComponent<Player>();
        Animator a = g.GetComponent<Animator>();
        CameraMover c = GameObject.FindWithTag("MainCamera").GetComponent<CameraMover>();

        //Change sprite
        c.Target = null;
        b.enabled = false;
        g.transform.position += new Vector3(0, -1f, 0);
        a.Play("PitFall");

        yield return null;

        //Fall down
        while (g.transform.localScale.x > 0)
        {
            g.transform.localScale -= new Vector3(0.015f, 0.015f, 0);

            yield return new WaitForFixedUpdate();
        }

        yield return new WaitForSeconds(2f);

        //Respawn somewhere
        g.transform.localScale = new Vector3(1, 1, 1);
        g.transform.position = spawnLocation;

        //Iframes??
        b.enabled = true;

        //Enable controls
        c.Target = g.transform;

        if (p.Balancing)
        {
            p.Balancing = false;
        }

        //p.Controls_ON();
    }
}
