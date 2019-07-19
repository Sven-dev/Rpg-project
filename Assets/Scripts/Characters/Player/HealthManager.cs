using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthManager : MonoBehaviour
{
    [Tooltip("Can't be changed in editor, it's just here for testing")]
    public int Health; //A number between 0 & 100

    private Renderer Renderer;
    private Collider2D Collider;

    public delegate void ValueChange(int value);
    public event ValueChange OnValueChange;

    private void Start()
    {
        Renderer = GetComponentInParent<Renderer>();
        Collider = GetComponent<Collider2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Bullet bullet = collision.transform.GetComponent<Bullet>();
        if (bullet != null)
        {
            ChangeHealth(bullet.Damage);
        }        
    }

    private void ChangeHealth(int value)
    {
        Health += value;
        Iframes();

        if (OnValueChange != null)
        {
            OnValueChange(Health);
        }
    }
    
    private void Iframes()
    {
        StartCoroutine(_Iframes());
    }

    IEnumerator _Iframes()
    {
        Collider.enabled = false;

        //Blink slowly
        float timer = 3;
        while(timer > 0)
        {
            Renderer.enabled = !Renderer.enabled;
            timer -= 0.2f;
            yield return new WaitForSeconds(0.2f);
        }

        Renderer.enabled = true;

        //Blink faster
        timer = 1.5f;
        while(timer > 0)
        {
            Renderer.enabled = !Renderer.enabled;
            timer -= 0.1f;
            yield return new WaitForSeconds(0.1f);
        }

        Renderer.enabled = true;

        yield return new WaitForSeconds(0.2f);
        Collider.enabled = true;
    }
}