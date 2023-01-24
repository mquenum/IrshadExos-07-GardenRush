using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Spawner : MonoBehaviour
{
    [SerializeField] private GameObject _objectToSpawn;
    [SerializeField] private int _nbToSpawn;
    [SerializeField] private float spawnInterval =5f ;
    private Queue<Pooltem> pool = new Queue<Pooltem>();
    // max distance from the center to spawn prefab
    private float _spawnRadius = 5f;
    private float time = 0f;

    private void Start()
    {
        // spawn x nb of time
        for (int i = 0; i < _nbToSpawn; i++)
        {
            FillObjectPool();
        }
    }

    private void Update()
    {
        time += Time.deltaTime;
        if (time >= spawnInterval)
        {
            //On reset le timer
            time = 0f;

            //On spawn un objet si possible
            if (pool.Count == 0 )
            {
                return;
            }
            else
            {
                SpawnObject();
            }

        }


    }

    private void SpawnObject()
    {
        Pooltem spawnedObject = pool.Dequeue();
        Vector3  newPos = RandomNavmeshPosition(transform.position, _spawnRadius);
        spawnedObject.transform.position = newPos;
        spawnedObject.gameObject.SetActive(true);
    }

    void FillObjectPool()
    {
        // get random position on the NavMesh within the spawn radius
        Vector3 randomPos = RandomNavmeshPosition(transform.position, _spawnRadius);

        // Instantiate the prefab at the random position
        GameObject spawnedObject = Instantiate(_objectToSpawn, randomPos, Quaternion.identity);
        Pooltem poolItem = spawnedObject.AddComponent<Pooltem>();
        poolItem.spawner = this;

        // On cache l'objet
        spawnedObject.SetActive(false);

        // add to the object Pool
        pool.Enqueue(poolItem);
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

    public void AddToPool(Pooltem itemToAdd)
    {
        pool.Enqueue(itemToAdd);

    }

}
