using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Draws the borders of the camera bounds
[ExecuteInEditMode]
public class CameraBoundGizmo : MonoBehaviour
{
    private List<Transform> Bounds;
    private Color BoundColor;

    private void Start()
    {
        if (Application.isEditor)
        {
            BoundColor = new Color(0, 0, 1, 0.5f);
            Bounds = new List<Transform>();
            foreach (Transform child in transform)
            {
                Bounds.Add(child);
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = BoundColor;
        if (Bounds != null)
        {
            Vector2 topleft = new Vector2(Bounds[0].position.x, Bounds[1].position.y);
            Vector2 bottomright = new Vector2(Bounds[1].position.x, Bounds[0].position.y);

            Gizmos.DrawLine(Bounds[0].position, topleft);
            Gizmos.DrawLine(topleft, Bounds[1].position);
            Gizmos.DrawLine(Bounds[1].position, bottomright);
            Gizmos.DrawLine(bottomright, Bounds[0].position);

            Gizmos.DrawCube(Bounds[0].position, Vector3.one / 10);
            Gizmos.DrawCube(Bounds[1].position, Vector3.one / 10);
            Gizmos.DrawCube(topleft, Vector3.one / 20);
            Gizmos.DrawCube(bottomright, Vector3.one / 20);
        }
    }
}