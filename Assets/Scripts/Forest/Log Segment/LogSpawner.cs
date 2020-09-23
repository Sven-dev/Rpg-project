using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class LogSpawner : MonoBehaviour {

    public string Direction;
    public float RollingSpeed;

    #region UnityFields
    public GameObject LogPrefab;
    private List<GameObject> objectlist;
    private SpriteRenderer renderer;

    public Sprite SprClosed;
    public Sprite SprOpen;

    #endregion

    // Use this for initialization
    void Start()
    {
        objectlist = new List<GameObject>();

        renderer = GetComponent<SpriteRenderer>();
    }

    //Spawns a log, and sets its variables (direction and speed)
    public void SpawnLog()
    {
        Vector3 position = transform.position;

        if (Direction == "S")
        {
            position.y -= 0.2f;
        }

        else if (Direction == "W")
        {
            position.y += 0.2f;
        }

        else // alternate direction
        {
            Debug.Log("ERROR: WRONG DIRECTION");
        }

        GameObject LogClonecurrent = GameObject.Instantiate(LogPrefab, position, new Quaternion(0, 0, 0, 0)) as GameObject;
        RollingLog currentlog = LogClonecurrent.GetComponent<RollingLog>();
        currentlog.SetVariables(Direction, RollingSpeed);

        OpenHatch();
    }

    void OpenHatch()
    {
        renderer.sprite = SprOpen;

        StartCoroutine(Wait());
    }

    IEnumerator Wait()
    {
        yield return new WaitForSeconds(0.5f);

        renderer.sprite = SprClosed;
    }
}
