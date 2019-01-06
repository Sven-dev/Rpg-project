using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System;

public class BossBar : HealthBar
{
    private List<Transform> Bounds;
    [Space]
    public Image ValueIndicator;
    public Image DamageIndicator;
    public Image HealIndicator;
    [Space]
    private int CurrentHealth;
    private bool Fading;
    public List<Image> Bar;

    // Use this for initialization
    void Awake()
    {
        Bounds = new List<Transform>();
        foreach(Transform bound in transform.GetChild(1))
        {
            Bounds.Add(bound);
        }

        //should probably be somewhere else
        CurrentHealth = 100;
	}

    protected override void SetUI(int health)
    {
        CurrentHealth = health;
    }

    protected override IEnumerator _UpdateUI(int health)
    {
        SetBarAlpha(1);

        //Heal
        if (health > CurrentHealth)
        {
            #region Heal
            //Sets the heal indicator to the right position
            HealIndicator.enabled = true;
            HealIndicator.transform.position = LinearPercentage(health);

            yield return new WaitForSeconds(0.5f);
            //Moves the damage indicator to the health indicator over time
            while (HealIndicator.transform.position != ValueIndicator.transform.position)
            {
                ValueIndicator.transform.position = Vector2.MoveTowards(
                    ValueIndicator.transform.position,
                    HealIndicator.transform.position,
                    Time.deltaTime * 5);

                yield return new WaitForSeconds(Time.deltaTime * 2.5f);
            }

            HealIndicator.enabled = false;
            #endregion
        }
        //Damage
        else if (health < CurrentHealth)
        {
            #region Damage
            //Sets the health indicator to the right position
            ValueIndicator.transform.position = LinearPercentage(health);
            DamageIndicator.enabled = true;
            //Sets the damage indicator to the right position
            DamageIndicator.transform.position = LinearPercentage(CurrentHealth);

            yield return new WaitForSeconds(0.5f);
            //Moves the damage indicator to the health indicator over time
            while (DamageIndicator.transform.position != ValueIndicator.transform.position)
            {
                DamageIndicator.transform.position = Vector2.MoveTowards(
                    DamageIndicator.transform.position,
                    ValueIndicator.transform.position,
                    Time.deltaTime * 5);

                yield return new WaitForSeconds(Time.deltaTime * 2.5f);
            }

            DamageIndicator.enabled = false;
            #endregion
        }

        CurrentHealth = health;
    }

    //Calculates the linear fill of the bar
    private Vector2 LinearPercentage(float percentage)
    {
        print(Vector2.Lerp(Bounds[0].position, Bounds[1].position, percentage / 100));
        return Vector2.Lerp(Bounds[0].position, Bounds[1].position, percentage / 100);
    }

    //Fades out the bar after showing it a bit
    protected virtual IEnumerator _FadeOut()
    {
        Fading = true;
        yield return new WaitForSeconds(0.5f);

        while (Fading && Bar[0].color.a > 0)
        {
            SetBarAlpha(-0.1f);
            yield return new WaitForSeconds(0.1f);
        }
    }
    
    //Highers or lowers the oppacity of the healthbar
    void SetBarAlpha(float alpha)
    {
        Color temp = new Color(0, 0, 0, alpha);
        foreach (Image i in Bar)
        {
            i.color += temp;
        }
    }
}