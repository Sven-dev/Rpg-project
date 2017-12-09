using UnityEngine;
using System.Collections;

public class LevelLoader : MonoBehaviour
{
    public string levelFileName;
    public float TileSize;
    public ColorToPrefab[] colorToPrefab;


    // Use this for initialization
    void Start ()
    {
        LoadMap();
	}

    void LoadMap()
    {
        // Read the image data from the file in StreamingAssets
        string filePath = Application.dataPath + "/StreamingAssets/" + levelFileName;
        byte[] bytes = System.IO.File.ReadAllBytes(filePath);
        Texture2D levelMap = new Texture2D(0, 0);
        levelMap.LoadImage(bytes);

        // Get the raw pixels from the level imagemap
        Color32[] allPixels = levelMap.GetPixels32();
        int width = levelMap.width;
        int height = levelMap.height;

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {

                SpawnTileAt(allPixels[(y * width) + x], x, y);

            }
        }
    }

    void SpawnTileAt(Color32 c, int x, int y)
    {
        // If this is a transparent pixel, then it's meant to just be empty.
        if (c.a <= 0)
        {
            return;
        }

        // Find the right color in our map
        foreach (ColorToPrefab ctp in colorToPrefab)
        {

            if (c.Equals(ctp.color))
            {
                // Spawn the prefab at the right location
                GameObject go = (GameObject)Instantiate(ctp.prefab, new Vector3(x * TileSize, y * TileSize, 0), Quaternion.identity);
                go.transform.SetParent(this.transform);
                return;
            }
        }

        // If we got to this point, it means we did not find a matching color in our array.
        Debug.LogError("No color to prefab found for: " + c.ToString());
    }
}