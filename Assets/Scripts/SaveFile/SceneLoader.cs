using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SceneLoader : MonoBehaviour
{
    public abstract void LoadData();
    public abstract void SaveData();
}