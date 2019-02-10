using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//shows the hitbox or trigger
[ExecuteInEditMode]
public class ColliderGizmo : MonoBehaviour
{
    private BoxCollider2D Boxcollider;
    private CircleCollider2D Circlecollider;
    private Color ColliderColor;
    private Color TriggerColor;

	// Use this for initialization
	void Start ()
    {
        if (Application.isEditor)
        {
            Boxcollider = GetComponent<BoxCollider2D>();
            Circlecollider = GetComponent<CircleCollider2D>();

            ColliderColor = new Color(1, 0, 0, 0.5f);
            TriggerColor = new Color(1, 0.92f, 0.016f, 0.5f);
        }
    }

    private void OnDrawGizmos()
    {
        if (Boxcollider != null)
        {
            if (Boxcollider.isTrigger)
            {
                Gizmos.color = TriggerColor;
            }
            else
            {
                Gizmos.color = ColliderColor;
            }

            Vector2 position = new Vector2(transform.position.x, transform.position.y) + Boxcollider.offset;
            Gizmos.DrawCube(position, Boxcollider.size);
        }

        if (Circlecollider != null)
        {
            if (Circlecollider.isTrigger)
            {
                Gizmos.color = TriggerColor;
            }
            else
            {
                Gizmos.color = ColliderColor;
            }

            Vector2 position = new Vector2(transform.position.x, transform.position.y) + Circlecollider.offset;
            Gizmos.DrawSphere(position, Circlecollider.radius);
        }
    }
}