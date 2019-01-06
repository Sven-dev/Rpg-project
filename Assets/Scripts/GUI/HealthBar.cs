using System.Collections;
using UnityEngine;

public abstract class HealthBar : MonoBehaviour
{
    public void Link(HealthManager manager)
    {
        manager.OnHealthChange += UpdateUI;
        SetUI(manager.health);
    }

    private void UpdateUI(int health)
    {
        StartCoroutine(_UpdateUI(health));
    }

    protected abstract IEnumerator _UpdateUI(int health);

    protected abstract void SetUI(int health);
}