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
        [SerializeField]
        private GameState State;

        [SerializeField]
        private int CurrentLevel = 1;

        [Header("UI")]
        [SerializeField]
        private TMP_Text LevelText;

        [SerializeField]
        private GameObject LevelUp;

        [SerializeField]
        private GameObject LostPanel;

        [Header("Controllers")]
        private PunctuationController PunctuationController;
        private EnemiesController EnemiesController;
        private DefenseController DefenseController;
        private HiveController HiveController;

        void Awake()
        {
            PunctuationController = GameObject.FindGameObjectWithTag(Tags.PunctuationController)
                .GetComponent<PunctuationController>();
            EnemiesController = GameObject.FindGameObjectWithTag(Tags.EnemiesController)
                .GetComponent<EnemiesController>();
            DefenseController = GameObject.FindGameObjectWithTag(Tags.DefenseController)
                .GetComponent<DefenseController>();
            HiveController = GameObject.FindGameObjectWithTag(Tags.Hive)
                .GetComponent<HiveController>();
        }

        void Start()
        {
            LevelText.text = $"Level {CurrentLevel}";
        }

        void Update()
        {
            OnLost();
        }

        public void NextLevel()
        {
            CurrentLevel++;
            LevelText.text = $"Level {CurrentLevel}";
            EnemiesController.OnNextLevel();
            DefenseController.OnNextLevel();

            StartCoroutine(HandleLevelUp());
        }

        public void OnLost()
        {
            if (!HiveController.EnemyAttacked)
                return;

            LostPanel.SetActive(true);
            Time.timeScale = 0;
        }

        private IEnumerator HandleLevelUp()
        {
            LevelUp.SetActive(true);
            LevelUp.GetComponent<Animator>().SetBool("Start", true);

            yield return new WaitForSeconds(1f);

            LevelUp.GetComponent<Animator>().SetBool("Start", false);
            LevelUp.SetActive(false);
        }

        public static void QuitGame() => Application.Quit();
    }
}
