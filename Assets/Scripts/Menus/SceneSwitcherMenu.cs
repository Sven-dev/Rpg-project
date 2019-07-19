using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneSwitcherMenu : MonoBehaviour
{
    private KeyboardControllerMenu Controller;
    private SpriteRenderer Fadeout;
    private bool FadingOut;
    private bool Loading;

    private void Start()
    {
        Controller = GetComponent<KeyboardControllerMenu>();
        Fadeout = transform.GetChild(0).GetComponent<SpriteRenderer>();
    }

    //Switches between 2 scenes
    public void SwitchScene()
    {
        Controller.ToggleControls();
        StartCoroutine(_SwitchScene());
    }

    IEnumerator _SwitchScene()
    {
        //Fade out
        StartCoroutine(_FadeOut());
        while (FadingOut)
        {
            yield return null;
        }
        
        //Switch scenes
        StartCoroutine(_Switch());
        while (Loading)
        {
            yield return null;
        }
    }

    IEnumerator _Switch()
    {
        Loading = true;
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(1);
        while (!asyncLoad.isDone)
        {
            yield return null;
        }

        SceneManager.SetActiveScene(SceneManager.GetActiveScene());
        Loading = false;
    }

    //Fades the game out
    IEnumerator _FadeOut()
    {
        FadingOut = true;
        if (Fadeout != null)
        {
            Fadeout.color = new Color(
                Fadeout.color.r,
                Fadeout.color.g,
                Fadeout.color.b,
                0);
            while (Fadeout.color.a < 1)
            {
                Fadeout.color = new Color(
                    Fadeout.color.r,
                    Fadeout.color.g,
                    Fadeout.color.b,
                    Fadeout.color.a + 0.1f);
                yield return new WaitForSeconds(0.05f);
            }
        }

        FadingOut = false;
    }
}
