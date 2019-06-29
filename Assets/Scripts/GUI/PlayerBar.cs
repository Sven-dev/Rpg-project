using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System;

public class PlayerBar : MonoBehaviour
{
    public float Min;
    public float Max;
    [Space]
    public Image ValueIndicator;
    public Image DamageIndicator;
    public Image HealIndicator;
    [Space]
    private int CurrentHealth;
    //private bool Fading;

        /*
    protected override void SetUI(int health)
    {
        ValueIndicator.fillAmount = RadialPercentage(health);
        DamageIndicator.fillAmount = RadialPercentage(health);
        HealIndicator.fillAmount = RadialPercentage(health);

        CurrentHealth = health;
    }

    protected override IEnumerator _UpdateUI(int health)
    {
        print("currenthealth: " + CurrentHealth);
        print("health: " + health);

        //Heal
        if (health > CurrentHealth)
        {
            #region Heal
            //Sets the heal-indicator to the right position
            HealIndicator.enabled = true;
            HealIndicator.fillAmount = RadialPercentage(health);

            yield return new WaitForSeconds(0.5f);
            //Moves the value-indicator to the heal-indicator over time
            while (HealIndicator.fillAmount != ValueIndicator.fillAmount)
            {
                ValueIndicator.fillAmount = Mathf.MoveTowards(ValueIndicator.fillAmount, HealIndicator.fillAmount, Time.deltaTime);
                yield return new WaitForSeconds(Time.deltaTime * 2.5f);
            }

            HealIndicator.enabled = false;
            #endregion
        }
        //Damage
        else if (health < CurrentHealth)
        {
            #region Damage
            //Sets the value-indicator to the right position
            ValueIndicator.fillAmount = RadialPercentage(health);

            //Sets the damage-indicator to the right position
            DamageIndicator.enabled = true;
            DamageIndicator.fillAmount = RadialPercentage(CurrentHealth);

            yield return new WaitForSeconds(0.5f);
            //Moves the damage-indicator to the value-indicator over time
            while (DamageIndicator.fillAmount != ValueIndicator.fillAmount)
            {
                DamageIndicator.fillAmount = Mathf.MoveTowards(DamageIndicator.fillAmount, ValueIndicator.fillAmount, Time.deltaTime);
                yield return new WaitForSeconds(Time.deltaTime * 2.5f);
            }

            DamageIndicator.enabled = false;
            #endregion
        }

        CurrentHealth = health;
        print("end health: " + CurrentHealth);
    }
    */

    //Calculates the radial fill of the bar
    private float RadialPercentage(float percentage)
    {
        return Mathf.Lerp(Min, Max, percentage / 100);
    }
}