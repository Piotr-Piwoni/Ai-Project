using System;
using UnityEngine;
using UnityEngine.Serialization;
using static Enemy.EnemyAi;
using Random = UnityEngine.Random;

public class Spawner : MonoBehaviour
{
    [SerializeField] private bool spawning;
    [SerializeField] private int spawnAmount;
    [SerializeField] private float spawnDistance = 10f;
    
    private Vector3 _spawnPosition;

    public GameObject spawnObject;

    private void Start()
    {
        _spawnPosition = transform.position;
        
        
        if (spawnAmount > 0)
        {
            for (int i = 1; i <= spawnAmount; i++)
            {
                Instantiate(spawnObject, _spawnPosition, Quaternion.identity);
            }
        }
    }

    private void Update()
    {
        _spawnPosition +=  GetRandomDirection() * Random.Range(2f, spawnDistance);
        
        if (spawning)
        {
            Instantiate(spawnObject, _spawnPosition, Quaternion.identity);
        }
    }
}
