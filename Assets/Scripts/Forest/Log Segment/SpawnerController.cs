using UnityEngine;
using System.Collections.Generic;

public class SpawnerController : Controller
{
    #region Fields
    private float coolDown;
    private float timeStamp;
    private int SpawnIndex;

    public GameObject Spawns;
    private bool[,] SpawnArray;
    public List<LogSpawner> SpawnerList;
    #endregion

    // Use this for initialization
    void Start()
    {
        coolDown = 0.55f;
        timeStamp = Time.time;
        SpawnIndex = 0;

        SpawnArray = Spawns.GetComponent<SpawnLogic>().SpawnArray();
    }

    //Checks the boolean array to check wether a log needs to be spawned
    public override void CheckLogic()
    {
        //Checks if the timer is off cooldown
        if (Time.time > timeStamp + coolDown)
        {
            Debug.Log("tick");
            timeStamp = Time.time;

            int arraycount = SpawnerList.Count;
            for (int i = 0; i < arraycount; i++)
            {
                if (SpawnArray[i, SpawnIndex] == true)
                {
                    SpawnerList[i].SpawnLog();
                }
            }

            //Upps the spawnindex counter, or resets it if it's at max
            if (SpawnIndex >= SpawnArray.GetLength(1) -1)
            {
                SpawnIndex = -1;
            }
            SpawnIndex++;
        }
    }
}
