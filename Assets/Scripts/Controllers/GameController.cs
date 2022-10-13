using Bee.Defenses;
using Bee.Enums;
using Bee.Interfaces;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Bee.Controllers
{
    public class GameController : MonoBehaviour
    {
        [Header("UI")]
        [SerializeField]
        private GameObject PinParent;

        [SerializeField]
        private GameObject Pin;

        [SerializeField]
        private TMP_Text BeesCounter;

        [Header("Controllers")]
        private DefenseController DefenseController;

        [SerializeField]
        private int QuantityOfBees = 50_000;

        [SerializeField]
        private int BeesQuantityInSwarm = 10_000;

        [Header("Enemy")]
        [SerializeField]
        private GameObject SelectedEnemy;

        void Awake()
        {
            DefenseController = GameObject.FindGameObjectWithTag(Tags.DefenseController)
               .GetComponent<DefenseController>();

            ChangeBeesCounterText(QuantityOfBees);
        }

        void Update()
        {
            HandleGameDefense();
        }

        private void HandleGameDefense()
        {
            if (!Input.GetMouseButtonDown(0))
                return;

            if (CurrentQuantityOfBees() <= 0)
            {
                print("Has reached the limit of swarms");
                return;
            }

            CreatePin();
            CreateDefense();
        }

        private void CreatePin()
        {
            // In the case of an enemy already was selected isn't necessary to create the pin
            if (SelectedEnemy != null)
                return;

            Vector3 mousePos = Input.mousePosition;
            mousePos.z = Camera.main.nearClipPlane;
            var position = Camera.main.ScreenToWorldPoint(mousePos);

            var createdPin = Instantiate(Pin, PinParent.transform);

            createdPin.transform.localPosition = position;

            SetSelectedEnemy(createdPin);
        }

        /// <summary>
        /// Define the current enemy selected by the player that will be attacked by the defenses
        /// </summary>
        /// <param name="enemy"></param>
        public void SetSelectedEnemy(GameObject enemy)
        {
            SelectedEnemy = enemy;
        }

        public void AddSwarm()
        {
            ChangeBeesCounterText(CurrentQuantityOfBees() + BeesQuantityInSwarm);
        }

        public void UseSwarm()
        {
            ChangeBeesCounterText(CurrentQuantityOfBees() - BeesQuantityInSwarm);
        }

        private void CreateDefense()
        {
            if (SelectedEnemy == null)
                return;

            DefenseController.CreateDefenses(SelectedEnemy);

            SelectedEnemy = null;
        }

        private void ChangeBeesCounterText(int quantity)
        {
            BeesCounter.text = quantity.ToString().PadLeft(5, '0');
        }

        private int CurrentQuantityOfBees()
            => int.Parse(BeesCounter.text);
    }
}
