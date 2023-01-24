using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Spawner : MonoBehaviour
{
    [SerializeField] private GameObject _objectToSpawn;
    [SerializeField] private int _nbToSpawn;
    private List<GameObject> Pool = new List<GameObject>();
    // max distance from the center to spawn prefab
    private float _spawnRadius = 5f;

    void SpawnObject()
    {
        // get random position on the NavMesh within the spawn radius
        Vector3 randomPos = RandomNavmeshPosition(transform.position, _spawnRadius);

        // Instantiate the prefab at the random position
        GameObject spawnedObject = Instantiate(_objectToSpawn, randomPos, Quaternion.identity);

        // add to the object Pool
        Pool.Add(spawnedObject);
    }

    Vector3 RandomNavmeshPosition(Vector3 center, float range)
    {
        // get random position inside a specified spherical range
        Vector3 randomPos = center + Random.insideUnitSphere * range;
        NavMeshHit hit;

        // Finds the nearest point based on the NavMesh within a specified range
        NavMesh.SamplePosition(randomPos, out hit, range, 1);

        return hit.position;
    }

    private void Start()
    {
        // spawn x nb of time
        for (int i = 0; i < _nbToSpawn; i++)
        {
            SpawnObject();
        }
    }
}
