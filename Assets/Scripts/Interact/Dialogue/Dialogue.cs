using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public abstract class Dialogue : MonoBehaviour
{
    [HideInInspector]
    public bool Active;
    [HideInInspector]
    public bool Writing;
    public string Text;
}