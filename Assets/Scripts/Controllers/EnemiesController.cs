using System.Collections;
using System.Collections.Generic;
using Bee.Enums;
using Bee.Interfaces;
using Bee.Spawners;
using UnityEngine;

namespace Bee.Controllers
{
    public class EnemiesController : MonoBehaviour
    {
        [SerializeField]
        private int QuantityOfEnemies = 5;

        [SerializeField]
        private int QuantityOfFakeEnemies = 0;

        [SerializeField]
        private int TimeToAwaitToSpawn = 3;

        [SerializeField]
        private GameObject AllEnemiesSpawner;

        [SerializeField]
        private GameObject EnemiesParent;

        [SerializeField]
        private Transform PositionToCreate;

        /// <summary>
        /// Define the enemies spawner, currently it will be an EnemiesSpawner, but can be another
        /// type of the enemies that implements ISpawner
        /// </summary>
        private ISpawner EnemiesSpawner;

        [Header("Controllers")]
        [SerializeField]
        private PunctuationController PunctuationController;

        void Awake()
        {
            EnemiesSpawner = AllEnemiesSpawner.GetComponent<EnemiesSpawner>();
            PunctuationController = GameObject.FindGameObjectWithTag(Tags.PunctuationController)
                .GetComponent<PunctuationController>();
        }

        void Start()
        {
            PunctuationController.SetQuantityOfBeesByEnemies(QuantityOfEnemies);
            StartCoroutine(CreateEnemies());
        }

        public IEnumerator CreateEnemies()
        {
            EnemiesSpawner.Spawn(PositionToCreate, EnemiesParent.transform);

            QuantityOfEnemies--;

            yield return new WaitForSeconds(TimeToAwaitToSpawn);

            if (QuantityOfEnemies > 0)
            {
                StartCoroutine(CreateEnemies());
                yield return null;
            }

            yield return null;
        }
    }
}
