using Bee.Interfaces;
using Bee.Spawners;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bee.Controllers
{
    public class DefenseController : MonoBehaviour
    {
        [SerializeField]
        private GameObject SwarmOfBeesSpawner;

        /// <summary>
        /// Define the defense spawner, currently it will be an SwarmOfBeesSpawner, but can be another
        /// type of the defense that implements ISpawner
        /// </summary>
        private ISpawner DefenseSpawner;

        void Awake()
        {
            DefenseSpawner = SwarmOfBeesSpawner.GetComponent<SwarmOfBeesSpawner>();
        }

        public void CreateDefenses(GameObject enemyToAttack)
        {
            DefenseSpawner.Spawn(enemyToAttack);
        }
    }
}
