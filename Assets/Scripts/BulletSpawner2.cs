using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Spawns a line of bullets that moves from right to left, with one bullet missing.
/// </summary>
public class BulletSpawner2 : MonoBehaviour
{
    public Lever Lever;
    public List<Transform> Rotators;
    [Space]
    public float Speed;

    private bool State = false;

    /// <summary>
    /// Subscribes the spawner to a lever
    /// </summary>
    private void Start()
    {
        Lever.OnStateChange += Toggle;
    }

    /// <summary>
    /// Loops through a cooldown, and spawns bullets occasionally
    /// </summary>
    /// <returns></returns>
    protected virtual IEnumerator Loop()
    {
        while (State)
        {
            int direction = 1;
            for (int i = 0; i < Rotators.Count; i++)
            {
                Rotators[i].Rotate(Vector3.forward * direction * Speed * Time.deltaTime);
                direction *= -1;
            }

            yield return null;
        }
    }

    /// <summary>
    /// Turns the spawner on or off
    /// </summary>
    /// <param name="state"></param>
    public void Toggle(bool state)
    {
        State = state;
        if (State)
            StartCoroutine(Loop());
    }
}