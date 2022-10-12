using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Bee.Controllers
{
    public class SwarmOfBeesController : MonoBehaviour
    {
        [SerializeField]
        private int InternalQuantityOfBees;

        [Header("UI")]
        [SerializeField]
        private TMP_Text Counter;

        private void Awake()
        {
            int.TryParse(Counter.text, out var quantityOfBees);
            InternalQuantityOfBees = quantityOfBees;
        }

        void Update()
        { 
            Counter.text = InternalQuantityOfBees.ToString().PadLeft(5, '0');
        }

        public void AddSwarm(int quantityToAdd)
        {
            InternalQuantityOfBees += quantityToAdd;
        }

        public void UseSwarm(int quantityToUse)
        {
            InternalQuantityOfBees -= quantityToUse;
        }
    }
}
