using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Transform Bar;
    private Image[] Images;

	// Use this for initialization
	void Start ()
    {
        Images = GetComponentsInChildren<Image>();
        Bar.localPosition = new Vector2(0, 0);
        UpdateUI();
	}

    void UpdateUI()
    {
        SetBars(1);
        //25 needs to be health value
        Bar.localPosition = new Vector2(25 - 100, 0);

        StartCoroutine(_VisibilityTimer());
    }

    IEnumerator _VisibilityTimer()
    {
        yield return new WaitForSeconds(0.25f);

        while (Images[0].color.a > 0)
        {
            SetBars(-0.2f);
            yield return new WaitForSeconds(0.05f);
        }
    }
    
    void SetBars(float alpha)
    {
        Color temp = new Color(0, 0, 0, alpha);
        foreach (Image i in Images)
        {
            i.color += temp;
        }
    }
}
