using Bee.Interfaces;
using Bee.Spawners;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bee.Controllers
{
    public class DefenseController : MonoBehaviour
    {
        private ISpawner DefenseSpawner;

        void Awake()
        {
            DefenseSpawner = new SwarmOfBeesSpawner();
        }

        public void CreateSwarmOfBess(Transform[] Positions)
        {
            DefenseSpawner.Spawn(Positions);
        }
    }
}
