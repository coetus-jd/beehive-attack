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
        private GameObject NormalPerson;

        [SerializeField]
        private GameObject Beekeeper;

        public void Spawn()
        {
            Instantiate(
                NormalPerson,
                position: new Vector3(),
                rotation: NormalPerson.transform.rotation
            );
        }

        public void Spawn(Transform transform)
        {
            Instantiate(
                NormalPerson,
                position: transform.position,
                rotation: NormalPerson.transform.rotation
            );
        }

        public void Spawn(GameObject target)
        {
            Instantiate(
                NormalPerson,
                position: new Vector3(),
                rotation: NormalPerson.transform.rotation
            );
        }
    }
}