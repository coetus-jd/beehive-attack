using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Bee.Enemies;
using Bee.Enums;
using Bee.Interfaces;
using Bee.Spawners;
using UnityEngine;

namespace Bee.Controllers
{
    public class EnemiesController : MonoBehaviour
    {
        [Header("Enemies quantity control")]
        [SerializeField]
        private int QuantityOfNormalEnemies;
        
        [SerializeField]
        private int NormalEnemiesSpawned;

        [SerializeField]
        private int QuantityOfFakeEnemies;

        [SerializeField]
        private int FakeEnemiesSpawned;

        [SerializeField]
        private int QuantityOfBeeKeepers;

        [SerializeField]
        private int BeeKeepersSpawned;

        private int TotalNumberOfEnemies
        {
            get
            {
                return QuantityOfNormalEnemies + QuantityOfFakeEnemies + QuantityOfBeeKeepers;
            }
        }

        private int TotalNumberOfSpawnedEnemies
        {
            get
            {
                return NormalEnemiesSpawned + FakeEnemiesSpawned + BeeKeepersSpawned;
            }
        }

        public int TotalNumberOfRealEnemies
        {
            get
            {
                return QuantityOfNormalEnemies + QuantityOfBeeKeepers;
            }
        }

        [Header("Spawn")]
        [SerializeField]
        private int LevelToSpawnBeeKeeper = 3;

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

        void Start()
        {
            EnemiesCreated = new List<GameObject>();
            EnemiesSpawner = AllEnemiesSpawner.GetComponent<EnemiesSpawner>();
            PunctuationController = GameObject.FindGameObjectWithTag(Tags.PunctuationController)
                .GetComponent<PunctuationController>();
            GameController = GameObject.FindGameObjectWithTag(Tags.GameController)
                .GetComponent<GameController>();

            PunctuationController.SetQuantityOfBeesByEnemies(QuantityOfNormalEnemies);
            StartCoroutine(CreateEnemies());
        }

        void Update()
        {
            CleanDeadEnemies();

            if (!AllEnemiesHaveDied())
                return;

            if (GameController == null)
                GameController = GameObject.FindGameObjectWithTag(Tags.GameController)
                .GetComponent<GameController>();
                
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
            if (TotalNumberOfSpawnedEnemies < TotalNumberOfEnemies)
                return false;

            return EnemiesCreated.Count == 0;
        }

        /// <summary>
        /// Mechanics that have to happen when the player go to the next phase
        /// </summary>
        public void OnNextLevel(int newLevel)
        {
            NormalEnemiesSpawned = 0;
            FakeEnemiesSpawned = 0;
            BeeKeepersSpawned = 0;

            QuantityOfNormalEnemies++;
            QuantityOfFakeEnemies++;
            // TimeToAwaitToSpawn = TimeToAwaitToSpawn - (TimeToAwaitToSpawn * 0.05f);

            if (newLevel >= LevelToSpawnBeeKeeper)
                QuantityOfBeeKeepers++;

            // BeeKeepers are more stronger, so they will need 2 swarms to be destroyed
            // + 1 is a workaround
            var totalOfEnemies = QuantityOfNormalEnemies + (QuantityOfBeeKeepers * 2);

            PunctuationController.SetQuantityOfBeesByEnemies(totalOfEnemies);
        }

        //Create random enemy
        public IEnumerator CreateEnemies()
        {
            var spawnFake = Random.Range(0, 2) == 1; // Bool that decides if the spawn will be fake or not.

            var createdEnemy = EnemiesSpawner.Spawn(PositionToCreate, EnemiesParent.transform); //Where to create the enemy

            if (spawnFake && FakeEnemiesSpawned < QuantityOfFakeEnemies) //Spawn fake enemy
            {
                var pathFinder = createdEnemy.GetComponent<PathFinderAi>();
                pathFinder.SetAsFakeEnemy();

                FakeEnemiesSpawned++;
            }
            else if (NormalEnemiesSpawned < QuantityOfNormalEnemies) //Spawn true enemy (Only normal and big person)
            {
                EnemiesCreated.Add(createdEnemy);
                NormalEnemiesSpawned++;
            }
            else if (
                NormalEnemiesSpawned >= QuantityOfNormalEnemies
                && FakeEnemiesSpawned >= QuantityOfFakeEnemies
                && BeeKeepersSpawned < QuantityOfBeeKeepers
            ) //Spawn BeeKeeper
            {
                var spawner = EnemiesSpawner as EnemiesSpawner;
                var createdBeeKeeper = spawner.SpawnBeeKeeper(EnemiesParent.transform);

                EnemiesCreated.Add(createdBeeKeeper);
                BeeKeepersSpawned++;
            }

            yield return new WaitForSeconds(TimeToAwaitToSpawn);

            if (TotalNumberOfSpawnedEnemies < TotalNumberOfEnemies)
            {
                StartCoroutine(CreateEnemies());
                yield return null;
            }

            yield return null;
        }
    }
}
