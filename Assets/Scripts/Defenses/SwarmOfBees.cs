using Assets.Scripts.Interfaces;
using Bee.Controllers;
using Bee.Enums;
using System.Collections;
using System.Linq;
using UnityEngine;

namespace Bee.Defenses
{
    public class SwarmOfBees : MonoBehaviour, IDefense
    {
        [Header("Movement")]
        [SerializeField]
        private float MoveSpeed = 2f;

        /// <summary>
        /// Number of seconds to await to return to the hive when the swarm
        /// has reached nothing
        /// </summary>
        [SerializeField]
        private float SecondsToReturnToHive = 2f;

        [Header("Position")]
        private float MovementTime;

        [Header("Controllers")]
        private GameController GameController;

        [SerializeField]
        private float Life = 100;

        private GameObject Hive;

        [Header("Enemy")]
        [SerializeField]
        private GameObject TargetToReach;

        void Awake()
        {
            GameController = GameObject.FindGameObjectWithTag(Tags.GameController)
                .GetComponent<GameController>();
            Hive = GameObject.FindGameObjectWithTag(Tags.Hive);
        }

        void Update()
        {
            Attack();
        }

        public void Attack()
        {
            if (TargetToReach == null) return;

            Move();
        }

        public void SetEnemyToAttack(GameObject enemy)
        {
            TargetToReach = enemy;
        }

        public void TakeDamage(float damage)
        {
            Life -= damage;
        }

        private void Move()
        {
            transform.position = Vector3.Lerp(
                transform.position,
                TargetToReach.transform.position,
                MovementTime
            );

            MovementTime += 0.02f * Time.deltaTime;

            var hasReachedTarget = Vector3.Distance(transform.position, TargetToReach.transform.position) < 1f;

            if (!hasReachedTarget)
                return;

            if (TargetToReach.tag == Tags.Hive)
            {
                Destroy(gameObject);
                return;
            }

            StartCoroutine(ReturnToHive());
        }

        private void DestroySwarm()
        {
            GameController.UseSwarm();
            Destroy(gameObject);
        }

        void OnTriggerEnter2D(Collider2D collider)
        {
            if (!collider.gameObject.CompareTag(Tags.Enemy))
                return;

            DestroySwarm();
        }

        private IEnumerator ReturnToHive()
        {
            yield return new WaitForSeconds(SecondsToReturnToHive);

            MovementTime = 0f;
            TargetToReach = Hive;
        }
    }
}