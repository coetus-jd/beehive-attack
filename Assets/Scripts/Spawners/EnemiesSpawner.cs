using Bee.Defenses;
using Bee.Enums;
using Bee.Interfaces;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Bee.Spawners
{
    public class EnemiesSpawner : MonoBehaviour, ISpawner
    {
        [SerializeField]
        private List<GameObject> EnemiesPrefabs;

        // [SerializeField]
        // private GameObject NormalPerson;

        // [SerializeField]
        // private GameObject BigPerson;

        [SerializeField]
        private GameObject BeeKeeper;

        private int PastIndex = -1;

        public GameObject Spawn(Transform parent = null)
        {
            var randomEnemy = GetRandomEnemyToSpawn();

            return Instantiate(
                randomEnemy,
                position: new Vector3(),
                rotation: randomEnemy.transform.rotation,
                parent: parent
            );
        }

        public GameObject Spawn(Transform transform, Transform parent = null)
        {
            var randomEnemy = GetRandomEnemyToSpawn();

            return Instantiate(
                randomEnemy,
                position: transform.position,
                rotation: randomEnemy.transform.rotation,
                parent: parent
            );
        }

        public GameObject Spawn(GameObject target, Transform parent = null)
        {
            var randomEnemy = GetRandomEnemyToSpawn();

            return Instantiate(
                GetRandomEnemyToSpawn(),
                position: new Vector3(),
                rotation: randomEnemy.transform.rotation,
                parent: parent
            );
        }

        public GameObject SpawnBeeKeeper(GameObject target, Transform parent = null)
        {
            return Instantiate(
                BeeKeeper,
                position: new Vector3(),
                rotation: BeeKeeper.transform.rotation,
                parent: parent
            );
        }

        private GameObject GetRandomEnemyToSpawn()
        {
            return EnemiesPrefabs[GetRandomNumber()];
        }

        private int GetRandomNumber()
        {
            var index = Random.Range(0, EnemiesPrefabs.Count - 1);
            
            if (index == PastIndex)
            {
                PastIndex = -1;
                return index == 0
                    ? EnemiesPrefabs.Count - 1
                    : index;
            }
            
            PastIndex = index;
            return index;
        }
    }
}