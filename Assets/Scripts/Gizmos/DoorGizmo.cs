using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if  UNITY_EDITOR
using UnityEditor;
#endif

//shows the door hitbox
[ExecuteInEditMode]
public class DoorGizmo : MonoBehaviour
{
    private BoxCollider2D Boxcollider;
    private Color DoorCollor;
    private RoomDoor Door;

    // Use this for initialization
    void Start()
    {
        if (Application.isEditor)
        {
            Boxcollider = GetComponent<BoxCollider2D>();

            DoorCollor = new Color(0, 1, 0, 0.5f);
            Door = GetComponent<RoomDoor>();
        }
    }

    //Returns true if this object or its children are selected
    private bool Selected()
    {
        #if UNITY_EDITOR
        if (Selection.activeGameObject == gameObject || Selection.activeGameObject == transform.GetChild(0).gameObject)
        {
            return true;
        }

        #endif
        return false;
    }

    private void OnDrawGizmos()
    {
        //Draw a shape where the door is
        Gizmos.color = DoorCollor;
        Vector2 position = transform.position;

        if (Boxcollider != null)
        {
            position = new Vector2(transform.position.x, transform.position.y) + Boxcollider.offset;
            Gizmos.DrawCube(position, Boxcollider.size);
        }

        if (Selected())
        {
            //Draw a shape where the spawn position is
            if (transform.childCount != 0)
            {
                Gizmos.DrawCube(transform.GetChild(0).position, Vector3.one * 0.2f);
            }

            //Draw a line between the two doors
            if (Door.Destination != null)
            {
                DoorGizmo dest = Door.Destination.GetComponent<DoorGizmo>();
                if (dest.Boxcollider != null)
                {
                    Vector3 offset = dest.Boxcollider.offset;
                    Gizmos.DrawLine(position, Door.Destination.transform.position + offset);
                }
            }
        }
    }
}