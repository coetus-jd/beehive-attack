using Bee.Controllers;
using Bee.Defenses;
using Bee.Enums;
using Bee.Interfaces;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bee.Enemies
{
    public class BeeKeeper : PathFinderAi, IEnemy
    {
        public Transform[] GetPaths() => PathChosen;

        void OnMouseDown()
        {
            DefenseController.SetSelectedEnemy(gameObject);
        }
    }
}
