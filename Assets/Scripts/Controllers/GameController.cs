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
        private int CurrentLevel = 1;

        [Header("UI")]
        [SerializeField]
        private TMP_Text LevelText;

        [Header("Controllers")]
        private PunctuationController PunctuationController;
        private EnemiesController EnemiesController;
        private DefenseController DefenseController;

        void Awake()
        {
            PunctuationController = GameObject.FindGameObjectWithTag(Tags.PunctuationController)
                .GetComponent<PunctuationController>();
            EnemiesController = GameObject.FindGameObjectWithTag(Tags.EnemiesController)
                .GetComponent<EnemiesController>();
            DefenseController = GameObject.FindGameObjectWithTag(Tags.DefenseController)
                .GetComponent<DefenseController>();
        }

        void Start()
        {
            LevelText.text = $"Level {CurrentLevel}";
        }

        public void NextLevel()
        {
            CurrentLevel++;
            LevelText.text = $"Level {CurrentLevel}";
            EnemiesController.OnNextLevel();
            DefenseController.OnNextLevel();
        }

        public static void QuitGame() => Application.Quit();
    }
}
