using Bee.Defenses;
using Bee.Interfaces;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bee.Spawners
{
    public class SwarmOfBeesSpawner : MonoBehaviour, ISpawner
    {
        [SerializeField]
        private GameObject Parent;

        [SerializeField]
        private GameObject SwarmOfBees;

        public void Spawn()
        {
            Instantiate(SwarmOfBees, position: new Vector3(), rotation: Quaternion.identity, Parent.transform);
        }

        public void Spawn(Transform transform)
        {
            Instantiate(SwarmOfBees, position: transform.position, rotation: Quaternion.identity, Parent.transform);
        }

        public void Spawn(Transform[] transforms)
        {
            var createdSwarm = Instantiate(SwarmOfBees, position: new Vector3(), rotation: Quaternion.identity, Parent.transform);

            createdSwarm.GetComponent<SwarmOfBees>().SetPathToEnemy(transforms);
        }
    }
}