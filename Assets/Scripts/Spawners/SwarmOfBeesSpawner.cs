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
        private List<Transform> PositionsToSpawn;

        [SerializeField]
        private GameObject SwarmOfBees;

        private void Start()
        {
            Spawn(0);
            Spawn(1);
            Spawn(2);
        }

        public void Spawn(int index)
        {
            Instantiate(SwarmOfBees, position: PositionsToSpawn[index].position, rotation: Quaternion.identity, Parent.transform);
        }

        public void Spawn()
        {
            throw new System.NotImplementedException();
        }
    }

}