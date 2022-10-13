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

        public void Spawn(Transform parent = null)
        {
            Instantiate(
                NormalPerson,
                position: new Vector3(),
                rotation: NormalPerson.transform.rotation,
                parent: parent
            );
        }

        public void Spawn(Transform transform, Transform parent = null)
        {
            Instantiate(
                NormalPerson,
                position: transform.position,
                rotation: NormalPerson.transform.rotation,
                parent: parent
            );
        }

        public void Spawn(GameObject target, Transform parent = null)
        {
            Instantiate(
                NormalPerson,
                position: new Vector3(),
                rotation: NormalPerson.transform.rotation,
                parent: parent
            );
        }
    }
}