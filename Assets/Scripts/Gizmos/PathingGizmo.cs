using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
#if UNITY_EDITOR
using UnityEditor;
#endif

//Draws a path between all waypoints and the associated character
[ExecuteInEditMode]
public class PathingGizmo : MonoBehaviour
{
    public Movement Character;
    private List<Transform> Waypoints;
    private Color PathingColor;

    private void Start()
    {
        if (Application.isEditor)
        {
            PathingColor = new Color(1, 0, 1, 0.5f);
            InvokeRepeating("UpdateGizmos", 0, 1.0f);
        }
    }

    private void UpdateGizmos()
    {
        if (Waypoints == null || Waypoints.Count != transform.childCount)
        {
            Waypoints = transform.Cast<Transform>().ToList();
        }
    }

    //Returns true if this object, its parent or any of its waypoints are selected
    private bool Selected()
    {
        #if UNITY_EDITOR
        if (Selection.activeGameObject == gameObject)
        {
            return true;
        }

        if (Selection.activeGameObject == transform.parent.gameObject)
        {
            return true;
        }

        foreach (Transform waypoint in Waypoints)
        {
            if (Selection.activeGameObject == waypoint.gameObject)
            {
                return true;
            }
        }
             
        #endif
        return false;
    }

    private void OnDrawGizmos()
    {
        if (Selected())
        {
            Gizmos.color = PathingColor;
            if (Character != null)
            {
                Gizmos.DrawLine(Character.transform.position, Waypoints[0].transform.position);
                for (int i = 0; i < Waypoints.Count - 1; i++)
                {
                    Gizmos.DrawCube(Waypoints[i].transform.position, Vector3.one / 10);
                    Gizmos.DrawLine(Waypoints[i].transform.position, Waypoints[i + 1].transform.position);
                }

                Gizmos.DrawCube(Waypoints[Waypoints.Count - 1].transform.position, Vector3.one / 10);
            }
        }
    }

    private void DrawStraight(Vector2 position, Vector2 target)
    {
        Gizmos.DrawLine(position, target);
    }
}