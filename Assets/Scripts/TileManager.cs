using System.Collections.Generic;
using UnityEngine;

public class TileManager : MonoBehaviour
{
    public GameObject[] tiles;
    public GameObject tileForest;
    public float zSpawn = 0;
    public static float tileLength = 15;
    private static readonly int numberOfTiles = 8;

    private List<GameObject> activeTiles = new List<GameObject>();

    void Start()
    {
        initTiles();
    }

    void Update()
    {
        if (Player.GetPos().z - tileLength > zSpawn - (numberOfTiles * tileLength)){
            SpawnTile(tiles[Random.Range(0, tiles.Length)]);
            SpawnForest();
            DeleteTile();
            zSpawn += tileLength;
        }
    }

    public void initTiles()
    {
        for (int i = 0; i < numberOfTiles; i++)
        {
            SpawnTile(tiles[Random.Range(0, tiles.Length)]);
            SpawnForest();
            zSpawn += tileLength;
        }
    }

    public void SpawnTile(GameObject tile)
    {
        GameObject obj = Instantiate(tile, transform.forward * zSpawn, transform.rotation);
        activeTiles.Add(obj);
    }

    public void SpawnForest()
    {
        Vector3 pos = transform.forward * zSpawn;
        GameObject obj = Instantiate(tileForest, pos - new Vector3(15, 0, 0), transform.rotation);
        activeTiles.Add(obj);
        obj = Instantiate(tileForest, pos - new Vector3(-15, 0, 0), transform.rotation);
        activeTiles.Add(obj);
    }

    public void DeleteTile()
    {
        Destroy(activeTiles[0]);
        activeTiles.RemoveAt(0);
    }

    public void DeleteTiles()
    {
        for (int i = 0; i < activeTiles.Count; i++)
        {
            Destroy(activeTiles[i]);
            activeTiles.RemoveAt(i);
        }
    }
}
