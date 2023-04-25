using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileManager : MonoBehaviour
{
    public GameObject[] tilePrefabs;
    public float zSpawn = 0;
    public float tileLength = 30; // Length of road tile
    public int numberOfTiles = 5;
    private List<GameObject> activeTiles = new List<GameObject>();

    public Transform playerTransform;

    // Start is called before the first frame update
    void Start()
    {
        for(int i = 0; i < numberOfTiles; i++)
        {
            if(i == 0) // Always spawn first tile, first
                SpawnTile(0);
            else
                SpawnTile(Random.Range(0, tilePrefabs.Length - 1));
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Spawn new tile in front of player, delete active tiles behind player
        if(playerTransform.position.z - 35 > zSpawn - (numberOfTiles * tileLength)) 
        {
            SpawnTile(Random.Range(0, tilePrefabs.Length - 1));
            DeleteTile();
        }
    }

    public void SpawnTile(int tileIndex)
    {
        GameObject go = Instantiate(tilePrefabs[tileIndex], transform.forward * zSpawn, transform.rotation);
        activeTiles.Add(go);
        zSpawn += tileLength;
    }

    private void DeleteTile()
    {
        Destroy(activeTiles[0]);
        activeTiles.RemoveAt(0);
    }
}
