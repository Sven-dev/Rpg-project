using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;

//Draws a path between all waypoints and the associated character
[ExecuteInEditMode]
public class PathingGizmo : MonoBehaviour
{
    public Movement Character;
    private List<Waypoint> Waypoints;
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
        if (Waypoints == null)
        {
            Waypoints = transform.GetComponentsInChildren<Waypoint>().ToList();
        }
        else if (Waypoints.Count != transform.childCount)
        {
            Waypoints = transform.GetComponentsInChildren<Waypoint>().ToList();
        }
    }

    //Returns true if this object, its parent or any of its waypoints are selected
    private bool Selected()
    {
        if (Selection.activeGameObject == gameObject)
        {
            return true;
        }

        if (Selection.activeGameObject == transform.parent.gameObject)
        {
            return true;
        }

        foreach (Waypoint waypoint in Waypoints)
        {
            if (Selection.activeGameObject == waypoint.gameObject)
            {
                return true;
            }
        }

        return false;
    }

    private void OnDrawGizmos()
    {
        if (Selected())
        {
            Gizmos.color = PathingColor;
            if (Character != null)
            {
                if (Waypoints[0].Pattern == MoveOption.X_then_Y)
                {
                    DrawXY(Character.transform.position, Waypoints[0].transform.position);
                }
                else if (Waypoints[0].Pattern == MoveOption.Y_then_X)
                {
                    DrawYX(Character.transform.position, Waypoints[0].transform.position);
                }
                else if (Waypoints[0].Pattern == MoveOption.Diagonal)
                {
                    DrawDiagonal(Character.transform.position, Waypoints[0].transform.position);
                }
                else if (Waypoints[0].Pattern == MoveOption.Straight)
                {
                    DrawStraight(Character.transform.position, Waypoints[0].transform.position);
                }
            }

            for (int i = 0; i < Waypoints.Count - 1; i++)
            {
                Gizmos.DrawCube(Waypoints[i].transform.position, Vector3.one / 10);
                if (Waypoints[i + 1].Pattern == MoveOption.X_then_Y)
                {
                    DrawXY(Waypoints[i].transform.position, Waypoints[i + 1].transform.position);
                }
                else if (Waypoints[i + 1].Pattern == MoveOption.Y_then_X)
                {
                    DrawYX(Waypoints[i].transform.position, Waypoints[i + 1].transform.position);
                }
                else if (Waypoints[i + 1].Pattern == MoveOption.Diagonal)
                {
                    DrawDiagonal(Waypoints[i].transform.position, Waypoints[i + 1].transform.position);
                }
                else if (Waypoints[i + 1].Pattern == MoveOption.Straight)
                {
                    DrawStraight(Waypoints[i].transform.position, Waypoints[i + 1].transform.position);
                }
            }

            Gizmos.DrawCube(Waypoints[Waypoints.Count - 1].transform.position, Vector3.one / 10);
        }
    }

    private void DrawXY(Vector2 position, Vector2 target)
    {
        Vector2 corner = new Vector2(target.x, position.y);
        Gizmos.DrawLine(position, corner);
        Gizmos.DrawLine(corner, target);
        Gizmos.DrawCube(corner, Vector3.one / 20);
    }

    private void DrawYX(Vector2 position, Vector2 target)
    {
        Vector2 corner = new Vector2(position.x, target.y);
        Gizmos.DrawLine(position, corner);
        Gizmos.DrawLine(corner, target);
        Gizmos.DrawCube(corner, Vector3.one / 20);
    }

    private void DrawDiagonal(Vector2 position, Vector2 target)
    {

    }

    private void DrawStraight(Vector2 position, Vector2 target)
    {
        Gizmos.DrawLine(position, target);
    }
}