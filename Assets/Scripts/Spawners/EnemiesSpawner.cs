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
        private GameObject BigPerson;

        [SerializeField]
        private GameObject Beekeeper;

        public GameObject Spawn(Transform parent = null)
        {
            return Instantiate(
                NormalPerson,
                position: new Vector3(),
                rotation: NormalPerson.transform.rotation,
                parent: parent
            );
        }

        public GameObject Spawn(Transform transform, Transform parent = null)
        {
            return Instantiate(
                NormalPerson,
                position: transform.position,
                rotation: NormalPerson.transform.rotation,
                parent: parent
            );
        }

        public GameObject Spawn(GameObject target, Transform parent = null)
        {
            return Instantiate(
                NormalPerson,
                position: new Vector3(),
                rotation: NormalPerson.transform.rotation,
                parent: parent
            );
        }
    }
}