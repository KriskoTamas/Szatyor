using System.Collections.Generic;
using UnityEngine;

public class TileManager : MonoBehaviour
{
    public GameObject[] tiles;
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
            SpawnTile(Random.Range(0, tiles.Length));
            DeleteTile();
        }
    }

    public void initTiles()
    {
        for (int i = 0; i < numberOfTiles; i++)
        {
            SpawnTile(Random.Range(0, tiles.Length));
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

    public void DeleteTiles()
    {
        for (int i = 0; i < activeTiles.Count; i++)
        {
            Destroy(activeTiles[i]);
            activeTiles.RemoveAt(i);
        }
    }
}
