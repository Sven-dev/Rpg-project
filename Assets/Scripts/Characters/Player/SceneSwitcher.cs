using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//Allows the object to move from oen scene to another
public class SceneSwitcher : MonoBehaviour
{
    public SpriteRenderer Fadeout;
    private bool FadingIn;
    private bool FadingOut;

    //Switches active scenes, and sets the object's position
    public void Switch(string scene, Vector2 spawn)
    {
        SceneManager.LoadScene(scene);
        Global.Player.transform.position = spawn;

        Global.Save.ActiveScene = scene;
        Global.Save.PlayerLocation = spawn;
    }

    public void SwitchInOut(string scene, Vector2 spawnposition)
    {
        StartCoroutine(_SwitchInOut(scene, spawnposition));
    }

    //Switches between 2 scenes by fading out and in
    IEnumerator _SwitchInOut(string scene, Vector2 spawnposition)
    {
        Global.PlayerMovement.Immobile = true;
        StartCoroutine(_FadeOut());
        while(FadingOut)
        {
            yield return null;
        }

        Switch(scene, spawnposition);

        StartCoroutine(_FadeIn());
        while(FadingIn)
        {
            yield return null;
        }

        Global.PlayerMovement.Immobile = false;
    }

    //Switches between 2 scenes, but only fades in
    public void SwitchIn(string scene, Vector2 spawnposition)
    {
        StartCoroutine(_SwitchIn(scene, spawnposition));
    }

    IEnumerator _SwitchIn(string scene, Vector2 spawnposition)
    {
        Global.PlayerMovement.Immobile = true;
        Switch(scene, spawnposition);

        StartCoroutine(_FadeIn());
        while (FadingIn)
        {
            yield return null;
        }

        Global.PlayerMovement.Immobile = false;
    }

    //Switches between 2 scenes, but only fades out
    public void SwitchOut(string scene, Vector2 spawnposition)
    {
        StartCoroutine(_SwitchOut(scene, spawnposition));
    }

    IEnumerator _SwitchOut(string scene, Vector2 spawnposition)
    {
        Global.PlayerMovement.Immobile = true;
        StartCoroutine(_FadeOut());
        while (FadingOut)
        {
            yield return null;
        }

        Switch(scene, spawnposition);
        Global.PlayerMovement.Immobile = false;
    }

    //Fades the game to black
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
                yield return new WaitForSeconds(0.025f);
            }
        }

        FadingOut = false;
    }

    //Fades from black back to the game
    IEnumerator _FadeIn()
    {
        FadingIn = true;
        if (Fadeout != null)
        {
            Fadeout.color = new Color(
                Fadeout.color.r,
                Fadeout.color.g,
                Fadeout.color.b,
                1);
            while (Fadeout.color.a > 0)
            {
                Fadeout.color = new Color(
                    Fadeout.color.r,
                    Fadeout.color.g,
                    Fadeout.color.b,
                    Fadeout.color.a - 0.1f);
                yield return new WaitForSeconds(0.025f);
            }
        }

        FadingIn = false;
    }
}