using System.Collections;
using System.Collections.Generic;
using Bee.Controllers;
using Bee.Enums;
using UnityEngine;
using Bee.Ui;

namespace Bee.Controllers
{
    public class HiveController : MonoBehaviour
    {
        public bool EnemyAttacked { get; private set; }

        public float Life { get; private set; } = 3;

        private float InitialLife;

        [Header("Controllers")]
        private GameController GameController;

        [SerializeField]
        private GameObject LifeUI;

        void Start()
        {
            GameController = GameObject.FindGameObjectWithTag(Tags.GameController)
                .GetComponent<GameController>();

            InitialLife = Life;
            LifeUI.GetComponent<LifeUI>().HeartSetUp((int)Life);
        }

        public void OnNextLevel(int newLevel)
        {
            Life = InitialLife;
            LifeUI.GetComponent<LifeUI>().HeartSetUp((int)Life);
        }

        void OnTriggerEnter2D(Collider2D collider)
        {
            if (HasLostGame(collider))
                return;

            DefenseCameBack(collider);
        }

        /// <summary>
        /// When a defense came back to the hive we add the number of swarm used back
        /// </summary>
        /// <param name="collider"></param>
        private void DefenseCameBack(Collider2D collider)
        {
            if (!collider.gameObject.CompareTag(Tags.Defense))
                return;
        }

        private bool HasLostGame(Collider2D collider)
        {
            if (!collider.gameObject.CompareTag(Tags.Enemy))
                return false;

            Destroy(collider.gameObject);

            Life--;
            LifeUI.GetComponent<LifeUI>().HeartControl((int)Life);

            if (Life == 0)
            {
                StartCoroutine(hurtCooldown(1f));

                IEnumerator hurtCooldown(float t)
                {
                    yield return new WaitForSeconds(t);
                    EnemyAttacked = true;
                }

                return true;
            }

            return false;
        }
    }

}
