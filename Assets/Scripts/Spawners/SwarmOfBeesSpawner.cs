using Bee.Defenses;
using Bee.Enums;
using Bee.Interfaces;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Bee.Spawners
{
    public class SwarmOfBeesSpawner : MonoBehaviour, ISpawner
    {
        [SerializeField]
        private GameObject SwarmOfBees;

        public GameObject Spawn(Transform parent = null)
        {
            return Instantiate(
                SwarmOfBees,
                position: new Vector3(),
                rotation: SwarmOfBees.transform.rotation,
                parent: parent
            );
        }

        public GameObject Spawn(Transform transform, Transform parent = null)
        {
            return Instantiate(
                SwarmOfBees,
                position: transform.position,
                rotation: SwarmOfBees.transform.rotation,
                parent: parent
            );
        }

        public GameObject Spawn(GameObject target, Transform parent = null)
        {
            var createdSwarm = Instantiate(
                SwarmOfBees,
                position: new Vector3(),
                rotation: SwarmOfBees.transform.rotation,
                parent: parent
            );

            createdSwarm.GetComponent<SwarmOfBees>().SetEnemyToAttack(target);

            return createdSwarm;
        }
    }
}