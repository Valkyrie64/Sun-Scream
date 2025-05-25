using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerScript : MonoBehaviour
{
    public GameObject[] spawnAreas;
    public GameObject[] itemsToSpawn;
    public float SpawnChance;
    public int SpawnType;
    public GameObject GOS;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < spawnAreas.Length;)
        {
            SpawnChance = Random.Range(0, 100);
            SpawnType = Random.Range(0, 2);
            switch (SpawnChance)
            {
                case > 65f:
                    Instantiate(itemsToSpawn[SpawnType], spawnAreas[i].transform.position, Quaternion.identity, GOS.transform);
                    break;
                case < 35f:
                    break;
            }
            i++;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
