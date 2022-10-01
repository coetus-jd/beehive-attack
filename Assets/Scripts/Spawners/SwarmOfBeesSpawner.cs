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

        public void Spawn()
        {
            Instantiate(SwarmOfBees, position: new Vector3(), rotation: Quaternion.identity);
        }

        public void Spawn(Transform transform)
        {
            Instantiate(SwarmOfBees, position: transform.position, rotation: Quaternion.identity);
        }

        public void Spawn(Transform[] transforms)
        {
            var createdSwarm = Instantiate(
                SwarmOfBees,
                position: transforms.Last().position,
                rotation: Quaternion.identity
            );

            createdSwarm.GetComponent<SwarmOfBees>().SetPathToEnemy(transforms);
        }
    }
}