using Assets.Scripts.Interfaces;
using Bee.Controllers;
using Bee.Enums;
using System.Linq;
using UnityEngine;

namespace Bee.Defenses
{
    public class SwarmOfBees : MonoBehaviour, IDefense
    {
        [Header("Movement")]
        [SerializeField]
        private float MoveSpeed = 2f;

        [Header("Position")]
        private float MovementTime;

        [Header("Controllers")]
        private GameController GameController;

        private SwarmOfBeesController SwarmOfBeesController;

        [SerializeField]
        private int BeesQuantityInSwarm = 10_000;

        [SerializeField]
        private float Life = 100;

        [Header("Enemy")]
        private GameObject EnemyToAttack;

        void Awake()
        {
            GameController = GameObject.FindGameObjectWithTag(Tags.GameController)
                .GetComponent<GameController>();
            SwarmOfBeesController = GameObject.FindGameObjectWithTag(Tags.SwarmOfBeesController)
                .GetComponent<SwarmOfBeesController>();
        }

        void Update()
        {
            Attack();
        }

        public void Attack()
        {
            if (EnemyToAttack == null) return;

            Move();
        }

        public void SetEnemyToAttack(GameObject enemy)
        {
            EnemyToAttack = enemy;
        }

        public void TakeDamage(float damage)
        {
            Life -= damage;
        }

        private void Move()
        {
            transform.position = Vector3.Lerp(transform.position, EnemyToAttack.transform.position, MovementTime);

            // .. and increase the t interpolater
            MovementTime += 0.002f * Time.deltaTime;

            // now check if the interpolator has reached 1.0
            // and swap maximum and minimum so game object moves
            // in the opposite direction.
            if (MovementTime > 1.0f)
            {
                // DestroySwarm();
                //InitialPosition = FinalPosition;
                //FinalPosition = 0f;
                //Time = 0f;


                // float temp = FinalPosition;
                // FinalPosition = InitialPosition;
                // InitialPosition = temp;
                // Time = 0f;
            }
        }

        private void DestroySwarm()
        {
            SwarmOfBeesController.UseSwarm(BeesQuantityInSwarm);
            Destroy(gameObject);
        }

        void OnTriggerEnter2D(Collider2D collider)
        {
            if (!collider.gameObject.CompareTag(Tags.Enemy))
                return;

            DestroySwarm();
        }
    }
}