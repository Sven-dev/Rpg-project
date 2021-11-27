using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class ObjectSorter : MonoBehaviour
{
    private SpriteRenderer Renderer;

    // Update is called once per frame
    #if UNITY_EDITOR
    private void Update()
    {
        if (Renderer == null)
        {
            Renderer = GetComponent<SpriteRenderer>();
        }

        if (!Application.isPlaying && transform.hasChanged)
        {
            SetDepth();
        }
    }
    #endif

    public void SetDepth()
    {
        Renderer.sortingOrder = -(int)(transform.position.y * 10);
    }
}