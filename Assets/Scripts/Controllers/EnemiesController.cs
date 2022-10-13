using System.Collections;
using System.Collections.Generic;
using Bee.Interfaces;
using Bee.Spawners;
using UnityEngine;

public class EnemiesController : MonoBehaviour
{
    [SerializeField]
    private int QuantityOfEnemies = 5;

    [SerializeField]
    private int TimeToAwaitToSpawn = 6;

    [SerializeField]
    private GameObject AllEnemiesSpawner;

    /// <summary>
    /// Define the enemies spawner, currently it will be an EnemiesSpawner, but can be another
    /// type of the enemies that implements ISpawner
    /// </summary>
    private ISpawner EnemiesSpawner;

    void Awake()
    {
        EnemiesSpawner = AllEnemiesSpawner.GetComponent<EnemiesSpawner>();
    }

    void Start()
    {
        StartCoroutine(CreateEnemies());
    }

    public IEnumerator CreateEnemies()
    {
        if (QuantityOfEnemies <= 0)
            yield return null;

        EnemiesSpawner.Spawn();

        QuantityOfEnemies--;

        yield return new WaitForSeconds(TimeToAwaitToSpawn);

        StartCoroutine(CreateEnemies());
    }
}
