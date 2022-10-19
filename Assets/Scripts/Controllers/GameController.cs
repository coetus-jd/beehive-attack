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


        public void NextLevel()
        {
            CurrentLevel++;
            // aumentar a quantidade de inimigos
            EnemiesController.OnNextLevel();
            // diminuir o tempo de spawn de inimigos?
            // resetar a quantidade de abelhas
        }
    }
}
