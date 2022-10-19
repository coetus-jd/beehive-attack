using Bee.Enums;
using Bee.Interfaces;
using Bee.Spawners;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bee.Controllers
{
    public class DefenseController : MonoBehaviour
    {
        [Header("UI")]
        [SerializeField]
        private GameObject PinParent;

        [SerializeField]
        private GameObject Pin;

        [Header("Spawner")]
        [SerializeField]
        private GameObject SwarmOfBeesSpawner;

        [SerializeField]
        private float TimeToSpawn = 2;

        [SerializeField]
        private float TimeBetweenSpawn = 0;

        /// <summary>
        /// Define the defense spawner, currently it will be an SwarmOfBeesSpawner, but can be another
        /// type of the defense that implements ISpawner
        /// </summary>
        private ISpawner DefenseSpawner;

        [SerializeField]
        private GameObject DefensesParent;

        [Header("Enemy")]
        [SerializeField]
        private GameObject SelectedEnemy;

        [Header("Controllers")]
        [SerializeField]
        private PunctuationController PunctuationController;

        void Awake()
        {
            DefenseSpawner = SwarmOfBeesSpawner.GetComponent<SwarmOfBeesSpawner>();
            PunctuationController = GameObject.FindGameObjectWithTag(Tags.PunctuationController)
                .GetComponent<PunctuationController>();
        }

        void Update()
        {
            TimeBetweenSpawn += Time.deltaTime;

            HandleDefense();
        }

        private void HandleDefense()
        {
            if (!Input.GetMouseButtonDown(0) || TimeBetweenSpawn < TimeToSpawn)
                return;

            TimeBetweenSpawn = 0;

            if (PunctuationController.CurrentQuantityOfBees <= 0)
            {
                print("Has reached the limit of swarms");
                return;
            }

            CreatePin();
            CreateDefenses();
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

        private void CreateDefenses()
        {
            if (SelectedEnemy == null)
                return;

            DefenseSpawner.Spawn(SelectedEnemy, DefensesParent.transform);

            SelectedEnemy = null;
        }
    }
}
