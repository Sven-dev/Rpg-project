using System.Collections;
using UnityEngine;

public abstract class HealthBar : MonoBehaviour
{
    private void Awake()
    {
        Global.PlayerHealth.OnHealthChange += UpdateUI;
    }

    private void UpdateUI(int health)
    {
        StartCoroutine(_UpdateUI(health));
    }

    protected abstract IEnumerator _UpdateUI(int health);

    protected abstract void SetUI(int health);
}