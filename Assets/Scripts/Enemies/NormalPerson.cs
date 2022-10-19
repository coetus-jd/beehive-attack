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

        void Awake()
        {
            DefenseController = GameObject.FindGameObjectWithTag(Tags.DefenseController)
               .GetComponent<DefenseController>();
        }

        public Transform[] GetPaths() => PathChosen;

        void OnMouseDown()
        {
            DefenseController.SetSelectedEnemy(gameObject);
        }

        void OnTriggerEnter2D(Collider2D collider)
        {
            if (!collider.gameObject.CompareTag(Tags.Defense))
                return;

            var defense = collider.gameObject
                .GetComponent<SwarmOfBees>();

            // If already is attacking something and accidentally collides with another
            // enemy the 
            if (defense.Attacking && defense.TargetToReach.GetInstanceID() == gameObject.GetInstanceID())
                BeingAttacked = true;
            
        }
    }
}
