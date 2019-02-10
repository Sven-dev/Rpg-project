using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//shows the door hitbox
[ExecuteInEditMode]
public class DoorGizmo : MonoBehaviour
{
    private BoxCollider2D Boxcollider;
    private CircleCollider2D Circlecollider;
    private Color DoorCollor;

    // Use this for initialization
    void Start()
    {
        if (Application.isEditor)
        {
            Boxcollider = GetComponent<BoxCollider2D>();
            Circlecollider = GetComponent<CircleCollider2D>();

            DoorCollor = new Color(0, 1, 0, 0.5f);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = DoorCollor;
        if (Boxcollider != null)
        {
            Vector2 position = new Vector2(transform.position.x, transform.position.y) + Boxcollider.offset;
            Gizmos.DrawCube(position, Boxcollider.size);
        }
        else if (Circlecollider != null)
        {
            Vector2 position = new Vector2(transform.position.x, transform.position.y) + Circlecollider.offset;
            Gizmos.DrawSphere(position, Circlecollider.radius);
        }
    }
}