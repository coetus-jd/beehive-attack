using Bee.Defenses;
using Bee.Enums;
using Bee.Interfaces;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Bee.Ui
{
    public class BeeQueen : MonoBehaviour
    {
        [SerializeField]
        private float TimeToDestroy = 15;

        void Start()
        {
            Destroy(gameObject, TimeToDestroy);
        }
    }
}
