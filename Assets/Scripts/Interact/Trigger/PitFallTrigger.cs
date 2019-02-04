using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PitFallTrigger : Trigger
{
    public Vector2 spawnLocation;

    public override void ExecuteTrigger()
    {
        StartCoroutine(_Fall(Global.Player));
    }

    IEnumerator _Fall(GameObject player)
    {
        PlayerAnimator animator = player.GetComponent<PlayerAnimator>();
        CameraMover camera = Camera.main.GetComponent<CameraMover>();

        camera.Target = null;
        player.transform.position += new Vector3(0, -1f, 0);
        //a.Play("PitFall"); play the falling animation
        yield return null;

        //Fall down
        while (player.transform.localScale.x > 0)
        {
            player.transform.localScale -= new Vector3(0.015f, 0.015f, 0);
            yield return null;
        }
        yield return new WaitForSeconds(2f);

        //Respawn somewhere
        player.transform.localScale = new Vector3(1, 1, 1);
        player.transform.position = spawnLocation;
    }
}
