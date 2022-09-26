using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Bee.Controllers
{
    public class SwarmOfBeesController : MonoBehaviour
    {
        [SerializeField]
        private int Quantity = 100;

        [Header("UI")]
        [SerializeField]
        private TMP_Text Counter;

        void Update()
        {
            Counter.text = Quantity.ToString().PadLeft(4, '0');
        }

        public void AddSwarm(int quantityToAdd)
        {
            Quantity += quantityToAdd;
        }

        public void UseSwarm(int quantityToUse)
        {
            Quantity -= quantityToUse;
        }
    }
}
