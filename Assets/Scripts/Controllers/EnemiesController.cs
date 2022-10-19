using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
        private int EnemiesSpawned = 0;

        [SerializeField]
        private int QuantityOfFakeEnemies = 0;

        [SerializeField]
        private float TimeToAwaitToSpawn = 3;

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

        [SerializeField]
        private List<GameObject> EnemiesCreated;

        [Header("Controllers")]
        [SerializeField]
        private GameController GameController;
        private PunctuationController PunctuationController;

        void Awake()
        {
            EnemiesCreated = new List<GameObject>();
            EnemiesSpawner = AllEnemiesSpawner.GetComponent<EnemiesSpawner>();
            PunctuationController = GameObject.FindGameObjectWithTag(Tags.PunctuationController)
                .GetComponent<PunctuationController>();
            GameController = GameObject.FindGameObjectWithTag(Tags.GameController)
                .GetComponent<GameController>();
        }

        void Start()
        {
            PunctuationController.SetQuantityOfBeesByEnemies(QuantityOfEnemies);
            StartCoroutine(CreateEnemies());
        }

        void Update()
        {
            CleanDeadEnemies();

            if (!AllEnemiesHaveDied())
                return;

            GameController.NextLevel();
            StartCoroutine(CreateEnemies());
        }

        private void CleanDeadEnemies()
        {
            EnemiesCreated = EnemiesCreated.Where(x => x != null).ToList();
        }

        public bool AllEnemiesHaveDied()
        {
            // Not all enemies were spawned yet
            if (EnemiesSpawned < (QuantityOfEnemies + QuantityOfFakeEnemies))
                return false;

            return EnemiesCreated.Count == 0;
        }

        /// <summary>
        /// Mechanics that have to happen when the player go to the next phase
        /// </summary>
        public void OnNextLevel()
        {
            EnemiesSpawned = 0;
            QuantityOfEnemies++;
            QuantityOfFakeEnemies++;
            TimeToAwaitToSpawn = TimeToAwaitToSpawn - (TimeToAwaitToSpawn * 0.05f);

            PunctuationController.SetQuantityOfBeesByEnemies(QuantityOfEnemies);
        }

        public IEnumerator CreateEnemies()
        {
            var createdEnemy = EnemiesSpawner.Spawn(PositionToCreate, EnemiesParent.transform);

            EnemiesCreated.Add(createdEnemy);

            EnemiesSpawned++;

            yield return new WaitForSeconds(TimeToAwaitToSpawn);

            if (EnemiesSpawned < (QuantityOfEnemies + QuantityOfFakeEnemies))
            {
                StartCoroutine(CreateEnemies());
                yield return null;
            }

            yield return null;
        }
    }
}
