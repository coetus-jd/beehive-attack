using Bee.Enums;
using Bee.Interfaces;
using Bee.Spawners;
using Bee.Scenario;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bee.Controllers
{
    public class DefenseController : MonoBehaviour
    {
        [Header("UI")]
        [SerializeField]
        private GameObject Canvas;

        [SerializeField]
        private GameObject PinParent;

        [SerializeField]
        private GameObject Pin;

        [SerializeField]
        private GameObject BeeQueen;

        [Header("Spawner")]
        [SerializeField]
        private GameObject SwarmOfBeesSpawner;

        [SerializeField]
        private float TimeToSpawn = 2;

        private float TimeBetweenSpawn = 0;

        /// <summary>
        /// Define the defense spawner, currently it will be an SwarmOfBeesSpawner, but can be another
        /// type of the defense that implements ISpawner
        /// </summary>
        private ISpawner DefenseSpawner;

        [SerializeField]
        private GameObject DefensesParent;

        private Hive HiveAnim;

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

        void Start()
        {
            HiveAnim = GetComponent<Hive>();
        }

        void Update()
        {
            TimeBetweenSpawn += Time.deltaTime;

            HandleDefense();
        }

        /// <summary>
        /// Define the current enemy selected by the player that will be attacked by the defenses
        /// </summary>
        /// <param name="enemy"></param>
        public void SetSelectedEnemy(GameObject enemy)
        {
            SelectedEnemy = enemy;
        }

        /// <summary>
        /// Mechanics that have to happen when the player go to the next phase
        /// </summary>
        public void OnNextLevel(int newLevel)
        {
            TimeBetweenSpawn = 0;
            TimeToSpawn = TimeToSpawn - (TimeToSpawn * 0.04f);
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

            HiveAnim.HiveClick();
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

        private void CreateDefenses()
        {
            if (SelectedEnemy == null)
                return;

            if (SelectedEnemy.tag == Tags.Enemy)
                Instantiate(BeeQueen, parent: Canvas.transform);

            DefenseSpawner.Spawn(SelectedEnemy, DefensesParent.transform);

            SelectedEnemy = null;
        }
    }
}
