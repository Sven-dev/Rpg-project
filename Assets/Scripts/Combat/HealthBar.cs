using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Linq;

public class HealthBar : MonoBehaviour
{
    public List<Image> Bar;
    [Space]
    public Image HealthIndicator;
    public Image DamageIndicator;
    public Image HealIndicator;

    private List<Transform> Bounds;
    private int CurrentHealth;
    private bool Fading;

    // Use this for initialization
    void Awake()
    {
        Bounds = new List<Transform>();
        foreach(Transform bound in transform.GetChild(1))
        {
            Bounds.Add(bound);
        }
	}

    public void Link(HealthManager manager)
    {
        manager.OnHealthChange += UpdateUI;
    }

    void UpdateUI(int health)
    {
        StartCoroutine(_UpdateUI(health));
    }

    IEnumerator _UpdateUI(int health)
    {
        //In case of rapid updated, disables fading of earlier updates (still needs stress-testing)
        Fading = false;
        SetBarAlpha(1);

        //Heal
        if (health > CurrentHealth)
        {
            #region Heal
            //Sets the heal indicator to the right position
            HealIndicator.enabled = true;
            HealIndicator.transform.position = Vector2.Lerp(Bounds[0].position, Bounds[1].position, health / 100f);

            yield return new WaitForSeconds(0.5f);
            //Moves the damage indicator to the health indicator over time
            while (HealIndicator.transform.position != HealthIndicator.transform.position)
            {
                HealthIndicator.transform.position = Vector2.MoveTowards(
                    HealthIndicator.transform.position,
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
            HealthIndicator.transform.position = Vector2.Lerp(Bounds[0].position, Bounds[1].position, health / 100f);
            DamageIndicator.enabled = true;
            //Sets the damage indicator to the right position
            DamageIndicator.transform.position = Vector2.Lerp(Bounds[0].position, Bounds[1].position, CurrentHealth / 100f);

            yield return new WaitForSeconds(0.5f);
            //Moves the damage indicator to the health indicator over time
            while(DamageIndicator.transform.position != HealthIndicator.transform.position)
            {
                DamageIndicator.transform.position = Vector2.MoveTowards(
                    DamageIndicator.transform.position,
                    HealthIndicator.transform.position,
                    Time.deltaTime * 5);

                yield return new WaitForSeconds(Time.deltaTime * 2.5f);
            }

            DamageIndicator.enabled = false;
            #endregion
        }

        CurrentHealth = health;
        StartCoroutine(_FadeOut());
    }

    //Fades out the bar after showing it a bit
    IEnumerator _FadeOut()
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