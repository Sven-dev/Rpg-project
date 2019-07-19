using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy1 : MonoBehaviour
{
    public Bullet BulletPrefab;

    private Transform Bulletholder;
    private List<Bullet> Bullets;
    private List<Vector3> Directions;
    private Renderer Renderer;

	// Use this for initialization
	void Start ()
    {
        Bulletholder = transform.GetChild(0);
        Renderer = GetComponent<Renderer>();
        Directions = new List<Vector3>();
        Directions.Add(new Vector3(0, 1.5f));
        Directions.Add(new Vector3(1, 1));
        Directions.Add(new Vector3(1.5f, 0));
        Directions.Add(new Vector3(1, -1));
        Directions.Add(new Vector3(0, -1.5f));
        Directions.Add(new Vector3(-1, -1));
        Directions.Add(new Vector3(-1.5f, 0));
        Directions.Add(new Vector3(-1, 1));

        SpawnBullets();
    }

    //spawn bullets around itself
    private void SpawnBullets()
    {
        //Copy directions
        List<Vector3> directions = Directions;

        //remove one direction
        int remove = Random.Range(0, 7);
        Bullets = new List<Bullet>();

        //Spawn 7 bullets and start moving them
        for (int i = 0; i < 7; i++)
        {
            Bullet b = Instantiate(BulletPrefab, Bulletholder);
            Bullets.Add(b);
            StartCoroutine(_MoveBullet(Bullets[i], directions[i]));
        }

        StartCoroutine(_Rotate());
    }

    IEnumerator _MoveBullet(Bullet bullet, Vector3 direction)
    {
        float progress = 0;
        while (progress < 1)
        {
            bullet.transform.localPosition = Vector3.Lerp(Bulletholder.localPosition, direction * 0.15f, progress);
            progress += Time.deltaTime;
            yield return null;
        }

        progress = 0;
        while (bullet != null)
        {
            bullet.transform.localPosition += direction * Mathf.Sin(progress * 4) / 1000;
            progress += Time.deltaTime;
            yield return null;
        }
    }

    //Bullets spin around
    IEnumerator _Rotate()
    {
        while(true)
        {
            Bulletholder.Rotate(0, 0, Time.deltaTime * 50);
            yield return null;
        }
    }

    //When enemy gets hit, destroy bullets


    //Sets the render-order, relative to the other objects in the scene,
    //making it look like the object walks behind other objects
    void SetSortingLayer()
    {
        float objHeight = transform.position.y - Renderer.bounds.size.y / 2f;
        Renderer.sortingOrder = (int)(objHeight * -100);
    }
}
