using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class CharChoice : Choice {

    #region UnityFields
    public Image Picture;
    #endregion

    #region Spawning
    //Spawns the proper dialoguebox on the position of the camera
    public override void SpawnDialogueBox()
    {
        base.SpawnDialogueBox();

        Image I = DialogueBoxClone.transform.Find("Canvas/Image").GetComponent<Image>();

        if (I != null)
        {
            I = Picture;
        }
    }
    #endregion
}
