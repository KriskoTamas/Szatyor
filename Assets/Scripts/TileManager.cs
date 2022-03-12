using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileManager : MonoBehaviour
{
    public GameObject[] tiles;
    public Transform playerTransform;
    public float zSpawn = 0;
    public float tileLength = 30;
    public int numberOfTiles = 7;

    private List<GameObject> activeTiles = new List<GameObject>();

    void Start()
    {
        for (int i = 0; i < numberOfTiles; i++)
        {
            SpawnTile(0);
        }
    }

    void Update()
    {
        if (playerTransform.position.z - tileLength > zSpawn - (numberOfTiles * tileLength)){
            SpawnTile(0);
            DeleteTile();
        }
    }

    public void SpawnTile(int tileIndex)
    {
        GameObject obj = Instantiate(tiles[tileIndex], transform.forward * zSpawn, transform.rotation);
        activeTiles.Add(obj);
        zSpawn += tileLength;
    }

    public void DeleteTile()
    {
        Destroy(activeTiles[0]);
        activeTiles.RemoveAt(0);
    }
}
