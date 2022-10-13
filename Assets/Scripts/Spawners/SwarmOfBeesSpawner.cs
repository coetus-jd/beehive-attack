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

        public void Spawn(Transform parent = null)
        {
            Instantiate(
                SwarmOfBees,
                position: new Vector3(),
                rotation: SwarmOfBees.transform.rotation,
                parent: parent
            );
        }

        public void Spawn(Transform transform, Transform parent = null)
        {
            Instantiate(
                SwarmOfBees,
                position: transform.position,
                rotation: SwarmOfBees.transform.rotation,
                parent: parent
            );
        }

        public void Spawn(GameObject target, Transform parent = null)
        {
            var createdSwarm = Instantiate(
                SwarmOfBees,
                position: new Vector3(),
                rotation: SwarmOfBees.transform.rotation,
                parent: parent
            );

            createdSwarm.GetComponent<SwarmOfBees>().SetEnemyToAttack(target);
        }
    }
}