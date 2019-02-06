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
    private bool Loading;

    //Switches between 2 rooms by disabling the original room, and enabling the new one
    public void SwitchRoom(GameObject room, Vector2 spawn, bool fadeout, bool fadein)
    {
        StartCoroutine(_SwitchRoom(room, spawn, fadeout, fadein));
    }

    IEnumerator _SwitchRoom(GameObject room, Vector2 spawn, bool fadeout, bool fadein)
    {
        //Freeze player
        Global.PlayerMovement.Immobile = true;

        //If fadeout is true, fade out
        if (fadeout)
        {
            StartCoroutine(_FadeOut());
            while (FadingOut)
            {
                yield return null;
            }
        }

        Switch(room, spawn);

        //If fadein is true, fade in
        if (fadein)
        {
            StartCoroutine(_FadeIn());
            while (FadingIn)
            {
                yield return null;
            }
        }

        //Unfreeze player
        Global.PlayerMovement.Immobile = false;
    }

    public void Switch(GameObject room, Vector2 spawn)
    {
        //Enable correct room
        Global.ActiveRoom.SetActive(false);
        Global.ActiveRoom = room;
        Global.Save.ActiveRoom = ConvertRoom(room);
        Global.CameraMover.SetBounds();
        Global.ActiveRoom.SetActive(true);

        //Set player location
        Global.Player.transform.position = spawn;
        Global.Save.PlayerLocation = spawn;
    }

    //Switches between 2 scenes
    public void SwitchScene(string scene, int room, Vector2 spawn, bool fadeout, bool fadein)
    {
        StartCoroutine(_SwitchScene(scene, room, spawn, fadeout, fadein));
    }

    IEnumerator _SwitchScene(string scene, int room, Vector2 spawn, bool fadeout, bool fadein)
    {
        //Freeze player
        Global.PlayerMovement.Immobile = true;

        //If fadeout is true, fade out
        if (fadeout)
        {
            StartCoroutine(_FadeOut());
            while (FadingOut)
            {
                yield return null;
            }
        }

        //Switch scenes
        StartCoroutine(_Switch(scene, room, spawn));
        while(Loading)
        {
            yield return null;
        }

        //If fadein is true, fade in
        if (fadein)
        {
            StartCoroutine(_FadeIn());
            while (FadingIn)
            {
                yield return null;
            }
        }

        //Unfreeze player
        Global.PlayerMovement.Immobile = false;
    }

    IEnumerator _Switch(string scene, int room, Vector2 spawn)
    {
        Loading = true;

        //Switch scene
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(scene);
        while (!asyncLoad.isDone)
        {
            yield return null;
        }

        SceneManager.SetActiveScene(SceneManager.GetActiveScene());
        Global.Save.ActiveScene = scene;

        //Enable correct room
        Global.ActiveRoom = ConvertRoom(room);
        Global.Save.ActiveRoom = room;
        Global.CameraMover.SetBounds();
        Global.ActiveRoom.SetActive(true);

        //Set player spawn
        Global.Player.transform.position = spawn;
        Global.Save.PlayerLocation = spawn;

        Loading = false;
    }

    //Converts the room from gameobject to hierarchy-location
    private int ConvertRoom(GameObject room)
    {
        return room.transform.GetSiblingIndex();
    }

    //Converts the room from hierarchy-location to gameobject
    private GameObject ConvertRoom(int index)
    {
        GameObject[] root = SceneManager.GetActiveScene().GetRootGameObjects();
        return root[0].transform.GetChild(index).gameObject;
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
                yield return new WaitForSeconds(0.025f);
            }
        }

        FadingOut = false;
    }

    //Fades the game in
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