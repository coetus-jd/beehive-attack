using Bee.Enums;
using Bee.Interfaces;
using Bee.Spawners;
using Bee.Scenario;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Bee.Enemies;
using UnityEngine.UI;

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

        private bool Pause = false;

        [Header("Spawner")]
        [SerializeField]
        private GameObject SwarmOfBeesSpawner;

        [SerializeField]
        private float TimeToSpawn = 0.8f;

        private float TimeBetweenSpawn = 0;

        /// <summary>
        /// Define the defense spawner, currently it will be an SwarmOfBeesSpawner, but can be another
        /// type of the defense that implements ISpawner
        /// </summary>
        private ISpawner DefenseSpawner;

        [Header("Automatic attack control")]
        [SerializeField]
        private bool AutomaticDefense = false;

       
        [SerializeField]
        private List<int> AttackedEnemiesIds = new List<int>();

        [SerializeField]
        private Button AttackButton;

        [SerializeField]
        private Slider QueenPowerSlider;

        [SerializeField]
        private GameObject DefensesParent;

        private Hive HiveAnim;

        [Header("Enemy")]
        [SerializeField]
        private GameObject SelectedEnemy;

        [Header("Controllers")]
        [SerializeField]
        private PunctuationController PunctuationController;

        [SerializeField]
        private EnemiesController EnemiesController;

        [SerializeField]
        private GameController GameController;

        void Start()
        {
            DefenseSpawner = SwarmOfBeesSpawner.GetComponent<SwarmOfBeesSpawner>();
            PunctuationController = GameObject.FindGameObjectWithTag(Tags.PunctuationController)
                .GetComponent<PunctuationController>();
            EnemiesController = GameObject.FindGameObjectWithTag(Tags.EnemiesController)
                .GetComponent<EnemiesController>();
            GameController = GameObject.FindGameObjectWithTag(Tags.GameController)
                .GetComponent<GameController>();

            HiveAnim = GetComponent<Hive>();
        }

        void Update()
        {
            TimeBetweenSpawn += Time.deltaTime;

            if (AutomaticDefense)
            {
                AutomaticCreateDefenses();
                return;
            }

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

        public void StopAction()
        {
            Pause = !Pause;
        }

        private void HandleDefense()
        {
            if ((!Input.GetMouseButtonDown(0) && Input.touchCount == 0) || TimeBetweenSpawn < TimeToSpawn)
                return;

            if (Pause)
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

        // Create a pin to guide the bees to the position
        private void CreatePin()
        {
            // In the case of an enemy already was selected isn't necessary to create the pin
            if (SelectedEnemy != null)
                return;

            var pointerPosition = new Vector3();

#if UNITY_STANDALONE || UNITY_WEBPLAYER || UNITY_EDITOR //if it's in the computer

            pointerPosition = Input.mousePosition;

#elif UNITY_IOS || UNITY_ANDROID //if it's in the Mobile

            pointerPosition = Input.touches[0].position;
#endif

            pointerPosition.z = Camera.main.nearClipPlane; //Don't really need z position
            var position = Camera.main.ScreenToWorldPoint(pointerPosition); // get the position on the main camera screen.
            var createdPin = Instantiate(Pin, PinParent.transform); // Create the pin

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

        private void AutomaticCreateDefenses()
        {
            var allEnemies = GameObject.FindGameObjectsWithTag(Tags.Enemy).ToList();

            for (int index = 0; index < allEnemies.Count; index++)
            {
                var enemy = allEnemies[index].GetComponent<PathFinderAi>();

                if (enemy.IsFakeEnemy || enemy.IsBeingAttacked || AttackedEnemiesIds.Contains(enemy.GetInstanceID()))
                    continue;

                DefenseSpawner.Spawn(allEnemies[index], DefensesParent.transform);
                AttackedEnemiesIds.Add(enemy.GetInstanceID());
            }

            AutomaticDefense = false;
        }

        public void AttackByButton()
        {
            GameController.ResetQueenPower();
            AutomaticDefense = true;
            DisableAttackButton();
        }

        public void EnableAttackButton()
        {
            AttackButton.interactable = true;
            var defaultColor = AttackButton.GetComponent<Image>().color;
            AttackButton.GetComponentInChildren<Image>().color = new Color(defaultColor.r, defaultColor.g, defaultColor.b, 1);
        }

        public void DisableAttackButton()
        {
            AttackButton.interactable = false;
            var defaultColor = AttackButton.GetComponent<Image>().color;
            AttackButton.GetComponentInChildren<Image>().color = new Color(defaultColor.r, defaultColor.g, defaultColor.b, 0.5f);
        }
    }
}
