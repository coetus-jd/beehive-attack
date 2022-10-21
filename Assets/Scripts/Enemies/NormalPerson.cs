using Bee.Controllers;
using Bee.Defenses;
using Bee.Enums;
using Bee.Interfaces;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bee.Enemies
{
    public class NormalPerson : PathFinderAi, IEnemy
    {
        [Header("Controllers")]
        private DefenseController DefenseController;

        public Transform[] GetPaths() => PathChosen;

        void Awake()
        {
            DefenseController = GameObject.FindGameObjectWithTag(Tags.DefenseController)
               .GetComponent<DefenseController>();
        }

        void OnMouseDown()
        {
            DefenseController.SetSelectedEnemy(gameObject);
        }
    }
}
